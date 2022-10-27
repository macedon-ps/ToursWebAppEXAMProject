using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Diagnostics;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Repositories;

namespace ToursWebAppEXAMProject.Controllers
{
	public class SearchController: Controller
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		private readonly DataManager DataManager;

		public SearchController(DataManager DataManager)
		{
			this.DataManager = DataManager;
		}

		/// <summary>
		/// Метод Index(), кот. выводит дефолтный вид страницы Search
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			logger.Trace("Переход по маршруту /Search/Index. Возвращено представление Search/Index.cshtml\n");
			Console.WriteLine("Переход по маршруту /Search/Index. Возвращено представление Search/Index.cshtml\n");
			return View(DataManager);
		}

		/// <summary>
		/// Метод GetProduct(), кот. возвращает данные турпродукта по его id
		/// </summary>
		/// <param name="id">уникальный идентификатор турпродукта</param>
		/// <returns></returns>
		public IActionResult GetProduct(int id)
		{
			logger.Trace($"Переход по маршруту /Search/GetProduct?id={id}");
			Console.WriteLine($"Переход по маршруту /Search/GetProduct?id={id}");

			var product = DataManager.ProductBaseInterface.GetItemById(id);
			
			if (product.Id == 0)
			{
				logger.Warn($"Возвращено представление /Error.cshtml\n");
				Console.WriteLine($"Возвращеноя представление /Error.cshtml\n");

				// задаем входные параметры для объекта ErrorViewModel
				// можно передать: modelType, id - обязательные параметры, message - опционально;
				// message = "";
				var errorInfo = new ErrorViewModel(typeof(Product), id);
				return View("Error", errorInfo);
			}

			logger.Debug($"Возвращено представление /Search/GetProduct.cshtml\n");
			Console.WriteLine($"Возвращено представление /Search/GetProduct.cshtml\n");
			return View(product);
		}

		/// <summary>
		/// Метод GetAllProducts(), кот. возвращает данные всех туристических продуктов из БД
		/// </summary>
		/// <returns></returns>
		public IActionResult GetAllProducts()
		{
			logger.Trace("Переход по маршруту /Search/GetAllProducts");
			Console.WriteLine("Переход по маршруту /Search/GetAllProducts");

			var products = DataManager.ProductBaseInterface.GetAllItems();
			
			if (products == null)
			{
				logger.Warn("Возвращено представление /Error.cshtml\n");
				Console.WriteLine("Возвращено представление /Error.cshtml\n");

				// задаем входные параметры для объекта ErrorViewModel
				// можно передать: modelType, id - обязательные параметры, message - опционально;
				// message = "";
				var errorInfo = new ErrorViewModel(typeof(List<Product>));
				return View("Error", errorInfo);
			}
			
			logger.Debug("Возвращено представление /Search/GetAllProducts.cshtml\n");
			Console.WriteLine("Возвращено представление /Search/GetAllProducts.cshtml\n");
			return View(products);
		}

		/// <summary>
		/// Метод GetQueryResultProducts(), кот. возвращает данные некоторых туристических продуктов из БД по имени / ключевому слову
		/// </summary>
		/// <returns></returns>
		public IActionResult GetQueryResultProducts(string keyword, bool isFullName)
		{
			logger.Trace("Переход по маршруту /Search/GetAllProducts");
			Console.WriteLine("Переход по маршруту /Search/GetAllProducts");

			var products = DataManager.ProductBaseInterface.GetQueryResultItems(keyword, isFullName);

			if (products == null)
			{
				logger.Warn("Возвращено представление /Error.cshtml\n");
				Console.WriteLine("Возвращено представление /Error.cshtml\n");

				// задаем входные параметры для объекта ErrorViewModel
				// можно передать: modelType, id - обязательные параметры, message - опционально;
				// message = "";
				var errorInfo = new ErrorViewModel(typeof(List<Product>));
				return View("Error", errorInfo);
			}

			logger.Debug("Возвращено представление /Search/GetAllProducts.cshtml\n");
			Console.WriteLine("Возвращено представление /Search/GetAllProducts.cshtml\n");
			return View(products);
		}

		public IActionResult TechTaskSearch()
		{
			logger.Trace("Переход по маршруту Search/TechTaskSearch. Возвращаено представление Search/TechTaskSearch.cshtml\n");
			Console.WriteLine("Переход по маршруту Search/TechTaskSearch. Возвращаено представление Search/TechTaskSearch.cshtml\n");

			var pageName = "Search";
			var model = DataManager.TechTaskInterface.GetTechTasksForPage(pageName);

			return View(model);
		}

		[HttpPost]
		public IActionResult TechTaskSearch(TechTaskViewModel model)
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
				logger.Debug("Возвращено представление /Search/TechTaskSearch.cshtml\n");
				Console.WriteLine("Возвращено представление /Search/TechTaskSearch.cshtml\n");
				
				return View(model);
			}
			logger.Debug("Модель TechTaskViewModel не прошла валидацию");
			Console.WriteLine("Модель TechTaskViewModel не прошла валидацию");
			logger.Debug("Возвращено представление /Search/TechTaskSearch.cshtml\n");
			Console.WriteLine("Возвращено представление /Search/TechTaskSearch.cshtml\n");

			return View(model);
		}
	}
}
