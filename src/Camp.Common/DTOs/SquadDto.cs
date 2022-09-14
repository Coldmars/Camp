namespace Camp.Common.DTOs
{
    public class SquadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public long CreateDate { get; set; }

        public string PhoneNumber { get; set; }

        public string Location { get; set; }

        public bool IsVerify { get; set; }

        public bool IsLock { get; set; }
    }
}
