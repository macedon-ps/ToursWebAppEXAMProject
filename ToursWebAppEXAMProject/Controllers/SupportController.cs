using Microsoft.AspNetCore.Mvc;
using NLog;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Repositories;

namespace ToursWebAppEXAMProject.Controllers
{
	public class SupportController : Controller
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		private readonly DataManager DataManager;

		public SupportController(DataManager DataManager)
		{
			this.DataManager = DataManager;
		}

		public IActionResult Index()
		{
			logger.Trace("Переход по маршруту /Support/Index. Возвращено представление Support/Index.cshtml\n");
			Console.WriteLine("Переход по маршруту /Support/Index. Возвращено представление Support/Index.cshtml\n");
			
			return View();
		}
		public IActionResult GetSupport(string service)
		{
			logger.Trace("Переход по маршруту /Support/Index. Возвращено представление Support/Index.cshtml\n");
			Console.WriteLine("Переход по маршруту /Support/Index. Возвращено представление Support/Index.cshtml\n");

			var serviceItem = service;
			return View("GetSupport", serviceItem);
		}

		public IActionResult TechTaskSupport()
		{
			logger.Trace("Переход по маршруту Support/TechTaskSupport. Возвращаено представление Support/TechTaskSupport.cshtml\n");
			Console.WriteLine("Переход по маршруту Support/TechTaskSupport. Возвращаено представление Support/TechTaskSupport.cshtml\n");

			var pageName = "Support";
			var model = DataManager.TechTaskInterface.GetTechTasksForPage(pageName);

			return View(model);
		}

		[HttpPost]
		public IActionResult TechTaskSupport(TechTaskViewModel model)
		{
			logger.Debug("Запущен процесс сохранения показателей выполнения тех. задания в БД");
			Console.WriteLine("Запущен процесс сохранения показателей выполнения тех. задания в БД");

			if (ModelState.IsValid)
			{
				logger.Debug("Модель TechTaskViewModel успешно прошла валидацию");
				Console.WriteLine("Модель TechTaskViewModel успешно прошла валидацию");

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
				logger.Debug("Показатели выполнения тех. задания успешно сохранены в БД");
				Console.WriteLine("Показатели выполнения тех. задания успешно сохранены в БД");
				logger.Debug("Возвращено представление /Support/TechTaskSupport.cshtml\n");
				Console.WriteLine("Возвращено представление /Support/TechTaskSupport.cshtml\n");
				
				return View(model);
			}
			logger.Debug("Модель TechTaskViewModel не прошла валидацию");
			Console.WriteLine("Модель TechTaskViewModel не прошла валидацию");
			logger.Debug("Возвращено представление /Support/TechTaskSupport.cshtml\n");
			Console.WriteLine("Возвращено представление /Support/TechTaskSupport.cshtml\n");

			return View(model);
		}
	}
}
