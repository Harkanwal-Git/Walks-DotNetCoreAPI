using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Walks.API.Models.Domain;
using Walks.API.Models.DTO;
using Walks.API.Repositories;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this._imageRepository = imageRepository;
        }
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length
                };
                //User repository to upload image2
               await _imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);

            }
            return BadRequest(ModelState);

        }
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", "pdf" };
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "Upload file smaller than 10MB");
            }
        }
    }
}
