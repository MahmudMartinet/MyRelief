using Relief.Identity;
using System.Linq.Expressions;

namespace Relief.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUser(Expression<Func<User, bool>> expression);
    }
}
