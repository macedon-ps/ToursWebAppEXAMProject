using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using static TourWebAppEXAMProject.Services.LogsMode.LogsMode;
using Microsoft.AspNetCore.Identity;
using TourWebAppEXAMProject.Utils;
using TourWebAppEXAMProject.Services.Email;
using Microsoft.AspNetCore.Authentication;

namespace ToursWebAppEXAMProject.Controllers
{
    public class AboutController : Controller
	{
        private readonly IBaseInterface<EditAboutPageViewModel> _AboutPage;
        private readonly UserManager<User> _UserManager;
        private readonly IBaseInterface<Asker> _AllAskers;
        private readonly IBaseInterface<Customer> _AllCustomers;
        private readonly IBaseInterface<Correspondence> _AllCorrespondences;
        private readonly IEditTechTaskInterface _AllTasks;

        public AboutController(IBaseInterface<EditAboutPageViewModel> AboutPage, UserManager<User> UserManager, IBaseInterface<Asker> AllAskers, IBaseInterface<Customer> AllCustomers, IBaseInterface<Correspondence> AllCorrespondences, IEditTechTaskInterface Tasks)
		{
            this._AboutPage = AboutPage;
            this._UserManager = UserManager;
            this._AllAskers = AllAskers;
            this._AllCustomers = AllCustomers;
            this._AllCorrespondences = AllCorrespondences;
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
                    await FileUtils.SaveFileIfExistPath(folder, changeMainImagePath);
                    viewModel.MainImagePath = $"{folder}{changeMainImagePath.FileName}";
                }

                // About
                if (changeAboutImagePath != null)
                {
                    var folder = "/images/AboutPage/About/";
                    await FileUtils.SaveFileIfExistPath(folder, changeAboutImagePath);
                    viewModel.AboutImagePath = $"{folder}{changeAboutImagePath.FileName}";
                }

                // Details
                if (changeDetailsImagePath != null)
                {
                    var folder = "/images/AboutPage/Details/";
                    await FileUtils.SaveFileIfExistPath(folder, changeDetailsImagePath);
                    viewModel.DetailsImagePath = $"{folder}{changeDetailsImagePath.FileName}";
                }
                // OperationMode
                if (changeOperationModeImagePath != null)
                {
                    var folder = "/images/AboutPage/OperationMode/";
                    await FileUtils.SaveFileIfExistPath(folder, changeOperationModeImagePath);
                    viewModel.OperationModeImagePath = $"{folder}{changeOperationModeImagePath.FileName}";
                }
                // PhotoGallery
                if (changePhotoGalleryImagePath != null)
                {
                    var folder = "/images/AboutPage/PhotoGallery/";
                    await FileUtils.SaveFileIfExistPath(folder, changePhotoGalleryImagePath);
                    viewModel.PhotoGalleryImagePath = $"{folder}{changePhotoGalleryImagePath.FileName}";
                }
                // Feedback
                if (changeFeedbackImagePath != null)
                {
                    var folder = "/images/AboutPage/Feedback/";
                    await FileUtils.SaveFileIfExistPath(folder, changeFeedbackImagePath);
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

            var viewModel = new CorrespondenceViewModel();
            return View(viewModel);
        }

        /// <summary>
        /// Метод вывода формы обратной связи с данными, введенными пользователями сайта
        /// </summary>
        /// <param name="customer">Модель пользователя сайта</param>
        /// <param name="textAreaForm">Данные формы ввода типа IFormCollection</param>
        /// <returns></returns>
        [HttpPost]
		public async Task<IActionResult> FeedBackForm(CorrespondenceViewModel model, IFormCollection textAreaForm)
		{
			if (ModelState.IsValid)
			{
                var user = Feedback.GetUser(model, _UserManager);
                
                if (user != null)
                {
                    if (!user.EmailConfirmed)
                    {
                        return RedirectToAction("NotConfirmedEmail", "Account");
                    }

                    // находим asker по параметрам или создаем новый объект
                    var asker = Feedback.GetAsker(model, _AllAskers);
                    
                    if(asker == null)
                    {
                        asker = new Asker(model.Name, model.Surname, model.Email, model.Gender, model.BirthDay);
                        
                        var isCustomer = Feedback.IsCustomer(model, _AllCustomers);
                        if (isCustomer)
                        {
                            asker.IsCustomer = true;
                        }

                        // сохраняем объект типа Asker в БД
                        _AllAskers.SaveItem(asker, asker.Id);
                    }

                    model.Question = textAreaForm["textArea"].ToString();
                    model.QuestionDate = DateTime.Now;

                    var correspondence = new Correspondence(model.Question, model.QuestionDate, asker.Id, asker.IsCustomer);

                    // сохраняем объект типа correspondence в БД
                    _AllCorrespondences.SaveItem(correspondence, correspondence.Id);
                    
                    // отправляем через форму обратной связи вопрос турфирме от пользователя
                    var questionToCompany = new EmailService();
                    var isSendMessage = questionToCompany.SendEmailFromClientAsync(model.Email, $"{model.Name} {model.Surname}", model.Question);

                    if(isSendMessage.Id != 0)
                    {
                        // автоматически формируется ответ турфирмы
                        var answerToPerson = new EmailService();
                        await answerToPerson.SendEmailAsync(model.Email, $"Ответ для {model.Name} {model.Surname}", $"Спасибо, что обратились к нам. На Ваш вопрос:\n{model.Question}\n в ближайшее время будет подготовлен ответ");
                    }
                    
                    var userRoles = await _UserManager.GetRolesAsync(user);
                    if (!userRoles.Contains("asker"))
                    {
                        // устанавливаем роль "asker"
                        Feedback.AddToRole(user, "asker", _UserManager);
                    }

                    return View("Success", model);
                }
                else
                {
                    return RedirectToAction("Register", "Account");
                }
            }

            // 1.2. Пользователь не зарегистрирован или не прошел подтверждение email
            model.Question = textAreaForm["textArea"].ToString();
            model.QuestionDate = DateTime.Now;

            //WriteLogs("FeedBackForm не прошла валидацию. ", NLogsModeEnum.Warn);
            //WriteLogs("Возвращено /About/FeedBackForm.cshtml\n", NLogsModeEnum.Trace);

            return View(model);
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
