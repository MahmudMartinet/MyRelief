using Microsoft.EntityFrameworkCore;
using Relief.Contexts;
using Relief.Entities;
using Relief.Interfaces.Repositories;

namespace Relief.Implementations.Repositories
{
    public class DonationRepository : BaseRepository<Donation>, IDonationRepository
    {
        public DonationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IList<Donation>> GetAll()
        {
            var donations = await _context.Donations.Where(x => x.IsDeleted == false).Include(x => x.Donor).Include(x => x.ReqeustDonation).ToListAsync();
            return donations;
        }

        public async Task<IList<Donation>> GetByDonorId(int id)
        {
            var donations = await _context.Donations.Where(x => x.Donor.Id == id).Include(x => x.ReqeustDonation).ToListAsync();
            return donations;
        }

        public async Task<IList<Donation>> GetByRequestId(int id)
        {
            var donations = await _context.Donations.Where(x => x.ReqeustDonationId == id).Include(x => x.Donor).ToListAsync();
            return donations;
        }

        

        public async Task<Donation> GetDonation(int id)
        {
            var donation = await _context.Donations.Include(x => x.ReqeustDonation).Include(x => x.Donor).Include(x => x.DonationPayment).SingleOrDefaultAsync(x => x.Id == id);
            return donation;
        }


    }
}
