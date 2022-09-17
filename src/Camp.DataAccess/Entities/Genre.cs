namespace Camp.DataAccess.Entities
{
    public class Genre
    {
        public Genre()
        {
            ReportGenres = new List<ReportGenre>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ReportGenre> ReportGenres { get; set; }
    }
}
