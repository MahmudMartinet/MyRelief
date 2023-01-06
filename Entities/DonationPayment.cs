namespace Relief.Entities
{
    public class DonationPayment
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int RequestId { get; set; }
        public RequestDonation RequestDonation { get; set; }
        public int DonorId  { get; set; }
        public Donor Donor { get; set; }
        public DateTime DonationDate { get; set; }
        public string Reference { get; set; }
        public bool IsVerified { get; set; }
    }
}
