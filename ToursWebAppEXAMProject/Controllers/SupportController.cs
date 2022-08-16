using Microsoft.AspNetCore.Mvc;
using NLog;

namespace ToursWebAppEXAMProject.Controllers
{
	public class SupportController : Controller
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();
		public IActionResult Index()
		{
			logger.Trace("Переход по маршруту /Support/Index. Возвращено представление Support/Index.cshtml");
			Console.WriteLine("Переход по маршруту /Support/Index. Возвращено представление Support/Index.cshtml");
			return View();
		}
	}
}
