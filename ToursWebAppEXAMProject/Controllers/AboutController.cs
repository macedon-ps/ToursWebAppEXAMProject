using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.EnumsDictionaries;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Repositories;
using ToursWebAppEXAMProject.ViewModels;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
	public class AboutController : Controller
	{
		private readonly DataManager DataManager;

		public AboutController(DataManager DataManager)
		{
			this.DataManager = DataManager;
		}
		public IActionResult Index()
		{
			WriteLogs("Переход по маршруту /About/Index.\n", NLogsModeEnum.Trace);

			return View();
		}

		public IActionResult FeedBackForm()
		{
            WriteLogs("Переход по маршруту /About/FeedBackForm.\n", NLogsModeEnum.Trace);

            var customer = new Customer();
			return View(customer);
		}

		[HttpPost]
		public IActionResult FeedBackForm(Customer customer, IFormCollection textAreaForm)
		{
			if (ModelState.IsValid)
			{
                WriteLogs("FeedBackForm прошла валидацию. ", NLogsModeEnum.Debug);
				WriteLogs($"Получены данные: Имя: {customer.Name}  Фамилия: {customer.Surname}  Возраст: {customer.Age}  Пол: {customer.Gender}  Вопрос: {textAreaForm["textArea"]}\n", NLogsModeEnum.Debug);
                WriteLogs("Возвращено /About/FeedBackForm.cshtml\n", NLogsModeEnum.Trace);

                // TODO: нужно делать ViewModel для пользователя и его вопроса, чтобы сохранить текст и передать в представление
                // var textArea = textAreaForm["textArea"];
                // ??? customer.question = textArea;  // возможно так

                return View(customer);
			}
            WriteLogs("FeedBackForm не прошла валидацию. ", NLogsModeEnum.Warn);
            WriteLogs("Возвращено /About/FeedBackForm.cshtml\n", NLogsModeEnum.Trace);

            return View(new Customer());
		}


		public IActionResult TechTaskAbout()
		{
            WriteLogs("Переход по маршруту About/TechTaskAbout.\n", NLogsModeEnum.Trace);
            
			var pageName = "About";
			var model = DataManager.TechTaskInterface.GetTechTasksForPage(pageName);

			return View(model);
		}

		[HttpPost]
		public IActionResult TechTaskAbout(TechTaskViewModel model)
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
                WriteLogs("Возвращено /About/TechTaskAbout.cshtml\n", NLogsModeEnum.Trace);
                
				return View(model);
			}
            WriteLogs("TechTaskViewModel не прошла валидацию. Показатели выполнения ТЗ не сохранены. ", NLogsModeEnum.Warn);
            WriteLogs("Возвращено /About/TechTaskAbout.cshtml\n", NLogsModeEnum.Trace);

            return View(model);
		}
	}
}
