using Microsoft.EntityFrameworkCore;
using Relief.Contexts;
using Relief.DTOs;
using Relief.Entities;
using Relief.Interfaces.Repositories;

namespace Relief.Implementations.Repositories
{
    public class NgoRepository : BaseRepository<NGO>, INgoRepository
    {
        public NgoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IList<NGO>> GetAllWithCategory()
        {
            var ngos = await _context.NGOs.Include(x => x.User).Include(x => x.Category).Include(x => x.Particulars).Where(x => x.IsDeleted == false && x.IsApproved == true).ToListAsync();
            return ngos;
        }
        public async Task<IList<NGO>> GetAll()
        {
            var ngos = await _context.NGOs.Include(x => x.User).Include(x => x.Particulars).Include(x => x.Category).Where(x => x.IsDeleted == false && x.IsApproved == true).ToListAsync();
            return ngos;
        }

        public async Task<HashSet<NGO>> GetByDescriptionContent(string content)
        {
            var ngos = await _context.NGOs.Include(x => x.User).Where(x => x.Description.ToLower().Contains(content.ToLower()) && x.IsDeleted == false && x.IsApproved == true).Include(x => x.Category).Include(x => x.Particulars).ToListAsync();
            return ngos.ToHashSet();
        }

        public async Task<NGO> GetNgo(int id)
        {
            var ngo = await _context.NGOs.Include(x => x.User).Include(x => x.Category).Include(x => x.Particulars).SingleOrDefaultAsync(x => x.Id == id);
            return ngo;
        }

        public async Task<NGO> GetNgoByEmail(string email)
        {
            var ngo = await _context.NGOs.Include(x => x.User).Include(x => x.Category).Include(x => x.Particulars).SingleOrDefaultAsync(x => x.Email == email);
            return ngo;
        }

        public async Task<HashSet<NGO>> GetNgoByName(string name)
        {
            var ngo = await _context.NGOs.Include(x => x.User).Where(x => x.Name.ToLower().Contains(name.ToLower()) && x.IsDeleted == false && x.IsBan == false).Include(x => x.Category).Include(x => x.Particulars).ToListAsync();
            
            return ngo.ToHashSet();
        }

        public async Task<IList<NGO>> GetByCategoryId(int id)
        {
            var ngos = await _context.NGOs.Include(x => x.User).Where(x => x.CategoryId == id).Include(x => x.Category).Include(x => x.Particulars).ToListAsync();
            return ngos;
        }

        public async Task<IList<NGO>> GetUnapprovedNgos()
        {
            var ngos = await _context.NGOs.Include(x => x.User).Include(x => x.Category).Where(x => x.IsApproved == false).Include(x => x.Category).Include(x => x.Particulars).ToListAsync();
            return ngos;
        }
    }
}
