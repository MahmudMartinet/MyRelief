using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relief.DTOs.RequestModel;
using Relief.Interfaces.Services;

namespace Relief.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly IDonationService _donationService;

        public DonationController(IDonationService donationService)
        {
            _donationService = donationService;
        }

        [HttpPost("MakeDonation/{donorId}/{requestId}")]
        public async Task<IActionResult> MakeDonation([FromForm]CreateDonationRequestModel model, [FromRoute]int donorId, [FromRoute]int requestId)
        {
            var donation = await _donationService.MakeDonation(model, donorId, requestId);
            if (donation.Success == false)
            {
                return BadRequest(donation);
            }
            return Ok(donation);
        }

        [HttpPut("UpdateDonation/{id}")]
        public async Task<IActionResult> UpdateDonation([FromForm]UpdateDonationRequestModel model, [FromRoute]int id)
        {
            var update = await _donationService.UpdateDonation(model, id);
            if (update.Success == false)
            {
                return BadRequest(update);
            }
            return Ok(update);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var get = await _donationService.GetAll();
            if (get.Success == false)
            {
                return BadRequest(get);
            }
            return Ok(get);
        }

        [HttpGet("GetByDonorId/{id}")]
        public async Task<IActionResult> GetByDonorId([FromRoute] int id)
        {
            var get = await _donationService.GetByDonorId(id);
            if(get.Success == false)
            {
                return BadRequest(get);
            }
            return Ok(get);
        }


        [HttpGet("GetByRequestId/{id}")]
        public async Task<IActionResult> GetByRequestId([FromRoute] int id)
        {
            var get = await _donationService.GetByRequestId(id);
            if (get.Success == false)
            {
                return BadRequest(get);
            }
            return Ok(get);
        }


        [HttpGet("GetDonationById/{id}")]
        public async Task<IActionResult> GetDonationById([FromRoute] int id)
        {
            var get = await _donationService.GetDonationById(id);
            if(get.Success == false)
            {
                return BadRequest(get);
            }
            return Ok(get);
        }
    }
}
