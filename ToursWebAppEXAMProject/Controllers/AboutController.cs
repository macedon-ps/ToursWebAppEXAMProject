using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.ViewModels;
using ToursWebAppEXAMProject.Utils;
using ToursWebAppEXAMProject.Services.Email;
using NLog;

namespace ToursWebAppEXAMProject.Controllers
{
    public class AboutController : Controller
	{
        private readonly AboutUtils _AboutUtils;
        private readonly FileUtils _FileUtils;
        private readonly FeedbackUtils _FeedbackUtils;
        private readonly TechTaskUtils _TechTaskUtils;
        private readonly EmailService _EmailService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

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
			var viewModel = _AboutUtils.GetModel();
            _logger.Debug("Получена вью-модель EditAboutPageViewModel. ");

            if (viewModel == null || viewModel.Id == 0)
            {
                _logger.Warn("Стартовая страница /About/Index.cshtml не создана или ни одна из версий страницы не является актуальной");

                _logger.Trace("Переход по маршруту /About/Sorry.\n");
                return View("Sorry");
            }

            _logger.Trace("Переход по маршруту /About/Index.\n");
            return View(viewModel);
		}

        /// <summary>
        /// Метод вывода меню редактирования страницы About
        /// </summary>
        /// <returns></returns>
        public IActionResult EditMenuAboutPage()
        {
            var allPages = _AboutUtils.GetAllModel();
            _logger.Debug("Получена вью-модель IEnumerable<EditAboutPageViewModel>. ");

            _logger.Trace("Переход по маршруту /About/EditMenuAboutPage.\n");
            return View(allPages);
        }

        /// <summary>
        /// Метод создания новой версии страницы About
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateAboutPage()
        {
            var newViewModel = _AboutUtils.CreateModel();
            _logger.Debug("Создана вью-модель EditAboutPageViewModel. ");

            _logger.Trace("Переход по маршруту /About/EditAboutPage.\n");
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
            _logger.Debug($"Получена вью-модель EditAboutPageViewModel по id={id}. ");

            _logger.Trace("Переход по маршруту /About/EditAboutPage.\n");
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
            _logger.Debug($"Удалена вью-модель EditAboutPageViewModel по id={id}. ");

            _logger.Trace("Возвращено ../Shared/SuccessForDelete.cshtml\n");
            return View("SuccessForDelete", deleteModel);
        }

        public IActionResult DeletePicture(string fullPathToFile)
        {
            _FileUtils.DeletePhoto(fullPathToFile);
            _logger.Debug($"Удалена картинка по пути: {fullPathToFile}. ");

            _logger.Trace("Переход по маршруту /About/DeletePicture.\n");
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
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Debug("Вью-модель EditAboutPageViewModel прошла валидацию. ");
                    
                    var newViewModel = _AboutUtils.SetEditAboutViewModelAndSave(viewModel, formValues, changeMainImagePath, changeAboutImagePath, changeDetailsImagePath, changeOperationModeImagePath, changePhotoGalleryImagePath, changeFeedbackImagePath);
                    _logger.Debug("Вью-модель EditAboutPageViewModel заполнена данными из формыи успешно сохранена в БД. ");
           
                    _logger.Trace("Возвращено ../Shared/Success.cshtml\n");
                    return View("Success", newViewModel);
                }
                else
                {
                    _logger.Warn("Вью-модель EditAboutPageViewModel не прошла валидацию. ");
                  
                    viewModel = _AboutUtils.SetEditAboutViewByFormValues(viewModel, formValues);
                    _logger.Debug("Вью-модель EditAboutPageViewModel заполнена отдельными данными из формы. ");

                    _logger.Trace("Возвращено EditAboutPage.cshtml\n");
                    return View("EditAboutPage", viewModel);
                }
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);

                _logger.Trace("Возвращено ../Shared/Error.cshtml\n");
                return View("Error", error.Message);
            }
        }

        /// <summary>
        /// Метод вывода формы обратной связи с пользователями сайта
        /// </summary>
        /// <returns></returns>
        public IActionResult FeedBackForm()
        {
            var viewModel = new CorrespondenceViewModel();
            _logger.Debug("Создаем вью-модель CorrespondenceViewModel");

            _logger.Trace("Переход по маршруту /About/FeedBackForm.\n");
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
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Debug("Вью-модель CorrespondenceViewModel прошла валидацию. ");

                    var user = _FeedbackUtils.GetUser(model);

                    if (user != null)
                    {
                        _logger.Debug($"Получен зарегистрированный пользователь {user.UserName}. ");
                        
                        if (!user.EmailConfirmed)
                        {
                            _logger.Debug("В доступе отказано, т.к. пользователь не подтвердил свой email");

                            _logger.Trace("Переход по маршруту / Account / NotConfirmedEmail.\n");
                            return RedirectToAction("NotConfirmedEmail", "Account");
                        }

                        // находим asker по параметрам, если null, то создаем новый объект
                        var asker = _FeedbackUtils.GetAsker(model);

                        if (asker == null)
                        {
                            asker = _FeedbackUtils.CreateAsker(model);

                            // сохраняем объект типа Asker в БД
                            _FeedbackUtils.SaveAsker(asker);
                            _logger.Debug($"Создан новый asker {asker.Name} с email: {asker.Email}");
                        }
                        else
                        {
                            _logger.Debug($"Получен asker {asker.Name} с email: {asker.Email}");
                        }
                        

                        model.Question = textAreaForm["textArea"].ToString();
                        model.QuestionDate = DateTime.Now;

                        var correspondence = _FeedbackUtils.CreateCorrespondence(model.Question, model.QuestionDate, asker.Id, asker.IsCustomer);
                        
                        // сохраняем объект типа correspondence в БД
                        _FeedbackUtils.SaveCorrespondence(correspondence);
                        _logger.Debug("Создано новое сообщение и сохранено в БД");

                        // отправляем через форму обратной связи вопрос турфирме от пользователя
                        var isSendMessage = _EmailService.SendEmailFromClientAsync(model.Email, $"{model.Name} {model.Surname}", model.Question);
                        _logger.Debug($"Пришло сообщение по email от {model.Email}");

                        if (isSendMessage.Id != 0)
                        {
                            // автоматически формируется ответ турфирмы
                            await _EmailService.SendEmailAsync(model.Email, $"Ответ для {model.Name} {model.Surname}", $"Спасибо, что обратились к нам. На Ваш вопрос:\n{model.Question}\n в ближайшее время будет подготовлен ответ");
                            _logger.Debug($"Сформирован и отправлен по email ответ для {model.Email}");
                        }

                        var userRoles = await _FeedbackUtils.GetAllRolesForUser(user);
                        if (!userRoles.Contains("asker"))
                        {
                            // устанавливаем роль "asker"
                            _FeedbackUtils.AddToRole(user, "asker");
                            _logger.Debug($"Для {user.UserName} присвоена роль asker");
                        }

                        _logger.Trace("Возвращено ../Shared/Success.cshtml\n");
                        return View("Success", model);
                    }
                    else
                    {
                        _logger.Debug($"Данный пользователь не зарегистрирован. ");

                        _logger.Trace("Перенапраление по маршруту /Account/Register.\n");
                        return RedirectToAction("Register", "Account");
                    }
                }
                else
                {
                    _logger.Debug("Вью-модель CorrespondenceViewModel не прошла валидацию. ");

                    model.Question = textAreaForm["textArea"].ToString();
                    model.QuestionDate = DateTime.Now;

                    _logger.Trace("Возвращено /About/FeedBackForm.\n");
                    return View(model);
                }
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);

                _logger.Trace("Возвращено ../Shared/Error.cshtml\n");
                return View("Error", new ErrorViewModel(error.Message));
            }
		}

        /// <summary>
        /// Метод вывода ТЗ и прогресса его выполнения для страницы About
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        public IActionResult TechTaskAbout()
		{
            var viewModel = _TechTaskUtils.GetTechTaskForPage("About");
            _logger.Debug("Получена вью-модель TechTaskViewModel. ");

            _logger.Trace("Переход по маршруту About/TechTaskSupport.\n");
            return View(viewModel);
		}

        /// <summary>
        /// Метод редактирования и сохранения данных о прогресса его выполнения ТЗ для страницы About
        /// </summary>
        /// <param name="model">Данные с формы для ТЗ и прогресса его выполнени</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        [HttpPost]
		public IActionResult TechTaskAbout(TechTaskViewModel viewModel)
		{
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Debug("Вью-модель TechTaskViewModel прошла валидацию. ");

                    _TechTaskUtils.SetTechTaskProgressAndSave(viewModel);
                    _logger.Debug("Вью-модель TechTaskViewModel заполнена данными и сохранена. ");

                    _logger.Trace("Возвращено /About/TechTaskHome.cshtml\n");
                    return View(viewModel);
                }
                else
                {
                    _logger.Warn("Вью-модель TechTaskViewModel не прошла валидацию. Данные модели не сохранены. ");

                    _logger.Trace("Возвращено /About/TechTaskHome.cshtml\n");
                    return View(viewModel);
                }
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);

                _logger.Trace("Возвращено ../Shared/Error.cshtml\n");
                return View("Error", new ErrorViewModel(error.Message));
            }
 		}
	}
}
