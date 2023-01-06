using Microsoft.EntityFrameworkCore;
using Relief.Contexts;
using Relief.Identity;
using Relief.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Relief.Implementations.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(Expression<Func<User, bool>> expression)
        {
            var ans = await _context.Users.Include(x => x.NGO).Include(x => x.Donor).Include(x => x.Admin).FirstOrDefaultAsync(expression);
            return ans;
        }
    }
}
