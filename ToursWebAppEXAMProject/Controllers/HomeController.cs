using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		/// <summary>
		/// Метод GetProduct(int id), кот. возвращает данные одного туристического тура
		/// </summary>
		/// <param name="id">id тура</param>
		/// <returns></returns>
		public IActionResult GetProduct(int id)
		{
			var productRepository = new ProductsRepository(new TourFirmaDBContext());

			var product = productRepository.GetProduct(id);

			if(product.Id == 0) return View("Error", id);

			return View(product);

		}
		/// <summary>
		/// Метод GetAllProducts(), кот. возвращает данные всех туристических продуктов из БД
		/// </summary>
		/// <returns></returns>
		public IActionResult GetAllProducts()
		{
			var productRepository = new ProductsRepository(new TourFirmaDBContext());

			var products = productRepository.GetAllProducts().ToList<Product>();

			if (products == null) return View("Error");

			return View(products);
		}
	}
}