using NLog;

namespace ToursWebAppEXAMProject.Utils
{
    public class FileUtils
    {
        private readonly IWebHostEnvironment _env;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public FileUtils(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Метод сохранения картинки для страницы About по указанному пути с проверкой пути
        /// </summary>
        /// <param name="filePath">путь сохранения картинки</param>
        /// <param name="changeTitleImagePath">значение поля типа IFormFile для названия сохраняемого файла</param>
        /// <param name="webHostEnvironment">webHostEnvironment</param>
        public async Task SaveImageToFolder(string folder, IFormFile? imageFileName)
        {
            // проверка существования папки сохранения, если ее нет, то она создается + полный путь к папке
            var fullPathToFolder = GetOrCreateFolderPath(folder);

            // создаем абсолютный и относительный пути к файлу
            var fullImageFilePath = string.Empty;
            var relativeImageFilePath = string.Empty;

            if (imageFileName != null)
            {
                fullImageFilePath = $"{fullPathToFolder}{imageFileName.FileName}";
                relativeImageFilePath = $"{folder}{imageFileName.FileName}";
            }
            else
            {
                return;
            }

            // сохраняем картинку и в свойство MainImagePath сохраняем путь к ней
            using (var fstream = new FileStream(fullImageFilePath, FileMode.Create))
            {
                if (imageFileName != null)
                {
                    await imageFileName.CopyToAsync(fstream);
                    _logger.Debug($"Новая картинка сохранена по пути: {relativeImageFilePath}\n");
                }
                else
                {
                    _logger.Error($"Ошибка сохранения картинки. Путь: {relativeImageFilePath}\n");
                    return;
                }
            }
        }

        /// <summary>
        /// Метод проверки существования картинки по ее относительному пути.
        /// </summary>
        /// <param name="relativePath">Относительный путь к картинке</param>
        /// <returns>Результат проверки физич. существования картинки (True or False)</returns>
        private bool IsImageExists(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return false;

            var fullPath = Path.Combine(_env.WebRootPath, relativePath.TrimStart('/'));

            return System.IO.File.Exists(fullPath);
        }


        /// <summary>
        /// Метод проверки существования пути сохранения (папки сохранения); если ее нет, она создается
        /// </summary>
        /// <param name="folderPath">путь к папке</param>
        public string GetOrCreateFolderPath(string folder)
        {
            var pathTillFolder = Path.GetFullPath("wwwroot");
            var fullPathToFolder = pathTillFolder + folder;
            bool exists = Directory.Exists(fullPathToFolder);

            // если папки нет, то создаем ее
            if (!exists)
            {
                Directory.CreateDirectory(fullPathToFolder);
                _logger.Debug($"Новая папка создана по пути: {fullPathToFolder}\n");
            }

            return fullPathToFolder;
        }

        public string GetFullPathToFolderPhotoGallery()
        {
            // TODO: заменить ручной ввод абсолютного пути на поиск через метод, использовать этот метод везде, где нужно получить абсолютный путь к папке PhotoGallery
            return "E:/C#_VS_2022/ToursWebAppEXAMProject/ToursWebAppEXAMProject/wwwroot/images/AboutPage/PhotoGallery";
        }
        public string GetRelativePath(string fullPath)
        {
            // TODO: заменить ручной ввод относительного пути на поиск через метод, использовать этот метод везде, где нужно получить относительный путь к папке PhotoGallery
            var relativePath = fullPath.Replace("E:/C#_VS_2022/ToursWebAppEXAMProject/ToursWebAppEXAMProject/wwwroot", "");
            return relativePath;
        }

        public void DeletePhoto(string fullPathToFile)
        {
            File.Delete(fullPathToFile);
        }
    }
}
