using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<Guid> GetListOfActivityMimeAttachmentGuids(IOrganizationService service)
        {
            List<Guid> oMimeAttachments = new List<Guid>();
            OrganizationServiceContext xrmContext = new OrganizationServiceContext(service);
            oMimeAttachments = xrmContext.CreateQuery("activitymimeattachment").Where(e => e["filename"] != null).Select(e => Guid.Parse(e["activitymimeattachmentid"].ToString())).ToList();
            return oMimeAttachments;
        }

        public Entity GetActivityMimeAttachmentData(Guid mimeID, IOrganizationService service)
        {
            ColumnSet cols = new ColumnSet(true);
            Entity activityMimeAttachment = new Entity("activitymimeattachment");
            activityMimeAttachment = service.Retrieve(activityMimeAttachment.LogicalName, mimeID, cols);
            return activityMimeAttachment;
        }
    }
}
