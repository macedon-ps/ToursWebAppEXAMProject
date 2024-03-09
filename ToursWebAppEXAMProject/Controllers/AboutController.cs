using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.ViewModels;
using static TourWebAppEXAMProject.Services.LogsMode.LogsMode;
using TourWebAppEXAMProject.Utils;
using TourWebAppEXAMProject.Services.Email;

namespace ToursWebAppEXAMProject.Controllers
{
    public class AboutController : Controller
	{
        private readonly AboutUtils _AboutUtils;
        private readonly FileUtils _FileUtils;
        private readonly FeedbackUtils _FeedbackUtils;
        private readonly TechTaskUtils _TechTaskUtils;
        private readonly EmailService _EmailService;

        public AboutController(AboutUtils AboutUtils, FileUtils FileUtils, FeedbackUtils FeedbackUtils, TechTaskUtils TechTaskUtils, EmailService EmailService)
		{
            _AboutUtils = AboutUtils;
            _FileUtils = FileUtils;
            _FeedbackUtils = FeedbackUtils;
            _TechTaskUtils = TechTaskUtils;
            _EmailService = EmailService;
        }
        
        /// <summary>
        /// Метод вывода стартовой страницы About
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
		{
			WriteLogs("Переход по маршруту /About/Index.\n", NLogsModeEnum.Trace);

            var viewModel = _AboutUtils.GetModel();
            
            if (viewModel == null || viewModel.Id == 0)
            {
                return View("Sorry");
            }

            return View(viewModel);
		}

        /// <summary>
        /// Метод вывода меню редактирования страницы About
        /// </summary>
        /// <returns></returns>
        public IActionResult EditMenuAboutPage()
        {
            var allPages = _AboutUtils.GetAllModel();

            return View(allPages);
        }

        /// <summary>
        /// Метод создания новой версии страницы About
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateAboutPage()
        {
            var newViewModel = _AboutUtils.CreateModel();
            
            return View("EditAboutPage", newViewModel);
        }

        /// <summary>
        /// Метод вывода на редактирование страницы About по ее id
        /// </summary>
        /// <param name="id">идентификатор страницы About</param>
        /// <returns></returns>
        public IActionResult EditAboutPage(int id)
        {
            var editViewModel = _AboutUtils.GetModel(id);
            
            return View(editViewModel);
        }

        /// <summary>
        /// Метод удаления страницы About по ее id
        /// </summary>
        /// <param name="id">идентификатор страницы About</param>
        /// <returns></returns>
        public IActionResult DeleteAboutPage(int id)
        {
            var deleteModel = _AboutUtils.GetModel(id);
            _AboutUtils.DeleteModel(id);
            
           WriteLogs("Возвращено ../Shared/SuccessForDelete.cshtml\n", NLogsModeEnum.Trace);

            return View("../Shared/SuccessForDelete", deleteModel);
        }

        public IActionResult DeletePicture(string fullPathToFile)
        {
            _FileUtils.DeletePhoto(fullPathToFile);
            
            return View("DeletePicture", fullPathToFile);
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
        public IActionResult SaveAboutPage(EditAboutPageViewModel viewModel, IFormCollection formValues, IFormFile? changeMainImagePath, IFormFile? changeAboutImagePath, IFormFile? changeDetailsImagePath, IFormFile? changeOperationModeImagePath, IFormFile? changePhotoGalleryImagePath, IFormFile? changeFeedbackImagePath)
        {
            WriteLogs("Запущен процесс сохранения вью модели EditAboutPageViewMode в БД. ", NLogsModeEnum.Debug);

            if (ModelState.IsValid)
            {
                WriteLogs("Вью модель EditAboutPageViewMode прошла валидацию. ", NLogsModeEnum.Debug);

                var newViewModel =  _AboutUtils.SetEditAboutViewModelAndSave(viewModel, formValues, changeMainImagePath, changeAboutImagePath, changeDetailsImagePath, changeOperationModeImagePath, changePhotoGalleryImagePath, changeFeedbackImagePath);

                WriteLogs("Вью модель с данными страницы About успешно сохранена в БД. ", NLogsModeEnum.Debug);
                WriteLogs("Возвращено ../Shared/Success.cshtml\n", NLogsModeEnum.Trace);

                return View("../Shared/Success", newViewModel);
            }
            else
            {
                WriteLogs("Вью модель с данными страницы About не прошла валидацию. ", NLogsModeEnum.Warn);
                WriteLogs("Возвращено EditAboutPage.cshtml\n", NLogsModeEnum.Trace);

                viewModel = _AboutUtils.SetEditAboutViewByFormValues(viewModel, formValues);
                
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
                var user = _FeedbackUtils.GetUser(model);
                
                if (user != null)
                {
                    if (!user.EmailConfirmed)
                    {
                        return RedirectToAction("NotConfirmedEmail", "Account");
                    }

                    // находим asker по параметрам, если null, то создаем новый объект
                    var asker = _FeedbackUtils.GetAsker(model);
                    
                    if(asker == null)
                    {
                        asker = _FeedbackUtils.CreateAsker(model);

                        // сохраняем объект типа Asker в БД
                        _FeedbackUtils.SaveAsker(asker);
                    }

                    model.Question = textAreaForm["textArea"].ToString();
                    model.QuestionDate = DateTime.Now;

                    var correspondence = _FeedbackUtils.CreateCorrespondence(model.Question, model.QuestionDate, asker.Id, asker.IsCustomer);

                    // сохраняем объект типа correspondence в БД
                    _FeedbackUtils.SaveCorrespondence(correspondence);
                                                           
                    // отправляем через форму обратной связи вопрос турфирме от пользователя
                    var isSendMessage = _EmailService.SendEmailFromClientAsync(model.Email, $"{model.Name} {model.Surname}", model.Question);

                    if(isSendMessage.Id != 0)
                    {
                        // автоматически формируется ответ турфирмы
                        await _EmailService.SendEmailAsync(model.Email, $"Ответ для {model.Name} {model.Surname}", $"Спасибо, что обратились к нам. На Ваш вопрос:\n{model.Question}\n в ближайшее время будет подготовлен ответ");
                    }
                    
                    var userRoles = await _FeedbackUtils.GetAllRolesForUser(user);
                    if (!userRoles.Contains("asker"))
                    {
                        // устанавливаем роль "asker"
                        _FeedbackUtils.AddToRole(user, "asker");
                    }

                    return View("Success", model);
                }
                else
                {
                    return RedirectToAction("Register", "Account");
                }
            }

            // Пользователь не зарегистрирован или не прошел подтверждение email
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
            
			var model = _TechTaskUtils.GetTechTaskForPage("About");

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
                
				_TechTaskUtils.SetTechTaskProgressAndSave(model);

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
