using Relief.Entities;

namespace Relief.DTOs
{
    public class RequestDonationDTO
    {
        public int Id { get; set; }
        public int RequestTypeId { get; set; }
        public int? RequestQuantity { get; set; }
        public double? RequestInMoney { get; set; }
        public string Type { get; set; }
        public string Detail { get; set; }
        public string GroupName { get; set; }
        public int GroupId { get; set; }
        public bool IsCompleted { get; set; }
        public IList<DocumentDTO>? Particulars { get; set; } = new List<DocumentDTO>();
        public decimal Progress { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime DeadLine { get; set; }
        public string NgoEmail { get; set; }
        public int NgoId { get; set; }
        public string? NgoName { get; set; }
        public string? NgoLogo { get; set; }
        public IList<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
        public IList<DonationDTO>? Donations { get; set; } = new List<DonationDTO>();
    }
}
