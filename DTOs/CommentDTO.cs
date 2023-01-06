using Relief.Entities;

namespace Relief.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Detail { get; set; }
        public string? NgoName { get; set; }
        public string DonorName { get; set; }
        public string? DonorImage { get; set; }
        public string? NgoImage { get; set; }
        public int DonorId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
