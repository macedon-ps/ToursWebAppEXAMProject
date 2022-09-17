using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Collections.Generic;
using System.Diagnostics;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Repositories;

namespace ToursWebAppEXAMProject.Controllers
{
	public class HomeController : Controller
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

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
			logger.Trace("Переход по маршруту /. Возвращаено представление Home/Index.cshtml\n");
			Console.WriteLine("Переход по маршруту /. Возвращено представление Home/Index.cshtml\n");
			
			return View();
		}

		/// <summary>
		/// Метод GetAllNews(), кот. возвращает все новости
		/// </summary>
		/// <returns></returns>
		public IActionResult GetALLNews()
		{
			logger.Trace("Переход по маршруту /Home/GetAllNews. Возвращаено представление Home/GetAllNews.cshtml\n");
			Console.WriteLine("Переход по маршруту /Home/GetAllNews. Возвращаено представление Home/GetAllNews.cshtml\n");

			var news = DataManager.NewBaseInterface.GetAllItems();
			
			if (news == null) 
			{
				// задаем входные параметры для объекта ErrorViewModel
				// можно передать: modelType - обязательный параметр, message - опционально;
				// message = "";
				var errorInfo = new ErrorViewModel(typeof(List<New>));

				logger.Warn("Возвращено представление /Error.cshtml\n");
				Console.WriteLine("Возвращено представление /Error.cshtml\n");
				return View("Error", errorInfo);
			}
			logger.Debug("Возвращено представление /Home/GetAllNews.cshtml\n");
			Console.WriteLine("Возвращено представление /Home/GetAllNews.cshtml\n");
			return View(news);
		}

		/// <summary>
		/// Метод GetNew(int id), кот. возвращает одну новость
		/// </summary>
		/// <param name="id">id новости</param>
		/// <returns></returns>
		public IActionResult GetNew(int id)
		{
			logger.Trace($"Переход по маршруту /Home/GetNew?id={id}");
			Console.WriteLine($"Переход по маршруту /Home/GetNew?id={id}");

			var new_ = DataManager.NewBaseInterface.GetItemById(id);

			if (new_.Id == 0) 
			{
				logger.Warn($"Возвращено представление /Error.cshtml\n");
				Console.WriteLine($"Возвращеноя представление /Error.cshtml\n");

				// задаем входные параметры для объекта ErrorViewModel
				// можно передать: modelType, id - обязательные параметры, message - опционально;
				// message = "";
				var errorInfo = new ErrorViewModel(typeof(New), id);
				return View("Error", errorInfo);
			}

			logger.Debug($"Возвращено представление Home/GetNew.cshtml\n");
			Console.WriteLine($"Возвращено представление Home/GetNew.cshtml\n");
			return View(new_);
		}

		/// <summary>
		/// Метод GetAllBlogs(), кот. возвращает все блоги
		/// </summary>
		/// <returns></returns>
		public IActionResult GetAllBlogs()
		{
			logger.Trace("Переход по маршруту /Home/GetAllBlogs. Возвращаено представление Home/GetAllBlogs.cshtml\n");
			Console.WriteLine("Переход по маршруту /Home/GetAllBlogs. Возвращаено представление Home/GetAllBlogs.cshtml\n");

			var blogs = DataManager.BlogBaseInterface.GetAllItems();

			if (blogs == null)
			{
				// задаем входные параметры для объекта ErrorViewModel
				// можно передать: modelType - обязательный параметр, message - опционально;
				// message = "";
				var errorInfo = new ErrorViewModel(typeof(List<Blog>));

				logger.Warn("Возвращено представление /Error.cshtml\n");
				Console.WriteLine("Возвращено представление /Error.cshtml\n");
				return View("Error", errorInfo);
			}
			logger.Debug("Возвращено представление /Home/GetAllBlogs.cshtml\n");
			Console.WriteLine("Возвращено представление /Home/GetAllBlogs.cshtml\n");
			return View(blogs);
		}

		/// <summary>
		/// Метод GetBlog(int id), кот. возвращает один блог
		/// </summary>
		/// <param name="id">id блога</param>
		/// <returns></returns>
		public IActionResult GetBlog(int id)
		{
			logger.Trace($"Переход по маршруту /Home/GetBlog?id={id}");
			Console.WriteLine($"Переход по маршруту /Home/GetBlog?id={id}");

			var blog = DataManager.BlogBaseInterface.GetItemById(id);

			logger.Debug($"Возвращено представление Home/GetBlog.cshtml\n");
			Console.WriteLine($"Возвращено представление Home/GetBlog.cshtml\n");

			if (blog.Id == 0) 
			{
				// задаем входные параметры для объекта ErrorViewModel
				// можно передать: modelType, id - обязательные параметры, message - опционально;
				// message = "";
				var errorInfo = new ErrorViewModel(typeof(Blog), id);

				logger.Warn($"Возвращено представление /Error.cshtml\n");
				Console.WriteLine($"Возвращеноя представление /Error.cshtml\n");
				return View("Error", errorInfo);
			}
			
			return View(blog);
		}
	}
}