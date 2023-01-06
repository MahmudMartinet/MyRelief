using Microsoft.EntityFrameworkCore;
using Relief.Contexts;
using Relief.DTOs;
using Relief.Entities;
using Relief.Interfaces.Repositories;

namespace Relief.Implementations.Repositories
{
    public class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IList<DocumentDTO>> GetAllWithRequest()
        {
            var doc = await _context.Documents.Include(x => x.NGO).Include(x => x.RequestDonation).Include(x => x.Report).Include(x => x.Comment).Select(x => new DocumentDTO
            {
                Name = x.Path,
                RequestId = x.RequestId
            }).ToListAsync();
            return doc;
        }

        public  async Task<IList<DocumentDTO>> GetDocumentsByRequestId(int id)
        {
            var doc = await _context.Documents.Include(x => x.NGO).Include(x => x.RequestDonation).Include(x => x.Report).Include(x => x.Comment).Where(x => x.RequestId == id).Select(d => new DocumentDTO
            {
                Name = d.Path
            }).ToListAsync();
            return doc;
        }

        public async Task<IList<DocumentDTO>> GetAllWithNgo()
        {
            var doc = await _context.Documents.Include(x => x.NGO).Include(x => x.RequestDonation).Include(x => x.Report).Include(x => x.Comment).Select(x => new DocumentDTO
            {
                Name = x.Path,
                NgoId = x.NgoId,
            }).ToListAsync();
            return doc;
        }

        public async Task<IList<DocumentDTO>> GetDocumentsByNgoId(int id)
        {
            var doc = await _context.Documents.Include(x => x.NGO).Include(x => x.RequestDonation).Include(x => x.Report).Include(x => x.Comment).Where(x => x.NgoId == id).Select(d => new DocumentDTO
            {
                Name = d.Path
            }).ToListAsync();
            return doc;
        }
    }
}
