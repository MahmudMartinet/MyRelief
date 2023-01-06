using Microsoft.EntityFrameworkCore;
using Relief.Contexts;
using Relief.Entities;
using Relief.Interfaces.Repositories;


namespace Relief.Implementations.Repositories
{
    public class RequestDonationRepository : BaseRepository<RequestDonation>, IRequestDonationRepository
    {
        public RequestDonationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IList<RequestDonation>> GetAll()
        {
            var requests = await _context.RequestDonations.Where(x => x.IsDeleted == false).Include(x => x.NGO).Include(x => x.Group).Include(x => x.Particulars).Include(x => x.RequestType).Include(x => x.Donations).ToListAsync();
            return requests;
        }

        public async Task<RequestDonation> GetRequest(int id)
        {
            var request = await _context.RequestDonations.Include(x => x.NGO).Include(x => x.Group).Include(x => x.Particulars).Include(x => x.RequestType).Include(x => x.Donations).SingleOrDefaultAsync(x => x.Id == id);
            return request;
        }

        public async Task<HashSet<RequestDonation>> GetRequestDonationsByDetail(string detail)
        {
            var requests = await _context.RequestDonations.Where(x => x.Details.ToLower().Contains(detail.ToLower()) && x.IsDeleted == false).Include(x => x.NGO).Include(x => x.Group).Include(x => x.Particulars).Include(x => x.RequestType).Include(x => x.Donations).ToListAsync();
            return requests.ToHashSet();
        }

        public async Task<IList<RequestDonation>> GetUncompletedRequests()
        {
            var requests = await _context.RequestDonations.Where(x => x.IsCompleted == false && x.IsDeleted == false).Include(x => x.NGO).ThenInclude(x => x.ReqeustDonation).Include(x => x.Group).Include(x => x.Particulars).Include(x => x.RequestType).Include(x => x.Donations).ToListAsync();
            return requests;
        }

        public async Task<IList<RequestDonation>> GetCompletedRequests()
        {
            var requests = await _context.RequestDonations.Where(x => x.IsCompleted == true && x.IsDeleted == false).Include(x => x.NGO).Include(x => x.Group).Include(x => x.Particulars).Include(x => x.RequestType).Include(x => x.Donations).ToListAsync();
            return requests;
        }
    }
}
