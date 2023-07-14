using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.EnumsDictionaries;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Repositories;
using ToursWebAppEXAMProject.ViewModels;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
	public class HomeController : Controller
	{
		private readonly DataManager DataManager;

		public HomeController(DataManager DataManager)
		{
			this.DataManager = DataManager;
		}

		/// <summary>
		/// Метод Index() для стартовой страницы Home
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			WriteLogs("Переход по маршруту /.\n", NLogsModeEnum.Trace);
			
			return View(DataManager);
		}

		/// <summary>
		/// Метод GetAllNews(), кот. возвращает все новости
		/// </summary>
		/// <returns></returns>
		public IActionResult GetALLNews()
		{
			WriteLogs("Переход по маршруту /Home/GetAllNews. ", NLogsModeEnum.Trace);
			
			var news = DataManager.NewBaseInterface.GetAllItems();
			
			if (news == null) 
			{
				var errorInfo = new ModelsErrorViewModel(typeof(List<New>));

				WriteLogs("Нет новостей. Возвращено /ModelsError.cshtml.\n", NLogsModeEnum.Warn);

				return View("ModelsError", errorInfo);
			}
			WriteLogs("Выводятся все новости.\n", NLogsModeEnum.Debug);

			return View(news);
		}

		/// <summary>
		/// Метод GetNew(int id), кот. возвращает одну новость
		/// </summary>
		/// <param name="id">id новости</param>
		/// <returns></returns>
		public IActionResult GetNew(int id)
		{
			WriteLogs($"Переход по маршруту /Home/GetNew?id={id}. ", NLogsModeEnum.Trace);

			var new_ = DataManager.NewBaseInterface.GetItemById(id);

			if (new_.Id == 0) 
			{
                WriteLogs($"Нет новости с id = {id}. Возвращено /ModelsError.cshtml\n", NLogsModeEnum.Warn);
                
				var errorInfo = new ModelsErrorViewModel(typeof(New), id);

				return View("ModelsError", errorInfo);
			}

			WriteLogs($"Выводится новость с id = {id}.\n", NLogsModeEnum.Debug);

            return View(new_);
		}

		/// <summary>
		/// Метод GetAllBlogs(), кот. возвращает все блоги
		/// </summary>
		/// <returns></returns>
		public IActionResult GetAllBlogs()
		{
			WriteLogs("Переход по маршруту /Home/GetAllBlogs. ", NLogsModeEnum.Trace);

			var blogs = DataManager.BlogBaseInterface.GetAllItems();

			if (blogs == null)
			{
				var errorInfo = new ModelsErrorViewModel(typeof(List<Blog>));

                WriteLogs("Нет блогов. Возвращено /ModelsError.cshtml\n", NLogsModeEnum.Warn);

                return View("ModelsError", errorInfo);
			}

            WriteLogs("Выводятся все блоги\n", NLogsModeEnum.Debug);

            return View(blogs);
		}

		/// <summary>
		/// Метод GetBlog(int id), кот. возвращает один блог
		/// </summary>
		/// <param name="id">id блога</param>
		/// <returns></returns>
		public IActionResult GetBlog(int id)
		{
            WriteLogs($"Переход по маршруту /Home/GetBlog?id={id}. ", NLogsModeEnum.Trace);
            
			var blog = DataManager.BlogBaseInterface.GetItemById(id);

			if (blog.Id == 0)
			{
				var errorInfo = new ModelsErrorViewModel(typeof(Blog), id);

                WriteLogs($"Нет блога с id = {id}. Возвращено /ModelsError.cshtml\n", NLogsModeEnum.Warn);

                return View("ModelsError", errorInfo);
			}

            WriteLogs($"Выводится блог с id = {id}.\n", NLogsModeEnum.Debug);
            
            return View(blog);
		}
		
		public IActionResult TechTaskHome()
		{
            WriteLogs("Переход по маршруту Home/TechTaskHome.\n", NLogsModeEnum.Trace);
            
			var pageName = "Home";
			var model = DataManager.TechTaskInterface.GetTechTasksForPage(pageName);

			return View(model);
		}

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

				DataManager.TechTaskInterface.SaveProgressTechTasks(model);

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