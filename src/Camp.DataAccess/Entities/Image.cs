namespace Camp.DataAccess.Entities
{
    public class Image
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public Report Report { get; set; }
    }
}
