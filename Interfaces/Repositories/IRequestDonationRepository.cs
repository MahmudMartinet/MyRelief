using Relief.Entities;
using System.Collections.Generic;

namespace Relief.Interfaces.Repositories
{
    public interface IRequestDonationRepository : IRepository<RequestDonation>
    {
        Task<RequestDonation> GetRequest(int id);
        Task<IList<RequestDonation>> GetAll();
        Task<HashSet<RequestDonation>> GetRequestDonationsByDetail(string detail);
        Task<IList<RequestDonation>> GetCompletedRequests();
        Task<IList<RequestDonation>> GetUncompletedRequests();
    }
}
