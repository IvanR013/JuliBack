using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using JuliBack.Models;
using JuliBack.Repositories;

namespace JuliBack.Services
{
    public class CloudinaryService
    {

        private readonly Cloudinary _cloudinary;
        private readonly IimageRepository _imageRepository;

        public CloudinaryService(IimageRepository imageRepository, Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
            _imageRepository = imageRepository;
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };

            try
            {
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult == null)
                {
                    throw new InvalidOperationException("El resultado de la carga es nulo.");
                }

                if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new InvalidOperationException($"Error en la carga de la imagen: {uploadResult.Error?.Message}");
                }

                if (uploadResult.SecureUrl == null)
                {
                    throw new InvalidOperationException("La URL segura no fue generada correctamente.");
                }

                return uploadResult.SecureUrl.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al subir la imagen: " + ex.Message);
                throw new Exception("Hubo un error al subir la imagen.", ex);
            }
        }

        public async Task SaveImageUrlToDatabaseAsync(string url)
        {
            var image = new Images { PublicUrl = url};

            await _imageRepository.AddImagesAsync(image);

            await _imageRepository.SaveChangesAsync();
        }

        public async Task SaveManager(IFormFile file, string title, string description, string section, string redirectUrl)
        {
            var publicUrl = await UploadImageAsync(file);
            int UserId = 3;
            var image = new Images
            {
                Tittle = title,
                Description = description,
                Section = section,
                RedirectUrl = redirectUrl,
                PublicUrl = publicUrl,
                Userid = UserId
            };
            
            await _imageRepository.AddImagesAsync(image);
            await _imageRepository.SaveChangesAsync();
        }
    }
}
