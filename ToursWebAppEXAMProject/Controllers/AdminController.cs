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
		public IActionResult EditMenu(string type)
		{
			logger.Trace("Переход по маршруту /Admin/EditMenu. Возвращено представление Admin/EditMenu.cshtml\n");
			Console.WriteLine("Переход по маршруту /Admin/EditMenu. Возвращено представление Admin/EditMenu.cshtml\n");
			var model = new EditMenuViewModel(true, "", type);

			return View(model);
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

		[HttpGet]
		public IActionResult EditItem(string type, int id)
		{
			object model = new object();
			switch (type)
			{
				case "ToursWebAppEXAMProject.Models.New":
					model = DataManager.NewBaseInterface.GetItemById(id);
					return View("EditItemNew", model);
				case "ToursWebAppEXAMProject.Models.Blog":
					model = DataManager.BlogBaseInterface.GetItemById(id);
					return View("EditItemBlog", model);
				case "ToursWebAppEXAMProject.Models.Product":
					model = DataManager.ProductBaseInterface.GetItemById(id);
					return View("EditItemProduct", model);
			}
			var message = "Ошибка обработки метода EditItem()";
			var errorInfo = new ErrorViewModel(typeof(Object), id, message);
			return View("Error", errorInfo);
		}

        [HttpPost]
		public IActionResult EditItem(Object model)
        {
			// TODO: реализоывть сохранение данных формы
			return View(model);
        }
	}
}
