using Relief.Contracts;

namespace Relief.Entities
{
    public class RequestDonation : AuditableEntity
    {
        public int RequestTypeId { get; set; }
        public int? RequestQuantity { get; set; }
        public  double PricePerOne { get; set; }
        public double? RequestInMoney { get; set; }
        public double AmountGathered { get; set; }
        public int QuantityGathered { get; set; }
        public RequestType RequestType { get; set; }  //emergency, educational,financial...
        public string Details { get; set; }
        public string? AccountNumber { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public IList<Document>? Particulars { get; set; } = new List<Document>();
        public bool IsCompleted { get; set; }
        public decimal Progress { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadLine { get; set; }
        public int NgoId { get; set; }
        public NGO? NGO { get; set; }
        public IList<Donation>? Donations { get; set; } = new List<Donation>();
        public IList<Comment> Comments { get; set; } = new List<Comment>();
    }
}
