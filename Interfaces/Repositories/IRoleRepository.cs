using Relief.Identity;

namespace Relief.Interfaces.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        IList<Role> GetRolesByUserId(int id);
    }
}
