namespace Camp.Common.DTOs
{
    public class LinkCheckDto
    {
        public int CheckCount { get; set; }

        public DateTimeOffset? LastCheckAt { get; set; }

        public DateTimeOffset? BlockAt { get; set; }
    }
}
