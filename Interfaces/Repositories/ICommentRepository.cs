using Relief.Entities;
using Relief.Implementations.Repositories;

namespace Relief.Interfaces.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<Comment> GetComment(int id);
        Task<IList<Comment>> GetAll();
        Task<IList<Comment>> GetCommentsByContent(string content);
        Task<IList<Comment>> GetCommentByNgoId(int id);
        Task<IList<Comment>> GetByRequestId(int id);
    }
}
