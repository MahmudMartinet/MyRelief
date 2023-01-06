using Relief.DTOs;
using Relief.Entities;
using System.Collections.Generic;

namespace Relief.Interfaces.Repositories
{
    public interface IReportRepository : IRepository<Report>
    {
        Task<Report> GetReport(int id);
        Task<IList<Report>> GetAll();
        Task<HashSet<Report>> GetReportsByContent(string content);
    }
}
