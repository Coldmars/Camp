using Camp.BusinessLogicLayer.Services;
using Camp.BusinessLogicLayer.Services.Interfaces;
using Camp.Common.ExternalServices;
using Camp.Common.ExternalServices.Cloudinary;
using Camp.DataAccess.Repositories;
using Camp.Security;
using Camp.Security.Interfaces;

namespace Camp.WebApi.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILinkRepository, LinkRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILinkService, LinkService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();

            return services;
        }
    }
}
