using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.ViewModels;
using static TourWebAppEXAMProject.Services.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
    [Authorize]
    public class EditController : Controller
	{
		private readonly IEditTechTaskInterface _AllTasks;

		public EditController(IEditTechTaskInterface Tasks)
		{
			this._AllTasks = Tasks;
		}
		     
        /// <summary>
        /// Метод вывода стартовой страницы Edit
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		public IActionResult Index(string type = "New")
		{
			WriteLogs("Переход по маршруту /Edit/Index.\n", NLogsModeEnum.Trace);

            // страница Index.cshtml по умолчанию принимает тип New, по нажатию на кнопки - др.типы (New, Blog, Product)
            var model = new EditMenuViewModel(false, "", type);

			return View(model);
		}

        /// <summary>
        /// Метод вывода ТЗ и прогресса его выполнения для страницы Edit
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        public IActionResult TechTaskEdit()
		{
            WriteLogs("Переход по маршруту Edit/TechTaskEdit.\n", NLogsModeEnum.Trace);
            
			var pageName = "Edit";
			var model = _AllTasks.GetTechTasksForPage(pageName);

			return View(model);
		}

        /// <summary>
        /// Метод редактирования и сохранения данных о прогресса его выполнения ТЗ для страницы Edit
        /// </summary>
        /// <param name="model">Данные с формы для ТЗ и прогресса его выполнения</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        [HttpPost]
		public IActionResult TechTaskEdit(TechTaskViewModel model)
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

				_AllTasks.SaveProgressTechTasks(model);

                WriteLogs("Показатели выполнения ТЗ сохранены. ", NLogsModeEnum.Debug);
                WriteLogs("Возвращено /Edit/TechTaskEdit.cshtml\n", NLogsModeEnum.Trace);
                				
				return View(model);
			}

            WriteLogs("TechTaskViewModel не прошла валидацию. Показатели выполнения ТЗ не сохранены. ", NLogsModeEnum.Warn);
            WriteLogs("Возвращено /Edit/TechTaskEdit.cshtml\n", NLogsModeEnum.Trace);
            
			return View(model);
		}
	}
}
