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
            var viewModel = _service.GetPageViewModel(pageName);

            return View("TechTask", viewModel);
        }


        // POST
        [HttpPost]
        public IActionResult Page(TechTaskPageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                
                var model = _service.GetPageFromViewModel(viewModel);
                viewModel.Progress = _service.CalculateProgress(model);
                _service.Save(model);
            }

            return View("TechTask", viewModel);
        }
    }
}
