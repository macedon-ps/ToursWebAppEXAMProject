using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Controllers
{
    [Authorize]
    public class EditController : Controller
    {
        private readonly ILogger<EditController> _logger;

        public EditController(ILogger<EditController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Метод вывода стартовой страницы Edit
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		public IActionResult Index(string type = "New")
		{
            var viewModel = new EditMenuViewModel(false, "", type);
            _logger.LogDebug("Получена вью-модель EditMenuViewModel с дефолтными параметрами. ");

            _logger.LogTrace("Переход по маршруту /Edit/Index.\n");
            return View(viewModel);
		}
	}
}
