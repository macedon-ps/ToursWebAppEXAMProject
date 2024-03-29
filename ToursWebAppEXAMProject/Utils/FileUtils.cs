﻿using NLog;

namespace ToursWebAppEXAMProject.Utils
{
    public class FileUtils
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Метод сохранения картинки для страницы About по указанному пути с проверкой пути
        /// </summary>
        /// <param name="filePath">путь сохранения картинки</param>
        /// <param name="changeTitleImagePath">значение поля типа IFormFile для названия сохраняемого файла</param>
        /// <param name="webHostEnvironment">webHostEnvironment</param>
        public async Task SaveImageToFolder(string folder, IFormFile? changeImagePath)
        {
            // проверка существования папки сохранения, если ее нет, то она создается + полный путь к папке
            var fullPathToFolder = GetOrCreateFolderPath(folder);

            // создаем абсолютный и относительный пути к файлу
            var fullFilePath = string.Empty;
            var relativeFilePath = string.Empty;

            if (changeImagePath != null)
            {
                fullFilePath = $"{fullPathToFolder}{changeImagePath.FileName}";
                relativeFilePath = $"{folder}{changeImagePath.FileName}";
            }
            else
            {
                return;
            }

            // сохраняем картинку и в свойство MainImagePath сохраняем путь к ней
            using (var fstream = new FileStream(fullFilePath, FileMode.Create))
            {
                if (changeImagePath != null)
                {
                    await changeImagePath.CopyToAsync(fstream);
                    _logger.Debug($"Новая картинка сохранена по пути: {relativeFilePath}\n");
                }
                else
                {
                    _logger.Error($"Ошибка сохранения картинки. Путь: {relativeFilePath}\n");
                    return;
                }
            }
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
            return "E:/C#_VS_2022/ToursWebAppEXAMProject/ToursWebAppEXAMProject/wwwroot/images/AboutPage/PhotoGallery";
        }
        public string GetRelativePath(string fullPath)
        {
            var relativePath = fullPath.Replace("E:/C#_VS_2022/ToursWebAppEXAMProject/ToursWebAppEXAMProject/wwwroot", "");
            return relativePath;
        }

        public void DeletePhoto(string fullPathToFile)
        {
            File.Delete(fullPathToFile);
        }
    }
}
