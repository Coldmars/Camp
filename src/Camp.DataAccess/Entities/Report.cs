namespace Camp.DataAccess.Entities
{
    public class Report
    {
        public Report()
        {
            ReportGenres = new List<ReportGenre>();
        }

        public int Id { get; set; }

        public string Url { get; set; }

        public string Comment { get; set; }

        public DateTimeOffset Time  { get; set; }

        public int ImageId { get; set; }

        public int UserId { get; set; }

        public int TypeId { get; set; }

        public ICollection<ReportGenre> ReportGenres { get; set; }

        public Image Image { get; set; }

        public User User { get; set; }

        public Type Type { get; set; }
    }
}
