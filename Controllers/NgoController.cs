using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relief.DTOs.RequestModel;
using Relief.Entities;
using Relief.Interfaces.Repositories;
using Relief.Interfaces.Services;

namespace Relief.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NgoController : ControllerBase
    {
        private readonly INgoService _ngoService;
        private readonly IDocumentRepository _documentRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NgoController(INgoService ngoservice, IDocumentRepository documentRepository, IWebHostEnvironment webHostEnvironment)
        {
            _ngoService = ngoservice;
            _documentRepository = documentRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPut("ApproveNgo/{id}")]
        public async Task<IActionResult> ApproveNgo([FromRoute] int id)
        {
            var approve = await _ngoService.ApproveNgo(id);
            if(approve.Success == false)
            {
                return BadRequest(approve);
            }
            return Ok(approve);
        }

        [HttpPut("BanNgo/{id}")]
        public async Task<IActionResult> BanNgo([FromRoute] int id)
        {
            var ban = await _ngoService.BanNgo(id);
            if (ban.Success == false)
            {
                return BadRequest(ban);
            }
            return Ok(ban);
        }

        [HttpPut("UnbanNgo/{id}")]
        public async Task<IActionResult> UnbanNgo([FromRoute] int id)
        {
            var ban = await _ngoService.UnbanNgo(id);
            if (ban.Success == false)
            {
                return BadRequest(ban);
            }
            return Ok(ban);
        }

        [HttpPut("DeleteNgo/{id}")]
        public async Task<IActionResult> DeleteNgo([FromRoute] int id)
        {
            var ban = await _ngoService.DeleteNgo(id);
            if (ban.Success == false)
            {
                return BadRequest(ban);
            }
            return Ok(ban);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm]CreateNgoRequestModel model)
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
                    model.Image = image;
                }
            }
            var register = await _ngoService.Register(model);
            if (register.Success == false)
            {
                return BadRequest(register);
            }
            return Ok(register);
        }

        [HttpPut("UploadDocuments/{ngoId}")]
        public async Task<IActionResult> UploadDocuments([FromForm] UploadRequestModel model, [FromRoute] int ngoId)
        {
            var files = HttpContext.Request.Form;

            if (files != null)
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
            //if (model.Documents != null)
            //{
            //    foreach (var img in model.Documents)
            //    {
            //        var image = new Document
            //        {
            //            Path = img,
            //            NgoId = ngoId
            //        };
            //        await _documentRepository.Register(image);
            //    }
            //}
            var doc = await _ngoService.UploadDocuments(model, ngoId);
            if(doc.Success == false)
            {
                return BadRequest(doc);
            }
            return Ok(doc);
        }

        [HttpPut("UpdateAddress/{id}")]
        public async Task<IActionResult> UpdateAddress([FromForm] AddressRequestModel model, [FromRoute]int id)
        {
            var address = await _ngoService.UpdateAddress(model, id);
            if(address.Success == false)
            {
                return BadRequest(address);
            }
            return Ok(address);
        }

        [HttpPut("UpdateBankDetails/{id}")]
        public async Task<IActionResult> UpdateBankDetails([FromForm] AccountDetailsRequestModel model, [FromRoute] int id)
        {
            var account = await _ngoService.UpdateBankDetails(model, id);
            if (account.Success == false)
            {
                return BadRequest(account);
            }
            return Ok(account);
        }

        [HttpGet("GetAllWithCategory")]
        public async Task<IActionResult> GetAllWithCategory()
        {
            var get = await _ngoService.GetAllWithCategory();
            if(get.Success == false)
            {
                return BadRequest(get);
            }
            return Ok(get);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromForm]UpdateNgoRequestModel model, [FromRoute]int id)
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
            var update = await _ngoService.Update(model, id);
            if(update.Success == false)
            {
                return BadRequest(update);
            }
            return Ok(update);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _ngoService.GetAll();
            if(list.Success == false) { return BadRequest(list); }
            return Ok(list);
        }

        [HttpGet("GetAllCount")]
        public async Task<IActionResult> GetAllCount()
        {
            var count = await _ngoService.GetAllCount();
            return Ok(count);
        }

        [HttpGet("GetByDescriptionContent/{content}")]
        public async Task<IActionResult> GetByDescriptionContent([FromRoute] string content)
        {
            var ngo = await _ngoService.GetByDescriptionContent(content);
            if(ngo.Success == false) { return BadRequest(ngo); }
            return Ok(ngo);
        }

        [HttpGet("GetNgo/{id}")]
        public async Task<IActionResult> GetNgo([FromRoute] int id)
        {
            var ngo = await _ngoService.GetNgo(id);
            if(ngo.Success == false)
            {
                return BadRequest(ngo);
            }
            return Ok(ngo);
        }

        [HttpGet("GetNgoByEmail/{email}")]
        public async Task<IActionResult> GetNgoByEmail([FromRoute] string email)
        {
            var ngo = await _ngoService.GetNgoByEmail(email);
            if(ngo.Success == false)
            {
                return BadRequest(ngo);
            }
            return Ok(ngo);
        }

        [HttpGet("GetNgoByName/{name}")]
        public async Task<IActionResult> GetNgoByName([FromRoute] string name)
        {
            var ngos = await _ngoService.GetNgoByName(name);
            if(ngos.Success == false) { return BadRequest(ngos); }
            return Ok(ngos);
        }

        [HttpGet("GetUnapprovedNgos")]
        public async Task<IActionResult> GetUnapprovedNgos()
        {
            var ngos = await _ngoService.GetUnapprovedNgos();
            if(ngos.Success == false) { return BadRequest(ngos); }
            return Ok(ngos);
        }

        [HttpGet("GetUnapprovedNgosCount")]
        public async Task<IActionResult> GetUnapprovedNgosCount()
        {
            var count = await _ngoService.GetUnapprovedNgosCount();
            return Ok(count);
        }

        [HttpGet("GetBannedNgos")]
        public async Task<IActionResult> GetBannedNgos()
        {
            var ngos = await _ngoService.GetBannedNgos();
            if (ngos.Success == false)
            { 
                return BadRequest(ngos);
            }
            return Ok(ngos);
        }
    }
}
