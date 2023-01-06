using Relief.Contracts;

namespace Relief.Entities
{
    public class Comment : AuditableEntity // donors comments on ngos
    {
        public string Detail { get; set; }
        public int DonorId { get; set; }
        public Donor? Donor { get; set; }
        public int? NGOId    { get; set; }
        public NGO? NGO { get; set; }
        public RequestDonation? Request { get; set; }
        public int? RequestId { get; set; }
        public IList<Document> Documents { get; set; } = new List<Document>();
    }
}
