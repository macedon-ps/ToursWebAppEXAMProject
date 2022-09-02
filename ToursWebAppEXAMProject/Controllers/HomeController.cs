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

			var news = DataManager.newBaseInterface.GetAllItems();
			
			if (news == null) 
			{
				logger.Warn("Возвращено представление /Home/Error.cshtml\n");
				Console.WriteLine("Возвращено представление /Home/Error.cshtml\n");
				return View("Error");
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

			var new_ = DataManager.newBaseInterface.GetItemById(id);

			logger.Debug($"Возвращено представление Home/GetNew.cshtml\n");
			Console.WriteLine($"Возвращено представление Home/GetNew.cshtml\n");

			if (new_.Id == 0) return View("/Home/Error");
			logger.Warn($"Возвращено представление /Home/Error.cshtml\n");
			Console.WriteLine($"Возвращеноя представление /Home/Error.cshtml\n");

			return View(new_);
		}
		
		public IActionResult GetAllBlogs()
		{
			logger.Trace("Переход по маршруту /Home/GetAllBlogs. Возвращаено представление Home/GetAllBlogs.cshtml\n");
			Console.WriteLine("Переход по маршруту /Home/GetAllBlogs. Возвращаено представление Home/GetAllBlogs.cshtml\n");

			var blogs = DataManager.blogBaseInterface.GetAllItems();

			if (blogs == null)
			{
				logger.Warn("Возвращено представление /Home/Error.cshtml\n");
				Console.WriteLine("Возвращено представление /Home/Error.cshtml\n");
				return View("Error");
			}
			logger.Debug("Возвращено представление /Home/GetAllBlogs.cshtml\n");
			Console.WriteLine("Возвращено представление /Home/GetAllBlogs.cshtml\n");
			return View(blogs);
		}

		public IActionResult GetBlog(int id)
		{
			logger.Trace($"Переход по маршруту /Home/GetBlog?id={id}");
			Console.WriteLine($"Переход по маршруту /Home/GetBlog?id={id}");

			var blog = DataManager.blogBaseInterface.GetItemById(id);

			logger.Debug($"Возвращено представление Home/GetBlog.cshtml\n");
			Console.WriteLine($"Возвращено представление Home/GetBlog.cshtml\n");

			if (blog.Id == 0) return View("/Home/Error");
			logger.Warn($"Возвращено представление /Home/Error.cshtml\n");
			Console.WriteLine($"Возвращеноя представление /Home/Error.cshtml\n");

			return View(blog);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			logger.Trace($"Переход по маршруту /Home/Error\n");
			Console.WriteLine($"Переход по маршруту /Home/Error\n");
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}