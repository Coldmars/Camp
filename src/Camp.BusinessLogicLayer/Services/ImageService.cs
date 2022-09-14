using Camp.BusinessLogicLayer.Services.Interfaces;
using Camp.Common.DTOs;
using Camp.DataAccess.Repositories;
using Camp.Common.Exceptions;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Camp.DataAccess.Entities;
using Camp.Common.ExternalServices;
using Microsoft.EntityFrameworkCore;

namespace Camp.BusinessLogicLayer.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public ImageService(IImageRepository imageRepository,
                            IUserRepository userRepository,
                            ICloudinaryService cloudinaryService)
        {
            _imageRepository = imageRepository;
            _userRepository = userRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<ImageDto> UploadImage(int userId, 
                                                string base64Image)
        {
            if (base64Image is null)
                throw new ValidateException("Image must not be null", "Image_Upload");

            var image = GetBytes(base64Image);

            await CheckVerifityUser(userId);

            Stream imageStream = new MemoryStream(image);

            var uploadParams =
                new ImageUploadParams()
                {
                    File = new FileDescription(Guid.NewGuid().ToString(), imageStream)
                };

            var cloudinary = _cloudinaryService.GetCloudinary();
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            imageStream.Dispose();

            if (uploadResult.Url is null)
                throw new ValidateException("Error in try to upload image", "Image_Upload");

            var uploadImage = await _imageRepository
                .AddImage(new Image
                {
                    Url = uploadResult.Url.ToString()
                });

            return new ImageDto
            {
                Id = uploadImage.Id,
                Url = uploadImage.Url
            };
        }

        private byte[] GetBytes(string image)
        {
            try
            {
                return Convert.FromBase64String(image);
            }
            catch (FormatException)
            {
                throw new ValidateException("Image must be in Base64 encode", "Image_Upload");
            }
        }

        private async Task CheckVerifityUser(int userId)
        {
            var isUserVerify = await _userRepository
                .GetUserById(userId)
                .Select(user => user.IsVerify)
                .SingleOrDefaultAsync();

            if (!isUserVerify)
                throw new ForbiddenException("User must be verify.", "User_Verify");
        }
    }
}
