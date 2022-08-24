namespace Camp.Common.DTOs
{
    public class VolunteerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public long CreateDate { get; set; }

        public bool IsVerify { get; set; }

        public bool IsLock { get; set; }
    }
}
