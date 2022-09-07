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
			return View();
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
				var errorInfo = ErrorViewModel.GetErrorInfo(typeof(Product), id);
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
				var errorInfo = ErrorViewModel.GetErrorInfo(typeof(List<Product>));
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
				var errorInfo = ErrorViewModel.GetErrorInfo(typeof(List<Product>));
				return View("Error", errorInfo);
			}

			logger.Debug("Возвращено представление /Search/GetAllProducts.cshtml\n");
			Console.WriteLine("Возвращено представление /Search/GetAllProducts.cshtml\n");
			return View(products);
		}
	}
}
