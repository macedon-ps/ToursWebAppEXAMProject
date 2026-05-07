using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToursWebAppEXAMProject.Utils;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Controllers
{
    public class SupportController : Controller
	{
        private readonly SupportUtils _SupportUtils;
        private readonly ILogger<SupportController> _logger;

        public SupportController(SupportUtils SupportUtils, ILogger<SupportController> logger)
		{
            _SupportUtils = SupportUtils;
            _logger = logger;
        }


        /// <summary>
        /// Метод вывода стартовой страницы Support
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
		{
			_logger.LogTrace("Переход по маршруту /Support/Index.\n");
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
                _logger.LogDebug("Получена вью-модель TranslateTextViewModel. ");

                _logger.LogTrace("Переход по маршруту /Support/Index.\n");
                return View(viewModel);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Ошибка при получении вью-модели TranslateTextViewModel.");
                _logger.LogTrace("Возвращено ../Shared/Error.cshtml\n");

                var errorInfo = new ErrorViewModel() 
                { 
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                
                return View("Error", errorInfo);
            }
        }
        

        /// <summary>
        /// Метод вывода страницы для перевода текста с данными перевода
        /// </summary>
        /// <param name="viewModel">вью-модель перевода текста</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Translate(TranslateTextViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogDebug("Вью-модель TranslateTextViewModel прошла валидацию. ");
                    
                    var newViewModel = _SupportUtils.GetModel(viewModel);
                    _logger.LogDebug("Вью-модель TranslateTextViewModel заполнена данными из формы. ");

                    _logger.LogTrace("Переход по маршруту /Support/Translate.\n");
                    return View(viewModel);
                }
                else
                {
                    _logger.LogWarning("Вью-модель TranslateTextViewModel не прошла валидацию. Данные модели не сохранены. ");
                    _logger.LogTrace("Возвращено /Support/Translate.cshtml\n");
                    
                    return View(viewModel);
                }
                    
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Ошибка при обработке вью-модели TranslateTextViewModel.");
                _logger.LogTrace("Возвращено ../Shared/Error.cshtml\n");

                var errorInfo = new ErrorViewModel()
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };

                return View("Error", errorInfo);
            }
        }


        public IActionResult GetMap()
        {
            return View("OdessaStepMap");
        }


        public IActionResult GetMap2()
        {
            return View("KhersonStepMap");
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
                        
            var serviceItem = service;
			return View("GetSupport", serviceItem);
		}

	}
}
