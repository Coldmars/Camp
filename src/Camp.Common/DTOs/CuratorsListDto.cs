namespace Camp.Common.DTOs
{
    public class CuratorsListDto
    {
        public CuratorsListDto()
        {
            Curators = new List<UserDto>();
        }

        public ICollection<UserDto> Curators { get; set; }
    }
}
