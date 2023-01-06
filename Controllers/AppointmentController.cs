using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relief.DTOs.RequestModel;
using Relief.Interfaces.Services;

namespace Relief.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("CreateAppointment/{donorId}/{requestId}")]
        public async Task<IActionResult> CreateAppointment([FromForm] CreateAppointmentRequestModel model, [FromRoute]int donorId, [FromRoute]int requestId)
        {
            var appoint = await _appointmentService.CreateAppointment(model, donorId, requestId);
            if(appoint.Success == false)
            {
                return BadRequest(appoint);
            }
            return Ok(appoint);
        }

        [HttpPut("ApproveAppointment/{id}")]
        public async Task<IActionResult> ApproveAppointment([FromRoute] int id)
        {
            var appoint = await _appointmentService.ApproveAppointment(id);
            if (appoint.Success == false)
            {
                return BadRequest(appoint);
            }
            return Ok(appoint);
        }

        [HttpPut("UpdateAppointment/{id}")]
        public async Task<IActionResult> UpdateAppointment([FromForm]UpdateAppointmentRequestModel model, [FromRoute]int id)
        {
            var update = await _appointmentService.UpdateAppointment(model, id);
            if(update.Success == false)
            {
                return BadRequest(update);
            }
            return Ok(update);
        }

        [HttpGet("GetByVenue/{venue}")]
        public async Task<IActionResult> GetByVenue([FromRoute] string venue)
        {
            var appointments = await _appointmentService.GetByVenue(venue);
            if(appointments.Success == false)
            {
                return BadRequest(appointments);
            }
            return Ok(appointments);
        }

        [HttpGet("GetByRequestId/{id}")]
        public async Task<IActionResult> GetByRequestId([FromRoute] int id)
        {
            var appointments = await _appointmentService.GetByRequestId(id);
            if (appointments.Success == false)
            {
                return BadRequest(appointments);
            }
            return Ok(appointments);
        }

        [HttpGet("GetByNgoId/{id}")]
        public async Task<IActionResult> GetByNgoId([FromRoute] int id)
        {
            var appointments = await _appointmentService.GetByNgoId(id);
            if (appointments.Success == false)
            {
                return BadRequest(appointments);
            }
            return Ok(appointments);
        }

        [HttpGet("GetAccomplishedByDonorId/{id}")]
        public async Task<IActionResult> GetAccomplishedByDonorId([FromRoute] int id)
        {
            var appointments = await _appointmentService.GetAccomplishedByDonorId(id);
            if(appointments.Success == false)
            {
                return BadRequest(appointments);
            }
            return Ok(appointments);
        }

        [HttpGet("GetUnaccomplishedByDonorId/{id}")]
        public async Task<IActionResult> GetUnaccomplishedByDonorId([FromRoute] int id)
        {
            var appointments = await _appointmentService.GetUnaccomplishedByDonorId(id);
            if (appointments.Success == false)
            {
                return BadRequest(appointments);
            }
            return Ok(appointments);
        }

        [HttpGet("GetAccomplishedByNgoId/{id}")]
        public async Task<IActionResult> GetAccomplishedByNgoId([FromRoute] int id)
        {
            var appointments = await _appointmentService.GetAccomplishedByNgoId(id);
            if(appointments.Success == false)
            {
                return BadRequest(appointments);
            }
            return Ok(appointments);
        }

        [HttpGet("GetUnaccomplishedByNgoId/{id}")]
        public async Task<IActionResult> GetUnaccomplishedByNgoId([FromRoute] int id)
        {
            var appointments = await _appointmentService.GetUnaccomplishedByNgoId(id);
            if (appointments.Success == false)
            {
                return BadRequest(appointments);
            }
            return Ok(appointments);
        }

        [HttpGet("GetUnapprovedByNgoId/{id}")]
        public async Task<IActionResult> GetUnapprovedByNgoId([FromRoute] int id)
        {
            var appointments = await _appointmentService.GetUnapprovedByNgoId(id);
            if (appointments.Success == false)
            {
                return BadRequest(appointments);
            }
            return Ok(appointments);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var appointments = await _appointmentService.GetById(id);
            if (appointments.Success == false)
            {
                return BadRequest(appointments);
            }
            return Ok(appointments);
        }

        [HttpGet("GetByDonorId/{id}")]
        public async Task<IActionResult> GetByDonorId([FromRoute] int id)
        {
            var appointments = await _appointmentService.GetByDonorId(id);
            if (appointments.Success == false)
            {
                return BadRequest(appointments);
            }
            return Ok(appointments);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var appointment = await _appointmentService.GetAll();
            if(appointment.Success == false)
            {
                return BadRequest(appointment);
            }
            return Ok(appointment);
        }
    }
}
