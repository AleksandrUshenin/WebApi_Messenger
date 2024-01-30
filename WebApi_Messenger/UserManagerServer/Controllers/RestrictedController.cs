using Messenger.Models;
using Messenger.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UserManagerServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestrictedController : ControllerBase
    {
        public RestrictedController()
        { }


        [HttpGet]
        [Route("AdminEndPoint")]
        [Authorize(Roles = "Administrator")]
        public IActionResult AdminEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi you are an Role:{currentUser.Role}, Name:{currentUser.Name}");
        }

        [HttpGet]
        [Route("UserEndPoint")]
        [Authorize(Roles = "Administrator, User")]
        public IActionResult UserEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi you are an Role:{currentUser.Role}, Name:{currentUser.Name}");
        }
        private UserDTO GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserDTO
                {
                    Name = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    Role = (UserRole)Enum.Parse(typeof(UserRole),
                        userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value)
                };
            }

            return null;
        }
    }
}
