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
		public IActionResult EditMenuNews()
		{
			logger.Trace("Переход по маршруту /Admin/EditMenu. Возвращено представление Admin/EditMenuNews.cshtml\n");
			Console.WriteLine("Переход по маршруту /Admin/EditMenu. Возвращено представление Admin/EditMenuNews.cshtml\n");
			
			return View();
		}
		
		[HttpGet]
		public IActionResult EditMenuBlogs()
		{
			logger.Trace("Переход по маршруту /Admin/EditMenu. Возвращено представление Admin/EditMenuBlogs.cshtml\n");
			Console.WriteLine("Переход по маршруту /Admin/EditMenu. Возвращено представление Admin/EditMenuBlogs.cshtml\n");

			return View();
		}
		
		[HttpGet]
		public IActionResult EditMenuProducts()
		{
			logger.Trace("Переход по маршруту /Admin/EditMenu. Возвращено представление Admin/EditMenuProducts.cshtml\n");
			Console.WriteLine("Переход по маршруту /Admin/EditMenu. Возвращено представление Admin/EditMenuProducts.cshtml\n");

			return View();
		}

		[HttpGet]
		public IActionResult GetQueryResultEntities(bool isFullName, string fullNameOrKeywordOfItem, string type)
		{
			logger.Trace("Переход по маршруту /Admin/GetQueryResultItems");
			Console.WriteLine("Переход по маршруту /Admin/GetQueryResultItems");

			// реализовано switch(type) для выборки items по типам (New, Blog, Product)
			object items = new object();
			
			switch (type)
			{
				case "New":
					items = DataManager.NewBaseInterface.GetQueryResultItems(fullNameOrKeywordOfItem, isFullName);
					break;
				case "Blog":
					items = DataManager.BlogBaseInterface.GetQueryResultItems(fullNameOrKeywordOfItem, isFullName);
					break;
				case "Product":
					items = DataManager.ProductBaseInterface.GetQueryResultItems(fullNameOrKeywordOfItem, isFullName);
					break;
			}

			if (items == null)
			{
				logger.Warn("Возвращено представление /Error.cshtml\n");
				Console.WriteLine("Возвращено представление /Error.cshtml\n");

				// задаем входные параметры для объекта ErrorViewModel
				// можно передать: modelType, id - обязательные параметры, message - опционально;
				// message = "";
				var errorInfo = new ErrorViewModel(typeof(List<New>));
				return View("Error", errorInfo);
			}

			logger.Debug("Возвращено представление /Admin/GetQueryResultItems.cshtml\n");
			Console.WriteLine("Возвращено представление /Admin/GetQueryResultItems.cshtml\n");
			return View(items);
		}
	}
}
