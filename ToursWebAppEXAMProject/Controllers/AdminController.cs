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

		public IActionResult Index()
		{
			logger.Trace("Переход по маршруту /Admin/Index. Возвращено представление Admin/Index.cshtml");
			Console.WriteLine("Переход по маршруту /Admin/Index. Возвращено представление Admin/Index.cshtml");
			
			var products = new List<Product>();
			products = DataManager.productsRepository.GetAllProducts().ToList();
			
			return View(products);
		}

	}
}
