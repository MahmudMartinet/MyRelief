using Relief.Contracts;

namespace Relief.Entities
{
    public class Category : AuditableEntity //ngo category
    {
        public string Name { get; set; }
        public IList<NGO> NGOs { get; set; } = new List<NGO>();
    }
}
