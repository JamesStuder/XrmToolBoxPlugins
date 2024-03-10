using BulkAttachmentManagementPlugin.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace BulkAttachmentManagementPlugin.Data_Access_Objects
{
    public class LocalFileSystemDAO
    {
        public List<Guid> ReadFromCSV(string fileLocation)
        {
            string line = null;
            List<Guid> oGuids = new List<Guid>();
            StreamReader reader = new StreamReader(fileLocation);
            while((line = reader.ReadLine()) != null)
            {
                oGuids.Add(Guid.Parse(line));
            }
            return oGuids;
        }

        public void ExportResultsToCSV(List<OutputModel> oOutputModel, string fileLocation)
        {
            //Insert header
            string rowToInsert = "Date Processed|GUID|File Name|Download Location|Regarding Entity|Regarding ID|Error Message \r\n";
            File.AppendAllText(fileLocation, rowToInsert);
            //Insert each row
            foreach (OutputModel row in oOutputModel)
            {
                rowToInsert = $"{row.DateTimeProcessed}|{row.GUID}|{row.FileName}|{row.DownloadLocation}|{row.RegardingEntity}|{row.RegardingID}|{row.ErrorMessage}{"\r\n"}";
                File.AppendAllText(fileLocation,rowToInsert);
            }
        }

        public string CreateLocalDirectory(string fileLocation, bool isNewAttachment, bool isCSVLocation, bool isMimeAttachment)
        {
            string filePath = (isCSVLocation) ? Path.GetDirectoryName(fileLocation) : fileLocation;
            
            if(!isNewAttachment)
            {
                if (filePath != null)
                {
                    filePath = Path.Combine(filePath, isMimeAttachment ? "E-Mail Attachments" : "Note Attachments");
                }
                    
            }

            if (!Directory.Exists(filePath))
            {
                if (filePath != null)
                {
                    Directory.CreateDirectory(filePath);
                }
            }

            return filePath;
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
