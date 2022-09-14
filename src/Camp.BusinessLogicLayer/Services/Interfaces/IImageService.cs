using Camp.Common.DTOs;

namespace Camp.BusinessLogicLayer.Services.Interfaces
{
    public interface IImageService
    {
        Task<ImageDto> UploadImage(int userId, string base64Image);
    }
}
