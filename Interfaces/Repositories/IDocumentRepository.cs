using Relief.DTOs;
using Relief.Entities;

namespace Relief.Interfaces.Repositories
{
    public interface IDocumentRepository : IRepository<Document>
    {
        Task<IList<DocumentDTO>> GetDocumentsByRequestId(int id);
        Task<IList<DocumentDTO>> GetAllWithRequest();
        Task<IList<DocumentDTO>> GetAllWithNgo();
        Task<IList<DocumentDTO>> GetDocumentsByNgoId(int id);
    }
}
