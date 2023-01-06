using Relief.Entities;

namespace Relief.Interfaces.Repositories
{
    public interface IRequestTypeRepository : IRepository<RequestType>
    {
        Task<RequestType> GetRequestTypeByName(string name);
        Task<IList<RequestType>> GetAll();
        Task<RequestType> GetById(int id);

    }
}
