using Microsoft.EntityFrameworkCore;
using Relief.Contexts;
using Relief.Entities;
using Relief.Interfaces.Repositories;

namespace Relief.Implementations.Repositories
{
    public class RequestTypeRepository : BaseRepository<RequestType>, IRequestTypeRepository
    {
        public RequestTypeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IList<RequestType>> GetAll()
        {
            var types = await _context.RequestTypes.ToListAsync();
            return types;
        }

        public async Task<RequestType> GetRequestTypeByName(string name)
        {
            var type = await _context.RequestTypes.SingleOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
            return type;
        }

        public async Task<RequestType> GetById(int id)
        {
            var type = await _context.RequestTypes.SingleOrDefaultAsync(x => x.Id == id);
            return type;
        }
    }
}
