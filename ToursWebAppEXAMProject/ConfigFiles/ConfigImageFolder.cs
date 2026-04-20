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
                { ImageFolder.Cities, "images/CitiesTitleImages" },
                { ImageFolder.About_Main, "images/AboutPage/Main/" },
                { ImageFolder.About_About, "images/AboutPage/About/" },
                { ImageFolder.About_Details, "images/AboutPage/Details/" },
                { ImageFolder.About_OperationMode, "images/AboutPage/OperationMode/" },
                { ImageFolder.About_PhotoGallery, "images/AboutPage/PhotoGallery/" },
                { ImageFolder.About_PhotoGallery_Collection, "images/AboutPage/PhotoGallery_Collection/" },
                { ImageFolder.About_Feedback, "images/AboutPage/Feedback/" }
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
