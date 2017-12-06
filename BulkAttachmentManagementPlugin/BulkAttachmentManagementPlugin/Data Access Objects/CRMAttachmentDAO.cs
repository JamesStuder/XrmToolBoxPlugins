using BulkAttachmentManagementPlugin.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkAttachmentManagementPlugin.Data_Access_Objects
{
    public class CRMAttachmentDAO
    {
        public List<Guid> GetListOfAttachments(IOrganizationService service)
        {
            List<Guid> oAttachmentGuids = new List<Guid>();
            OrganizationServiceContext xrmContext = new OrganizationServiceContext(service);
            oAttachmentGuids = xrmContext.CreateQuery("annotation").Where(n => n["filename"] != null).Select(n => Guid.Parse(n["annotationid"].ToString())).ToList();
            return oAttachmentGuids;
        }

        public Entity GetNoteAttachmentData(Guid noteID, IOrganizationService service)
        {
            ColumnSet cols = new ColumnSet(true);
            Entity annotation = new Entity("annotation");
            annotation = service.Retrieve(annotation.LogicalName, noteID, cols);
            return annotation;
        }
    }
}
