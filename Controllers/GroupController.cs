using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relief.DTOs.RequestModel;
using Relief.Interfaces.Services;

namespace Relief.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost("CreateGroup")]
        public async Task<IActionResult> CreateGroup([FromForm]CreateGroupRequestModel model)
        {
            var group = await _groupService.CreateGroup(model);
            if(group.Success == false)
            {
                return BadRequest(group);
            }
            return Ok(group);
        }

        [HttpPut("UpdateGroup/{id}")]
        public async Task<IActionResult> UpdateGroup([FromForm] UpdateGroupRequestModel model, [FromRoute]int id)
        {
            var group = await _groupService.UpdateGroup(model, id);
            if(group.Success == false)
            {
                return BadRequest(group);
            }
            return Ok(group);
        }

        [HttpGet("GetByContent/{content}")]
        public async Task<IActionResult> GetByContent([FromRoute] string content)
        {
            var groups = await _groupService.GetByContent(content);
            if (groups.Success == false)
            {
                return BadRequest(groups);
            }
            return Ok(groups);
        }

        [HttpGet("GetGroup/{id}")]
        public async Task<IActionResult> GetGroup([FromRoute] int id)
        {
            var group = await _groupService.GetGroup(id);
            if (group.Success == false)
            {
                return BadRequest(group);
            }
            return Ok(group);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var groups = await _groupService.GetAll();
            if (groups.Success == false)
            {
                return BadRequest(groups);
            }
            return Ok(groups);
        }
    }
}
