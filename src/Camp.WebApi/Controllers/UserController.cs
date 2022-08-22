using Camp.BusinessLogicLayer.Services.Interfaces;
using Camp.Common.DTOs;
using Camp.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Camp.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/profiles")]
    public class UserController : IdentityController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("curators")]
        public async Task<CuratorsListDto> GetCurators() =>
            await _userService.GetCurators();

        [AllowAnonymous]
        [HttpGet]
        [Route("squads")]
        public async Task<SquadsListDto> GetNotLockSquadsVerify() =>
            await _userService.GetNotLockSquadsVerify();

        [HttpGet]
        [Route("me")]
        public async Task<dynamic> GetMe() =>
            await _userService.GetMe(UserID);
        
    }
}
