using Relief.Contracts;

namespace Relief.Entities
{
    public class Donation : AuditableEntity
    {
        public bool IsApproved { get; set; }
        public bool IsFund { get; set; }
        public int DonorId { get; set; }
        public int? AppointmentId { get; set; }
        public int? DonationPaymentId { get; set; }
        public int ReqeustDonationId { get; set; }
        public DonationPayment? DonationPayment { get; set; }
        public DonationAppointment? DonationAppointment { get; set; }
        public Donor Donor { get; set; }
        public RequestDonation ReqeustDonation { get; set; }
    }
}
