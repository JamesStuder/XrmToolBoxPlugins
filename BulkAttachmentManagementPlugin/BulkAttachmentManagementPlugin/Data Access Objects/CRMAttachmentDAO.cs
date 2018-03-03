using BulkAttachmentManagementPlugin.Models;
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
        /// <summary>
        /// MEthod to get a list of notes that have an attachment
        /// </summary>
        /// <param name="service">CRM Service</param>
        /// <returns></returns>
        public List<Guid> GetListOfAttachments(IOrganizationService service)
        {
            List<Guid> oAttachmentGuids = new List<Guid>();
            OrganizationServiceContext xrmContext = new OrganizationServiceContext(service);
            oAttachmentGuids = xrmContext.CreateQuery("annotation").Where(n => n["filename"] != null).Select(n => Guid.Parse(n["annotationid"].ToString())).ToList();
            return oAttachmentGuids;
        }

        /// <summary>
        /// Method to get note attachment
        /// </summary>
        /// <param name="noteID">Note Guid</param>
        /// <param name="service">CRM Service</param>
        /// <returns></returns>
        public Entity GetNoteAttachmentData(Guid noteID, IOrganizationService service)
        {
            ColumnSet cols = new ColumnSet(true);
            Entity annotation = new Entity("annotation");
            annotation = service.Retrieve(annotation.LogicalName, noteID, cols);
            return annotation;
        }

        /// <summary>
        /// Method to get list of mime attachments where file name is not null
        /// </summary>
        /// <param name="service">CRM Service</param>
        /// <returns></returns>
        public List<Guid> GetListOfActivityMimeAttachmentGuids(IOrganizationService service)
        {
            List<Guid> oMimeAttachments = new List<Guid>();
            OrganizationServiceContext xrmContext = new OrganizationServiceContext(service);
            oMimeAttachments = xrmContext.CreateQuery("activitymimeattachment").Where(e => e["filename"] != null).Select(e => Guid.Parse(e["activitymimeattachmentid"].ToString())).ToList();
            return oMimeAttachments;
        }

        /// <summary>
        /// Method to mime attachment.
        /// </summary>
        /// <param name="mimeID">Guid of the e-mail attachment</param>
        /// <param name="service">CRM Service</param>
        /// <returns></returns>
        public Entity GetActivityMimeAttachmentData(Guid mimeID, IOrganizationService service)
        {
            ColumnSet cols = new ColumnSet(true);
            Entity activityMimeAttachment = new Entity("activitymimeattachment");
            activityMimeAttachment = service.Retrieve(activityMimeAttachment.LogicalName, mimeID, cols);
            return activityMimeAttachment;
        }

        /// <summary>
        /// Method to get data for reporting on attachments in CRM
        /// </summary>
        /// <param name="service">CRM Service</param>
        /// <returns></returns>
        public List<OutputModel> ReportNoteAttachments(IOrganizationService service)
        {
            List<OutputModel> oNotes = new List<OutputModel>();
            OrganizationServiceContext xrmContext = new OrganizationServiceContext(service);

            oNotes = (from n in xrmContext.CreateQuery("annotation")
                      where n["filename"] != null
                      select new OutputModel
                      {
                          DateTimeProcessed = DateTime.Now.ToString(),
                          GUID = n["annotationid"].ToString(),
                          FileName = n["filename"].ToString(),
                          FileSize = n["filesize"].ToString(),
                          DownloadLocation = "Report Only",
                          RegardingEntity = n["objecttypecode"].ToString(),
                          RegardingID = ((EntityReference)n["objectid"]).Id.ToString()
                      }).ToList();

            return oNotes;
        }

        /// <summary>
        /// Method to get data for reporting on mime attachments in CRM
        /// </summary>
        /// <param name="service">CRM Service</param>
        /// <returns></returns>
        public List<OutputModel> ReportMimeAttachments(IOrganizationService service)
        {
            List<OutputModel> oMimeAttachments = new List<OutputModel>();
            OrganizationServiceContext xrmContext = new OrganizationServiceContext(service);

            oMimeAttachments = (from e in xrmContext.CreateQuery("activitymimeattachment")
                                where e["filename"] != null
                                select new OutputModel
                                {
                                    DateTimeProcessed = DateTime.Now.ToString(),
                                    GUID = e["activitymimeattachmentid"].ToString(),
                                    FileName = e["filename"].ToString(),
                                    FileSize = e["filesize"].ToString(),
                                    DownloadLocation = "Report Only",
                                    RegardingEntity = e["objecttypecode"].ToString(),
                                    RegardingID = ((EntityReference)e["objectid"]).Id.ToString()
                                }).ToList();

            return oMimeAttachments;
        }
    }
}
