using BulkAttachmentManagementPlugin.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BulkAttachmentManagementPlugin.Constants;
using Microsoft.Crm.Sdk.Messages;

namespace BulkAttachmentManagementPlugin.Data_Access_Objects
{
    public class CrmAttachmentDao
    {
        /// <summary>
        /// Method to get a list of notes that have an attachment
        /// </summary>
        /// <param name="service">CRM Service</param>
        /// <returns></returns>
        public List<Guid> GetListOfAttachments(IOrganizationService service)
        {
            OrganizationServiceContext xrmContext = new OrganizationServiceContext(service);
            List<Guid> oAttachmentGuids = xrmContext.CreateQuery(Annotation.EntityLogicalName).Where(n => n[Annotation.FileName] != null).Select(n => Guid.Parse(n[Annotation.PrimaryKey].ToString())).ToList();
            return oAttachmentGuids;
        }

        /// <summary>
        /// Method to get note attachment
        /// </summary>
        /// <param name="noteId">Note Guid</param>
        /// <param name="service">CRM Service</param>
        /// <returns></returns>
        public Entity GetNoteAttachmentData(Guid noteId, IOrganizationService service)
        {
            OrganizationServiceContext xrmContext = new OrganizationServiceContext(service);
            return xrmContext.CreateQuery(Annotation.EntityLogicalName).SingleOrDefault(n => (Guid)n[Annotation.PrimaryKey] == noteId);
        }

        /// <summary>
        /// Method to get list of mime attachments where file name is not null
        /// </summary>
        /// <param name="service">CRM Service</param>
        /// <returns></returns>
        public List<Guid> GetListOfActivityMimeAttachmentGuids(IOrganizationService service)
        {
            OrganizationServiceContext xrmContext = new OrganizationServiceContext(service);
            List<Guid> oMimeAttachments = xrmContext.CreateQuery(ActivityMimeAttachment.EntityLogicalName).Where(e => e[ActivityMimeAttachment.FileName] != null).Select(e => Guid.Parse(e[ActivityMimeAttachment.PrimaryKey].ToString())).ToList();
            return oMimeAttachments;
        }

        /// <summary>
        /// Method to mime attachment.
        /// </summary>
        /// <param name="mimeId">Guid of the e-mail attachment</param>
        /// <param name="service">CRM Service</param>
        /// <returns></returns>
        public Entity GetActivityMimeAttachmentData(Guid mimeId, IOrganizationService service)
        {
            OrganizationServiceContext xrmContext = new OrganizationServiceContext(service);
            return xrmContext.CreateQuery(ActivityMimeAttachment.EntityLogicalName).SingleOrDefault(n => (Guid)n[ActivityMimeAttachment.PrimaryKey] == mimeId);
        }

        /// <summary>
        /// Method to get data for reporting on attachments in CRM
        /// </summary>
        /// <param name="service">CRM Service</param>
        /// <returns></returns>
        public List<OutputModel> ReportNoteAttachments(IOrganizationService service)
        {
            OrganizationServiceContext xrmContext = new OrganizationServiceContext(service);

            List<OutputModel> oNotes = (from n in xrmContext.CreateQuery(Annotation.EntityLogicalName)
                                        where n[Annotation.FileName] != null
                                        select new OutputModel
                                        {
                                            DateTimeProcessed = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                            Guid = n[Annotation.PrimaryKey].ToString(),
                                            FileName = n[Annotation.FileName].ToString(),
                                            FileSize = n[Annotation.FileSize].ToString(),
                                            DownloadLocation = PluginConstants.ReportsOnlyDownloadLocation,
                                            RegardingEntity = n[Annotation.ObjectTypeCode].ToString(),
                                            RegardingId = ((EntityReference)n[Annotation.ObjectId]).Id.ToString()
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
            OrganizationServiceContext xrmContext = new OrganizationServiceContext(service);

            List<OutputModel> oMimeAttachments = (from e in xrmContext.CreateQuery(ActivityMimeAttachment.EntityLogicalName)
                                                    where e[ActivityMimeAttachment.FileName] != null
                                                    select new OutputModel
                                                    {
                                                        DateTimeProcessed = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                                        Guid = e[ActivityMimeAttachment.PrimaryKey].ToString(),
                                                        FileName = e[ActivityMimeAttachment.FileName].ToString(),
                                                        FileSize = e[ActivityMimeAttachment.FileSize].ToString(),
                                                        DownloadLocation = PluginConstants.ReportsOnlyDownloadLocation,
                                                        RegardingEntity = e[ActivityMimeAttachment.ObjectTypeCode].ToString(),
                                                        RegardingId = ((EntityReference)e[ActivityMimeAttachment.ObjectId]).Id.ToString()
                                                    }).ToList();

            return oMimeAttachments;
        }

        /// <summary>
        /// Method to get list of file attachments where file name is not null
        /// </summary>
        /// <param name="service">CRM Service</param>
        /// <returns>List of guids</returns>
        public List<Guid> GetListOfFileAttachmentGuids(IOrganizationService service)
        {
            OrganizationServiceContext xrmContext = new OrganizationServiceContext(service);
            List<Guid> oFileAttachments = xrmContext.CreateQuery(FileAttachment.EntityLogicalName).Where(e => e[FileAttachment.FileName] != null).Select(e => Guid.Parse(e[FileAttachment.PrimaryKey].ToString())).ToList();
            return oFileAttachments;
        }
        
        /// <summary>
        /// Method to get data for reporting on file attachments in CRM
        /// </summary>
        /// <param name="service">CRM Service</param>
        /// <returns></returns>
        public List<OutputModel> ReportFileAttachments(IOrganizationService service)
        {
            OrganizationServiceContext xrmContext = new OrganizationServiceContext(service);

            List<OutputModel> oFileAttachments = (from e in xrmContext.CreateQuery(FileAttachment.EntityLogicalName)
                                                    where e[FileAttachment.FileName] != null
                                                    select new OutputModel
                                                    {
                                                        DateTimeProcessed = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                                        Guid = e[FileAttachment.PrimaryKey].ToString(),
                                                        FileName = e[FileAttachment.FileName].ToString(),
                                                        FileSize = e[FileAttachment.FileSize].ToString(),
                                                        DownloadLocation = PluginConstants.ReportsOnlyDownloadLocation,
                                                        RegardingEntity = e[FileAttachment.ObjectTypeCode].ToString(),
                                                        RegardingId = ((EntityReference)e[FileAttachment.ObjectId]).Id.ToString()
                                                    }).ToList();

            return oFileAttachments;
        }

        /// <summary>
        /// Method to File attachment.
        /// </summary>
        /// <param name="fileAttachmentId">Guid of the file attachment</param>
        /// <param name="service">CRM Service</param>
        /// <returns></returns>
        public Entity GetFileAttachmentData(Guid fileAttachmentId, IOrganizationService service)
        {
            OrganizationServiceContext xrmContext = new OrganizationServiceContext(service);
            return xrmContext.CreateQuery(FileAttachment.EntityLogicalName).SingleOrDefault(n => (Guid)n[FileAttachment.PrimaryKey] == fileAttachmentId);
        }
        
        /// <summary>
        /// Retrieve file stored in file attribute for entity
        /// </summary>
        /// <param name="entityLogicalName">Logic name of the entity</param>
        /// <param name="recordId">Guid of the record</param>
        /// <param name="fileAttribute">File attribute</param>
        /// <param name="service">CRM Service</param>
        /// <returns>Byte array containing the file data</returns>
        public byte[] DownloadFileAttribute(string entityLogicalName, Guid recordId, string fileAttribute, IOrganizationService service)
        {
            InitializeFileBlocksDownloadRequest fileDownloadRequest = new InitializeFileBlocksDownloadRequest
            {
                Target = new EntityReference(entityLogicalName, recordId),
                FileAttributeName = fileAttribute
            };

            InitializeFileBlocksDownloadResponse filesBlocksDownloadResponse = (InitializeFileBlocksDownloadResponse)service.Execute(fileDownloadRequest);
            
            DownloadBlockRequest downloadBlockRequest = new DownloadBlockRequest
            {
                FileContinuationToken = filesBlocksDownloadResponse.FileContinuationToken
            };

            DownloadBlockResponse downloadBlockResponse = (DownloadBlockResponse)service.Execute(downloadBlockRequest);
            return downloadBlockResponse.Data;
        }
    }
}