using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.ViewModels;
using TourWebAppEXAMProject.Utils;
using static TourWebAppEXAMProject.Services.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
    public class SupportController : Controller
	{
        private readonly SupportUtils _SupportUtils;
        private readonly TechTaskUtils _TechTaskUtils;
        
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
			WriteLogs("Переход по маршруту /Support/Index.\n", NLogsModeEnum.Trace);

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
           
                return View(viewModel);
            }
            catch (Exception ex)
            {
                var error = new ErrorViewModel(ex.Message);

                return View("Error", error);
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
                var newViewModel = _SupportUtils.GetModel(viewModel, formValues);
                
                return View(viewModel);
            }
            catch (Exception ex)
            {
                var error = new ErrorViewModel(ex.Message);
                return View("Error", error);
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

            ViewData["service"] = service;
			if (!(service == "map" | service == "translate" | service == "weatherForecast"))
			{
				var errorInfo = new ErrorViewModel($"Не передано название сервиса, который должен быть реализован в методе GetSupport(string service) или сервис = \"{service}\" не существует / или не обработан");

                WriteLogs($"Не передано название сервиса, который должен быть реализован в методе GetSupport(string service) или сервис = \"{service}\" не существует / или не обработан", NLogsModeEnum.Warn);
				WriteLogs("Возвращено Error.cshtml\n", NLogsModeEnum.Trace);
				
				return View("Error", errorInfo);
			}

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
            WriteLogs("Переход по маршруту Support/TechTaskSupport.\n", NLogsModeEnum.Trace);
            
			var model = _TechTaskUtils.GetTechTaskForPage("Support");
            
			return View(model);
		}

        /// <summary>
        /// Метод редактирования и сохранения данных о прогресса его выполнения ТЗ для страницы Support
        /// </summary>
        /// <param name="model">Данные с формы для ТЗ и прогресса его выполнени</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        [HttpPost]
		public IActionResult TechTaskSupport(TechTaskViewModel model)
		{
            WriteLogs("Сохранение выполнения ТЗ в БД. ", NLogsModeEnum.Debug);
            
			if (ModelState.IsValid)
			{
                WriteLogs("TechTaskViewModel прошла валидацию", NLogsModeEnum.Debug);
                
				_TechTaskUtils.SetTechTaskProgressAndSave(model);

                WriteLogs("Показатели выполнения ТЗ сохранены. ", NLogsModeEnum.Debug);
                WriteLogs("Возвращено /Support/TechTaskSupport.cshtml\n", NLogsModeEnum.Trace);
                				
				return View(model);
			}
            WriteLogs("TechTaskViewModel не прошла валидацию. Показатели выполнения ТЗ не сохранены. ", NLogsModeEnum.Warn);
            WriteLogs("Возвращено /Support/TechTaskSupport.cshtml\n", NLogsModeEnum.Trace);
            
			return View(model);
		}
	}
}
