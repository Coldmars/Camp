using Camp.Common.DTOs;
using Camp.Common.Models;

namespace Camp.BusinessLogicLayer.Services.Interfaces
{
    public interface ILoginService
    {
        Task<UserWithTokenDto> SignUpAsync(RegisterUserModel registerUser,
                                           int roleId);

        Task<UserWithTokenDto> SignInAsync(string login, string password);
    }
}
