using Relief.Contexts;
using Relief.Entities;
using Relief.Interfaces.Repositories;

namespace Relief.Implementations.Repositories
{
    public class CreateUserRepository : BaseRepository<CreateUser>, ICreateUserRepository
    {
        public CreateUserRepository(ApplicationContext context)
        {
            _context = context;
        }
    }
}
