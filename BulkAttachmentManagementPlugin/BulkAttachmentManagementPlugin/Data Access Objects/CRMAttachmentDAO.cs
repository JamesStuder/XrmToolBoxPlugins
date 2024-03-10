using BulkAttachmentManagementPlugin.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BulkAttachmentManagementPlugin.Constants;

namespace BulkAttachmentManagementPlugin.Data_Access_Objects
{
    public class CRMAttachmentDAO
    {
        /// <summary>
        /// Method to get a list of notes that have an attachment
        /// </summary>
        /// <param name="service">CRM Service</param>
        /// <returns></returns>
        public List<Guid> GetListOfAttachments(IOrganizationService service)
        {
            List<Guid> oAttachmentGuids = new List<Guid>();
            OrganizationServiceContext xrmContext = new OrganizationServiceContext(service);
            oAttachmentGuids = xrmContext.CreateQuery(Annotation.EntityLogicalName).Where(n => n[Annotation.FileName] != null).Select(n => Guid.Parse(n[Annotation.PrimaryKey].ToString())).ToList();
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
            Entity annotation = new Entity(Annotation.EntityLogicalName);
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
            oMimeAttachments = xrmContext.CreateQuery(ActivityMimeAttachment.EntityLogicalName).Where(e => e[ActivityMimeAttachment.FileName] != null).Select(e => Guid.Parse(e[ActivityMimeAttachment.PrimaryKey].ToString())).ToList();
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
            Entity activityMimeAttachment = new Entity(ActivityMimeAttachment.EntityLogicalName);
            activityMimeAttachment = service.Retrieve(ActivityMimeAttachment.EntityLogicalName, mimeID, cols);
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

            oNotes = (from n in xrmContext.CreateQuery(Annotation.EntityLogicalName)
                      where n[Annotation.FileName] != null
                      select new OutputModel
                      {
                          DateTimeProcessed = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                          GUID = n[Annotation.PrimaryKey].ToString(),
                          FileName = n[Annotation.FileName].ToString(),
                          FileSize = n[Annotation.FileSize].ToString(),
                          DownloadLocation = PluginConstants.ReportsOnlyDownloadLocation,
                          RegardingEntity = n[Annotation.ObjectTypeCode].ToString(),
                          RegardingID = ((EntityReference)n[Annotation.ObjectId]).Id.ToString()
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

            oMimeAttachments = (from e in xrmContext.CreateQuery(ActivityMimeAttachment.EntityLogicalName)
                                where e[ActivityMimeAttachment.FileName] != null
                                select new OutputModel
                                {
                                    DateTimeProcessed = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                    GUID = e[ActivityMimeAttachment.PrimaryKey].ToString(),
                                    FileName = e[ActivityMimeAttachment.FileName].ToString(),
                                    FileSize = e[ActivityMimeAttachment.FileSize].ToString(),
                                    DownloadLocation = PluginConstants.ReportsOnlyDownloadLocation,
                                    RegardingEntity = e[ActivityMimeAttachment.ObjectTypeCode].ToString(),
                                    RegardingID = ((EntityReference)e[ActivityMimeAttachment.ObjectId]).Id.ToString()
                                }).ToList();

            return oMimeAttachments;
        }
    }
}
