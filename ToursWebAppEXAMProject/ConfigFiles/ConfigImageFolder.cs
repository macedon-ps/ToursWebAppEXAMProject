using ToursWebAppEXAMProject.Enums;

namespace ToursWebAppEXAMProject.ConfigFiles
{
    /// <summary>
    /// Реализует конфигурацию для папок сохранения файлов изображений в проекте.
    /// </summary>
    public static class ConfigImageFolder
    {
        /// <summary>
        /// Сопоставляет типы папок с изображениями с соответствующими относительными путями к каталогам.
        /// </summary>
        private static readonly Dictionary<ImageFolder, string> _paths =
            new()
            {
                { ImageFolder.News, "images/NewsTitleImages" },
                { ImageFolder.Blogs, "images/BlogsTitleImages" },
                { ImageFolder.Products, "images/ProductsTitleImages" },
                { ImageFolder.Countries, "images/CountriesTitleImages" },
                { ImageFolder.Cities, "images/CitiesTitleImages" }
            };

        /// <summary>
        /// Получает путь к файловой системе, связанный с указанной папкой с образом.
        /// </summary>
        /// <param name="folder">Папка с изображением, для которой нужно получить путь.</param>
        /// <returns>Путь в файловой системе, соответствующий указанной папке с изображением.</returns>
        public static string GetPath(ImageFolder folder)
        {
            return _paths[folder];
        }
    }
}
