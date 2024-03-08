using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using TourWebAppEXAMProject.Utils;
using static TourWebAppEXAMProject.Services.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
    public class HomeController : Controller
	{
		private readonly IBaseInterface<Blog> _AllBlogs;
		private readonly IBaseInterface<New> _AllNews;
		private readonly TechTaskUtils _TechTaskUtils;

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
			WriteLogs("Переход по маршруту /.\n", NLogsModeEnum.Trace);

			var viewModel = new NewsAndBlogsViewModel();
			viewModel.AllBlogs = _AllBlogs.GetAllItems();
			viewModel.AllNews = _AllNews.GetAllItems();	

			return View(viewModel);
		}

        /// <summary>
        /// Метод вывода ТЗ и прогресса его выполнения для страницы Home
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        public IActionResult TechTaskHome()
		{
            WriteLogs("Переход по маршруту Home/TechTaskHome.\n", NLogsModeEnum.Trace);

            var model = _TechTaskUtils.GetTechTaskForPage("Home");

            return View(model);
		}

        /// <summary>
        /// Метод редактирования и сохранения данных о прогресса его выполнения ТЗ для страницы Home
        /// </summary>
        /// <param name="model">Данные с формы для ТЗ и прогресса его выполнения</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        [HttpPost]
		public IActionResult TechTaskHome(TechTaskViewModel model)
		{
            WriteLogs("Сохранение выполнения ТЗ в БД. ", NLogsModeEnum.Debug);
            
			if (ModelState.IsValid)
			{
                WriteLogs("TechTaskViewModel прошла валидацию. ", NLogsModeEnum.Debug);
                
				_TechTaskUtils.SetTechTaskProgressAndSave(model);

				WriteLogs("Показатели выполнения ТЗ сохранены. ", NLogsModeEnum.Debug);
                WriteLogs("Возвращено /Home/TechTaskHome.cshtml\n", NLogsModeEnum.Trace);
                
				return View(model);
			}
            WriteLogs("TechTaskViewModel не прошла валидацию. Показатели выполнения ТЗ не сохранены. ", NLogsModeEnum.Warn);
            WriteLogs("Возвращено /Home/TechTaskHome.cshtml\n", NLogsModeEnum.Trace);
            
			return View(model);
		}
	}
}