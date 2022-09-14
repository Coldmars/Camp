using Microsoft.Extensions.Configuration;
using CloudinaryDotNet;

namespace Camp.Common.ExternalServices.Cloudinary
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IConfiguration _configuration;
        private CloudinarySettings _settings;

        public CloudinaryService(IConfiguration configuration)
        {
            _configuration = configuration;
            _settings = new CloudinarySettings();
        }

        public CloudinaryDotNet.Cloudinary GetCloudinary()
        {
            _settings = _configuration
                .GetSection("CloudinarySettings")
                .Get<CloudinarySettings>();

            Account account = new Account(_settings.CloudName,
                                          _settings.ApiKey,
                                          _settings.ApiSecret);

            CloudinaryDotNet.Cloudinary cloudinary = 
                new CloudinaryDotNet.Cloudinary(account);

            cloudinary.Api.Secure = true;

            return cloudinary;
        }
    }
}
