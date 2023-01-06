using Relief.Identity;

namespace Relief.Entities
{
    public class Donor : BaseUser
    {
        public bool IsBan { get; set; }
        public int BannedBy { get; set; }
        public DateTime BannedOn { get; set; }
        public IList<Donation> Donations { get; set; }
        public IList<Report> Reports { get; set; }
        public IList<Comment> Comments { get; set; }
        public IList<DonationAppointment> DonationAppointments { get; set; } = new List<DonationAppointment>();
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
