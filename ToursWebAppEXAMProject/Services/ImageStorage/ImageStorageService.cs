using ToursWebAppEXAMProject.ConfigFiles;
using ToursWebAppEXAMProject.Enums;

namespace ToursWebAppEXAMProject.Services.ImageStorage
{
    public class ImageStorageService
    {
        private readonly IWebHostEnvironment _env;

        public ImageStorageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Сохраняет загруженный файл изображения в соответствующую папку на сервере и возвращает относительный путь к сохраненному файлу.
        /// </summary>
        /// <param name="file">Имя файла картинки.</param>
        /// <param name="folder">Папка сохранения файла картинки.</param>
        /// <returns>Сохранение файла картинки и возвращение относительного пути к сохраненному файлу.</returns>
        public async Task<string?> SaveAsync(IFormFile? file, ImageFolder folder)
        {
            if (file == null)
            {
                return null;
            }

            // относительный путь к папке для сохранения изображения на основе переданного параметра folder
            var relativeFolder = ConfigImageFolder.GetPath(folder);

            // полный путь к папке для сохранения изображения на сервере
            var fullFolderPath = Path.Combine(_env.WebRootPath, relativeFolder);

            // если папки для сохранения изображения не существует, то создать её
            if (!Directory.Exists(fullFolderPath))
            {
                Directory.CreateDirectory(fullFolderPath);
            }

            // генерируем уникальное имя для файла изображения, используя как часть имени - оригинальное название, часть -  GUID и сохраняем его с расширением исходного файла
            var originalName = Path.GetFileNameWithoutExtension(file.FileName);
            var guidName = Guid.NewGuid().ToString("N");
            var fileName = $"{originalName}_{guidName}" + Path.GetExtension(file.FileName);

            // полный путь к файлу изображения на сервере
            var fullPath = Path.Combine(fullFolderPath, fileName);

            // сохраняем файл изображения на сервере
            using var stream = new FileStream(fullPath, FileMode.Create);

            // асинхронно копируем содержимое загруженного файла изображения в поток для сохранения на сервере
            await file.CopyToAsync(stream);

            return "/" + Path.Combine(relativeFolder, fileName)
                            .Replace("\\", "/");
        }
    }
}
