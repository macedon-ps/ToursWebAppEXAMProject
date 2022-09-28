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
			logger.Trace("Переход по маршруту /Admin/EditItem");
			Console.WriteLine("Переход по маршруту /Admin/EditItem");

			object model = new object();
			switch (type)
			{
				case "ToursWebAppEXAMProject.Models.New":
					model = DataManager.NewBaseInterface.GetItemById(id);
					logger.Debug("Возвращено представление /Admin/EditItemNew.cshtml\n");
					Console.WriteLine("Возвращено представление /Admin/EditItemNew.cshtml\n");
					return View("EditItemNew", model);

				case "ToursWebAppEXAMProject.Models.Blog":
					model = DataManager.BlogBaseInterface.GetItemById(id);
					logger.Debug("Возвращено представление /Admin/EditItemBlog.cshtml\n");
					Console.WriteLine("Возвращено представление /Admin/EditItemBlog.cshtml\n");
					return View("EditItemBlog", model);

				case "ToursWebAppEXAMProject.Models.Product":
					model = DataManager.ProductBaseInterface.GetItemById(id);
					logger.Debug("Возвращено представление /Admin/EditItemProduct.cshtml\n");
					Console.WriteLine("Возвращено представление /Admin/EditItemProduct.cshtml\n");
					return View("EditItemProduct", model);
			}

			logger.Debug("Возвращено представление /Admin/Index.cshtml\n");
			Console.WriteLine("Возвращено представление /Admin/Index.cshtml\n");
			return View("Index");
		}

        [HttpPost]
		public IActionResult SaveItemNew(New model)
        {
			if (ModelState.IsValid)
			{
				logger.Debug("Модель New model прошла валидацию");
				Console.WriteLine("Модель New model прошла валидацию");
				logger.Debug("Возвращено представление /Admin/Success.cshtml\n");
				Console.WriteLine("Возвращено представление /Admin/Success.cshtml\n");
				var id = model.Id;
				DataManager.NewBaseInterface.SaveItem(model, id);
				return View("Success", model);
			}

			logger.Debug("Модель New model не прошла валидацию");
			Console.WriteLine("Модель New model не прошла валидацию");
			logger.Debug("Возвращено представление /Admin/EditItemNew.cshtml\n");
			Console.WriteLine("Возвращено представление /Admin/EditItemNew.cshtml\n");
			return View("EditItemNew", model);
		}

		[HttpPost]
		public IActionResult SaveItemBlog(Blog model)
		{
			if (ModelState.IsValid)
			{
				logger.Debug("Модель Blog model прошла валидацию");
				Console.WriteLine("Модель Blog model прошла валидацию");
				logger.Debug("Возвращено представление /Admin/Success.cshtml\n");
				Console.WriteLine("Возвращено представление /Admin/Success.cshtml\n");
				var id = model.Id;
				DataManager.BlogBaseInterface.SaveItem(model, id);
				return View("Success", model);
			}

			logger.Debug("Модель Blog model не прошла валидацию");
			Console.WriteLine("Модель Blog model не прошла валидацию");
			logger.Debug("Возвращено представление /Admin/EditItemBlog.cshtml\n");
			Console.WriteLine("Возвращено представление /Admin/EditItemBlog.cshtml\n");
			return View("EditItemBlog", model);
		}

		[HttpPost]
		public IActionResult SaveItemProduct(Product model)
		{
			if (ModelState.IsValid)
			{
				logger.Debug("Модель Product model прошла валидацию");
				Console.WriteLine("Модель Product model прошла валидацию");
				logger.Debug("Возвращено представление /Admin/Success.cshtml\n");
				Console.WriteLine("Возвращено представление /Admin/Success.cshtml\n");
				var id = model.Id;
				DataManager.ProductBaseInterface.SaveItem(model, id);
				return View("Success", model);
			}
			logger.Debug("Модель Product model не прошла валидацию");
			Console.WriteLine("Модель Product model не прошла валидацию");
			logger.Debug("Возвращено представление /Admin/EditItemProduct.cshtml\n");
			Console.WriteLine("Возвращено представление /Admin/EditItemProduct.cshtml\n");
			return View("EditItemProduct", model);
		}

		[HttpGet]
		public IActionResult Success(Object model)
		{
			logger.Trace("Переход по маршруту /Admin/Success");
			Console.WriteLine("Переход по маршруту /Admin/Success");
			return View(model);
		}

		public IActionResult CreateEntityNew(string type) 
		{
			var model = new Object();
			if (type == "New") model = new New();
			return View("EditItemNew", model); 
		}

		public IActionResult CreateEntityBlog(string type)
		{
			var model = new Object();
			if (type == "Blog") model = new Blog();
			return View("EditItemBlog", model);
		}
		public IActionResult CreateEntityProduct(string type)
		{
			var model = new Object();
			if (type == "Product") model = new Product();
			return View("EditItemProduct", model);
		}
	}
}
