namespace Camp.DataAccess.Entities
{
    public class User
    {
        public User()
        {
            Children = new List<User>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber  { get; set; }

        public string Location { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public bool IsVerify { get; set; }

        public bool IsLock { get; set; }

        public string RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public User Parent { get; set; }

        public int? ParentId { get; set; }

        public ICollection<User> Children { get; set; }

        public Role Role { get; set; }

        public int RoleId { get; set; }
    }
}
