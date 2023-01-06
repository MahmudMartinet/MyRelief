using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relief.DTOs.RequestModel;
using Relief.Interfaces.Services;
using Relief.Interfaces.Repositories;

namespace Relief.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("MakeReport/{ngoId}/{donorId}")]
        public async Task<IActionResult> MakeReport([FromForm]CreateReportRequestModel model, [FromRoute]int ngoId, [FromRoute]int donorId)
        {
            var make = await _reportService.MakeReport(model, ngoId, donorId);
            if (make.Success == false)
            {
                return BadRequest(make);
            }
            return Ok(make);
        }

        [HttpPut("UpdateReport/{id}")]
        public async Task<IActionResult> UpdateReport([FromForm]UpdateReportRequestModel model, [FromRoute]int id)
        {
            var update = await _reportService.UpdateReport(model, id);
            if(update.Success == false)
            {
                return BadRequest(update);
            }
            return Ok(update);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var reports = await _reportService.GetAll();
            if (reports.Success == false)
            {
                return BadRequest(reports);
            }
            return Ok(reports);
        }

        [HttpGet("GetReport/{id}")]
        public async Task<IActionResult> GetReport([FromRoute] int id)
        {
            var report = await _reportService.GetReport(id);
            if(report.Success == false) 
            {
                return BadRequest(report);
            }
            return Ok(report);
        }

        [HttpGet("GetReportsByContent/{content}")]
        public async Task<IActionResult> GetReportsByContent([FromRoute] string content)
        {
            var reports = await _reportService.GetReportsByContent(content);
            if(reports.Success == false) { return BadRequest(reports); }
            return Ok(reports);
        }

        [HttpGet("GetByDonorId/{id}")]
        public async Task<IActionResult> GetByDonorId([FromRoute] int id)
        {
            var reports = await _reportService.GetByDonorId(id);
            if(reports.Success == false)
            {
                return BadRequest(reports);
            }
            return Ok(reports);
        }
    }
}
