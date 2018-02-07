namespace BulkAttachmentManagementPlugin.Models
{
    public class OutputModel
    {
        public string DateTimeProcessed { get; set; }
        public string GUID { get; set; }
        public string FileName { get; set; }
        public string DownloadLocation { get; set; }
        public string RegardingEntity { get; set; }
        public string RegardingID { get; set; }
        public string ErrorMessage { get; set; }
    }
}
