namespace BulkAttachmentManagementPlugin.Models
{
    public class OutputModel
    {
        public string DateTimeProcessed { get; set; }
        public string Guid { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string DownloadLocation { get; set; }
        public string RegardingEntity { get; set; }
        public string RegardingId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
