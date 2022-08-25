namespace Camp.DataAccess.Entities
{
    public class Link
    {
        public Link()
        {
            UserLinks = new List<UserLink>();
        }

        public int Id { get; set; }

        public string Url { get; set; }

        public bool IsLock { get; set; }

        public DateTimeOffset? LockDate { get; set; }

        public ICollection<UserLink> UserLinks { get; set; }
    }
}
