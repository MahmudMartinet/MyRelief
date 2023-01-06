using Relief.Contracts;

namespace Relief.Entities
{
    public class DonationAppointment : AuditableEntity
    {
        public DateTime Time { get; set; }
        public string Venue { get; set; }
        public bool IsAccomplished { get; set; }
        public int DonorId { get; set; }
        public int NgoId { get; set; }
        public int RequestId { get; set; }
        public Donor Donor { get; set; }
        public NGO NGO { get; set; }
        public RequestDonation? RequestDonation { get; set; }
        public bool IsApproved { get; set; }
        public int Quantity { get; set; }
    }
}
