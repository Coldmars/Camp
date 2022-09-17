namespace Camp.DataAccess.Entities
{
    public class Type
    {
        public Type()
        {
            Reports = new List<Report>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Report> Reports { get; set; }
    }
}
