using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using System.IO;
using BulkAttachmentManagementPlugin.Constants;
using BulkAttachmentManagementPlugin.Data_Access_Objects;
using Microsoft.Xrm.Sdk;
using BulkAttachmentManagementPlugin.Models;

namespace BulkAttachmentManagementPlugin
{
    public partial class PluginControl : PluginControlBase, IGitHubPlugin, IPayPalPlugin, IHelpPlugin
    {
        #region GitHub Info
        public string RepositoryName => "XrmToolBoxPlugins";
        public string UserName => "medicstuder";
        #endregion

        #region PayPal Info
        public string DonationDescription => "Support For Bulk Attachment Manager";
        public string EmailAccount => "jczstuder@gmail.com";
        #endregion

        #region Help Info
        public string HelpUrl => "https://github.com/medicstuder/XrmToolBoxPlugins/wiki";
        #endregion

        public PluginControl()
        {
            InitializeComponent();
        }

        #region Buttons
        private void butCSVBrowse_Click(object sender, EventArgs e)
        {
            if(RadioDownloadAll.Enabled && RadioDownloadSpecific.Enabled)
            {
                switch (RadioDownloadAll.Checked)
                {
                    case false when RadioDownloadSpecific.Checked == false:
                        MessageBox.Show(@"Please choose an option in step 2", @"Missing choice", MessageBoxButtons.OK,MessageBoxIcon.Error);
                        break;
                    case true:
                    {
                        DialogResult fbdResult = FolderBrowserDialogMainFile.ShowDialog();
                        if (fbdResult == DialogResult.OK)
                        {
                            TextBoxCsvLocation.Text = FolderBrowserDialogMainFile.SelectedPath;
                            GroupStep3.Enabled = true;
                        }

                        break;
                    }
                    default:
                    {
                        DialogResult ofdResult = OpenFileDianlogCvsFile.ShowDialog();
                        if (ofdResult == DialogResult.OK)
                        {
                            TextBoxCsvLocation.Text = OpenFileDianlogCvsFile.FileName;
                            GroupStep3.Enabled = true;
                        }

                        break;
                    }
                }
            }
            else
            {
                DialogResult fbdResult = FolderBrowserDialogMainFile.ShowDialog();
                if(fbdResult == DialogResult.OK)
                {
                    TextBoxCsvLocation.Text = FolderBrowserDialogMainFile.SelectedPath;
                    GroupStep3.Enabled = true;
                }
            }

        }
       
        private void butRun_Click(object sender, EventArgs e)
        {
            ExecuteMethod(PerformAction);
        }

        private void butExport_Click(object sender, EventArgs e)
        {
            DialogResult sfdResult = SaveFileDialogCsvFile.ShowDialog();
            if(sfdResult == DialogResult.OK)
            {
                LocalFileSystemDAO lfsDAO = new LocalFileSystemDAO();
                List<OutputModel> oOutputModel = new List<OutputModel>();
                if (ListViewMainOutput.Items.Count > 0 && ListViewMainOutput != null)
                {
                    foreach (ListViewItem item in ListViewMainOutput.Items)
                    {
                        oOutputModel.Add(new OutputModel
                        {
                            DateTimeProcessed = item.Text ?? string.Empty,
                            GUID = item.SubItems[1].Text ?? string.Empty,
                            FileName = item.SubItems[2].Text ?? string.Empty,
                            FileSize = item.SubItems[3].Text ?? string.Empty,
                            DownloadLocation = item.SubItems[4].Text ?? string.Empty,
                            RegardingID = item.SubItems[5].Text ?? string.Empty,
                            RegardingEntity = item.SubItems[6].Text ?? string.Empty,
                            ErrorMessage = item.SubItems[7].Text ?? string.Empty
                        });
                    }
                }
                WorkAsync(new WorkAsyncInfo
                {
                    Message = "Exporting Data...",
                    Work = (Worker, args) =>
                    {
                        lfsDAO.ExportResultsToCSV(oOutputModel, SaveFileDialogCsvFile.FileName);
                    }
                });
            }
        }
        #endregion

        #region Radio Buttons
        private void rbAllAttachments_CheckedChanged(object sender, EventArgs e)
        {
            if(RadioDownloadAll.Checked)
            {
                lbCSVLocation.Text = @"Please choose the location to download attachments to";
                TextBoxCsvLocation.ReadOnly = false;
                TextBoxCsvLocation.Enabled = true;
                ButtonCsvBrowse.Enabled = true;
                GroupStep3.Enabled = false;
            }
        }

        private void rbSpecificAttachments_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioDownloadSpecific.Checked)
            {
                lbCSVLocation.Text = @"Please choose a location of CSV file.  Folder containing attachments will be downloaded to same folder as CSV file";
                TextBoxCsvLocation.ReadOnly = false;
                TextBoxCsvLocation.Enabled = true;
                ButtonCsvBrowse.Enabled = true;
                GroupStep3.Enabled = false;
            }
        }

        private void Step1RadioButtons(object sender, EventArgs e)
        {
            GroupStep2.Enabled = true;
        }

        private void rbReportOnly_CheckedChanged(object sender, EventArgs e)
        {
            if(RadioReportOnly.Checked)
            {
                lbCSVLocation.Text = @"This option will ONLY report on the attachments.  Screen below will be populated and you can export the results";
                TextBoxCsvLocation.ReadOnly = false;
                TextBoxCsvLocation.Enabled = false;
                ButtonCsvBrowse.Enabled = false;
                GroupStep3.Enabled = true;
            }
        }

        #endregion

        int recordCount;
        int loopCounter = 1;

        private void PerformAction()
        {
            #region Download
            if(!RadioReportOnly.Checked)
            {
                #region Download Notes
                if (RadioNotes.Checked)
                {
                    ProcessNotes();
                }
                #endregion

                #region Download Emails
                if (RadioEmail.Checked)
                {
                    ProcessEmails();
                }
                #endregion

                #region Download Both
                if (RadioAll.Checked)
                {
                    ProcessNotes();
                    ProcessEmails();
                }
                #endregion
            }
            #endregion

            #region Report
            else
            {
                #region Report Notes
                if(RadioNotes.Checked)
                {
                    ReportNotes();
                }
                #endregion

                #region Report E-Mails
                if (RadioEmail.Checked)
                {
                    ReportEmails();
                }
                #endregion

                #region Report Both
                if (RadioAll.Checked)
                {
                    ReportNotes();
                    ReportEmails();
                }
                #endregion
            }
            #endregion

        }

        private void ProcessNotes()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Processing...",
                Work = (Worker, args) =>
                {
                    Entity oNoteData = new Entity();
                    string storeAttachmentDirectory = null;

                    CRMAttachmentDAO crmDAO = new CRMAttachmentDAO();
                    LocalFileSystemDAO localDAO = new LocalFileSystemDAO();

                    List<Guid> oAttachmentGuids = RadioDownloadAll.Checked ? crmDAO.GetListOfAttachments(Service) : localDAO.ReadFromCSV(TextBoxCsvLocation.Text);
                    recordCount = oAttachmentGuids.Count;
                    string fileDirectory = RadioDownloadAll.Checked ? localDAO.CreateLocalDirectory(TextBoxCsvLocation.Text, false, false, false) : localDAO.CreateLocalDirectory(TextBoxCsvLocation.Text, false, true, false);
                    foreach (Guid attachment in oAttachmentGuids)
                    {
                        try
                        {
                            oNoteData = null;
                            storeAttachmentDirectory = null;
                            oNoteData = crmDAO.GetNoteAttachmentData(attachment, Service);
                            if(oNoteData[Annotation.FileSize].ToString() != "0")
                            {
                                storeAttachmentDirectory = localDAO.CreateLocalDirectory(Path.Combine(fileDirectory, oNoteData.Id.ToString()), true, false, false);
                                localDAO.CreateAttachmentFile(oNoteData[Annotation.DocumentBody].ToString(), storeAttachmentDirectory, oNoteData[Annotation.FileName].ToString());

                                ListViewItem _ListViewItem = new ListViewItem(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                                _ListViewItem.SubItems.Add(oNoteData[Annotation.PrimaryKey].ToString());
                                _ListViewItem.SubItems.Add(oNoteData[Annotation.FileName].ToString());
                                _ListViewItem.SubItems.Add(oNoteData[Annotation.FileSize].ToString());
                                _ListViewItem.SubItems.Add(storeAttachmentDirectory);
                                _ListViewItem.SubItems.Add(oNoteData[Annotation.ObjectTypeCode].ToString());
                                _ListViewItem.SubItems.Add(((EntityReference)oNoteData[Annotation.ObjectId]).Id.ToString());
                                _ListViewItem.SubItems.Add("");

                                Worker.ReportProgress(0, _ListViewItem);
                                Worker.ReportProgress(1);
                                loopCounter++;
                            }
                            else
                            {
                                throw new Exception("File size is 0.");
                            }
                        }
                        catch (Exception ex)
                        {
                            ListViewItem _ListViewItem = new ListViewItem(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                            if (oNoteData != null)
                            {
                                _ListViewItem.SubItems.Add(oNoteData[Annotation.PrimaryKey].ToString());
                                _ListViewItem.SubItems.Add(oNoteData[Annotation.FileName].ToString());
                                _ListViewItem.SubItems.Add(oNoteData[Annotation.FileSize].ToString());
                                _ListViewItem.SubItems.Add(storeAttachmentDirectory);
                                _ListViewItem.SubItems.Add(oNoteData[Annotation.ObjectTypeCode].ToString());
                                _ListViewItem.SubItems.Add(((EntityReference)oNoteData[Annotation.ObjectId]).Id.ToString());
                            }

                            _ListViewItem.SubItems.Add(ex.Message);

                            Worker.ReportProgress(0, _ListViewItem);
                        }
                    }
                },
                ProgressChanged = pc =>
                {
                    if (pc.ProgressPercentage == 1)
                    {
                        SetWorkingMessage($"Processing Notes: {loopCounter} of {recordCount}");
                    }
                    else
                    {
                        ListViewMainOutput.Items.Add((ListViewItem)pc.UserState);
                    }
                }
            });
        }

        private void ProcessEmails()
        {
            WorkAsync(new WorkAsyncInfo()
            {
                Message = "Processing...",
                Work = (Worker, args) =>
                {
                    Entity oEMailData = new Entity();
                    string storeAttachmentDirectory = null;

                    CRMAttachmentDAO crmDAO = new CRMAttachmentDAO();
                    LocalFileSystemDAO localDAO = new LocalFileSystemDAO();

                    List<Guid> oAttachmentGuids = RadioDownloadAll.Checked ? crmDAO.GetListOfActivityMimeAttachmentGuids(Service) : localDAO.ReadFromCSV(TextBoxCsvLocation.Text);
                    recordCount = oAttachmentGuids.Count;
                    string fileDirectory = RadioDownloadAll.Checked ? localDAO.CreateLocalDirectory(TextBoxCsvLocation.Text, false, false, true) : localDAO.CreateLocalDirectory(TextBoxCsvLocation.Text, false, true, true);
                    foreach (Guid attachment in oAttachmentGuids)
                    {
                        try
                        {
                            oEMailData = null;
                            storeAttachmentDirectory = null;
                            oEMailData = crmDAO.GetActivityMimeAttachmentData(attachment, Service);

                            if (oEMailData[ActivityMimeAttachment.FileSize].ToString() != "0")
                            {
                                storeAttachmentDirectory = localDAO.CreateLocalDirectory(Path.Combine(fileDirectory, oEMailData.Id.ToString()), true, false, true);
                                localDAO.CreateAttachmentFile(oEMailData[ActivityMimeAttachment.Body].ToString(), storeAttachmentDirectory, oEMailData[ActivityMimeAttachment.FileName].ToString());

                                ListViewItem _ListViewItem = new ListViewItem(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                                _ListViewItem.SubItems.Add(oEMailData[ActivityMimeAttachment.PrimaryKey].ToString());
                                _ListViewItem.SubItems.Add(oEMailData[ActivityMimeAttachment.FileName].ToString());
                                _ListViewItem.SubItems.Add(oEMailData[ActivityMimeAttachment.FileSize].ToString());
                                _ListViewItem.SubItems.Add(storeAttachmentDirectory);
                                _ListViewItem.SubItems.Add(oEMailData[ActivityMimeAttachment.ObjectTypeCode].ToString());
                                _ListViewItem.SubItems.Add(((EntityReference)oEMailData[ActivityMimeAttachment.ObjectId]).Id.ToString());
                                _ListViewItem.SubItems.Add("");

                                Worker.ReportProgress(0, _ListViewItem);
                                Worker.ReportProgress(1);
                                loopCounter++;
                            }
                            else
                            {
                                throw new Exception("File size is 0.");
                            }
                        }
                        catch (Exception ex)
                        {
                            ListViewItem _ListViewItem = new ListViewItem(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                            if (oEMailData != null)
                            {
                                _ListViewItem.SubItems.Add(oEMailData[ActivityMimeAttachment.PrimaryKey].ToString());
                                _ListViewItem.SubItems.Add(oEMailData[ActivityMimeAttachment.FileName].ToString());
                                _ListViewItem.SubItems.Add(oEMailData[ActivityMimeAttachment.FileSize].ToString());
                                _ListViewItem.SubItems.Add(storeAttachmentDirectory);
                                _ListViewItem.SubItems.Add(oEMailData[ActivityMimeAttachment.ObjectTypeCode].ToString());
                                _ListViewItem.SubItems.Add(((EntityReference)oEMailData[ActivityMimeAttachment.ObjectId]).Id.ToString());
                            }

                            _ListViewItem.SubItems.Add(ex.Message);

                            Worker.ReportProgress(0, _ListViewItem);
                        }
                    }
                },
                ProgressChanged = pc =>
                {
                    if (pc.ProgressPercentage == 1)
                    {
                        SetWorkingMessage($"Processing E-mails: {loopCounter} of {recordCount}");
                    }
                    else
                    {
                        ListViewMainOutput.Items.Add((ListViewItem)pc.UserState);
                    }
                }
            });
        }

        private void ReportNotes()
        {
            OutputModel noteRecord = null;
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Processing...",
                Work = (Worker, args) =>
                {

                    CRMAttachmentDAO crmDAO = new CRMAttachmentDAO();

                    List<OutputModel> oNoteData = crmDAO.ReportNoteAttachments(Service);

                    foreach (OutputModel note in oNoteData)
                    {
                        try
                        {
                            noteRecord = note;
                            ListViewItem _ListViewItem = new ListViewItem(note.DateTimeProcessed);
                            _ListViewItem.SubItems.Add(note.GUID);
                            _ListViewItem.SubItems.Add(note.FileName);
                            _ListViewItem.SubItems.Add(note.FileSize);
                            _ListViewItem.SubItems.Add(note.DownloadLocation);
                            _ListViewItem.SubItems.Add(note.RegardingEntity);
                            _ListViewItem.SubItems.Add(note.RegardingID);
                            _ListViewItem.SubItems.Add("");

                            Worker.ReportProgress(0, _ListViewItem);
                        }
                        catch (Exception ex)
                        {
                            if (noteRecord != null)
                            {
                                ListViewItem _ListViewItem = new ListViewItem(noteRecord.DateTimeProcessed);
                                _ListViewItem.SubItems.Add(noteRecord.GUID);
                                _ListViewItem.SubItems.Add(noteRecord.FileName);
                                _ListViewItem.SubItems.Add(noteRecord.FileSize);
                                _ListViewItem.SubItems.Add(noteRecord.DownloadLocation);
                                _ListViewItem.SubItems.Add(noteRecord.RegardingEntity);
                                _ListViewItem.SubItems.Add(noteRecord.RegardingID);
                                _ListViewItem.SubItems.Add(ex.Message);

                                Worker.ReportProgress(0, _ListViewItem);
                            }
                        }
                    }
                },
                ProgressChanged = pc =>
                {
                    ListViewMainOutput.Items.Add((ListViewItem)pc.UserState);
                }
            });
        }

        private void ReportEmails()
        {
            OutputModel emailRecord = null;
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Processing...",
                Work = (Worker, args) =>
                {
                    try
                    {
                        CRMAttachmentDAO crmDAO = new CRMAttachmentDAO();

                        List<OutputModel> oEmailData = crmDAO.ReportMimeAttachments(Service);

                        foreach (OutputModel email in oEmailData)
                        {
                            emailRecord = email;
                            ListViewItem _ListViewItem = new ListViewItem(email.DateTimeProcessed);
                            _ListViewItem.SubItems.Add(email.GUID);
                            _ListViewItem.SubItems.Add(email.FileName);
                            _ListViewItem.SubItems.Add(email.FileSize);
                            _ListViewItem.SubItems.Add(email.DownloadLocation);
                            _ListViewItem.SubItems.Add(email.RegardingEntity);
                            _ListViewItem.SubItems.Add(email.RegardingID);
                            _ListViewItem.SubItems.Add("");

                            Worker.ReportProgress(0, _ListViewItem);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (emailRecord != null)
                        {
                            ListViewItem _ListViewItem = new ListViewItem(emailRecord.DateTimeProcessed);
                            _ListViewItem.SubItems.Add(emailRecord.GUID);
                            _ListViewItem.SubItems.Add(emailRecord.FileName);
                            _ListViewItem.SubItems.Add(emailRecord.FileSize);
                            _ListViewItem.SubItems.Add(emailRecord.DownloadLocation);
                            _ListViewItem.SubItems.Add(emailRecord.RegardingEntity);
                            _ListViewItem.SubItems.Add(emailRecord.RegardingID);
                            _ListViewItem.SubItems.Add(ex.Message);

                            Worker.ReportProgress(0, _ListViewItem);
                        }
                    }
                },
                ProgressChanged = pc =>
                {
                    ListViewMainOutput.Items.Add((ListViewItem)pc.UserState);
                }
            });
        }
    }
}