using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relief.DTOs.RequestModel;
using Relief.Interfaces.Services;

namespace Relief.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateUserController : ControllerBase
    {
        private readonly ICreateUserService _createUserService;
        public CreateUserController(ICreateUserService createUserService)
        {
            _createUserService = createUserService;
        }


        [HttpPost("CreateAdmin")]
        public async Task<IActionResult> CreateAdmin([FromForm] AddAdminRequestModel model)
        {
            var user = await _createUserService.CreateAdmin(model);
            if (user.Success == false)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }

        [HttpPost("CreateDonor")]
        public async Task<IActionResult> CreateDonor([FromForm] CreateUserRequestModel model)
        {
            var user = await _createUserService.CreateDonor(model);
            if (user.Success == false)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }

        [HttpPost("CreateNgo")]
        public async Task<IActionResult> CreateNgo([FromForm] CreateUserRequestModel model)
        {
            var user = await _createUserService.CreateNgo(model);
            if (user.Success == false)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }

        [HttpPost("VerifyUser")]
        public async Task<IActionResult> VerifyUser([FromForm] VerifyUserRequestModel model)
        {
            var user = await _createUserService.VerifyUser(model);
            if(user.Success == false)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser([FromRoute] string email)
        {
            var user = await _createUserService.GetUser(email);
            if (user.Success == false)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }
    }
}
