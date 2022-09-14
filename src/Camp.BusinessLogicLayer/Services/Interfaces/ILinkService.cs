using Camp.Common.DTOs;

namespace Camp.BusinessLogicLayer.Services.Interfaces
{
    public interface ILinkService
    {
        Task<LinkCheckDto> CheckLinkAsync(int volunteerId, string url);
    }
}
