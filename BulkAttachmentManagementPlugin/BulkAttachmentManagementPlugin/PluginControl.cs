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
using BulkAttachmentManagementPlugin.Services;
using System.IO;
using BulkAttachmentManagementPlugin.Data_Access_Objects;
using XrmToolBox.Extensibility.Args;

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
                    DialogResult ofdResult = ofdCVSFile.ShowDialog();
                    if (ofdResult == DialogResult.OK)
                    {
                        tbCSVLocation.Text = ofdCVSFile.FileName;
                        gbStep3.Enabled = true;
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
                pbMain.Minimum = 0;
                pbMain.Maximum = lvMainOutput.Items.Count;
                pbMain.Value = 0;
                LocalFileSystemDAO lfsDAO = new LocalFileSystemDAO();
                CSVExportService export = new CSVExportService();
                lfsDAO.CreateLocalFile(sfdCSVFile.FileName);
                export.ExportToCSV(lvMainOutput, sfdCSVFile.FileName, pbMain);
            }
        }
        #endregion

        #region Radio Buttons
        private void rbAllAttachments_CheckedChanged(object sender, EventArgs e)
        {
            if(rbAllAttachments.Checked)
            {
                tbCSVLocation.ReadOnly = true;
                tbCSVLocation.Enabled = false;
                butCSVBrowse.Enabled = false;
                tbCSVLocation.Text = string.Empty;
                gbStep3.Enabled = true;
            }
        }

        private void rbSpecificAttachments_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSpecificAttachments.Checked)
            {
                tbCSVLocation.ReadOnly = false;
                tbCSVLocation.Enabled = true;
                butCSVBrowse.Enabled = true;
                gbStep3.Enabled = false;
            }
        }

        private void rbDownload_CheckedChanged(object sender, EventArgs e)
        {
            gbStep2.Enabled = true;
            if(rbDownload.Checked)
            {
                gbStep2.Text = "Step 2: (What To Download - Attachment folder will be created in same folder as CSV file.)";
                rbAllAttachments.Enabled = true;
                rbSpecificAttachments.Enabled = true;
                tbCSVLocation.ReadOnly = false;
                tbCSVLocation.Enabled = true;
                butCSVBrowse.Enabled = true;
                tbCSVLocation.Text = string.Empty;
                gbStep3.Enabled = false;
            }
        }

        private void rbUpload_CheckedChanged(object sender, EventArgs e)
        {
            gbStep2.Enabled = true;
            if (rbUpload.Checked)
            {
                gbStep2.Text = "Step 2: (What To Upload - Choose root folder location.)";
                rbAllAttachments.Enabled = false;
                rbSpecificAttachments.Enabled = false;
                tbCSVLocation.ReadOnly = false;
                tbCSVLocation.Enabled = true;
                butCSVBrowse.Enabled = true;
                rbAllAttachments.Checked = false;
                rbSpecificAttachments.Checked = false;
                tbCSVLocation.Text = string.Empty;
                gbStep3.Enabled = false;
            }
        }
        #endregion

        private void PerformAction()
        {
            CRMService crmService = new CRMService();
            WorkAsync(new WorkAsyncInfo
            {
                Work = (Worker, args) =>
                {
                    if(rbDownload.Checked)
                    {
                        crmService.DownloadRecords(lvMainOutput, Service, tbCSVLocation.Text, pbMain);
                    }
                    if(rbUpload.Checked)
                    {
                        crmService.UploadRecords(lvMainOutput, Service, tbCSVLocation.Text, pbMain);
                    }
                }
            });
        }
    }
}
