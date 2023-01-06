using Relief.Contracts;
using Relief.Entities;

namespace Relief.Identity
{
    public class User : AuditableEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Admin Admin { get; set; }
        public Donor Donor { get; set; }
        public NGO NGO { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
