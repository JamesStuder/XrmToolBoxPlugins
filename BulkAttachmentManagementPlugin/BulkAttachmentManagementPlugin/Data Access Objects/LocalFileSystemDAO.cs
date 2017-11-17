using BulkAttachmentManagementPlugin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulkAttachmentManagementPlugin.Data_Access_Objects
{
    public class LocalFileSystemDAO
    {
        public List<Guid> ReadFromCSV(string filelocation)
        {
            string line = null;
            List<Guid> oGuids = new List<Guid>();
            StreamReader reader = new StreamReader(filelocation);
            while((line = reader.ReadLine()) != null)
            {
                oGuids.Add(Guid.Parse(line));
            }
            return oGuids;
        }
        public void ExportResultsToCSV(List<OutputModel> oOutputModel, string filelocation, ProgressBar pBar)
        {
            //Insert header
            string rowToInsert = "Date Processed|GUID|File Name|Downloaded To|Error Message";
            File.AppendAllText(filelocation, rowToInsert);
            //Insert each row
            foreach (OutputModel row in oOutputModel)
            {
                rowToInsert = string.Format("{0}|{1}|{2}|{3}|{4}", row.DateTimeProcessed, row.GUID, row.FileName, row.DownloadLocation, row.ErrorMessage);
                File.AppendAllText(filelocation,rowToInsert);
                pBar.Value++;
            }
        }

        public void CreateLocalFile(string filelocation)
        {
            //Used file stream because file.create caused corruption error in excel.
            FileStream st = new FileStream(filelocation, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            st.Close();
        }
    }
}
