using Relief.Contracts;

namespace Relief.Entities
{
    public class BaseUser : AuditableEntity
    {
        public string? Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

    }
}
