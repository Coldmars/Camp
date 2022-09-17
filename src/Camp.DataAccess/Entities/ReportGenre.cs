namespace Camp.DataAccess.Entities
{
    public class ReportGenre
    {
        public int Id { get; set; }

        public int ReportId { get; set; }

        public int GenreId { get; set; }

        public Report Report { get; set; }

        public Genre Genre { get; set; }
    }
}
