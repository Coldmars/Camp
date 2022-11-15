using Camp.DataAccess.Enums;

namespace Camp.Common.Models
{
    public class ReportModel
    {
        public string Url { get; set; }

        public string Comment { get; set; }

        public int ImageId { get; set; }

        public TypesEnum.Type Type { get; set; }
    }
}
