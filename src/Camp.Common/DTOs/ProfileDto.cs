namespace Camp.Common.DTOs
{
    public class ProfileDto
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsVerify { get; set; }

        public DateTime CreateDate { get; set; }

        public UserDto Parent { get; set; }
    }
}
