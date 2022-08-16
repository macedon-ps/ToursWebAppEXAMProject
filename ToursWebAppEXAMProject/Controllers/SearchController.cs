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

		/// <summary>
		/// Метод Index(), кот. выводит дефолтный вид страницы Search
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			logger.Trace("Переход по маршруту /Search/Index. Возвращено представление Search/Index.cshtml");
			Console.WriteLine("Переход по маршруту /Search/Index. Возвращено представление Search/Index.cshtml");
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
			var productRepository = new ProductsRepository(new TourFirmaDBContext());

			var product = productRepository.GetProduct(id);
			logger.Debug($"Вывод турпродукта с id={id}. Возвращено представление /Search/GetProduct.cshtml");
			Console.WriteLine($"Вывод турпродукта с id={id}. Возвращено представление /Search/GetProduct.cshtml");

			if (product.Id == 0)
			{
				logger.Warn($"Отсутствие данных о турподукте с id={id}. Возвращено представление /Search/Error.cshtml");
				Console.WriteLine($"Отсутствие данных о турподукте с id={id}. Возвращеноя представление /Search/Error.cshtml");
				ViewData["id"] = id;
				return View("Error");
			}
			
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
			var productRepository = new ProductsRepository(new TourFirmaDBContext());

			var products = productRepository.GetAllProducts().ToList<Product>();
			logger.Debug("Вывод всех турпродуктов. Возвращено представление /Search/GetAllProducts.cshtml");
			Console.WriteLine("Вывод всех турпродуктов. Возвращено представление /Search/GetAllProducts.cshtml");

			if (products == null)
			{
				logger.Warn("Отсутствие данных о турподуктах. Возвращено представление /Search/Error.cshtml");
				Console.WriteLine("Отсутствие данных о турподуктах. Возвращено представление /Search/Error.cshtml");
				return View("Error");
			}
			
			return View(products);
		}
		
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			logger.Trace($"Переход по маршруту /Search/Error");
			Console.WriteLine($"Переход по маршруту /Search/Error");
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
