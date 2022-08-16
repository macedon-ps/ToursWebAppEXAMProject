using Microsoft.AspNetCore.Mvc;
using NLog;

namespace ToursWebAppEXAMProject.Controllers
{
	public class AboutController : Controller
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();
		public IActionResult Index()
		{
			logger.Trace("Переход по маршруту /About/Index. Возвращено представление About/Index.cshtml");
			Console.WriteLine("Переход по маршруту /About/Index. Возвращено представление About/Index.cshtml");
			return View();
		}
	}
}
