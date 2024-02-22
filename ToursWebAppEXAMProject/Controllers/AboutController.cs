using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.EnumsDictionaries;
using ToursWebAppEXAMProject.FileUtilities;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;
using ToursWebAppEXAMProject.Utils;

namespace ToursWebAppEXAMProject.Controllers
{
    public class AboutController : Controller
	{
        private readonly IBaseInterface<EditAboutPageViewModel> _AboutPage;
        private readonly IBaseInterface<Customer> _AllCustomers;
        private readonly IEditTechTaskInterface _AllTasks;

        public AboutController(IBaseInterface<EditAboutPageViewModel> AboutPage, IBaseInterface<Customer> AllCustomers, IEditTechTaskInterface Tasks)
		{
            this._AboutPage = AboutPage;
            this._AllCustomers = AllCustomers;
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
            
            if (isActualVersion != null && isActualVersion.Id != 0)
            {
                var pageVersion = isActualVersion.Id;
                var editAboutPageViewModel = _AboutPage.GetItemById(pageVersion);

                return View(editAboutPageViewModel);
            }
            else
            {
                return View("Sorry");
            }
			
		}

        /// <summary>
        /// Метод вывода меню редактирования страницы About
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateEditDeleteAboutPage()
        {
            var allVersionOfPage = _AboutPage.GetAllItems();

            return View(allVersionOfPage);
        }

        /// <summary>
        /// Метод создания новой версии страницы About
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateAboutPage()
        {
            var editAboutPageViewModel = new EditAboutPageViewModel();
            editAboutPageViewModel.IsActual = true;
            return View("EditAboutPage", editAboutPageViewModel);
        }

        /// <summary>
        /// Метод вывода на редактирование страницы About по ее id
        /// </summary>
        /// <param name="id">идентификатор страницы About</param>
        /// <returns></returns>
        public IActionResult EditAboutPage(int id)
        {
            var editAboutViewModel = _AboutPage.GetItemById(id);

            return View(editAboutViewModel);
        }

        /// <summary>
        /// Метод удаления страницы About по ее id
        /// </summary>
        /// <param name="id">идентификатор страницы About</param>
        /// <returns></returns>
        public IActionResult DeleteAboutPage(int id)
        {
            var aboutPage = _AboutPage.GetItemById(id);
            _AboutPage.DeleteItem(aboutPage, id);

            WriteLogs("Возвращено ../Shared/SuccessForDelete.cshtml\n", NLogsModeEnum.Trace);

            return View("../Shared/SuccessForDelete", aboutPage);
        }

        public IActionResult DeletePicture(string fullPathToFile)
        {
            FileUtils.DeletePhoto(fullPathToFile);
            
            return View("DeletePicture", fullPathToFile);
        }

        /// <summary>
        /// Метод изменения версий страницы About
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllAboutPageVersions()
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
        public async Task<IActionResult> SaveAboutPage(EditAboutPageViewModel viewModel, 
                                                        IFormCollection formValues, 
                                                        IFormFile? changeMainImagePath, 
                                                        IFormFile? changeAboutImagePath, 
                                                        IFormFile? changeDetailsImagePath, 
                                                        IFormFile? changeOperationModeImagePath, 
                                                        IFormFile? changePhotoGalleryImagePath, 
                                                        IFormFile? changeFeedbackImagePath)
        {
            WriteLogs("Запущен процесс сохранения вью модели EditAboutPageViewMode в БД. ", NLogsModeEnum.Debug);

            if (ModelState.IsValid)
            {
                WriteLogs("Вью модель EditAboutPageViewMode прошла валидацию. ", NLogsModeEnum.Debug);

                // Main
                if (changeMainImagePath != null)
                {
                    var folder = "/images/AboutPage/Main/";
                    FileUtils.SaveFileIfExistPath(folder, changeMainImagePath);
                    viewModel.MainImagePath = $"{folder}{changeMainImagePath.FileName}";
                }

                // About
                if (changeAboutImagePath != null)
                {
                    var folder = "/images/AboutPage/About/";
                    FileUtils.SaveFileIfExistPath(folder, changeAboutImagePath);
                    viewModel.AboutImagePath = $"{folder}{changeAboutImagePath.FileName}";
                }

                // Details
                if (changeDetailsImagePath != null)
                {
                    var folder = "/images/AboutPage/Details/";
                    FileUtils.SaveFileIfExistPath(folder, changeDetailsImagePath);
                    viewModel.DetailsImagePath = $"{folder}{changeDetailsImagePath.FileName}";
                }
                // OperationMode
                if (changeOperationModeImagePath != null)
                {
                    var folder = "/images/AboutPage/OperationMode/";
                    FileUtils.SaveFileIfExistPath(folder, changeOperationModeImagePath);
                    viewModel.OperationModeImagePath = $"{folder}{changeOperationModeImagePath.FileName}";
                }
                // PhotoGallery
                if (changePhotoGalleryImagePath != null)
                {
                    var folder = "/images/AboutPage/PhotoGallery/";
                    FileUtils.SaveFileIfExistPath(folder, changePhotoGalleryImagePath);
                    viewModel.PhotoGalleryImagePath = $"{folder}{changePhotoGalleryImagePath.FileName}";
                }
                // Feedback
                if (changeFeedbackImagePath != null)
                {
                    var folder = "/images/AboutPage/Feedback/";
                    FileUtils.SaveFileIfExistPath(folder, changeFeedbackImagePath);
                    viewModel.FeedbackImagePath = $"{folder}{changeFeedbackImagePath.FileName}";
                }

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

                return View("../Shared/Success", viewModel);
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
        /// Метод вывода формы обратной связи с пользователями сайта
        /// </summary>
        /// <returns></returns>
        public IActionResult FeedBackForm()
        {
            WriteLogs("Переход по маршруту /About/FeedBackForm.\n", NLogsModeEnum.Trace);

            var customer = new Correspondence();
            return View(customer);
        }

        /// <summary>
        /// Метод вывода формы обратной связи с данными, введенными пользователями сайта
        /// </summary>
        /// <param name="customer">Модель пользователя сайта</param>
        /// <param name="textAreaForm">Данные формы ввода типа IFormCollection</param>
        /// <returns></returns>
        [HttpPost]
		public IActionResult FeedBackForm(Correspondence message, IFormCollection textAreaForm)
		{
			if (ModelState.IsValid)
			{
                /*1. Проверка по ФИО и эл.почте, является ли клиентом фирмы
                2.1. Если нет, то данные обрабатываем
                2.2. Выводим вью Success / Error
                2.3. Сохраняем в Correspondence и Asker
                3.1. Есди да, то данные обрабатываем
                3.2. Выводим вью Success / Error. Из Customer выводим дополнительную информацию для Correspondence.
                3.3. сохраняем в Correspondence и Asker. 
                4. Отсылаем по почте
                5. Во вью просмотреть ответ на свой вопрос*/

                var name = message.Asker.Name;
                var surname = message.Asker.Surname;
                var email = message.Asker.Email;
                var customer = _AllCustomers.GetAllItems()
                    .FirstOrDefault(x => (x.Name == name) && (x.Surname == surname) && (x.Email == email));

                if(customer != null)
                {
                    message.IsExCustomerOfCompany = true;

                }
                else
                {
                    message.IsExCustomerOfCompany = false;
                }

                var age = Calculations.CalculateAge(message.Asker.BirthDay);
                var question = textAreaForm["textArea"].ToString();

                if (question != null)
                {
                    message.Question = question;
                }

                WriteLogs("FeedBackForm прошла валидацию. ", NLogsModeEnum.Debug);
				WriteLogs($"Получены данные: Имя: {name}  Фамилия: {surname}  Возраст: {age}  Пол: {message.Asker.Gender}  Вопрос: {message.Question}\n", NLogsModeEnum.Debug);
                WriteLogs("Возвращено /About/FeedBackForm.cshtml\n", NLogsModeEnum.Trace);

                // TODO: нужно делать ViewModel для пользователя и его вопроса, чтобы сохранить текст и передать в представление
                
                
                return View(message);
			}
            WriteLogs("FeedBackForm не прошла валидацию. ", NLogsModeEnum.Warn);
            WriteLogs("Возвращено /About/FeedBackForm.cshtml\n", NLogsModeEnum.Trace);

            return View(new Correspondence());
		}

        /// <summary>
        /// Метод вывода ТЗ и прогресса его выполнения для страницы About
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
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
        [Authorize(Roles = "superadmin,admin")]
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
