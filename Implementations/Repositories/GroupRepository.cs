using Microsoft.EntityFrameworkCore;
using Relief.Contexts;
using Relief.Entities;
using Relief.Interfaces.Repositories;

namespace Relief.Implementations.Repositories
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public GroupRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IList<Group>> GetAll()
        {
            var groups = await _context.Groups.Where(x => x.IsDeleted == false).Include(x => x.Donations).ToListAsync();
            return groups;
        }

        public async Task<IList<Group>> GetByContent(string content)
        {
            var groups = await _context.Groups.Where(x => x.Description.ToLower().Contains(content.ToLower())).Include(x => x.Donations).ToListAsync();
            return groups;
        }

        public async Task<Group> GetGroup(int id)
        {
            var group = await _context.Groups.Where(x => x.Id == id).Include(x => x.Donations).FirstOrDefaultAsync();
            return group;
        }

        public async Task<Group> GetByName(string name)
        {
            var group = await _context.Groups.Where(x => x.Name == name).Include(x => x.Donations).FirstOrDefaultAsync();
            return group;
        }
    }
}
