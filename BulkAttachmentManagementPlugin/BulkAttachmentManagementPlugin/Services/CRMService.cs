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
        public void DownloadRecords(ListView output, IOrganizationService service, string fileLocation, ProgressBar pregress)
        {
            CRMAttachmentDAO crmDAO = new CRMAttachmentDAO();
            LocalFileSystemDAO localDAO = new LocalFileSystemDAO();

            List<Guid> oAttachmentGuids = (fileLocation == null || fileLocation == string.Empty) ? crmDAO.GetListOfAttachments(service) : null;
        }

        public void UploadRecords(ListView output, IOrganizationService service, string fileLocation, ProgressBar pregress)
        {
            CRMAttachmentDAO crmDAO = new CRMAttachmentDAO();
        }
    }
}
