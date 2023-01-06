using Microsoft.EntityFrameworkCore;
using Relief.Contexts;
using Relief.DTOs;
using Relief.Entities;
using Relief.Interfaces.Repositories;

namespace Relief.Implementations.Repositories
{
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        public ReportRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IList<Report>> GetAll()
        {
            var reports = await _context.Reports.Where(x => x.IsDeleted == false).Include(x => x.NGO).Include(x => x.Donor).Include(x => x.Documents).ToListAsync(); 
            return reports;
        }

        public async Task<Report> GetReport(int id)
        {
            var report = await _context.Reports.Include(x => x.NGO).Include(x => x.Donor).Include(x => x.Documents).FirstOrDefaultAsync(x => x.Id == id);
            return report;
        }

        public async Task<HashSet<Report>> GetReportsByContent(string content)
        {
            var reports = await _context.Reports.Where(x => x.Detail.ToLower().Contains(content.ToLower())).Include(x => x.NGO).Include(x => x.Donor).Include(x => x.Documents).ToListAsync();
            return reports.ToHashSet();
        }
    }
}
