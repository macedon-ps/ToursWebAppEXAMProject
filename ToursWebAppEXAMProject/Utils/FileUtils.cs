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

       
        public void DeletePhoto(string fullPathToFile)
        {
            File.Delete(fullPathToFile);
        }
    }
}
