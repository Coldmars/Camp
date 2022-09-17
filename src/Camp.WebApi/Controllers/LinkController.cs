using Camp.BusinessLogicLayer.Services.Interfaces;
using Camp.Common.DTOs;
using Camp.Common.Models;
using Camp.DataAccess.Enums;
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

        [HttpPost]
        [Route("block")]
        [Authorize(Roles = "Volunteer")]
        public async Task Testing([FromQuery] TypesEnum.Type type, 
                               [FromQuery] List<GenresEnum.Genres> genres,
                               [FromBody] ReportModel model)
        {
            //return new
            //{
            //    type = type.ToString(),
            //    genre = new
            //    {
            //        id = genres.Select(x => ((int)x)),
            //        name = genres.Select(x => x.ToString())
            //    }
            //};

            await _linkService.BlockLink(UserID, model.Url, model.Comment, type, genres, model.ImageId);
        }
    }
}
