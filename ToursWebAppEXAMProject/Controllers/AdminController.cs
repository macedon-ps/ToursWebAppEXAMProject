using Microsoft.AspNetCore.Mvc;
using NLog;

namespace ToursWebAppEXAMProject.Controllers
{
	public class AdminController : Controller
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();
		public IActionResult Index()
		{
			logger.Trace("Переход по маршруту /Admin/Index. Возвращено представление Admin/Index.cshtml");
			Console.WriteLine("Переход по маршруту /Admin/Index. Возвращено представление Admin/Index.cshtml");
			return View();
		}
	}
}
