using System.ComponentModel.DataAnnotations;

namespace Relief.DTOs.RequestModel
{
    public class NgoRequestModel
    {
       
    }
    public class CreateNgoRequestModel
    {
        public string? Image { get; set; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 3000, MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string CategoryName { get; set; }
        
    }

    public class AddressRequestModel
    {
        public string Address { get; set; }
        public string LGA { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }

    public class AccountDetailsRequestModel
    {
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
    }

    public class UploadRequestModel
    {
        public IList<string> Documents { get; set; } = new List<string>();
    }

    public class UpdateNgoRequestModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Password { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? LGA { get; set; }
        public string? CategoryName { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountName { get; set; }
        public string? BankName { get; set; }
        public IList<string> Documents { get; set; } = new List<string>();
    }
}
