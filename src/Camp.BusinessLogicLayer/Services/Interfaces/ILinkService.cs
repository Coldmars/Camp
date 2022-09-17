using Camp.Common.DTOs;
using Camp.DataAccess.Enums;

namespace Camp.BusinessLogicLayer.Services.Interfaces
{
    public interface ILinkService
    {
        Task<LinkCheckDto> CheckLinkAsync(int volunteerId, string url);

        Task BlockLink(int userId,
                                    string url,
                                    string comment,
                                    TypesEnum.Type type,
                                    List<GenresEnum.Genres> genres,
                                    int imageId);
    }
}
