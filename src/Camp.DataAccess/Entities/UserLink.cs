namespace Camp.DataAccess.Entities
{
    public class UserLink
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int LinkId { get; set; }

        public DateTimeOffset CheckDate { get; set; }

        public int CheckedByRoleId { get; set; }

        public User User { get; set; }

        public Link Link { get; set; }
    }
}
