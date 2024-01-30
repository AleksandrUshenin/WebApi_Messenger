using Messenger.Models.DTO;
using Microsoft.AspNetCore.Mvc;

using Messenger.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Messenger.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserManagerServer.Controllers;

namespace Messenger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagerController : ControllerBase
    {
        private readonly IUserManeger _userManeger;
        private readonly IConfiguration _configuration;
        public UserManagerController(IUserManeger userManeger, IConfiguration configuration)
        {
            _userManeger = userManeger;
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public ActionResult Login([FromBody] LoginUser loginUser)
        {
            try
            {
                var roleId = _userManeger.UserCheck(loginUser);

                var user = new User { Name = loginUser.Name, Role = roleId };

                var token = GenerateToken(user);
                return Ok(token);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        private string GenerateToken(User user)
        {
            var securityKey = new RsaSecurityKey(RSATools.GetPrivateKey());

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256Signature);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("AddAdmin")]
        public ActionResult AddAdmin([FromBody] UserDTO userDTO)
        {
            try
            {
                _userManeger.UserAdd(userDTO, Models.UserRole.Administrator);
                return Ok();
            }
            catch (Exception e) 
            {
                return StatusCode(500, e.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("AddUser")]
        public ActionResult AddUser([FromBody] UserDTO userDTO)
        {
            try
            {
                _userManeger.UserAdd(userDTO, Models.UserRole.User);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
