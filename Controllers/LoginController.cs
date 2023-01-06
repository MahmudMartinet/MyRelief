using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relief.Authentication;
using Relief.DTOs;
using Relief.Interfaces.Services;

namespace Relief.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJWTAuthentication _auth;
        public LoginController(IUserService userService, IJWTAuthentication auth)
        {
            _userService = userService;
            _auth = auth;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserRequestModel model)
        {
            var login = await _userService.Login(model);
            if (!login.Success)
            {
                return BadRequest(login);
            }
            var token = _auth.GenerateToken(login);

            var response = new LoginResponse
            {
                Data = login,
                Token = token
            };
            return Ok(response);
        }
    }
}
