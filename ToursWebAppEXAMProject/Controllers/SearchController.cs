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

			var product = DataManager.productBaseInterface.GetItemById(id);
			
			if (product.Id == 0)
			{
				logger.Warn($"Отсутствие данных о турподукте с id={id}. Возвращено представление /Search/Error.cshtml\n");
				Console.WriteLine($"Отсутствие данных о турподукте с id={id}. Возвращеноя представление /Search/Error.cshtml\n");
				ViewData["id"] = id;
				return View("Error");
			}

			logger.Debug($"Вывод турпродукта с id={id}. Возвращено представление /Search/GetProduct.cshtml\n");
			Console.WriteLine($"Вывод турпродукта с id={id}. Возвращено представление /Search/GetProduct.cshtml\n");
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

			var products = DataManager.productBaseInterface.GetAllItems();
			
			if (products == null)
			{
				logger.Warn("Отсутствие данных о турподуктах. Возвращено представление /Search/Error.cshtml\n");
				Console.WriteLine("Отсутствие данных о турподуктах. Возвращено представление /Search/Error.cshtml\n");
				return View("Error");
			}
			
			logger.Debug("Вывод всех турпродуктов. Возвращено представление /Search/GetAllProducts.cshtml\n");
			Console.WriteLine("Вывод всех турпродуктов. Возвращено представление /Search/GetAllProducts.cshtml\n");
			return View(products);
		}
		
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			logger.Trace($"Переход по маршруту /Search/Error\n");
			Console.WriteLine($"Переход по маршруту /Search/Error\n");
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
