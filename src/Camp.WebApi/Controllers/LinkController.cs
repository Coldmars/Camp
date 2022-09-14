using Camp.BusinessLogicLayer.Services.Interfaces;
using Camp.Common.DTOs;
using Camp.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Camp.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/links")]
    public class LinkController : IdentityController
    {
        private readonly ILinkService _linkService;

        public LinkController(ILinkService linkService)
        {
            _linkService = linkService;
        }

        [HttpPost]
        [Route("check")]
        [Authorize(Roles = "Volunteer")]
        public async Task<LinkCheckDto> CheckLink([FromBody] LinkModel model) =>
            await _linkService.CheckLinkAsync(UserID, model.Url);
    }
}
