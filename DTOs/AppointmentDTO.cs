namespace Relief.DTOs
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Venue { get; set; }
        public bool IsAccomplished { get; set; }
        public int DonorId { get; set; }
        public string DonorName { get; set; }
        public int NgoId { get; set; }
        public string NgoName { get; set; }
        public int RequestId { get; set; }
        public string RequestDetail { get; set; }
        public bool IsApproved { get; set; }
       
    }
}
