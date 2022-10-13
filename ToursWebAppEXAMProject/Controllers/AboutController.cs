using Microsoft.AspNetCore.Mvc;
using NLog;
using ToursWebAppEXAMProject.Repositories;

namespace ToursWebAppEXAMProject.Controllers
{
	public class AboutController : Controller
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		private readonly DataManager DataManager;

		public AboutController(DataManager DataManager)
		{
			this.DataManager = DataManager;
		}
		public IActionResult Index()
		{
			logger.Trace("Переход по маршруту /About/Index. Возвращено представление About/Index.cshtml\n");
			Console.WriteLine("Переход по маршруту /About/Index. Возвращено представление About/Index.cshtml\n");
			return View();
		}

		public IActionResult TechTaskAbout()
		{
			logger.Trace("Переход по маршруту About/TechTaskAbout. Возвращаено представление About/TechTaskAbout.cshtml\n");
			Console.WriteLine("Переход по маршруту About/TechTaskAbout. Возвращаено представление About/TechTaskAbout.cshtml\n");
			return View();
		}
	}
}
