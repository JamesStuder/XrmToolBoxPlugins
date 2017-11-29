using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
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
    }
}
