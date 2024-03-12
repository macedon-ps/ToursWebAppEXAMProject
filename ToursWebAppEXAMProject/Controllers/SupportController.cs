using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ToursWebAppEXAMProject.ViewModels;
using ToursWebAppEXAMProject.Utils;

namespace ToursWebAppEXAMProject.Controllers
{
    public class SupportController : Controller
	{
        private readonly SupportUtils _SupportUtils;
        private readonly TechTaskUtils _TechTaskUtils;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public SupportController(SupportUtils SupportUtils, TechTaskUtils TechTaskUtils)
		{
            _SupportUtils = SupportUtils;
            _TechTaskUtils = TechTaskUtils;
        }

        /// <summary>
        /// Метод вывода стартовой страницы Support
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
		{
			_logger.Trace("Переход по маршруту /Support/Index.\n");
            return View();
		}

        [HttpGet]
        /// <summary>
        /// Метод вывода страницы для перевода текста
        /// </summary>
        /// <returns></returns>
        public IActionResult Translate()
		{
            try
            {
                var viewModel = _SupportUtils.GetModel();
                _logger.Debug("Получена вью-модель TranslateTextViewModel. ");

                _logger.Trace("Переход по маршруту /Support/Index.\n");
                return View(viewModel);
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);

                _logger.Trace("Возвращено ../Shared/Error.cshtml\n");
                return View("Error", new ErrorViewModel(error.Message));
            }
            
        }
        
        /// <summary>
        /// Метод вывода страницы для перевода текста с данными перевода
        /// </summary>
        /// <param name="viewModel">вью-модель перевода текста</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Translate(TranslateTextViewModel viewModel, IFormCollection formValues)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Debug("Вью-модель TranslateTextViewModel прошла валидацию. ");
                    
                    var newViewModel = _SupportUtils.GetModel(viewModel, formValues);
                    _logger.Debug("Вью-модель TranslateTextViewModel заполнена данными из формы. ");

                    _logger.Trace("Переход по маршруту /Support/Translate.\n");
                    return View(viewModel);
                }
                else
                {
                    _logger.Warn("Вью-модель TranslateTextViewModel не прошла валидацию. Данные модели не сохранены. ");

                    _logger.Trace("Возвращено /Support/Translate.cshtml\n");
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

        /// <summary>
        /// Метод предоставления услуги поддержки
        /// </summary>
        /// <param name="service">Услуга поддержки</param>
        /// <returns></returns>
        public IActionResult GetSupport(string service)
		{
            // TODO: разработать сервисы "map" и/или "weatherForecast"
            /*
            ViewData["service"] = service;
			if (!(service == "map" | service == "translate" | service == "weatherForecast"))
			{
				var errorInfo = new ErrorViewModel($"Не передано название сервиса, который должен быть реализован в методе GetSupport(string service) или сервис = \"{service}\" не существует / или не обработан");

                _logger.Warn($"Не передано название сервиса, который должен быть реализован в методе GetSupport(string service) или сервис = \"{service}\" не существует / или не обработан");
				_logger.Trace("Возвращено Error.cshtml\n");
				
				return View("Error", errorInfo);
			}
            */
            /*
            if (service == "map")
            {
                return View("Map");
            }
            else if (service == "translate")
			{
				return View("Translate");
			}
            else if (service == "weatherForecast")
            {
                return View("WeatherForecast");
            }
			*/
            // WriteLogs($"Переход по маршруту /Support/GetSupport?service={service}.\n");
            
            var serviceItem = service;
			return View("GetSupport", serviceItem);
		}

        /// <summary>
        /// Метод вывода ТЗ и прогресса его выполнения для страницы Support
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        public IActionResult TechTaskSupport()
		{
			var viewModel = _TechTaskUtils.GetTechTaskForPage("Support");
            _logger.Debug("Получена вью-модель TechTaskViewModel. ");

            _logger.Trace("Переход по маршруту Support/TechTaskSupport.\n");
            return View(viewModel);
		}

        /// <summary>
        /// Метод редактирования и сохранения данных о прогресса его выполнения ТЗ для страницы Support
        /// </summary>
        /// <param name="model">Данные с формы для ТЗ и прогресса его выполнени</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        [HttpPost]
		public IActionResult TechTaskSupport(TechTaskViewModel viewModel)
		{
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Debug("Вью-модель TechTaskViewModel прошла валидацию. ");

                    _TechTaskUtils.SetTechTaskProgressAndSave(viewModel);
                    _logger.Debug("Вью-модель TechTaskViewModel заполнена данными и сохранена. ");

                    _logger.Trace("Возвращено /Support/TechTaskHome.cshtml\n");
                    return View(viewModel);
                }
                else
                {
                    _logger.Warn("Вью-модель TechTaskViewModel не прошла валидацию. Данные модели не сохранены. ");

                    _logger.Trace("Возвращено /Support/TechTaskHome.cshtml\n");
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
