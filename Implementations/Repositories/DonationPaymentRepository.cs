using Relief.Contexts;
using Relief.Entities;
using Relief.Interfaces.Repositories;

namespace Relief.Implementations.Repositories
{
    public class DonationPaymentRepository : BaseRepository<DonationPayment>, IDonationPaymentRepository
    {
        public DonationPaymentRepository(ApplicationContext context)
        {
            _context = context;
        }


    }
}
