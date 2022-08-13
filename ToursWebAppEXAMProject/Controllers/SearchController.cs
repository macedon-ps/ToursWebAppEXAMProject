using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Repositories;

namespace ToursWebAppEXAMProject.Controllers
{
	public class SearchController: Controller
	{
		/// <summary>
		/// Метод Index(), кот. выводит дефолтный вид страницы Search
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			
			return View();
		}

		/// <summary>
		/// Метод GetProduct(), кот. возвращает данные турпродукта по его id
		/// </summary>
		/// <param name="id">уникальный идентификатор турпродукта</param>
		/// <returns></returns>
		public IActionResult GetProduct(int id)
		{
			var productRepository = new ProductsRepository(new TourFirmaDBContext());

			var product = productRepository.GetProduct(id);

			if (product.Id == 0) return View("Error", id);

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
		
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
