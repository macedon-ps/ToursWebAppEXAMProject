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
		private readonly TechTaskUtils _TechTaskUtils;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public EditController(TechTaskUtils TechTaskUtils)
		{
			_TechTaskUtils = TechTaskUtils;
		}
		     
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

        /// <summary>
        /// Метод вывода ТЗ и прогресса его выполнения для страницы Edit
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        public IActionResult TechTaskEdit()
		{
        	var model = _TechTaskUtils.GetTechTaskForPage("Edit");
            _logger.Debug("Получена вью-модель TechTaskViewModel. ");

            _logger.Trace("Переход по маршруту Edit/TechTaskEdit.\n");
            return View(model);
		}

        /// <summary>
        /// Метод редактирования и сохранения данных о прогресса его выполнения ТЗ для страницы Edit
        /// </summary>
        /// <param name="model">Данные с формы для ТЗ и прогресса его выполнения</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        [HttpPost]
		public IActionResult TechTaskEdit(TechTaskViewModel viewModel)
		{
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Debug("Вью-модель TechTaskViewModel прошла валидацию. ");

                    _TechTaskUtils.SetTechTaskProgressAndSave(viewModel);
                    _logger.Debug("Вью-модель TechTaskViewModel заполнена данными и сохранена. ");

                    _logger.Trace("Возвращено /Edit/TechTaskHome.cshtml\n");
                    return View(viewModel);
                }
                else
                {
                    _logger.Warn("Вью-модель TechTaskViewModel не прошла валидацию. Данные модели не сохранены. ");

                    _logger.Trace("Возвращено /Edit/TechTaskHome.cshtml\n");
                    return View(viewModel);
                }
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);

                _logger.Trace("Возвращено ../Shared/Error.cshtml\n");
                return View("Error", new ErrorViewModel(error.Message));
            }
		}
	}
}
