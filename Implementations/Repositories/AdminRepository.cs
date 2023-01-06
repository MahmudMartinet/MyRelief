using Microsoft.EntityFrameworkCore;
using Relief.Contexts;
using Relief.Entities;
using Relief.Interfaces.Repositories;

namespace Relief.Implementations.Repositories
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    { 
        public AdminRepository(ApplicationContext context)
        {
            _context = context;
        }



        public async Task<Admin> GetAdminInfo(int id)
        {
            var admin = await _context.Admins.Include(x => x.User).SingleOrDefaultAsync(x => x.Id == id);
            return admin;
        }

        public async Task<IList<Admin>> GetAllAdmins()
        {
            var admins = await _context.Admins.Include(x => x.User).Where(x => x.IsDeleted == false).ToListAsync();
            return admins;
        }

        public async Task<IList<Admin>> GetAllDeletedAdmin()
        {
            var admins = await _context.Admins.Include(x => x.User).Where(x => x.IsDeleted == true).ToListAsync();
            return admins;
        }

        public async Task<IList<Admin>> GetAllSuperAdmin()
        {
            var admins = await _context.Admins.Include(x => x.User).Where(x => x.IsSuperAdmin == true && x.IsDeleted == false).ToListAsync();
            return admins;
        }

        public async Task<IList<Admin>> GetNonSuperAdmins()
        {
            var admins = await _context.Admins.Include(x => x.User).Where(x => x.IsSuperAdmin == false && x.IsDeleted == false).ToListAsync();
            return admins;
        }
    }
}
