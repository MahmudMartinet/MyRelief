using Relief.Contracts;

namespace Relief.Entities
{
    public class Document : AuditableEntity
    {
        public string? Path { get; set; }
        public int? RequestId { get; set; }
        public int? NgoId { get; set; }
        public RequestDonation? RequestDonation { get; set; }
        public NGO? NGO { get; set; }
        public Comment? Comment { get; set; }
        public int? CommentId { get; set; }
        public Report? Report { get; set; }
        public int? ReportId { get; set; }
    }
}
