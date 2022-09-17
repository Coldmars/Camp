using Camp.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camp.BusinessLogicLayer.Services
{
    public class ReportService
    {
        private readonly IReportRepository _reports;
        private readonly ILinkRepository _links;
        private readonly IImageRepository _images;

        public ReportService(
                             IReportRepository reports,
                             ILinkRepository links,
                             IImageRepository images)
        {
            _reports = reports;
            _links = links;
            _images = images;
        }
    }
}
