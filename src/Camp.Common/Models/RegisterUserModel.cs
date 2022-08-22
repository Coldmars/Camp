namespace Camp.Common.Models
{
    public class RegisterUserModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public string Location { get; set; }

        public string PhoneNumber { get; set; }
    }
}
