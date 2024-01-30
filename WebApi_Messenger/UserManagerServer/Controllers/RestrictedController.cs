using Messenger.Models;
using Messenger.Models.DTO;
using Messenger.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UserManagerServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestrictedController : ControllerBase
    {
        private readonly IUserManeger _userManeger;
        public RestrictedController(IUserManeger userManeger)
        {
            _userManeger = userManeger;
        }


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
        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetAllUsers()
        {
            return Ok(_userManeger.GetUsers());
        }
        [HttpDelete]
        [Route("DeleteUser")]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteUser(string name)
        {
            try
            {
                _userManeger.DeleteUser(name);
                return Ok("Successfully!");
            }
            catch (Exception e) 
            {
                return BadRequest(e.Message);
            }
        }
    }
}
