namespace Relief.DTOs.RequestModel
{
    public class PaymentRequestModel
    {
        public int DonorId { get; set; }
        public int DonationId { get; set; }
        public double Amount { get; set; }
    }
}
