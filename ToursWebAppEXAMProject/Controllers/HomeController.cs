using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Controllers
{
    public class HomeController : Controller
	{
		private readonly IBaseInterface<Blog> _AllBlogs;
		private readonly IBaseInterface<New> _AllNews;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IBaseInterface<Blog> Blogs, IBaseInterface<New> News, ILogger<HomeController> logger)
		{
			_AllBlogs = Blogs;
			_AllNews = News;
            _logger = logger;
		}

		/// <summary>
		/// Метод вывода стартовой страницы Home
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
            _logger.LogInformation("Старт приложения");

            var viewModel = new NewsAndBlogsViewModel();
			viewModel.AllBlogs = _AllBlogs.GetAllItems();
			viewModel.AllNews = _AllNews.GetAllItems();
            _logger.LogDebug("Получена вью-модель NewsAndBlogsViewModel.\n");

            _logger.LogTrace("Переход по маршруту /.\n");
            return View(viewModel);
		}
	}
}