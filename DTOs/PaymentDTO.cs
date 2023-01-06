namespace Relief.DTOs
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public string ReferenceNo { get; set; }
        public int DonationId { get; set; }
        public int DonorId { get; set; }
        public double AmountPaid { get; set; }
        public double NgoShare { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
