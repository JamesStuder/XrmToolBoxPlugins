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
            DialogResult ofdResult = ofdCVSFile.ShowDialog();
            if(ofdResult == DialogResult.OK)
            {
                tbCSVLocation.Text = ofdCVSFile.FileName;
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

        private void PerformAction()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Work = (Worker, args) =>
                {
                    
                },
                PostWorkCallBack = (args) =>
                {

                }
            });
        }
    }
}
