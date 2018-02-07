using BulkAttachmentManagementPlugin.Models;
using System;
using System.Collections.Generic;
using System.IO;

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

        public void ExportResultsToCSV(List<OutputModel> oOutputModel, string filelocation)
        {
            //Insert header
            string rowToInsert = "Date Processed|GUID|File Name|Download Location|Regarding Enitty|Regarding ID|Error Message \r\n";
            File.AppendAllText(filelocation, rowToInsert);
            //Insert each row
            foreach (OutputModel row in oOutputModel)
            {
                rowToInsert = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}{7}", row.DateTimeProcessed, row.GUID, row.FileName, row.DownloadLocation, row.RegardingEntity, row.RegardingID, row.ErrorMessage,  "\r\n");
                File.AppendAllText(filelocation,rowToInsert);
            }
        }

        public void CreateLocalFile(string filelocation)
        {
            //Used file stream because file.create caused corruption error in excel.
            FileStream st = new FileStream(filelocation, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            st.Close();
        }

        public string CreateLocalDirectory(string fileLocation, bool isNewAttachment, bool isCSVLocation, bool isMimeAttachment)
        {
            string filepath = (isCSVLocation) ? Path.GetDirectoryName(fileLocation) : fileLocation;
            
            if(!isNewAttachment)
            {
                if(isMimeAttachment)
                {
                    filepath = Path.Combine(filepath, "E-Mail Attachments");
                }
                else
                {
                    filepath = Path.Combine(filepath, "Note Attachments");
                }
            }

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            return filepath;
        }

        public void CreateAttachmentFile(string documentBody, string filePath, string fileName)
        {
            string fullPath = Path.Combine(filePath, fileName);

            using (FileStream fStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                byte[] fileContent = Convert.FromBase64String(documentBody);
                fStream.Write(fileContent, 0, fileContent.Length);
            }
        }
    }
}
