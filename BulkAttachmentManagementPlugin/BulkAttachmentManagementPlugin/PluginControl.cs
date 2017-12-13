using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

using System.IO;
using BulkAttachmentManagementPlugin.Data_Access_Objects;
using XrmToolBox.Extensibility.Args;
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
                            gbStep4.Enabled = true;
                        }
                    }
                    else
                    {
                        DialogResult ofdResult = ofdCVSFile.ShowDialog();
                        if (ofdResult == DialogResult.OK)
                        {
                            tbCSVLocation.Text = ofdCVSFile.FileName;
                            gbStep4.Enabled = true;
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
                    gbStep4.Enabled = true;
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
                            DownloadLocation = (item.SubItems[3].Text != null) ? item.SubItems[3].Text : string.Empty,
                            RegardingID = (item.SubItems[4].Text != null) ? item.SubItems[4].Text : string.Empty,
                            RegardingEntity = (item.SubItems[5].Text != null) ? item.SubItems[5].Text : string.Empty,
                            ErrorMessage = (item.SubItems[6].Text != null) ? item.SubItems[6].Text : string.Empty
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
                lbCSVLocation.Text = "Please choose a location to download attachments to.";
                gbStep4.Enabled = false;
            }
        }

        private void rbSpecificAttachments_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSpecificAttachments.Checked)
            {
                lbCSVLocation.Text = "Folder containing attachments will be downloaded to same folder as CSV file.";
                tbCSVLocation.ReadOnly = false;
                tbCSVLocation.Enabled = true;
                butCSVBrowse.Enabled = true;
                gbStep4.Enabled = false;
            }
        }

        private void rbDownload_CheckedChanged(object sender, EventArgs e)
        {
            gbStep3.Enabled = true;
            if(rbDownload.Checked)
            {
                gbStep3.Text = "Step 3: (What To Download.)";
                rbAllAttachments.Enabled = true;
                rbSpecificAttachments.Enabled = true;
                tbCSVLocation.ReadOnly = false;
                tbCSVLocation.Enabled = true;
                butCSVBrowse.Enabled = true;
                tbCSVLocation.Text = string.Empty;
                gbStep4.Enabled = false;
            }
        }

        private void rbUpload_CheckedChanged(object sender, EventArgs e)
        {
            gbStep3.Enabled = true;
            if (rbUpload.Checked)
            {
                gbStep3.Text = "Step 3: (What To Upload - Choose root folder location.)";
                lbCSVLocation.Text = "";
                rbAllAttachments.Enabled = false;
                rbSpecificAttachments.Enabled = false;
                tbCSVLocation.ReadOnly = false;
                tbCSVLocation.Enabled = true;
                butCSVBrowse.Enabled = true;
                rbAllAttachments.Checked = false;
                rbSpecificAttachments.Checked = false;
                tbCSVLocation.Text = string.Empty;
                gbStep4.Enabled = false;
            }
        }
        #endregion

        int recordCount;
        int loopCounter = 1;
        private void PerformAction()
        {
            WorkAsync(new WorkAsyncInfo()
            {
                Message = "Processing...",
                Work = (Worker, args) =>
                {
                    if(rbDownload.Checked)
                    {
                        Entity oNoteData = new Entity();
                        string storeAttahmentDirectory = null;
                        try
                        {
                            CRMAttachmentDAO crmDAO = new CRMAttachmentDAO();
                            LocalFileSystemDAO localDAO = new LocalFileSystemDAO();

                            List<Guid> oAttachmentGuids = (rbAllAttachments.Checked) ? crmDAO.GetListOfAttachments(Service) : localDAO.ReadFromCSV(tbCSVLocation.Text);
                            recordCount = oAttachmentGuids.Count();
                            string fileDirectory = (rbAllAttachments.Checked) ? localDAO.CreateLocalDirectory(tbCSVLocation.Text, false, false) : localDAO.CreateLocalDirectory(tbCSVLocation.Text, false, true);
                            foreach (Guid attachment in oAttachmentGuids)
                            {
                                oNoteData = null;
                                storeAttahmentDirectory = null;
                                oNoteData = crmDAO.GetNoteAttachmentData(attachment, Service);
                                storeAttahmentDirectory = localDAO.CreateLocalDirectory(Path.Combine(fileDirectory, oNoteData.Id.ToString()), true, false);
                                localDAO.CreateAttachmentFile(oNoteData["documentbody"].ToString(), storeAttahmentDirectory, oNoteData["filename"].ToString());

                                ListViewItem _ListViewItem = new ListViewItem(DateTime.Now.ToString());
                                _ListViewItem.SubItems.Add(oNoteData["annotationid"].ToString());
                                _ListViewItem.SubItems.Add(oNoteData["filename"].ToString());
                                _ListViewItem.SubItems.Add(storeAttahmentDirectory);
                                _ListViewItem.SubItems.Add(oNoteData["objecttypecode"].ToString());
                                _ListViewItem.SubItems.Add(((EntityReference)oNoteData["objectid"]).Id.ToString());
                                _ListViewItem.SubItems.Add("");

                                Worker.ReportProgress(0, _ListViewItem);
                                Worker.ReportProgress(1);
                                loopCounter++;
                            }
                        }
                        catch (Exception ex)
                        {
                            ListViewItem _ListViewItem = new ListViewItem(DateTime.Now.ToString());
                            _ListViewItem.SubItems.Add(oNoteData?["annotationid"].ToString());
                            _ListViewItem.SubItems.Add(oNoteData?["filename"].ToString());
                            _ListViewItem.SubItems.Add(storeAttahmentDirectory);
                            _ListViewItem.SubItems.Add(oNoteData?["objecttypecode"].ToString());
                            _ListViewItem.SubItems.Add((oNoteData != null) ? ((EntityReference)oNoteData["objectid"]).Id.ToString() : null);
                            _ListViewItem.SubItems.Add(ex.Message);

                            Worker.ReportProgress(0, _ListViewItem);
                        }
                    }
                    if(rbUpload.Checked)
                    {

                    }
                },
                ProgressChanged = pc =>
                {
                    if(pc.ProgressPercentage == 1)
                    {
                        SetWorkingMessage(string.Format("Processing {0} of {1}", loopCounter, recordCount));
                    }
                    else
                    {
                        lvMainOutput.Items.Add((ListViewItem)pc.UserState);
                    }
                }
            });
        }
    }
}
