using Relief.Entities;

namespace Relief.DTOs
{
    public class NgoDTO
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? LGA { get; set; }
        public IList<DocumentDTO> Particulars { get; set; } = new List<DocumentDTO>();
        public string? AccountNumber { get; set; }
        public string? AccountName { get; set; }
        public string? BankName { get; set; }
        public string? CategoryName { get; set; }
        public int CategoryId { get; set;} 
        public bool IsDeleted { get; set; }
        public bool IsBanned { get; set; }
    }
}
