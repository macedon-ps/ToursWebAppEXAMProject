using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.EnumsDictionaries;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
    public class HomeController : Controller
	{
		private readonly IBaseInterface<Blog> _AllBlogs;
		private readonly IBaseInterface<New> _AllNews;
		private readonly IEditTechTaskInterface _AllTechTask;

		public HomeController(IBaseInterface<Blog> Blogs, IBaseInterface<New> News, IEditTechTaskInterface Tasks)
		{
			this._AllBlogs = Blogs;
			this._AllNews = News;
			this._AllTechTask = Tasks;
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
        public IActionResult TechTaskHome()
		{
            WriteLogs("Переход по маршруту Home/TechTaskHome.\n", NLogsModeEnum.Trace);
            
			var pageName = "Home";
			var model = _AllTechTask.GetTechTasksForPage(pageName);

			return View(model);
		}

        /// <summary>
        /// Метод редактирования и сохранения данных о прогресса его выполнения ТЗ для страницы Home
        /// </summary>
        /// <param name="model">Данные с формы для ТЗ и прогресса его выполнения</param>
        /// <returns></returns>
        [HttpPost]
		public IActionResult TechTaskHome(TechTaskViewModel model)
		{
            WriteLogs("Сохранение выполнения ТЗ в БД. ", NLogsModeEnum.Debug);
            
			if (ModelState.IsValid)
			{
                WriteLogs("TechTaskViewModel прошла валидацию. ", NLogsModeEnum.Debug);
                
				double TechTasksCount = 6;
				double TechTasksTrueCount = 0;
				if (model.IsExecuteTechTask1 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask2 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask3 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask4 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask5 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask6 == true) TechTasksTrueCount++;

				double ExecuteTechTasksProgress = Math.Round((TechTasksTrueCount / TechTasksCount) * 100);
				model.ExecuteTechTasksProgress = ExecuteTechTasksProgress;

				_AllTechTask.SaveProgressTechTasks(model);

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