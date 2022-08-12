using Microsoft.AspNetCore.Mvc;

namespace ToursWebAppEXAMProject.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
