using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relief.DTOs.RequestModel;
using Relief.Interfaces.Services;

namespace Relief.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IDonorService _donorService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DonorController(IDonorService donorService, IWebHostEnvironment webHostEnvironment)
        {
            _donorService = donorService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm]CreateDonorRequestModel model)
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

            var register = await _donorService.Register(model);
            if (register.Success == false)
            {
                return BadRequest(register);
            }
            return Ok(register);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromForm] UpdateDonorRequestModel model, [FromRoute] int id)
        {
            var update = await _donorService.Update(model, id);
            if (update.Success == false)
            {
                return BadRequest(update);
            }
            return Ok(update);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var donors = await _donorService.GetByName(name);
            if (donors.Success == false)
            {
                return BadRequest(donors);
            }
            return Ok(donors);
        }

        [HttpGet("GetDonor/{id}")]
        public async Task<IActionResult> GetDonor([FromRoute]int id)
        {
            var donor = await _donorService.GetDonor(id);
            if (donor.Success == false)
            {
                return BadRequest(donor);
            }
            return Ok(donor);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var donors = await _donorService.GetAll();
            if(donors.Success == false)
            {
                return BadRequest(donors);
            }
            return Ok(donors);
        }

        [HttpGet("GetAllCount")]
        public async Task<IActionResult> GetAllCount()
        {
            var count = await _donorService.GetAllCount();
            return Ok(count);
        }

        [HttpGet("GetDeletedDonors")]
        public async Task<IActionResult> GetDeletedDonors()
        {
            var donors = await _donorService.GetDeletedDonors();
            if (donors.Success == false)
            {
                return BadRequest(donors);
            }
            return Ok(donors);
        }

        [HttpGet("GetBannedDonors")]
        public async Task<IActionResult> GetBannedDonors()
        {
            var donors = await _donorService.GetBannedDonors();
            if (donors.Success == false)
            {
                return BadRequest(donors);
            }
            return Ok(donors);
        }

        [HttpGet("GetBannedDonorsCount")]
        public async Task<IActionResult> GetBannedDonorsCount()
        {
            var count = await _donorService.GetBannedDonorsCount();
            return Ok(count);
        }

        [HttpGet("GetActiveDonors")]
        public async Task<IActionResult> GetActiveDonors()
        {
            var donors = await _donorService.GetActiveDonors();
            if (donors.Success == false)
            {
                return BadRequest(donors);
            }
            return Ok(donors);
        }

        [HttpGet("GetActiveDonorsCount")]
        public async Task<IActionResult> GetActiveDonorsCount()
        {
            var count = await _donorService.GetActiveDonorsCount();
            return Ok(count);
        }

        [HttpPut("DeleteDonor/{donorId}/{adminId}")]
        public async Task<IActionResult> DeleteDonor([FromRoute]int donorId)
        {
            var delete = await _donorService.DeleteDonor(donorId);
            if (delete.Success == false)
            {
                return BadRequest(delete);
            }
            return Ok(delete);
        }

        [HttpPut("BanDonor/{donorId}/{adminId}")]
        public async Task<IActionResult> BanDonor([FromRoute] int donorId, [FromRoute] int adminId)
        {
            var ban = await _donorService.BanDonor(donorId, adminId);
            if (ban.Success == false)
            {
                return BadRequest(ban);
            }
            return Ok(ban);
        }

        [HttpPut("UnbanDonor/{donorId}/{adminId}")]
        public async Task<IActionResult> UnbanDonor([FromRoute] int donorId, [FromRoute] int adminId)
        {
            var unban = await _donorService.UnbanDonor(donorId, adminId);
            if (unban.Success == false)
            {
                return BadRequest(unban);
            }
            return Ok(unban);
        }
    }
}
