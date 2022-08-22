namespace Camp.DataAccess.Entities
{
    public class Role
    {
        public Role()
        {
            Users = new List<User>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
