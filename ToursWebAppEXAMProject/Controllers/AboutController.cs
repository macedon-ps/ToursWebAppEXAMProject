using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.EnumsDictionaries;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
    public class AboutController : Controller
	{
        private readonly IBaseInterface<EditAboutPageViewModel> _AboutPage;
        private readonly IWebHostEnvironment _HostingEnvironment;
        private readonly IEditTechTaskInterface _AllTasks;

        public AboutController(IBaseInterface<EditAboutPageViewModel> AboutPage, IWebHostEnvironment HostingEnvironment, IEditTechTaskInterface Tasks)
		{
            this._AboutPage = AboutPage;
            this._HostingEnvironment = HostingEnvironment;
            this._AllTasks = Tasks;
        }
        
        /// <summary>
        /// Метод вывода стартовой страницы About
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
		{
			WriteLogs("Переход по маршруту /About/Index.\n", NLogsModeEnum.Trace);

            // выводим всегда актуальную на данный момент версию страницы About
            var isActualVersion = _AboutPage.GetAllItems().FirstOrDefault(v => v.IsActual == true);
            
            var editAboutPageViewModel = new EditAboutPageViewModel();
            if (isActualVersion != null && isActualVersion.Id != 0)
            {
                var pageVersion = isActualVersion.Id;
                editAboutPageViewModel = _AboutPage.GetItemById(pageVersion);
                return View(editAboutPageViewModel);
            }
            else
            {
                return View(editAboutPageViewModel);
            }
			
		}

		/// <summary>
		/// Метод вывода формы обратной связи с пользователями сайта
		/// </summary>
		/// <returns></returns>
		public IActionResult FeedBackForm()
		{
            WriteLogs("Переход по маршруту /About/FeedBackForm.\n", NLogsModeEnum.Trace);

            var customer = new Customer();
			return View(customer);
		}

        public IActionResult EditAboutPage(int id)
        {
            var editAboutViewModel = _AboutPage.GetItemById(id);
            return View(editAboutViewModel);
        }

        public IActionResult DeleteAboutPage(int id)
        {
            var aboutPage = _AboutPage.GetItemById(id);
            _AboutPage.DeleteItem(aboutPage, id);

            WriteLogs("Возвращено ../Shared/SuccessForDelete.cshtml\n", NLogsModeEnum.Trace);

            return View("../Shared/SuccessForDelete", aboutPage);
        }

        public IActionResult ChangeAboutPageVersion()
        {
            var allVersionsAboutPage = _AboutPage.GetAllItems();
            return View(allVersionsAboutPage);
        }


        /// <summary>
        /// Метод сохранения страницы About с данными, введенными редактором
        /// </summary>
        /// <param name="viewModel">Вью модель EditAboutPageViewModel</param>
        /// <param name="formValues">Данные формы ввода типа IFormCollection</param>
        /// <param name="changeTitleImagePath">Данные формы ввода типа IFormFile</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpPost]
        public async Task<IActionResult> SaveAboutPage(EditAboutPageViewModel viewModel, IFormCollection formValues, IFormFile? changeTitleImagePath)
        {
            WriteLogs("Запущен процесс сохранения вью модели EditAboutPageViewMode в БД. ", NLogsModeEnum.Debug);

            if (ModelState.IsValid)
            {
                WriteLogs("Вью модель EditAboutPageViewMode прошла валидацию. ", NLogsModeEnum.Debug);

                /*  TODO: смена картинки
                // если мы хотим поменять картинку
                if (changeTitleImagePath != null)
                {
                    var filePath = $"/images/NewsTitleImages/{changeTitleImagePath.FileName}";

                    using (var fstream = new FileStream(_hostingEnvironment.WebRootPath + filePath, FileMode.Create))
                    {
                        await changeTitleImagePath.CopyToAsync(fstream);

                        WriteLogs($"Новая титульная картинка новости сохранена по пути: {filePath}\n", NLogsModeEnum.Debug);
                    }
                    newsItem.TitleImagePath = filePath;
                }*/

                viewModel.MainFullDescription = formValues["fullInfoMain"];
                viewModel.AboutFullDescription = formValues["fullInfoAbout"];
                viewModel.DetailsFullDescription = formValues["fullInfoDetails"];
                viewModel.OperationModeFullDescription = formValues["fullInfoOperationMode"];
                viewModel.PhotoGalleryFullDescription = formValues["fullInfoPhotoGallery"];
                viewModel.FeedbackFullDescription = formValues["fullInfoFeedback"];
                viewModel.DateAdded = DateTime.Now;
             
                _AboutPage.SaveItem(viewModel, viewModel.Id);

                WriteLogs("Вью модель с данными страницы About успешно сохранена в БД. ", NLogsModeEnum.Debug);
                WriteLogs("Возвращено ../Shared/Success.cshtml\n", NLogsModeEnum.Trace);

                // TODO: переделать представление вместо Success
                return View("EditAboutPage", viewModel);
            }
            else
            {
                WriteLogs("Вью модель с данными страницы About не прошла валидацию. ", NLogsModeEnum.Warn);
                WriteLogs("Возвращено EditAboutPage.cshtml\n", NLogsModeEnum.Trace);

                viewModel.MainFullDescription = formValues["fullInfoMain"];
                viewModel.AboutFullDescription = formValues["fullInfoAbout"];
                viewModel.DetailsFullDescription = formValues["fullInfoDetails"];
                viewModel.OperationModeFullDescription = formValues["fullInfoOperationMode"];
                viewModel.PhotoGalleryFullDescription = formValues["fullInfoPhotoGallery"];
                viewModel.FeedbackFullDescription = formValues["fullInfoFeedback"];

                return View("EditAboutPage", viewModel);
            }
        }


        /// <summary>
        /// Метод вывода формы обратной связи с данными, введенными пользователями сайта
        /// </summary>
        /// <param name="customer">Модель пользователя сайта</param>
        /// <param name="textAreaForm">Данные формы ввода типа IFormCollection</param>
        /// <returns></returns>
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

        /// <summary>
        /// Метод вывода ТЗ и прогресса его выполнения для страницы About
        /// </summary>
        /// <returns></returns>
        public IActionResult TechTaskAbout()
		{
            WriteLogs("Переход по маршруту About/TechTaskAbout.\n", NLogsModeEnum.Trace);
            
			var pageName = "About";
			var model = _AllTasks.GetTechTasksForPage(pageName);

			return View(model);
		}

        /// <summary>
        /// Метод редактирования и сохранения данных о прогресса его выполнения ТЗ для страницы About
        /// </summary>
        /// <param name="model">Данные с формы для ТЗ и прогресса его выполнени</param>
        /// <returns></returns>
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

				_AllTasks.SaveProgressTechTasks(model);

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
