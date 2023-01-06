using Microsoft.EntityFrameworkCore;
using Relief.Contexts;
using Relief.Entities;
using Relief.Interfaces.Repositories;

namespace Relief.Implementations.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IList<Comment>> GetAll()
        {
            var comments = await _context.Comments.Where(x => x.IsDeleted == false).Include(x => x.NGO).Include(x => x.Donor).Include(x => x.Documents).Include(x => x.Request).OrderByDescending(x => x.CreatedOn).ToListAsync();
            
            return comments;
        }

        public async Task<Comment> GetComment(int id)
        {
            var comment = await _context.Comments.Include(x => x.NGO).Include(x => x.Donor).Include(x => x.Documents).Include(x => x.Request).SingleOrDefaultAsync(x => x.Id == id);
            return comment;
        }

        public async Task<IList<Comment>> GetByRequestId(int id)
        {
            var comments = await _context.Comments.Include(x => x.Donor).Include(x => x.Documents).Include(x => x.Request).Where(x => x.RequestId == id).ToListAsync();
            return comments;
        }

        public async Task<IList<Comment>> GetCommentByNgoId(int id)
        {
            var comment = await _context.Comments.Where(x => x.NGOId == id && x.IsDeleted == false).Include(x => x.NGO).Include(x => x.Donor).Include(x => x.Request).Include(x => x.Documents).OrderByDescending(x => x.CreatedOn).ToListAsync();
            return comment;
        }

        public async Task<IList<Comment>> GetCommentsByContent(string content)
        {
            var comment = await _context.Comments.Where(x => x.IsDeleted == false && x.Detail.ToLower().Contains(content.ToLower())).Include(x => x.NGO).Include(x => x.Donor).Include(x => x.Request).Include(x => x.Documents).ToListAsync();
            return comment;
        }
    }
}
