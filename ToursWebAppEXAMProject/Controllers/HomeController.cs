using Microsoft.AspNetCore.Mvc;
using NLog;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Controllers
{
    public class HomeController : Controller
	{
		private readonly IBaseInterface<Blog> _AllBlogs;
		private readonly IBaseInterface<New> _AllNews;
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public HomeController(IBaseInterface<Blog> Blogs, IBaseInterface<New> News)
		{
			_AllBlogs = Blogs;
			_AllNews = News;
		}

		/// <summary>
		/// Метод вывода стартовой страницы Home
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
            _logger.Info("Старт приложения");

            var viewModel = new NewsAndBlogsViewModel();
			viewModel.AllBlogs = _AllBlogs.GetAllItems();
			viewModel.AllNews = _AllNews.GetAllItems();
            _logger.Debug("Получена вью-модель NewsAndBlogsViewModel.\n");

            _logger.Trace("Переход по маршруту /.\n");
            return View(viewModel);
		}
	}
}