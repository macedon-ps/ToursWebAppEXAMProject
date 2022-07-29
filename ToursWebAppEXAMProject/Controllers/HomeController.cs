using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
		public ActionResult GetTour(int id)
		{
			ProductsRepository repository = new ProductsRepository();

			Product product = repository.GetTour(id);

			return View(product);
		}
		public ActionResult GetAllTours()
		{
			ProductsRepository repository = new ProductsRepository();

			List<Product>? products = repository.Products;

			return View(products);
		}

	}
}