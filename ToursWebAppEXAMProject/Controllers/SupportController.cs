using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.EnumsDictionaries;
using ToursWebAppEXAMProject.GoogleApiClients;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.ViewModels;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
    public class SupportController : Controller
	{
        private readonly IEditTechTaskInterface _AllTasks;

        public SupportController(IEditTechTaskInterface Tasks)
		{
            this._AllTasks = Tasks;
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
            var viewModel = new TranslateTextViewModel();
            // TODO: уменьшить число запросов. Возможно, кешировать данные
            viewModel.LanguagesList = ClientGoogleTranslate.GetAllLanguages();
            return View(viewModel);
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
                if (viewModel.TextOrigin != null)
                {
                    viewModel.LanguageFrom = formValues["langFromSelect"];
                    viewModel.LanguageTo = formValues["langToSelect"];
                    // TODO: уменьшить число запросов. Возможно, кешировать данные
                    viewModel.LanguagesList = ClientGoogleTranslate.GetAllLanguages();

                    var translateText = ClientGoogleTranslate.TranslateText(viewModel.TextOrigin, viewModel.LanguageTo, viewModel.LanguageFrom);
                    if (translateText != null)
                    {
                        viewModel.TextTranslated = translateText;
                    }
                }
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
            // TODO: разработать сервисы "map", "translate", "mobileApp"
            
			ViewData["service"] = service;
			if (!(service == "map" | service == "translate" | service == "mobileApp"))
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
            else if (service == "mobileApp")
            {
                return View("MobileApp");
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
        public IActionResult TechTaskSupport()
		{
            WriteLogs("Переход по маршруту Support/TechTaskSupport.\n", NLogsModeEnum.Trace);
            
			var pageName = "Support";
			var model = _AllTasks.GetTechTasksForPage(pageName);

			return View(model);
		}

        /// <summary>
        /// Метод редактирования и сохранения данных о прогресса его выполнения ТЗ для страницы Support
        /// </summary>
        /// <param name="model">Данные с формы для ТЗ и прогресса его выполнени</param>
        /// <returns></returns>
        [HttpPost]
		public IActionResult TechTaskSupport(TechTaskViewModel model)
		{
            WriteLogs("Сохранение выполнения ТЗ в БД. ", NLogsModeEnum.Debug);
            
			if (ModelState.IsValid)
			{
                WriteLogs("TechTaskViewModel прошла валидацию", NLogsModeEnum.Debug);
                
				double TechTasksCount = 6;
				double TechTasksTrueCount = 0;
				if (model.IsExecuteTechTask1 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask2 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask3 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask4 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask5 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask6 == true) TechTasksTrueCount++;

				double ExecuteTechTasksProgress = Math.Round((TechTasksTrueCount / TechTasksCount) * 100);
				model.ExecuteTechTasksProgress = ExecuteTechTasksProgress;

				_AllTasks.SaveProgressTechTasks(model);

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
