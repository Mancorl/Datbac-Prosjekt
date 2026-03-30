using Microsoft.AspNetCore.Http;

namespace Unhosted_Api.Services
{
    public interface IFileUploadService
    {
        string UploadImage(IFormFile file, string folder = "images");
    }

    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _env;

        public FileUploadService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string UploadImage(IFormFile file, string folder = "images")
        {
            if (file == null || file.Length == 0)
                return $"{folder}/Default.jpg"; // fallback

            // Ensure folder exists
            var uploadPath = Path.Combine(_env.WebRootPath, folder);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // Unique filename
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"{folder}/{fileName}"; // relative path to serve
        }
    }
}