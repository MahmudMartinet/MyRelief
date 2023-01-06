using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relief.DTOs.RequestModel;
using Relief.Interfaces.Services;

namespace Relief.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CommentController(ICommentService commentService, IWebHostEnvironment webHostEnvironment)
        {
            _commentService = commentService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("CreateComment/{donorId}/{ngoId}")]
        public async Task<IActionResult> CreateComment([FromForm] CreateCommentRequestModel model, [FromRoute]int donorId, [FromRoute]int ngoId)
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
            var create = await _commentService.CreateComment(model, donorId, ngoId);
            if (create.Success == false)
            {
                return BadRequest(create);
            }
            return Ok(create);
        }

        [HttpPost("CreateRequestComment/{donorId}/{requestId}")]
        public async Task<IActionResult> CreateRequestComment([FromForm] CreateCommentRequestModel model, [FromRoute] int donorId, [FromRoute] int requestId)
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
            var create = await _commentService.CreateRequestComment(model, donorId, requestId);
            if (create.Success == false)
            {
                return BadRequest(create);
            }
            return Ok(create);
        }

        [HttpPut("UpdateComment/{id}")]
        public async Task<IActionResult> UpdateComment([FromForm] UpdateCommentRequestModel model, [FromRoute]int id)
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
            var update = await _commentService.UpdateComment(model, id);
            if(update.Success == false)
            {
                return BadRequest(update);
            }
            return Ok(update);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var all = await _commentService.GetAll();
            if (all.Success == false)
            {
                return BadRequest(all);
            }
            return Ok(all);
        }

        [HttpGet("GetComment/{id}")]
        public async Task<IActionResult> GetComment([FromRoute] int id)
        {
            var comment = await _commentService.GetComment(id);
            if (comment.Success == false)
            {
                return BadRequest(comment);
            }
            return Ok(comment);
        }

        [HttpGet("GetByRequestId/{id}")]
        public async Task<IActionResult> GetByRequestId([FromRoute] int id)
        {
            var comment = await _commentService.GetByRequestId(id);
            if (comment.Success == false)
            {
                return BadRequest(comment);
            }
            return Ok(comment);
        }
                
        [HttpGet("GetCommentByNgoId/{id}")]
        public async Task<IActionResult> GetCommentByNgoId([FromRoute] int id)
        {
            var comment = await _commentService.GetCommentByNgoId(id);
            if(comment.Success == false)
            {
                return BadRequest(comment);
            }
            return Ok(comment);
        }

        [HttpGet("GetCommentsByContent/{content}")]
        public async Task<IActionResult> GetCommentsByContent([FromRoute]string content)
        {
            var comments = await _commentService.GetCommentsByContent(content);
            if(comments.Success == false)
            {
                return BadRequest(comments);
            }
            return Ok(comments);
        }
    }
}
