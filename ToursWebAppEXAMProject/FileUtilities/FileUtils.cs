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
        public static async void SaveFileIsExistPath(string filePath, IFormFile? changeImagePath)
        {
            using (var fstream = new FileStream(filePath, FileMode.Create))
            {
                await changeImagePath.CopyToAsync(fstream);

                WriteLogs($"Новая картинка сохранена по пути: {filePath}\n", NLogsModeEnum.Debug);
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
    }
}
