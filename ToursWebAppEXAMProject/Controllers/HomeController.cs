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

		public IActionResult Index()
		{
			logger.Trace("Переход по маршруту /. Возвращаено представление Home/Index.cshtml");
			Console.WriteLine("Переход по маршруту /. Возвращено представление Home/Index.cshtml");
			return View();
		}

		public IActionResult Privacy()
		{
			logger.Trace("Переход по маршруту /Home/Privacy. Возвращено представление Home/Privacy.cshtml");
			Console.WriteLine("Переход по маршруту /Home/Privacy. Возвращено представление Home/Privacy.cshtml");
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			logger.Trace($"Переход по маршруту /Home/Error");
			Console.WriteLine($"Переход по маршруту /Home/Error");
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		/// <summary>
		/// Метод GetNew(int id), кот. возвращает одну новость
		/// </summary>
		/// <param name="id">id новости</param>
		/// <returns></returns>
		/*
		public IActionResult GetNew(int id)
		{
			logger.Trace($"Переход по маршруту /Home/GetNew?id={id}");
			Console.WriteLine($"Переход по маршруту /Home/GetNew?id={id}");
			
			var new = DataManager.newsRepository.GetNew(id);

			logger.Debug($"Вывод новости с id={id}. Возвращено представление Home/GetNew.cshtml\n");
			Console.WriteLine($"Вывод новости с id={id}. Возвращено представление Home/GetNew.cshtml\n");

			if (new.Id == 0) return View("/Home/Error");
			logger.Warn($"Отсутствие данных о новости с id={id}. Возвращено представление /Home/Error.cshtml\n");
			Console.WriteLine($"Отсутствие данных о новости с id={id}. Возвращеноя представление /Home/Error.cshtml\n");

			return View(new);
		}
		*/

		/// <summary>
		/// Метод GetAllNews(), кот. возвращает все новости
		/// </summary>
		/// <returns></returns>
		/*
		public IActionResult GetAllNews()
		{
			logger.Trace("Переход по маршруту /Home/GetAllNews");
			Console.WriteLine("Переход по маршруту /Home/GetAllNews");
			
			var news = DataManager.newsRepository.GetAllNews().ToList<New>();
			logger.Debug("Вывод всех новостей. Возвращено представление Home/GetAllNews.cshtml\n");
			Console.WriteLine("Вывод всех новостей. Возвращено представление Home/GetAllNews.cshtml\n");

			if (news == null) return View("Error");
			logger.Warn("Отсутствие данных о новостях. Возвращеноя представление /Home/Error.cshtml\n");
			Console.WriteLine("Отсутствие данных о новостях. Возвращенося представление /Home/Error.cshtml\n");

			return View(news);
		}
		*/
	}
}