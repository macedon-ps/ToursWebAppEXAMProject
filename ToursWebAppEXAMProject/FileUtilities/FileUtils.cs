using ToursWebAppEXAMProject.EnumsDictionaries;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.FileUtilities
{
    public class FileUtils
    {
        /// <summary>
        /// Метод сохранения картинки для страницы About по указанному пути с проверкой пути
        /// </summary>
        /// <param name="filePath">путь сохранения картинки</param>
        /// <param name="changeTitleImagePath">значение поля типа IFormFile для названия сохраняемого файла</param>
        /// <param name="webHostEnvironment">webHostEnvironment</param>
        public static async void SaveFileIfExistPath(string folder, IFormFile? changeImagePath)
        {
            // проверка существования папки сохранения, если ее нет, то она создается + полный путь к папке
            var fullPathToFolder = IsFolderExist(folder);

            // создаем абсолютный и относительный пути к файлу
            var fullFilePath = $"{fullPathToFolder}{changeImagePath.FileName}";
            var relativeFilePath = $"{folder}{changeImagePath.FileName}";

            // сохраняем картинку и в свойство MainImagePath сохраняем путь к ней
            using (var fstream = new FileStream(fullFilePath, FileMode.Create))
            {
                await changeImagePath.CopyToAsync(fstream);

                WriteLogs($"Новая картинка сохранена по пути: {relativeFilePath}\n", NLogsModeEnum.Debug);
            }
        }

        /// <summary>
        /// Метод проверки существования пути сохранения (папки сохранения); если ее нет, она создается
        /// </summary>
        /// <param name="folderPath">путь к папке</param>
        public static string IsFolderExist(string folder)
        {
            var pathTillFolder = Path.GetFullPath("wwwroot");
            var fullPathToFolder = pathTillFolder + folder;
            bool exists = Directory.Exists(fullPathToFolder);

            // если папки нет, то создаем ее
            if (!exists)
            {
                Directory.CreateDirectory(fullPathToFolder);
                WriteLogs($"Новая папка создана по пути: {fullPathToFolder}\n", NLogsModeEnum.Debug);
            }
            
            return fullPathToFolder;
        }

        public static string GetFullPathToFolderPhotoGallery()
        {
            return "E:/C#_VS_2022/ToursWebAppEXAMProject/ToursWebAppEXAMProject/wwwroot/images/AboutPage/PhotoGallery";
        }
        public static string GetRelativePath(string fullPath)
        {
            var relativePath = fullPath.Replace("E:/C#_VS_2022/ToursWebAppEXAMProject/ToursWebAppEXAMProject/wwwroot", "");
            return relativePath;
        }

        public static void DeletePhoto(string fullPathToFile)
        {
            File.Delete(fullPathToFile);
        }
    }
}
