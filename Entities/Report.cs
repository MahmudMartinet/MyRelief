using Relief.Contracts;

namespace Relief.Entities
{
    public class Report : AuditableEntity //report against donor
    {
        public string Detail { get; set; }
        public int NGOId { get; set; }
        public NGO NGO { get; set; }
        public int DonorId { get; set; }
        public Donor Donor { get; set; }
        public IList<Document> Documents { get; set; } = new List<Document>();
    }
}
