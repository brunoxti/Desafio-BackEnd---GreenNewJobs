namespace GreenNewJobs.Infrastructure.Services
{
    public class DeliveryPersonService
    {
        private readonly string _storageRootPath;

        public DeliveryPersonService()
        {
            _storageRootPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "cnh");

            if (!Directory.Exists(_storageRootPath))
            {
                Directory.CreateDirectory(_storageRootPath);
            }
        }

        public async Task<string> UploadCNHImageAsync(IFormFile image, string identifier)
        {
            if (image == null || image.Length == 0)
                throw new ArgumentException("Invalid image file.");

            var extension = Path.GetExtension(image.FileName);
            if (extension != ".png" && extension != ".bmp")
                throw new ArgumentException("Invalid image format. Only PNG and BMP are allowed.");

            // Cria um diretório específico para o motorista usando CPF ou CNPJ
            var driverDirectory = Path.Combine(_storageRootPath, identifier);
            if (!Directory.Exists(driverDirectory))
            {
                Directory.CreateDirectory(driverDirectory);
            }

            var fileName = $"CNH{extension}";
            var filePath = Path.Combine(driverDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}
