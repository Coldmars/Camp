using Camp.BusinessLogicLayer.Services;
using Camp.BusinessLogicLayer.Services.Interfaces;
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
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILinkService, LinkService>();

            return services;
        }
    }
}
