using Microsoft.AspNetCore.Mvc;
using NLog;
using ToursWebAppEXAMProject.Repositories;

namespace ToursWebAppEXAMProject.Controllers
{
	public class SupportController : Controller
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		private readonly DataManager DataManager;

		public SupportController(DataManager DataManager)
		{
			this.DataManager = DataManager;
		}

		public IActionResult Index()
		{
			logger.Trace("Переход по маршруту /Support/Index. Возвращено представление Support/Index.cshtml");
			Console.WriteLine("Переход по маршруту /Support/Index. Возвращено представление Support/Index.cshtml");
			return View();
		}
	}
}
