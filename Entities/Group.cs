using Relief.Contracts;

namespace Relief.Entities
{
    public class Group : AuditableEntity //donationrequest group
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Donation> Donations { get; set; } = new List<Donation>();
    }
}
