using System;
using System.Collections.Generic;
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
using Microsoft.Xrm.Sdk;

namespace BulkAttachmentManagementPlugin.Services
{
    public class CRMService
    {
        public void DownloadRecords(ListView output, IOrganizationService service, string fileLocation, bool allAttachments)
        {
            CRMAttachmentDAO crmDAO = new CRMAttachmentDAO();
            LocalFileSystemDAO localDAO = new LocalFileSystemDAO();

            List<Guid> oAttachmentGuids = (allAttachments) ? crmDAO.GetListOfAttachments(service) : localDAO.ReadFromCSV(fileLocation);

            string fileDirectory = (allAttachments) ? localDAO.CreateLocalDirectory(fileLocation, false, false) : localDAO.CreateLocalDirectory(fileLocation, false, true);

            foreach (Guid attachment in oAttachmentGuids)
            {
                Entity oNoteData = crmDAO.GetNoteAttachmentData(attachment, service);
                string storeAttahmentDirectory = localDAO.CreateLocalDirectory(Path.Combine(fileDirectory, oNoteData.Id.ToString()), true, false);
                localDAO.CreateAttachmentFile(oNoteData["documentbody"].ToString(), storeAttahmentDirectory, oNoteData["filename"].ToString());
            }
        }

        public void UploadRecords(ListView output, IOrganizationService service, string fileLocation)
        {
            CRMAttachmentDAO crmDAO = new CRMAttachmentDAO();
        }
    }
}
