using Relief.Entities;

namespace Relief.DTOs
{
    public class DonationDTO
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; }
        public string DonorName { get; set; } 
        public int DonorId { get; set; }
        public int RequestDonationId { get; set; }
        public string RequestDetail { get; set; }
    }
}
