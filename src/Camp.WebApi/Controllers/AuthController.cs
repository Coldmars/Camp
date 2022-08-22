using Camp.BusinessLogicLayer.Services.Interfaces;
using Camp.Common.DTOs;
using Camp.Common.Models;
using Microsoft.AspNetCore.Mvc;
using static Camp.Common.Models.RegisterRolesModel;

namespace Camp.WebApi.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class AuthController : IdentityController
    {
        private readonly ILoginService _loginService;

        public AuthController(ILoginService service)
        {
            _loginService = service;
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<UserWithTokenDto> SignUpAsync(Role role,
                                                        [FromBody] RegisterUserModel registerUser) =>
            await _loginService.SignUpAsync(registerUser, (int)role);
        

        [HttpPost]
        [Route("sign-in")]
        public async Task<UserWithTokenDto> SignInAsync([FromBody] LoginModel user) =>
            await _loginService.SignInAsync(user.Login, user.Password);

    }
}
