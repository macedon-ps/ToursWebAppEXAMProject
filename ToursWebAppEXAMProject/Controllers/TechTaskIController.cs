using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Controllers
{
    public class TechTaskController : Controller
    {
        private readonly ITechTaskService _service;

        public TechTaskController(ITechTaskService service)
        {
            _service = service;
        }

        // GET
        public IActionResult Page(string pageName)
        {
            var model = _service.GetPage(pageName);

            return View("TechTask", model);
        }

        // POST
        [HttpPost]
        public IActionResult Page(TechTaskPageViewModel model)
        {
            if (ModelState.IsValid)
            {
                _service.Save(model);
            }

            return View("TechTask", model);
        }
    }
}
