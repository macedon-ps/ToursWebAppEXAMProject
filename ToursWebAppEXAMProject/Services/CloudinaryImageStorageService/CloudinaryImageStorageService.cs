using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ToursWebAppEXAMProject.ConfigFiles;
using ToursWebAppEXAMProject.Enums;

namespace ToursWebAppEXAMProject.Services.CloudineryImageStorageService
{
    public class CloudinaryImageStorageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly ILogger<CloudinaryImageStorageService> _logger;

        public CloudinaryImageStorageService(Cloudinary cloudinary, ILogger<CloudinaryImageStorageService> logger)
        {
            _cloudinary = cloudinary;
            _logger = logger;
        }


        /// <summary>
        /// Сохраняет файл изображения в соответствующую папку на сервере Cloudinery  и возвращает полный путь к сохраненному файлу вместе с возможными обработкой и форматированием. Сохраняет в БД его PublicId .
        /// </summary>
        /// <param name="file">Имя файла картинки.</param>
        /// <param name="folder">Папка сохранения файла картинки на сервере Cloudinery.</param>
        /// <returns>Сохранение файла картинки и возвращение полного пути к сохраненному файлу вместе с возможными обработкой и форматированием.</returns>
        public async Task<(string Url, string outPublicId)> UploadAsync (ImageFolder folder, IFormFile file, string inPablicId)
        {
            using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),

                Folder = ConfigImageFolder.GetPath(folder),

                PublicId = inPablicId,

                Overwrite = true,

                Transformation = new Transformation()
                    .Width(1200)
                    .Height(800)
                    .Crop("fill")
                    .Quality("auto")
                    .FetchFormat("auto")
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            return (result.SecureUrl.ToString(), 
                result.PublicId.ToString());
            
        }

        public async Task DeleteAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            await _cloudinary.DestroyAsync(
                deleteParams);
        }
    }
    
}
