using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using System.IO;
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
            if(rbAllAttachments.Enabled == true && rbSpecificAttachments.Enabled == true)
            {
                if(rbAllAttachments.Checked == false && rbSpecificAttachments.Checked == false)
                {
                    MessageBox.Show("Please choose an option in step 2.", "Missing choice", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    if(rbAllAttachments.Checked)
                    {
                        DialogResult fbdResult = fbdMainFile.ShowDialog();
                        if (fbdResult == DialogResult.OK)
                        {
                            tbCSVLocation.Text = fbdMainFile.SelectedPath;
                            gbStep3.Enabled = true;
                        }
                    }
                    else
                    {
                        DialogResult ofdResult = ofdCVSFile.ShowDialog();
                        if (ofdResult == DialogResult.OK)
                        {
                            tbCSVLocation.Text = ofdCVSFile.FileName;
                            gbStep3.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                DialogResult fbdResult = fbdMainFile.ShowDialog();
                if(fbdResult == DialogResult.OK)
                {
                    tbCSVLocation.Text = fbdMainFile.SelectedPath;
                    gbStep3.Enabled = true;
                }
            }

        }
       
        private void butRun_Click(object sender, EventArgs e)
        {
            ExecuteMethod(PerformAction);
        }

        private void butExport_Click(object sender, EventArgs e)
        {
            DialogResult sfdResult = sfdCSVFile.ShowDialog();
            if(sfdResult == DialogResult.OK)
            {
                LocalFileSystemDAO lfsDAO = new LocalFileSystemDAO();
                List<OutputModel> oOutputModel = new List<OutputModel>();
                if (lvMainOutput.Items.Count > 0 && lvMainOutput != null)
                {
                    foreach (ListViewItem item in lvMainOutput.Items)
                    {
                        oOutputModel.Add(new OutputModel
                        {
                            DateTimeProcessed = (item.Text != null) ? item.Text : string.Empty,
                            GUID = (item.SubItems[1].Text != null) ? item.SubItems[1].Text : string.Empty,
                            FileName = (item.SubItems[2].Text != null) ? item.SubItems[2].Text : string.Empty,
                            FileSize = (item.SubItems[3].Text != null) ? item.SubItems[3].Text : string.Empty,
                            DownloadLocation = (item.SubItems[4].Text != null) ? item.SubItems[4].Text : string.Empty,
                            RegardingID = (item.SubItems[5].Text != null) ? item.SubItems[5].Text : string.Empty,
                            RegardingEntity = (item.SubItems[6].Text != null) ? item.SubItems[6].Text : string.Empty,
                            ErrorMessage = (item.SubItems[7].Text != null) ? item.SubItems[7].Text : string.Empty
                        });
                    }
                }
                WorkAsync(new WorkAsyncInfo
                {
                    Message = "Exporting Data...",
                    Work = (Worker, args) =>
                    {
                        lfsDAO.ExportResultsToCSV(oOutputModel, sfdCSVFile.FileName);
                    }
                });
            }
        }
        #endregion

        #region Radio Buttons
        private void rbAllAttachments_CheckedChanged(object sender, EventArgs e)
        {
            if(rbAllAttachments.Checked)
            {
                lbCSVLocation.Text = "Please choose the location to download attachments to.";
                tbCSVLocation.ReadOnly = false;
                tbCSVLocation.Enabled = true;
                butCSVBrowse.Enabled = true;
                gbStep3.Enabled = false;
            }
        }

        private void rbSpecificAttachments_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSpecificAttachments.Checked)
            {
                lbCSVLocation.Text = "Please choose a location of CSV file.  Folder containing attachments will be downloaded to same folder as CSV file.";
                tbCSVLocation.ReadOnly = false;
                tbCSVLocation.Enabled = true;
                butCSVBrowse.Enabled = true;
                gbStep3.Enabled = false;
            }
        }

        private void Step1RadioButtons(object sender, EventArgs e)
        {
            gbStep2.Enabled = true;
        }

        private void rbReportOnly_CheckedChanged(object sender, EventArgs e)
        {
            if(rbReportOnly.Checked)
            {
                lbCSVLocation.Text = "This option will ONLY report on the attachments.  Screen below will be populated and you can export the results.";
                tbCSVLocation.ReadOnly = false;
                tbCSVLocation.Enabled = false;
                butCSVBrowse.Enabled = false;
                gbStep3.Enabled = true;
            }
        }

        #endregion

        int recordCount;
        int loopCounter = 1;

        private void PerformAction()
        {
            #region Download
            if(!rbReportOnly.Checked)
            {
                #region Download Notes
                if (rbNotes.Checked)
                {
                    ProcessNotes();
                }
                #endregion

                #region Download Emails
                if (rbEMail.Checked)
                {
                    ProcessEmails();
                }
                #endregion

                #region Download Both
                if (rbBoth.Checked)
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
                if(rbNotes.Checked)
                {
                    ReportNotes();
                }
                #endregion

                #region Report E-Mails
                if (rbEMail.Checked)
                {
                    ReportEmails();
                }
                #endregion

                #region Report Both
                if (rbBoth.Checked)
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
            WorkAsync(new WorkAsyncInfo()
            {
                Message = "Processing...",
                Work = (Worker, args) =>
                {
                    Entity oNoteData = new Entity();
                    string storeAttahmentDirectory = null;

                    CRMAttachmentDAO crmDAO = new CRMAttachmentDAO();
                    LocalFileSystemDAO localDAO = new LocalFileSystemDAO();

                    List<Guid> oAttachmentGuids = (rbAllAttachments.Checked) ? crmDAO.GetListOfAttachments(Service) : localDAO.ReadFromCSV(tbCSVLocation.Text);
                    recordCount = oAttachmentGuids.Count();
                    string fileDirectory = (rbAllAttachments.Checked) ? localDAO.CreateLocalDirectory(tbCSVLocation.Text, false, false, false) : localDAO.CreateLocalDirectory(tbCSVLocation.Text, false, true, false);
                    foreach (Guid attachment in oAttachmentGuids)
                    {
                        try
                        {
                            oNoteData = null;
                            storeAttahmentDirectory = null;
                            oNoteData = crmDAO.GetNoteAttachmentData(attachment, Service);
                            if(oNoteData["filesize"].ToString() != "0")
                            {
                                storeAttahmentDirectory = localDAO.CreateLocalDirectory(Path.Combine(fileDirectory, oNoteData.Id.ToString()), true, false, false);
                                localDAO.CreateAttachmentFile(oNoteData["documentbody"].ToString(), storeAttahmentDirectory, oNoteData["filename"].ToString());

                                ListViewItem _ListViewItem = new ListViewItem(DateTime.Now.ToString());
                                _ListViewItem.SubItems.Add(oNoteData["annotationid"].ToString());
                                _ListViewItem.SubItems.Add(oNoteData["filename"].ToString());
                                _ListViewItem.SubItems.Add(oNoteData["filesize"].ToString());
                                _ListViewItem.SubItems.Add(storeAttahmentDirectory);
                                _ListViewItem.SubItems.Add(oNoteData["objecttypecode"].ToString());
                                _ListViewItem.SubItems.Add(((EntityReference)oNoteData["objectid"]).Id.ToString());
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
                            ListViewItem _ListViewItem = new ListViewItem(DateTime.Now.ToString());
                            _ListViewItem.SubItems.Add(oNoteData?["annotationid"].ToString());
                            _ListViewItem.SubItems.Add(oNoteData?["filename"].ToString());
                            _ListViewItem.SubItems.Add(oNoteData["filesize"].ToString());
                            _ListViewItem.SubItems.Add(storeAttahmentDirectory);
                            _ListViewItem.SubItems.Add(oNoteData?["objecttypecode"].ToString());
                            _ListViewItem.SubItems.Add((oNoteData != null) ? ((EntityReference)oNoteData["objectid"]).Id.ToString() : null);
                            _ListViewItem.SubItems.Add(ex.Message);

                            Worker.ReportProgress(0, _ListViewItem);
                        }
                    }
                },
                ProgressChanged = pc =>
                {
                    if (pc.ProgressPercentage == 1)
                    {
                        SetWorkingMessage(string.Format("Processing Notes: {0} of {1}", loopCounter, recordCount));
                    }
                    else
                    {
                        lvMainOutput.Items.Add((ListViewItem)pc.UserState);
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
                    string storeAttahmentDirectory = null;

                    CRMAttachmentDAO crmDAO = new CRMAttachmentDAO();
                    LocalFileSystemDAO localDAO = new LocalFileSystemDAO();

                    List<Guid> oAttachmentGuids = (rbAllAttachments.Checked) ? crmDAO.GetListOfActivityMimeAttachmentGuids(Service) : localDAO.ReadFromCSV(tbCSVLocation.Text);
                    recordCount = oAttachmentGuids.Count();
                    string fileDirectory = (rbAllAttachments.Checked) ? localDAO.CreateLocalDirectory(tbCSVLocation.Text, false, false, true) : localDAO.CreateLocalDirectory(tbCSVLocation.Text, false, true, true);
                    foreach (Guid attachment in oAttachmentGuids)
                    {
                        try
                        {
                            oEMailData = null;
                            storeAttahmentDirectory = null;
                            oEMailData = crmDAO.GetActivityMimeAttachmentData(attachment, Service);

                            if (oEMailData["filesize"].ToString() != "0")
                            {
                                storeAttahmentDirectory = localDAO.CreateLocalDirectory(Path.Combine(fileDirectory, oEMailData.Id.ToString()), true, false, true);
                                localDAO.CreateAttachmentFile(oEMailData["body"].ToString(), storeAttahmentDirectory, oEMailData["filename"].ToString());

                                ListViewItem _ListViewItem = new ListViewItem(DateTime.Now.ToString());
                                _ListViewItem.SubItems.Add(oEMailData["activitymimeattachmentid"].ToString());
                                _ListViewItem.SubItems.Add(oEMailData["filename"].ToString());
                                _ListViewItem.SubItems.Add(oEMailData["filesize"].ToString());
                                _ListViewItem.SubItems.Add(storeAttahmentDirectory);
                                _ListViewItem.SubItems.Add(oEMailData["objecttypecode"].ToString());
                                _ListViewItem.SubItems.Add(((EntityReference)oEMailData["objectid"]).Id.ToString());
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
                            ListViewItem _ListViewItem = new ListViewItem(DateTime.Now.ToString());
                            _ListViewItem.SubItems.Add(oEMailData?["activitymimeattachmentid"].ToString());
                            _ListViewItem.SubItems.Add(oEMailData?["filename"].ToString());
                            _ListViewItem.SubItems.Add(oEMailData["filesize"].ToString());
                            _ListViewItem.SubItems.Add(storeAttahmentDirectory);
                            _ListViewItem.SubItems.Add(oEMailData?["objecttypecode"].ToString());
                            _ListViewItem.SubItems.Add((oEMailData != null) ? ((EntityReference)oEMailData["objectid"]).Id.ToString() : null);
                            _ListViewItem.SubItems.Add(ex.Message);

                            Worker.ReportProgress(0, _ListViewItem);
                        }
                    }
                },
                ProgressChanged = pc =>
                {
                    if (pc.ProgressPercentage == 1)
                    {
                        SetWorkingMessage(string.Format("Processing E-mails: {0} of {1}", loopCounter, recordCount));
                    }
                    else
                    {
                        lvMainOutput.Items.Add((ListViewItem)pc.UserState);
                    }
                }
            });
        }

        private void ReportNotes()
        {
            OutputModel noteRecord = null;
            WorkAsync(new WorkAsyncInfo()
            {
                Message = "Processing...",
                Work = (Worker, args) =>
                {

                    CRMAttachmentDAO crmDAO = new CRMAttachmentDAO();
                    LocalFileSystemDAO localDAO = new LocalFileSystemDAO();

                    List<OutputModel> oNoteData = crmDAO.ReportNoteAttachments(Service);

                    foreach (OutputModel note in oNoteData)
                    {
                        try
                        {
                            noteRecord = note;
                            ListViewItem _ListViewItem = new ListViewItem(note.DateTimeProcessed);
                            _ListViewItem.SubItems.Add(note.GUID.ToString());
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
                },
                ProgressChanged = pc =>
                {
                    lvMainOutput.Items.Add((ListViewItem)pc.UserState);
                }
            });
        }

        private void ReportEmails()
        {
            OutputModel emailRecord = null;
            WorkAsync(new WorkAsyncInfo()
            {
                Message = "Processing...",
                Work = (Worker, args) =>
                {
                    try
                    {
                        CRMAttachmentDAO crmDAO = new CRMAttachmentDAO();
                        LocalFileSystemDAO localDAO = new LocalFileSystemDAO();

                        List<OutputModel> oEmailData = crmDAO.ReportMimeAttachments(Service);

                        foreach (OutputModel email in oEmailData)
                        {
                            emailRecord = email;
                            ListViewItem _ListViewItem = new ListViewItem(email.DateTimeProcessed);
                            _ListViewItem.SubItems.Add(email.GUID.ToString());
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
                },
                ProgressChanged = pc =>
                {
                    lvMainOutput.Items.Add((ListViewItem)pc.UserState);
                }
            });
        }
    }
}