using FileManager.Services.Abstract;

namespace FileManager.Services.Concrete
{
    public class FileService: IFileService
    {
        private readonly string _uploadFolderPath;

        public FileService()
        {
            _uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

            if (!Directory.Exists(_uploadFolderPath))
            {
                Directory.CreateDirectory(_uploadFolderPath);
            }
        }

        public async Task UploadFileAsync(IFormFile file,string fileName,string filePath)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("فایل معتبر نیست.");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }


        public string GenerateNewFileName(IFormFile file)
        {
            var originalFileName = Path.GetFileNameWithoutExtension(file.FileName);
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{originalFileName}-{Guid.NewGuid()}{extension}";

            return fileName;
        }

        public bool DeleteFile(string fileName)
        {
            var filePath = Path.Combine(_uploadFolderPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }

            return false;
        }
    }
}
