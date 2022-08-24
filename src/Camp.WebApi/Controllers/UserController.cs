using Camp.BusinessLogicLayer.Services.Interfaces;
using Camp.Common.DTOs;
using Camp.Common.Models;
using Camp.DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Camp.DataAccess.Enums.Roles;

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

        [HttpGet]
        [Route("me/squads")]
        [Authorize(Roles = "Curator")]
        public async Task<SquadProfilesListDto> GetCuratorSquads() =>
            await _userService.GetSquadsByUserId(UserID);

        [HttpGet]
        [Route("me/volunteers")]
        [Authorize(Roles = "Squad")]
        public async Task<VolunteerProfilesListDto> GetSquadVolunteers() =>
            await _userService.GetVolunteersByUserId(UserID);

        [HttpPost]
        [Route("{userID}/verify")]
        [Authorize(Roles = "Curator,Squad")]
        public async Task VerifyUser([FromRoute] int userID, 
                                     [FromBody] VerifyModel model)
        {
            await _userService.Verify(UserID, userID, model.IsVerify);
        }

    }
}
