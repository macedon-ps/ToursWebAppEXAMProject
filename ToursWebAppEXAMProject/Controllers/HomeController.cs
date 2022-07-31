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
		/// Метод GetTour(int id), кот. возвращает данные одного туристического тура
		/// </summary>
		/// <param name="id">id тура</param>
		/// <returns></returns>
		public IActionResult GetTour(int id)
		{
			TourFirmaDBContext context = new TourFirmaDBContext();

			var product = context.Products.FirstOrDefault(t => t.Id == id);
			if (product == null) return View("Error", id);
			
			return View(product);
		}
		public IActionResult GetAllTours()
		{
			TourFirmaDBContext context = new TourFirmaDBContext();

			var products = context.Products.ToList();

			return View(products);
		}

	}
}