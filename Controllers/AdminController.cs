using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relief.DTOs.RequestModel;
using Relief.Entities;
using Relief.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;

namespace Relief.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(IAdminService adminService, IWebHostEnvironment webHostEnvironment)
        {
            _adminService = adminService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromForm]CreateAdminRequestModel model)
        {
            var files = HttpContext.Request.Form;

            if (files != null && files.Count > 0)
            {
                string imageDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                Directory.CreateDirectory(imageDirectory);
                foreach (var file in files.Files)
                {
                    FileInfo info = new FileInfo(file.FileName);
                    string image = Guid.NewGuid().ToString() + info.Extension;
                    string path = Path.Combine(imageDirectory, image);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    model.Image = (image);
                }
            }

            var registerAdmin = await _adminService.RegisterAdmin(model);
            if (registerAdmin.Success == false)
            {
                return BadRequest(registerAdmin);
            }
            return Ok(registerAdmin);
        }

        [HttpPut("UpdateAdmin/{id}")]
        public async Task<IActionResult> UpdateAdmin([FromForm]UpdateAdminRequestModel model, [FromRoute]int id)
        {
            var files = HttpContext.Request.Form;

            if (files != null && files.Count > 0)
            {
                string imageDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                Directory.CreateDirectory(imageDirectory);
                foreach (var file in files.Files)
                {
                    FileInfo info = new FileInfo(file.FileName);
                    string image = Guid.NewGuid().ToString() + info.Extension;
                    string path = Path.Combine(imageDirectory, image);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    model.Image = (image);
                }
            }

            var updateAdmin = await _adminService.UpdateAdmin(model, id);
            if (updateAdmin.Success == false)
            {
                return BadRequest(updateAdmin);
            }
            return Ok(updateAdmin);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _adminService.GetAll();
            if (list.Success == false)
            {
                return BadRequest(list);
            }
            return Ok(list);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var admin = await _adminService.GetById(id);
            if (admin.Success == false)
            {
                return BadRequest(admin);
            }
            return Ok(admin);
        }

        [HttpGet("ShowData")]
        public async Task<IActionResult> ShowData()
        {
            var data = await _adminService.ShowData();
            return Ok(data);
        }
    }
}
