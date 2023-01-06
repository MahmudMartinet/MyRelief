using Relief.Identity;

namespace Relief.Entities
{
    public class Admin : BaseUser
    {
        public bool IsSuperAdmin { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }

}
