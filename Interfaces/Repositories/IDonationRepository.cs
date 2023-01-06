using Relief.Entities;

namespace Relief.Interfaces.Repositories
{
    public interface IDonationRepository : IRepository<Donation>
    {
        Task<Donation> GetDonation(int id);
        Task<IList<Donation>> GetAll();
        Task<IList<Donation>> GetByDonorId(int id);
        Task<IList<Donation>> GetByRequestId(int id);
    }
}
