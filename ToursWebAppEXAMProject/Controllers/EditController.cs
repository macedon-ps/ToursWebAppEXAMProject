using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ToursWebAppEXAMProject.ViewModels;
using ToursWebAppEXAMProject.Utils;

namespace ToursWebAppEXAMProject.Controllers
{
    [Authorize]
    public class EditController : Controller
    {
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        	     
        /// <summary>
        /// Метод вывода стартовой страницы Edit
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		public IActionResult Index(string type = "New")
		{
            var viewModel = new EditMenuViewModel(false, "", type);
            _logger.Debug("Получена вью-модель EditMenuViewModel с дефолтными параметрами. ");

            _logger.Trace("Переход по маршруту /Edit/Index.\n");
            return View(viewModel);
		}
	}
}
