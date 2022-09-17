using Camp.DataAccess.Entities;

namespace Camp.DataAccess.Repositories
{
    public interface IReportRepository
    {
        Task<Report> AddReport(Report report);

        Task<ReportGenre> AddReportGenre(ReportGenre rg);
    }
}
