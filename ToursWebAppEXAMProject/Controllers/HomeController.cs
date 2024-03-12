using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using ToursWebAppEXAMProject.Utils;

namespace ToursWebAppEXAMProject.Controllers
{
    public class HomeController : Controller
	{
		private readonly IBaseInterface<Blog> _AllBlogs;
		private readonly IBaseInterface<New> _AllNews;
		private readonly TechTaskUtils _TechTaskUtils;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public HomeController(IBaseInterface<Blog> Blogs, IBaseInterface<New> News, TechTaskUtils TechTaskUtils)
		{
			_AllBlogs = Blogs;
			_AllNews = News;
			_TechTaskUtils = TechTaskUtils;
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

        /// <summary>
        /// Метод вывода ТЗ и прогресса его выполнения для страницы Home
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        public IActionResult TechTaskHome()
		{
            var viewModel = _TechTaskUtils.GetTechTaskForPage("Home");
            _logger.Debug("Получена вью-модель TechTaskViewModel. ");

            _logger.Trace("Переход по маршруту Home/TechTaskHome.\n");
            return View(viewModel);
		}

        /// <summary>
        /// Метод редактирования и сохранения данных о прогресса его выполнения ТЗ для страницы Home
        /// </summary>
        /// <param name="model">Данные с формы для ТЗ и прогресса его выполнения</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        [HttpPost]
		public IActionResult TechTaskHome(TechTaskViewModel viewModel)
		{
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Debug("Вью-модель TechTaskViewModel прошла валидацию. ");

                    _TechTaskUtils.SetTechTaskProgressAndSave(viewModel);
                    _logger.Debug("Вью-модель TechTaskViewModel заполнена данными и сохранена. ");

                    _logger.Trace("Возвращено /Home/TechTaskHome.cshtml\n");
                    return View(viewModel);
                }
                else
                {
                    _logger.Warn("Вью-модель TechTaskViewModel не прошла валидацию. Данные модели не сохранены. ");

                    _logger.Trace("Возвращено /Home/TechTaskHome.cshtml\n");
                    return View(viewModel);
                }
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);

                _logger.Trace("Возвращено ../Shared/Error.cshtml\n");
                return View("Error", new ErrorViewModel(error.Message));
            }
		}
	}
}