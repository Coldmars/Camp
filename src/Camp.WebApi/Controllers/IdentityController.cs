using Camp.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Camp.WebApi.Controllers
{
    public class IdentityController : ControllerBase
    {
        protected int UserID => GetUserId();

        private int GetUserId()
        {
            var claim = HttpContext.User.FindFirst("UserId");

            if (claim == null)
                throw new UnauthorizedException();

            return int.Parse(claim.Value);
        }
    }
}
