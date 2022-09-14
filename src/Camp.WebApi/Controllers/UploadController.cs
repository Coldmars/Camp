using Camp.BusinessLogicLayer.Services.Interfaces;
using Camp.Common.DTOs;
using Camp.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Camp.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/upload")]
    public class UploadController : IdentityController
    {
        private readonly IImageService _imageService;

        public UploadController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        [Route("image")]
        public async Task<ImageDto> UploadImage(ImageModel imageModel)
        {
            return await _imageService.UploadImage(UserID, imageModel.Image);
        }
    }
}
