using Relief.Entities;

namespace Relief.DTOs.RequestModel
{
    public class DonationRequestModel
    {
        
    }
    public class CreateDonationRequestModel
    {
        public bool IsFund { get; set; }
        public double Amount { get; set; }
        public int Quantity { get; set; }
        public string? Venue { get; set; }
        public DateTime Time { get; set; }
        
       
    }

    public class UpdateDonationRequestModel
    {
        public bool IsFund { set; get; }
        public double Amount { get; set; }
        public int Quantity { get; set; }
        public bool IsApproved { get; set; }
        
    }
}
