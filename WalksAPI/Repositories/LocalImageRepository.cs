using Walks.API.Data;
using Walks.API.Models.Domain;

namespace Walks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly WalksDBContext _dBContext;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,WalksDBContext walksDBContext,
            IHttpContextAccessor httpContextAccessor)
        {
            this.webHostEnvironment = webHostEnvironment;
            this._dBContext = walksDBContext;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", image.FileName+image.FileExtension);

            //Upload image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName + image.FileExtension}";

            image.FilePath = urlFilePath;
            await _dBContext.Images.AddAsync(image);
            await _dBContext.SaveChangesAsync();
            return image;

        }
    }
}
