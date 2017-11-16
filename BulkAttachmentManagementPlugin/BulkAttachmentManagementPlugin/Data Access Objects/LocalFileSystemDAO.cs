using BulkAttachmentManagementPlugin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkAttachmentManagementPlugin.Data_Access_Objects
{
    public class LocalFileSystemDAO
    {
        public void ExportResultsToCSV(List<OutputModel> oOutputModel, string filelocation)
        {

        }

        public void CreateLocalFile(string filelocation)
        {
            //Used file stream because file.create caused corruption error in excel.
            FileStream st = new FileStream(filelocation, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            st.Close();
        }
    }
}
