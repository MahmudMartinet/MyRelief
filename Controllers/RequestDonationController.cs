using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relief.DTOs.RequestModel;
using Relief.Entities;
using Relief.Interfaces.Services;

namespace Relief.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestDonationController : ControllerBase
    {
        private readonly IRequestDonationService _requestDonationService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RequestDonationController(IRequestDonationService requestDonationService, IWebHostEnvironment webHostEnvironment)
        {
            _requestDonationService = requestDonationService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("CreateRequest/{ngoId}")]
        public async Task<IActionResult> CreateRequest([FromForm] CreateRequestDonationRequestModel model, [FromRoute] int ngoId)
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
                    model.Documents.Add(image);
                }
            }

            var request = await _requestDonationService.CreateRequest(model, ngoId);
            if (request.Success == false)
            {
                return BadRequest(request);
            }
            return Ok(request);
        }

        [HttpPut("UpdateRequest/{id}")]
        public async Task<IActionResult> UpdateRequest([FromForm] UpdateRequestDonationRequestModel model, [FromRoute] int id)
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
                    model.Documents.Add(image);
                }
            }

            var update = await _requestDonationService.UpdateRequest(model, id);
            if (update.Success == false)
            {
                return BadRequest(update);
            }
            return Ok(update);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var all = await _requestDonationService.GetAll();
            if(all.Success == false)
            {
                return BadRequest(all);
            }
            return Ok(all);
        }

        [HttpGet("GetAllCount")]
        public async Task<IActionResult> GetAllCount()
        {
            var count = await _requestDonationService.GetAllCount();
            return Ok(count);
        }

        [HttpGet("GetCompletedRequests")]
        public async Task<IActionResult> GetCompletedRequests()
        {
            var requests = await _requestDonationService.GetCompletedRequests();
            if (requests.Success == false)
            {
                return BadRequest(requests);
            }
            return Ok(requests);
        }

        [HttpGet("GetCompletedRequestsCount")]
        public async Task<IActionResult> GetCompletedRequestsCount()
        {
            var count = await _requestDonationService.GetCompletedRequestsCount();
            return Ok(count);
        }

        [HttpGet("GetUncompletedRequests")]
        public async Task<IActionResult> GetUncompletedRequests()
        {
            var requests = await _requestDonationService.GetUncompletedRequests();
            if (requests.Success == false)
            {
                return BadRequest(requests);
            }
            return Ok(requests);
        }

        [HttpGet("GetUncompletedRequestsCount")]
        public async Task<IActionResult> GetUncompletedRequestsCount()
        {
            var count = await _requestDonationService.GetUncompletedRequestsCount();
            return Ok(count);
        }

        [HttpGet("GetRequest/{id}")]
        public async Task<IActionResult> GetRequest([FromRoute] int id)
        {
            var request = await _requestDonationService.GetRequest(id);
            if(request.Success == false)
            {
                return BadRequest(request);
            }
            return Ok(request);
        }

        [HttpGet("GetRequestDonationsByDetail/{detail}")]
        public async Task<IActionResult> GetRequestDonationsByDetail([FromRoute] string detail)
        {
            var requests = await _requestDonationService.GetRequestDonationsByDetail(detail);
            if(requests.Success == false)
            {
                return BadRequest(requests);
            }
            return Ok(requests);
        }

        [HttpGet("GetUncompletedRequestByNgo/{id}")]
        public async Task<IActionResult> GetUncompletedRequestByNgo([FromRoute] int id)
        {
            var requests = await _requestDonationService.GetUncompletedRequestByNgo(id);
            if (requests.Success == false)
            {
                return BadRequest(requests);
            }
            return Ok(requests);
        }

        [HttpGet("GetCompletedRequestByNgo/{id}")]
        public async Task<IActionResult> GetCompletedRequestByNgo([FromRoute] int id)
        {
            var requests = await _requestDonationService.GetCompletedRequestByNgo(id);
            if (requests.Success == false)
            {
                return BadRequest(requests);
            }
            return Ok(requests);
        }

        [HttpGet("GetRequestByNgo/{id}")]
        public async Task<IActionResult> GetRequestByNgo([FromRoute] int id)
        {
            var requests = await _requestDonationService.GetRequestByNgo(id);
            if (requests.Success == false)
            {
                return BadRequest(requests);
            }
            return Ok(requests);
        }

        [HttpGet("GetCompletedByNgoCount/{id}")]
        public async Task<IActionResult> GetCompletedByNgoCount([FromRoute] int id)
        {
            var req = await _requestDonationService.GetCompletedByNgoCount(id);
            return Ok(req);
        }

        [HttpGet("GetUncompletedByNgoCount/{id}")]
        public async Task<IActionResult> GetUncompletedByNgoCount([FromRoute] int id)
        {
            var req = await _requestDonationService.GetUncompletedByNgoCount(id);
            return Ok(req);
        }

        [HttpGet("GetRequestByNgoCount/{id}")]
        public async Task<IActionResult> GetRequestByNgoCount([FromRoute] int id)
        {
            var req = await _requestDonationService.GetRequestByNgoCount(id);
            return Ok(req);
        }
    }
}
