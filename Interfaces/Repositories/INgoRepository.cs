using Relief.DTOs;
using Relief.Entities;
using System.Collections.Generic;

namespace Relief.Interfaces.Repositories
{
    public interface INgoRepository : IRepository<NGO>
    {
        Task<IList<NGO>> GetAll();
        Task<NGO> GetNgo(int id);
        Task<NGO> GetNgoByEmail(string email);
        Task<HashSet<NGO>> GetNgoByName(string name);
        Task<HashSet<NGO>> GetByDescriptionContent(string content);
        Task<IList<NGO>> GetByCategoryId(int id);
        Task<IList<NGO>> GetAllWithCategory();
        Task<IList<NGO>> GetUnapprovedNgos();
    }
}
