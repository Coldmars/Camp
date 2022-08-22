namespace Camp.Common.DTOs
{
    public class UserWithTokenDto
    {
        public string AccessToken { get; set; }

        public DateTime AccessExpiresIn { get; set; }
        
        public ProfileDto Profile { get; set; }
    }
}
