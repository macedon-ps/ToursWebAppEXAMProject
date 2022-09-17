using Microsoft.AspNetCore.Mvc;
using NLog;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Repositories;

namespace ToursWebAppEXAMProject.Controllers
{
	public class AdminController : Controller
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		private readonly DataManager DataManager;

		public AdminController(DataManager DataManager)
		{
			this.DataManager = DataManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			logger.Trace("Переход по маршруту /Admin/Index. Возвращено представление Admin/Index.cshtml\n");
			Console.WriteLine("Переход по маршруту /Admin/Index. Возвращено представление Admin/Index.cshtml\n");
			
			return View();
		}

		[HttpGet]
		public IActionResult EditMenu()
		{
			logger.Trace("Переход по маршруту /Admin/EditMenu. Возвращено представление Admin/EditMenu.cshtml\n");
			Console.WriteLine("Переход по маршруту /Admin/EditMenu. Возвращено представление Admin/EditMenu.cshtml\n");
			
			return View();
		}

		[HttpGet]
		public IActionResult GetQueryResultNews(bool isFullName, string fullNameOrKeywordOfItem)
		{
			logger.Trace("Переход по маршруту /Admin/GetQueryResultNews");
			Console.WriteLine("Переход по маршруту /Admin/GetQueryResultNews");

			var news = DataManager.NewBaseInterface.GetQueryResultItems(fullNameOrKeywordOfItem, isFullName);

			if (news == null)
			{
				logger.Warn("Возвращено представление /Error.cshtml\n");
				Console.WriteLine("Возвращено представление /Error.cshtml\n");

				// задаем входные параметры для объекта ErrorViewModel
				// можно передать: modelType, id - обязательные параметры, message - опционально;
				// message = "";
				var errorInfo = new ErrorViewModel(typeof(List<New>));
				return View("Error", errorInfo);
			}

			logger.Debug("Возвращено представление /Admin/GetQueryResultNews.cshtml\n");
			Console.WriteLine("Возвращено представление /Admin/GetQueryResultNews.cshtml\n");
			return View(news);
		}
	}
}
