using Relief.Entities;

namespace Relief.Interfaces.Repositories
{
    public interface IDonorRepository : IRepository<Donor>
    {
        Task<Donor> GetDonor(int id);
        Task<IList<Donor>> GetAll();
        Task<IList<Donor>> GetByName(string name);
        Task<IList<Donor>> GetAllDeletedDonors();
        Task<IList<Donor>> GetAllBannedDonors();
        Task<IList<Donor>> GetActiveDonors();
       
    }
}
