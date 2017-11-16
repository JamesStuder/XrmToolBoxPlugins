using BulkAttachmentManagementPlugin.Data_Access_Objects;
using BulkAttachmentManagementPlugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulkAttachmentManagementPlugin.Services
{
    public class CSVExportService
    {
        public void ExportToCSV(ListView mainListView, string filelocation)
        {
            if(mainListView.Items.Count > 0 && mainListView != null)
            {
                LocalFileSystemDAO lfsDAO = new LocalFileSystemDAO();
                List<OutputModel> oOutputModel = new List<OutputModel>();
                foreach(ListViewItem item in mainListView.Items)
                {
                    oOutputModel.Add(new OutputModel
                    {
                        DateTimeProcessed = (item.Text != null) ? item.Text : string.Empty,
                        GUID = (item.SubItems[1].Text != null) ? item.SubItems[1].Text : string.Empty,
                        FileName = (item.SubItems[2].Text != null) ? item.SubItems[2].Text : string.Empty,
                        DownloadLocation = (item.SubItems[3].Text != null) ? item.SubItems[3].Text : string.Empty,
                        ErrorMessage = (item.SubItems[4].Text != null) ? item.SubItems[4].Text : string.Empty
                    });
                }
                lfsDAO.ExportResultsToCSV(oOutputModel, filelocation);
            }
        }
    }
}
