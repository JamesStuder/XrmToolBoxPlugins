using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using System.IO;
using System.Linq;
using BulkAttachmentManagementPlugin.Constants;
using BulkAttachmentManagementPlugin.Data_Access_Objects;
using Microsoft.Xrm.Sdk;
using BulkAttachmentManagementPlugin.Models;

namespace BulkAttachmentManagementPlugin
{
    public partial class PluginControl : PluginControlBase, IGitHubPlugin, IHelpPlugin
    {
        #region GitHub Info
        public string RepositoryName => "XrmToolBoxPlugins";
        public string UserName => "medicstuder";
        #endregion

        #region Help Info
        public string HelpUrl => "https://github.com/medicstuder/XrmToolBoxPlugins/wiki";
        #endregion
        
        public PluginControl()
        {
            InitializeComponent();
        }

        #region Buttons
        private void ButtonCsvBrowse_Click(object sender, EventArgs e)
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
                if (fbdResult != DialogResult.OK)
                {
                    return;
                }
                TextBoxCsvLocation.Text = FolderBrowserDialogMainFile.SelectedPath;
                GroupStep3.Enabled = true;
            }

        }
       
        private void ButtonRun_Click(object sender, EventArgs e)
        {
            ExecuteMethod(PerformAction);
        }

        private void ButtonExport_Click(object sender, EventArgs e)
        {
            DialogResult sfdResult = SaveFileDialogCsvFile.ShowDialog();
            if (sfdResult != DialogResult.OK)
            {
                return;
            }
            LocalFileSystemDao lfsDao = new LocalFileSystemDao();
            List<OutputModel> oOutputModel = new List<OutputModel>();
            if (ListViewMainOutput.Items.Count > 0 && ListViewMainOutput != null)
            {
                oOutputModel.AddRange(from ListViewItem item in ListViewMainOutput.Items
                    select new OutputModel
                    {
                        DateTimeProcessed = item.Text ?? string.Empty,
                        Guid = item.SubItems[1].Text ?? string.Empty,
                        FileName = item.SubItems[2].Text ?? string.Empty,
                        FileSize = item.SubItems[3].Text ?? string.Empty,
                        DownloadLocation = item.SubItems[4].Text ?? string.Empty,
                        RegardingId = item.SubItems[5].Text ?? string.Empty,
                        RegardingEntity = item.SubItems[6].Text ?? string.Empty,
                        ErrorMessage = item.SubItems[7].Text ?? string.Empty
                    });
            }
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Exporting Data...",
                Work = (worker, args) =>
                {
                    lfsDao.ExportResultsToCsv(oOutputModel, SaveFileDialogCsvFile.FileName);
                }
            });
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            ListViewMainOutput.Clear();
            GroupStep2.Enabled = false;
            GroupStep3.Enabled = false;
        }
        #endregion

        #region Radio Buttons
        private void Step1RadioButtons(object sender, EventArgs e)
        {
            GroupStep2.Enabled = true;
        }
        
        private void RadioDownloadAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!RadioDownloadAll.Checked)
            {
                return;
            }
            
            LabelCsvLocation.Text = @"Please choose the location to download attachments to";
            TextBoxCsvLocation.ReadOnly = false;
            TextBoxCsvLocation.Enabled = true;
            ButtonCsvBrowse.Enabled = true;
            GroupStep3.Enabled = false;
        }

        private void RadioDownloadSpecific_CheckedChanged(object sender, EventArgs e)
        {
            if (!RadioDownloadSpecific.Checked)
            {
                return;
            }
            
            LabelCsvLocation.Text = @"Please choose a location of CSV file.  Folder containing attachments will be downloaded to same folder as CSV file";
            TextBoxCsvLocation.ReadOnly = false;
            TextBoxCsvLocation.Enabled = true;
            ButtonCsvBrowse.Enabled = true;
            GroupStep3.Enabled = false;
        }

        private void RadioReportOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (!RadioReportOnly.Checked)
            {
                return;
            }
            
            LabelCsvLocation.Text = @"This option will ONLY report on the attachments.  Screen below will be populated and you can export the results";
            TextBoxCsvLocation.ReadOnly = false;
            TextBoxCsvLocation.Enabled = false;
            ButtonCsvBrowse.Enabled = false;
            GroupStep3.Enabled = true;
        }
        #endregion

        private void PerformAction()
        {
            #region Download
            if(!RadioReportOnly.Checked)
            {
                
                if (RadioNotes.Checked)
                {
                    DownloadNotes();
                    return;
                } 
                else if (RadioEmail.Checked)
                {
                    DownloadEmails();
                    return;
                }
                else if (RadioFiles.Checked)
                {
                    DownloadFiles();
                    return;
                }
                else if (RadioAll.Checked)
                {
                    DownloadNotes();
                    DownloadEmails();
                    DownloadFiles();
                    return;
                }
            }
            #endregion

            #region Report
            if(RadioNotes.Checked)
            {
                ReportNotes();
            }
            else if (RadioEmail.Checked)
            {
                ReportEmails();
            }
            else if (RadioFiles.Checked)
            {
                ReportFiles();
            }
            else if (RadioAll.Checked)
            {
                ReportNotes();
                ReportEmails();
            }
            #endregion

        }

        #region Process Downloads
        private void DownloadNotes()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Processing...",
                Work = (worker, args) =>
                {
                    Entity oNoteData = new Entity();
                    string storeAttachmentDirectory = null;

                    CrmAttachmentDao crmDao = new CrmAttachmentDao();
                    LocalFileSystemDao localDao = new LocalFileSystemDao();

                    List<Guid> oAttachmentGuids = RadioDownloadAll.Checked ? crmDao.GetListOfAttachments(Service) : localDao.ReadFromCsv(TextBoxCsvLocation.Text);
                    string fileDirectory = RadioDownloadAll.Checked ? localDao.CreateLocalDirectory(TextBoxCsvLocation.Text, false, false, PluginConstants.FolderNotes) : localDao.CreateLocalDirectory(TextBoxCsvLocation.Text, false, true, PluginConstants.FolderNotes);
                    for (int index = 0; index < oAttachmentGuids.Count; index++)
                    {
                        string processingMessage = $"Processing {index} of {oAttachmentGuids.Count}";
                        int progressPercentage = (int)(index / (float)oAttachmentGuids.Count * 100);
                        
                        ListViewItem listViewItem = new ListViewItem(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                        Guid attachment = oAttachmentGuids[index];
                        try
                        {
                            oNoteData = null;
                            storeAttachmentDirectory = null;
                            oNoteData = crmDao.GetNoteAttachmentData(attachment, Service);
                            if (oNoteData != null && oNoteData[Annotation.FileSize].ToString() != "0")
                            {
                                storeAttachmentDirectory = localDao.CreateLocalDirectory(Path.Combine(fileDirectory, oNoteData.Id.ToString()), true, false, PluginConstants.FolderNotes);
                                localDao.CreateAttachmentFile(oNoteData[Annotation.DocumentBody].ToString(), storeAttachmentDirectory, oNoteData[Annotation.FileName].ToString());

                                listViewItem.SubItems.Add(oNoteData[Annotation.PrimaryKey].ToString());
                                listViewItem.SubItems.Add(oNoteData[Annotation.FileName].ToString());
                                listViewItem.SubItems.Add(oNoteData[Annotation.FileSize].ToString());
                                listViewItem.SubItems.Add(storeAttachmentDirectory);
                                listViewItem.SubItems.Add(oNoteData[Annotation.ObjectTypeCode].ToString());
                                listViewItem.SubItems.Add(((EntityReference)oNoteData[Annotation.ObjectId]).Id.ToString());
                                listViewItem.SubItems.Add("");
                            }
                        }
                        catch (Exception ex)
                        {
                            if (oNoteData != null)
                            {
                                listViewItem.SubItems.Add(oNoteData[Annotation.PrimaryKey].ToString());
                                listViewItem.SubItems.Add(oNoteData[Annotation.FileName].ToString());
                                listViewItem.SubItems.Add(oNoteData[Annotation.FileSize].ToString());
                                listViewItem.SubItems.Add(storeAttachmentDirectory);
                                listViewItem.SubItems.Add(oNoteData[Annotation.ObjectTypeCode].ToString());
                                listViewItem.SubItems.Add(((EntityReference)oNoteData[Annotation.ObjectId]).Id.ToString());
                                listViewItem.SubItems.Add(ex.Message);
                            }
                            
                        }
                        worker.ReportProgress(progressPercentage, processingMessage);
                        ListViewMainOutput.Invoke(new Action(() =>
                        {
                            ListViewMainOutput.Items.Add(listViewItem);
                        }));
                    }
                },
                ProgressChanged = args =>
                {
                    SetWorkingMessage($"{args.ProgressPercentage} Completed.  {args.UserState}");
                }
            });
        }

        private void DownloadEmails()
        {
            WorkAsync(new WorkAsyncInfo()
            {
                Message = "Processing...",
                Work = (worker, args) =>
                {
                    Entity oEMailData = new Entity();
                    string storeAttachmentDirectory = null;

                    CrmAttachmentDao crmDao = new CrmAttachmentDao();
                    LocalFileSystemDao localDao = new LocalFileSystemDao();

                    List<Guid> oAttachmentGuids = RadioDownloadAll.Checked ? crmDao.GetListOfActivityMimeAttachmentGuids(Service) : localDao.ReadFromCsv(TextBoxCsvLocation.Text);
                    
                    string fileDirectory = RadioDownloadAll.Checked ? localDao.CreateLocalDirectory(TextBoxCsvLocation.Text, false, false, PluginConstants.FolderEmails) : localDao.CreateLocalDirectory(TextBoxCsvLocation.Text, false, true, PluginConstants.FolderEmails);
                    for (int index = 0; index < oAttachmentGuids.Count; index++)
                    {
                        string processingMessage = $"Processing {index} of {oAttachmentGuids.Count}";
                        int progressPercentage = (int)(index / (float)oAttachmentGuids.Count * 100);

                        ListViewItem listViewItem = new ListViewItem(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                        Guid attachment = oAttachmentGuids[index];
                        try
                        {
                            oEMailData = null;
                            storeAttachmentDirectory = null;
                            oEMailData = crmDao.GetActivityMimeAttachmentData(attachment, Service);

                            if (oEMailData[ActivityMimeAttachment.FileSize].ToString() != "0")
                            {
                                storeAttachmentDirectory = localDao.CreateLocalDirectory(Path.Combine(fileDirectory, oEMailData.Id.ToString()), true, false, PluginConstants.FolderEmails);
                                localDao.CreateAttachmentFile(oEMailData[ActivityMimeAttachment.Body].ToString(), storeAttachmentDirectory, oEMailData[ActivityMimeAttachment.FileName].ToString());

                                listViewItem.SubItems.Add(oEMailData[ActivityMimeAttachment.PrimaryKey].ToString());
                                listViewItem.SubItems.Add(oEMailData[ActivityMimeAttachment.FileName].ToString());
                                listViewItem.SubItems.Add(oEMailData[ActivityMimeAttachment.FileSize].ToString());
                                listViewItem.SubItems.Add(storeAttachmentDirectory);
                                listViewItem.SubItems.Add(oEMailData[ActivityMimeAttachment.ObjectTypeCode].ToString());
                                listViewItem.SubItems.Add(((EntityReference)oEMailData[ActivityMimeAttachment.ObjectId]).Id.ToString());
                                listViewItem.SubItems.Add("");
                            }
                        }
                        catch (Exception ex)
                        {
                            if (oEMailData != null)
                            {
                                listViewItem.SubItems.Add(oEMailData[ActivityMimeAttachment.PrimaryKey].ToString());
                                listViewItem.SubItems.Add(oEMailData[ActivityMimeAttachment.FileName].ToString());
                                listViewItem.SubItems.Add(oEMailData[ActivityMimeAttachment.FileSize].ToString());
                                listViewItem.SubItems.Add(storeAttachmentDirectory);
                                listViewItem.SubItems.Add(oEMailData[ActivityMimeAttachment.ObjectTypeCode].ToString());
                                listViewItem.SubItems.Add(((EntityReference)oEMailData[ActivityMimeAttachment.ObjectId]).Id.ToString());
                                listViewItem.SubItems.Add(ex.Message);
                            }
                        }
                        worker.ReportProgress(progressPercentage, processingMessage);
                        ListViewMainOutput.Invoke(new Action(() =>
                        {
                            ListViewMainOutput.Items.Add(listViewItem);
                        }));
                    }
                },
                ProgressChanged = args =>
                {
                    SetWorkingMessage($"{args.ProgressPercentage} Completed.  {args.UserState}");
                }
            });
        }

        private void DownloadFiles()
        {
            WorkAsync(new WorkAsyncInfo()
            {
                Message = "Processing...",
                Work = (worker, args) =>
                {
                    Entity oFileData = new Entity();
                    string storeAttachmentDirectory = null;

                    CrmAttachmentDao crmDao = new CrmAttachmentDao();
                    LocalFileSystemDao localDao = new LocalFileSystemDao();

                    List<Guid> oAttachmentGuids = RadioDownloadAll.Checked ? crmDao.GetListOfFileAttachmentGuids(Service) : localDao.ReadFromCsv(TextBoxCsvLocation.Text);

                    string fileDirectory = RadioDownloadAll.Checked ? localDao.CreateLocalDirectory(TextBoxCsvLocation.Text, false, false, PluginConstants.FolderEmails) : localDao.CreateLocalDirectory(TextBoxCsvLocation.Text, false, true, PluginConstants.FolderFiles);
                    for (int index = 0; index < oAttachmentGuids.Count; index++)
                    {
                        string processingMessage = $"Processing {index} of {oAttachmentGuids.Count}";
                        int progressPercentage = (int)(index / (float)oAttachmentGuids.Count * 100);

                        ListViewItem listViewItem = new ListViewItem(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                        Guid attachment = oAttachmentGuids[index];
                        try
                        {
                            oFileData = null;
                            storeAttachmentDirectory = null;
                            oFileData = crmDao.GetFileAttachmentData(attachment, Service);

                            if (oFileData[FileAttachment.FileSize].ToString() != "0")
                            {
                                storeAttachmentDirectory = localDao.CreateLocalDirectory(Path.Combine(fileDirectory, oFileData.Id.ToString()), true, false, PluginConstants.FolderFiles);
                                byte[] fileAttachment = crmDao.DownloadFileAttribute(((EntityReference)oFileData[FileAttachment.ObjectId]).LogicalName, ((EntityReference)oFileData[FileAttachment.ObjectId]).Id, (string)oFileData[FileAttachment.RegardingFieldName], Service);
                                localDao.CreateAttachmentFile(fileAttachment, storeAttachmentDirectory, oFileData[FileAttachment.FileName].ToString());

                                listViewItem.SubItems.Add(oFileData[FileAttachment.PrimaryKey].ToString());
                                listViewItem.SubItems.Add(oFileData[FileAttachment.FileName].ToString());
                                listViewItem.SubItems.Add(oFileData[FileAttachment.FileSize].ToString());
                                listViewItem.SubItems.Add(storeAttachmentDirectory);
                                listViewItem.SubItems.Add(oFileData[FileAttachment.ObjectTypeCode].ToString());
                                listViewItem.SubItems.Add(((EntityReference)oFileData[FileAttachment.ObjectId]).Id.ToString());
                                listViewItem.SubItems.Add("");
                            }
                        }
                        catch (Exception ex)
                        {
                            if (oFileData != null)
                            {
                                listViewItem.SubItems.Add(oFileData[FileAttachment.PrimaryKey].ToString());
                                listViewItem.SubItems.Add(oFileData[FileAttachment.FileName].ToString());
                                listViewItem.SubItems.Add(oFileData[FileAttachment.FileSize].ToString());
                                listViewItem.SubItems.Add(storeAttachmentDirectory);
                                listViewItem.SubItems.Add(oFileData[FileAttachment.ObjectTypeCode].ToString());
                                listViewItem.SubItems.Add(((EntityReference)oFileData[FileAttachment.ObjectId]).Id.ToString());
                                listViewItem.SubItems.Add(ex.Message);
                            }
                        }
                        worker.ReportProgress(progressPercentage, processingMessage);
                        ListViewMainOutput.Invoke(new Action(() =>
                        {
                            ListViewMainOutput.Items.Add(listViewItem);
                        }));
                    }
                },
                ProgressChanged = args =>
                {
                    SetWorkingMessage($"{args.ProgressPercentage} Completed.  {args.UserState}");
                }
            });
        }
        #endregion

        #region Process Reporting
        private void ReportNotes()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Processing...",
                Work = (worker, args) =>
                {

                    CrmAttachmentDao crmDao = new CrmAttachmentDao();
                    List<OutputModel> oNoteData = crmDao.ReportNoteAttachments(Service);

                    for (int index = 0; index < oNoteData.Count; index++)
                    {
                        string processingMessage = $"Processing {index} of {oNoteData.Count}";
                        int progressPercentage = (int)(index / (float)oNoteData.Count * 100);
                        OutputModel note = oNoteData[index];
                        ListViewItem listViewItem = new ListViewItem(note.DateTimeProcessed);
                        
                        try
                        {
                            listViewItem.SubItems.Add(note.Guid);
                            listViewItem.SubItems.Add(note.FileName);
                            listViewItem.SubItems.Add(note.FileSize);
                            listViewItem.SubItems.Add(note.DownloadLocation);
                            listViewItem.SubItems.Add(note.RegardingEntity);
                            listViewItem.SubItems.Add(note.RegardingId);
                            listViewItem.SubItems.Add("");
                        }
                        catch (Exception ex)
                        {
                            listViewItem.SubItems.Add(note.Guid);
                            listViewItem.SubItems.Add(note.FileName);
                            listViewItem.SubItems.Add(note.FileSize);
                            listViewItem.SubItems.Add(note.DownloadLocation);
                            listViewItem.SubItems.Add(note.RegardingEntity);
                            listViewItem.SubItems.Add(note.RegardingId);
                            listViewItem.SubItems.Add(ex.Message);
                        }
                        worker.ReportProgress(progressPercentage, processingMessage);
                        ListViewMainOutput.Invoke(new Action(() =>
                        {
                            ListViewMainOutput.Items.Add(listViewItem);
                        }));
                    }
                },
                ProgressChanged = args =>
                {
                    SetWorkingMessage($"{args.ProgressPercentage} Completed.  {args.UserState}");
                }
            });
        }

        private void ReportEmails()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Processing...",
                Work = (worker, args) =>
                {
                    CrmAttachmentDao crmDao = new CrmAttachmentDao();
                    List<OutputModel> oEmailData = crmDao.ReportMimeAttachments(Service);
                    for (int index = 0; index < oEmailData.Count; index++)
                    {
                        string processingMessage = $"Processing {index} of {oEmailData.Count}";
                        int progressPercentage = (int)(index / (float)oEmailData.Count * 100);
                        OutputModel email = oEmailData[index];
                        ListViewItem listViewItem = new ListViewItem(email.DateTimeProcessed);
                        try
                        {
                            
                            listViewItem.SubItems.Add(email.Guid);
                            listViewItem.SubItems.Add(email.FileName);
                            listViewItem.SubItems.Add(email.FileSize);
                            listViewItem.SubItems.Add(email.DownloadLocation);
                            listViewItem.SubItems.Add(email.RegardingEntity);
                            listViewItem.SubItems.Add(email.RegardingId);
                            listViewItem.SubItems.Add("");
                        }
                        catch (Exception ex)
                        {
                            listViewItem.SubItems.Add(email.Guid);
                            listViewItem.SubItems.Add(email.FileName);
                            listViewItem.SubItems.Add(email.FileSize);
                            listViewItem.SubItems.Add(email.DownloadLocation);
                            listViewItem.SubItems.Add(email.RegardingEntity);
                            listViewItem.SubItems.Add(email.RegardingId);
                            listViewItem.SubItems.Add(ex.Message);
                        }
                        worker.ReportProgress(progressPercentage, processingMessage);
                        ListViewMainOutput.Invoke(new Action(() =>
                        {
                            ListViewMainOutput.Items.Add(listViewItem);
                        }));
                    }
                },
                ProgressChanged = args =>
                {
                    SetWorkingMessage($"{args.ProgressPercentage} Completed.  {args.UserState}");
                }
            });
        }

        private void ReportFiles()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Processing...",
                Work = (worker, args) =>
                {
                    CrmAttachmentDao crmDao = new CrmAttachmentDao();
                    List<OutputModel> oFileData = crmDao.ReportFileAttachments(Service);
                    for (int index = 0; index < oFileData.Count; index++)
                    {
                        string processingMessage = $"Processing {index} of {oFileData.Count}";
                        int progressPercentage = (int)(index / (float)oFileData.Count * 100);
                        OutputModel file = oFileData[index];
                        ListViewItem listViewItem = new ListViewItem(file.DateTimeProcessed);
                        try
                        {

                            listViewItem.SubItems.Add(file.Guid);
                            listViewItem.SubItems.Add(file.FileName);
                            listViewItem.SubItems.Add(file.FileSize);
                            listViewItem.SubItems.Add(file.DownloadLocation);
                            listViewItem.SubItems.Add(file.RegardingEntity);
                            listViewItem.SubItems.Add(file.RegardingId);
                            listViewItem.SubItems.Add("");
                        }
                        catch (Exception ex)
                        {
                            listViewItem.SubItems.Add(file.Guid);
                            listViewItem.SubItems.Add(file.FileName);
                            listViewItem.SubItems.Add(file.FileSize);
                            listViewItem.SubItems.Add(file.DownloadLocation);
                            listViewItem.SubItems.Add(file.RegardingEntity);
                            listViewItem.SubItems.Add(file.RegardingId);
                            listViewItem.SubItems.Add(ex.Message);
                        }
                        worker.ReportProgress(progressPercentage, processingMessage);
                        ListViewMainOutput.Invoke(new Action(() =>
                        {
                            ListViewMainOutput.Items.Add(listViewItem);
                        }));
                    }
                },
                ProgressChanged = args =>
                {
                    SetWorkingMessage($"{args.ProgressPercentage} Completed.  {args.UserState}");
                }
            });
        }
        #endregion
    }
}