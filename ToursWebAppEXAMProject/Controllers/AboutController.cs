using Microsoft.AspNetCore.Mvc;
using NLog;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Repositories;

namespace ToursWebAppEXAMProject.Controllers
{
	public class AboutController : Controller
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		private readonly DataManager DataManager;

		public AboutController(DataManager DataManager)
		{
			this.DataManager = DataManager;
		}
		public IActionResult Index()
		{
			logger.Trace("Переход по маршруту /About/Index. Возвращено представление About/Index.cshtml\n");
			Console.WriteLine("Переход по маршруту /About/Index. Возвращено представление About/Index.cshtml\n");
			return View();
		}

		public IActionResult FeedBackForm()
		{
			var customer = new Customer();
			return View(customer);
		}

		[HttpPost]
		public IActionResult FeedBackForm(Customer customer, IFormCollection textAreaForm)
		{
			if (ModelState.IsValid)
			{
				logger.Trace($"Все получилось!\nИмя: {customer.Name}\nФамилия: {customer.Surname}\nВозраст: {customer.Age}\nПол: {customer.Gender}\nВопрос: {textAreaForm["textArea"]}\n");
				Console.WriteLine($"Все получилось!\nИмя: {customer.Name}\nФамилия: {customer.Surname}\nВозраст: {customer.Age}\nПол: {customer.Gender}\nВопрос: {textAreaForm["textArea"]}\n");

				// TODO: нужно делать ViewModel для пользователя и его вопроса, чтобы сохранить текст и передать в представление
				// var textArea = textAreaForm["textArea"];
				// ??? customer.question = textArea;  // возможно так

				return View(customer);
			}
			logger.Trace($"Ничего не вышло!\nИмя: {customer.Name}\nФамилия: {customer.Surname}\nВозраст: {customer.Age}\nПол: {customer.Gender}\nВопрос: {textAreaForm["textArea"]}\n");
			Console.WriteLine($"Ничего не вышло!\nИмя: {customer.Name}\nФамилия: {customer.Surname}\nВозраст: {customer.Age}\nПол: {customer.Gender}\nВопрос: {textAreaForm["textArea"]}\n");
			return View(new Customer());
		}


		public IActionResult TechTaskAbout()
		{
			logger.Trace("Переход по маршруту About/TechTaskAbout. Возвращаено представление About/TechTaskAbout.cshtml\n");
			Console.WriteLine("Переход по маршруту About/TechTaskAbout. Возвращаено представление About/TechTaskAbout.cshtml\n");

			var pageName = "About";
			var model = DataManager.TechTaskInterface.GetTechTasksForPage(pageName);

			return View(model);
		}

		[HttpPost]
		public IActionResult TechTaskAbout(TechTaskViewModel model)
		{
			logger.Debug("Запущен процесс сохранения показателей выполнения тех. задания в БД");
			Console.WriteLine("Запущен процесс сохранения показателей выполнения тех. задания в БД");

			if (ModelState.IsValid)
			{
				logger.Debug("Модель TechTaskViewModel успешно прошла валидацию");
				Console.WriteLine("Модель TechTaskViewModel успешно прошла валидацию");

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
				logger.Debug("Показатели выполнения тех. задания успешно сохранены в БД");
				Console.WriteLine("Показатели выполнения тех. задания успешно сохранены в БД");
				logger.Debug("Возвращено представление /About/TechTaskAbout.cshtml\n");
				Console.WriteLine("Возвращено представление /About/TechTaskAbout.cshtml\n");

				return View(model);
			}
			logger.Debug("Модель TechTaskViewModel не прошла валидацию");
			Console.WriteLine("Модель TechTaskViewModel не прошла валидацию");
			logger.Debug("Возвращено представление /About/TechTaskAbout.cshtml\n");
			Console.WriteLine("Возвращено представление /About/TechTaskAbout.cshtml\n");

			return View(model);
		}
	}
}
