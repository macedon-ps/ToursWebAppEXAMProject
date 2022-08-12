using Microsoft.AspNetCore.Mvc;

namespace ToursWebAppEXAMProject.Controllers
{
	public class AboutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
