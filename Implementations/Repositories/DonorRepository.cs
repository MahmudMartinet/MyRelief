using Microsoft.EntityFrameworkCore;
using Relief.Contexts;
using Relief.Entities;
using Relief.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Relief.Implementations.Repositories
{
    public class DonorRepository : BaseRepository<Donor>, IDonorRepository
    {
        public DonorRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IList<Donor>> GetAll()
        {
            var donors = await _context.Donors.Include(x => x.User).Where(x => x.IsDeleted == false).ToListAsync();
            return donors;
        }



        public async Task<IList<Donor>> GetByName(string name)
        {
            var donors = await _context.Donors.Include(x => x.User).Where(x => x.FirstName.ToLower().Contains(name.ToLower()) || x.LastName.ToLower().Contains(name.ToLower())).ToListAsync();
            return donors;
        }

        public async Task<Donor> GetDonor(int id)
        {
            var donor = await _context.Donors.Include(x => x.User).Where(x => x.Id == id).FirstOrDefaultAsync();
            return donor;
        }

        public async Task<IList<Donor>> GetAllDeletedDonors()
        {
            var donors = await _context.Donors.Include(x => x.User).Where(x => x.IsDeleted == true).ToListAsync();
            return donors; 
        }

        public async Task<IList<Donor>> GetAllBannedDonors()
        {
            var donors = await _context.Donors.Include(x => x.User).Where(x => x.IsBan == true).ToListAsync();
            return donors;
        }

        public async Task<IList<Donor>> GetActiveDonors()
        {
            var active = await _context.Donors.Include(x => x.User).Where(x => x.IsBan == false).ToListAsync();
            return active;
        }
    } 
}
