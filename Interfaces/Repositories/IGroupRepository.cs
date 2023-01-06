
using Relief.Entities;

namespace Relief.Interfaces.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<Group> GetGroup(int id);
        Task<IList<Group>> GetAll();
        Task<IList<Group>> GetByContent(string content);
        Task<Group> GetByName(string name);
    }
}
