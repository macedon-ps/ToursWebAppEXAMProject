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

		private readonly IWebHostEnvironment hostingEnvironment;

		public AdminController(DataManager DataManager, IWebHostEnvironment hostingEnvironment)
		{
			this.DataManager = DataManager;
			this.hostingEnvironment = hostingEnvironment;
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

			switch (type)
			{
				case "ToursWebAppEXAMProject.Models.New":
					var modelNew =	DataManager.NewBaseInterface.GetItemById(id);
					logger.Debug("Возвращено представление /Admin/EditItemNew.cshtml\n");
					Console.WriteLine("Возвращено представление /Admin/EditItemNew.cshtml\n");
					return View("EditItemNew", modelNew);
				
				case "ToursWebAppEXAMProject.Models.Blog":
					var modelBlog = DataManager.BlogBaseInterface.GetItemById(id);
					logger.Debug("Возвращено представление /Admin/EditItemBlog.cshtml\n");
					Console.WriteLine("Возвращено представление /Admin/EditItemBlog.cshtml\n");
					return View("EditItemBlog", modelBlog);

				case "ToursWebAppEXAMProject.Models.Product":
					var modelProduct = DataManager.ProductBaseInterface.GetItemById(id);
					logger.Debug("Возвращено представление /Admin/EditItemProduct.cshtml\n");
					Console.WriteLine("Возвращено представление /Admin/EditItemProduct.cshtml\n");
					return View("EditItemProduct", modelProduct);
			}

			logger.Debug("Возвращено представление /Admin/Index.cshtml\n");
			Console.WriteLine("Возвращено представление /Admin/Index.cshtml\n");
			return View("Index");
		}

		[HttpGet]
		public IActionResult DeleteItem(string type, int id)
		{
			switch (type)
			{
				case "ToursWebAppEXAMProject.Models.New":
					var modelNew = DataManager.NewBaseInterface.GetItemById(id);
					DataManager.NewBaseInterface.DeleteItem(modelNew, id);
					logger.Debug("Возвращено представление /Admin/SuccessForDelete.cshtml\n");
					Console.WriteLine("Возвращено представление /Admin/SuccessForDelete.cshtml\n");
					return View("SuccessForDelete", modelNew);
				
				case "ToursWebAppEXAMProject.Models.Blog":
					var modelBlog = DataManager.BlogBaseInterface.GetItemById(id);
					DataManager.BlogBaseInterface.DeleteItem(modelBlog, id);
					logger.Debug("Возвращено представление /Admin/SuccessForDelete.cshtml\n");
					Console.WriteLine("Возвращено представление /Admin/SuccessForDelete.cshtml\n");
					return View("SuccessForDelete", modelBlog);
				
				case "ToursWebAppEXAMProject.Models.Product":
					var modelProduct = DataManager.ProductBaseInterface.GetItemById(id);
					DataManager.ProductBaseInterface.DeleteItem(modelProduct, id);
					logger.Debug("Возвращено представление /Admin/SuccessForDelete.cshtml\n");
					Console.WriteLine("Возвращено представление /Admin/SuccessForDelete.cshtml\n");
					return View("SuccessForDelete", modelProduct);
			}

			logger.Debug("Возвращено представление /Admin/Index.cshtml\n");
			Console.WriteLine("Возвращено представление /Admin/Index.cshtml\n");
			return View("Index");
		}

		[HttpPost]
		public async Task<IActionResult> SaveItemNew(New model, IFormCollection formValues, IFormFile titleImagePath)
       	{
			logger.Debug("Запущен процесс сохранения новости в БД");
			Console.WriteLine("Запущен процесс сохранения новости в БД");
			
			if (ModelState.IsValid)
			{
				logger.Debug("Модель New успешно прошла валидацию");
				Console.WriteLine("Модель New успешно прошла валидацию");

				if (titleImagePath != null)
				{
					var filePath = $"/images/NewsTitleImages/{titleImagePath.FileName}";

					using (var fstream = new FileStream(hostingEnvironment.WebRootPath + filePath, FileMode.Create))
					{
						await titleImagePath.CopyToAsync(fstream);
						logger.Debug($"Титульная картинка новости успешно сохранена по пути: {filePath}");
						Console.WriteLine($"Титульная картинка новости успешно сохранена по пути: {filePath}");
					}
					
					model.FullDescription = formValues["fullInfoAboutNew"];
					model.TitleImagePath = filePath;
					
					DataManager.NewBaseInterface.SaveItem(model, model.Id);
					logger.Debug("Новость успешно сохранена в БД");
					Console.WriteLine("Новость успешно сохранена в БД");

					logger.Debug("Возвращено представление /Admin/Success.cshtml\n");
					Console.WriteLine("Возвращено представление /Admin/Success.cshtml\n");
					return View("Success", model);
				}
			}
			logger.Debug("Модель New не прошла валидацию");
			Console.WriteLine("Модель New не прошла валидацию");
						
			if(titleImagePath == null)
			{
				model.FullDescription = formValues["fullInfoAboutNew"];
				logger.Debug("Возвращено представление /Admin/EditItemNew.cshtml\n");
				Console.WriteLine("Возвращено представление /Admin/EditItemNew.cshtml\n");
				return View("EditItemNew", model);
			}

			return View("EditItemNew", model);
		}

		[HttpPost]
		public async Task<IActionResult> SaveItemBlog(Blog model, IFormCollection formValues, IFormFile titleImagePath)
		{
			logger.Debug("Запущен процесс сохранения блога в БД");
			Console.WriteLine("Запущен процесс сохранения блога в БД");

			if (ModelState.IsValid)
			{
				logger.Debug("Модель Blog успешно прошла валидацию");
				Console.WriteLine("Модель Blog успешно прошла валидацию");

				if (titleImagePath != null)
				{
					var filePath = $"/images/BlogsTitleImages/{titleImagePath.FileName}";

					using (var fstream = new FileStream(hostingEnvironment.WebRootPath + filePath, FileMode.Create))
					{
						await titleImagePath.CopyToAsync(fstream);
						logger.Debug($"Титульная картинка блога успешно сохранена по пути: {filePath}");
						Console.WriteLine($"Титульная картинка блога успешно сохранена по пути: {filePath}");
					}

					model.FullDescription = formValues["fullInfoAboutBlog"];
					model.TitleImagePath = filePath;

					DataManager.BlogBaseInterface.SaveItem(model, model.Id);
					logger.Debug("Блог успешно сохранен в БД");
					Console.WriteLine("Блог успешно сохранен в БД");

					logger.Debug("Возвращено представление /Admin/Success.cshtml\n");
					Console.WriteLine("Возвращено представление /Admin/Success.cshtml\n");
					return View("Success", model);
				}
			}
			logger.Debug("Модель Blog не прошла валидацию");
			Console.WriteLine("Модель Blog не прошла валидацию");

			if (titleImagePath == null)
			{
				model.FullDescription = formValues["fullInfoAboutBlog"];
				logger.Debug("Возвращено представление /Admin/EditItemNew.cshtml\n");
				Console.WriteLine("Возвращено представление /Admin/EditItemNew.cshtml\n");
				return View("EditItemBlog", model);
			}

			return View("EditItemBlog", model);
		}

		[HttpPost]
		public async Task<IActionResult> SaveItemProduct(Product model, IFormCollection formValues, IFormFile titleImagePath)
		{
			logger.Debug("Запущен процесс сохранения туристического продукта в БД");
			Console.WriteLine("Запущен процесс сохранения туристического продукта в БД");

			if (ModelState.IsValid)
			{
				logger.Debug("Модель Product успешно прошла валидацию");
				Console.WriteLine("Модель Product успешно прошла валидацию");

				if (titleImagePath != null)
				{
					var filePath = $"/images/ProductsTitleImages/{titleImagePath.FileName}";

					using (var fstream = new FileStream(hostingEnvironment.WebRootPath + filePath, FileMode.Create))
					{
						await titleImagePath.CopyToAsync(fstream);
						logger.Debug($"Титульная картинка туристического продукта успешно сохранена по пути: {filePath}");
						Console.WriteLine($"Титульная картинка туристического продукта успешно сохранена по пути: {filePath}");
					}

					model.FullDescription = formValues["fullInfoAboutProduct"];
					model.TitleImagePath = filePath;

					DataManager.ProductBaseInterface.SaveItem(model, model.Id);
					logger.Debug("Туристический продукт успешно сохранен в БД");
					Console.WriteLine("Туристический продукт успешно сохранен в БД");

					logger.Debug("Возвращено представление /Admin/Success.cshtml\n");
					Console.WriteLine("Возвращено представление /Admin/Success.cshtml\n");
					return View("Success", model);
				}
			}
			logger.Debug("Модель Product не прошла валидацию");
			Console.WriteLine("Модель Product не прошла валидацию");

			if (titleImagePath == null)
			{
				model.FullDescription = formValues["fullInfoAboutProduct"];
				logger.Debug("Возвращено представление /Admin/EditItemNew.cshtml\n");
				Console.WriteLine("Возвращено представление /Admin/EditItemNew.cshtml\n");
				return View("EditItemProduct", model);
			}

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
			logger.Debug("Возвращено представление /Admin/EditItemNew.cshtml\n");
			Console.WriteLine("Возвращено представление /Admin/EditItemNew.cshtml\n");
			return View("EditItemNew", model); 
		}

		public IActionResult CreateEntityBlog(string type)
		{
			var model = new Object();
			if (type == "Blog") model = new Blog();
			logger.Debug("Возвращено представление /Admin/EditItemBlog.cshtml\n");
			Console.WriteLine("Возвращено представление /Admin/EditItemBlog.cshtml\n");
			return View("EditItemBlog", model);
		}

		public IActionResult CreateEntityProduct(string type)
		{
			var model = new Object();
			if (type == "Product") model = new Product();
			logger.Debug("Возвращено представление /Admin/EditItemProduct.cshtml\n");
			Console.WriteLine("Возвращено представление /Admin/EditItemProduct.cshtml\n");
			return View("EditItemProduct", model);
		}

		public IActionResult TechTaskAdmin()
		{
			logger.Trace("Переход по маршруту Admin/TechTaskAdmin. Возвращаено представление Admin/TechTaskAdmin.cshtml\n");
			Console.WriteLine("Переход по маршруту Admin/TechTaskAdmin. Возвращаено представление Admin/TechTaskAdmin.cshtml\n");

			var pageName = "Admin";
			var model = DataManager.TechTaskInterface.GetTechTasksForPage(pageName);

			return View(model);
		}

		[HttpPost]
		public IActionResult TechTaskAdmin(TechTaskViewModel model)
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
				logger.Debug("Возвращено представление /Admin/TechTaskAdmin.cshtml\n");
				Console.WriteLine("Возвращено представление /Admin/TechTaskAdmin.cshtml\n");
				
				return View(model);
			}
			logger.Debug("Модель TechTaskViewModel не прошла валидацию");
			Console.WriteLine("Модель TechTaskViewModel не прошла валидацию");
			logger.Debug("Возвращено представление /Admin/TechTaskAdmin.cshtml\n");
			Console.WriteLine("Возвращено представление /Admin/TechTaskAdmin.cshtml\n");

			return View(model);
		}
	}
}
