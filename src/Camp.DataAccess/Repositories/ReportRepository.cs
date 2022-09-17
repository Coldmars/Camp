using Camp.DataAccess.Data;
using Camp.DataAccess.Entities;

namespace Camp.DataAccess.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly DataContext _context;

        public ReportRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Report> AddReport(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return report;
        }

        public async Task<ReportGenre> AddReportGenre(ReportGenre rg)
        {
            _context.ReportGenres.Add(rg);
            await _context.SaveChangesAsync();

            return rg;
        }
    }
}
