using Relief.Entities;
using System.ComponentModel.DataAnnotations;

namespace Relief.DTOs.RequestModel
{
    public class RequestDonationRequestModel
    {
       
    }
    public class CreateRequestDonationRequestModel
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Type { get; set; }

        [Required]
        [StringLength(maximumLength: 3000, MinimumLength = 3)]
        public string Details { get; set; }
        public int? RequestQuantity { get; set; }
        public double? RequestInMoney { get; set; }
        public string? Group { get; set; }
        public DateTime DeadLine { get; set; }
        public DateTime StartDate { get; set; }
        public IList<string>? Documents { get; set; } = new List<string>();
    }

    public class UpdateRequestDonationRequestModel
    {
        public string Type { get; set; }
        public int? RequestQuantity { get; set; }
        public double? RequestInMoney { get; set; }
        public string Details { get; set; }
        public string Group { get; set; }
        public string DeadLine { get; set; }
        public IList<string>? Documents { get; set; } = new List<string>();
        public bool IsCompleted { get; set; }
    }
}
