using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.EnumsDictionaries;
using ToursWebAppEXAMProject.Repositories;
using ToursWebAppEXAMProject.ViewModels;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
	public class SupportController : Controller
	{
		private readonly DataManager DataManager;

		public SupportController(DataManager DataManager)
		{
			this.DataManager = DataManager;
		}

		public IActionResult Index()
		{
			WriteLogs("Переход по маршруту /Support/Index.\n", NLogsModeEnum.Trace);
						
			return View();
		}
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

		public IActionResult TechTaskSupport()
		{
            WriteLogs("Переход по маршруту Support/TechTaskSupport.\n", NLogsModeEnum.Trace);
            
			var pageName = "Support";
			var model = DataManager.TechTaskInterface.GetTechTasksForPage(pageName);

			return View(model);
		}

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

				DataManager.TechTaskInterface.SaveProgressTechTasks(model);

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
