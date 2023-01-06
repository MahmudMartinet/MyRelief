namespace Relief.DTOs
{
    public class ReportDTO
    {
        public int Id { get; set; }
        public string Detail { get; set; }
        public string? NgoName { get; set; }
        public string? DonorName { get; set; }
        public string? DonorImage { get; set; }
        public string? NgoImage { get; set; }
        public IList<DocumentDTO> Documents { get; set; } = new List<DocumentDTO>();
        public DateTime CreatedOn { get; set; }
    }
}
