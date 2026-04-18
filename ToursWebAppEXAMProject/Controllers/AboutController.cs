using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.ViewModels;
using ToursWebAppEXAMProject.Utils;
using ToursWebAppEXAMProject.Services.Email;
using NLog;
using ToursWebAppEXAMProject.Models;

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
			var model = _AboutUtils.GetModel();
            _logger.Debug("Получена модель AboutPageVersion. ");

            if (model == null || model.Id == 0)
            { 
                _logger.Warn("Стартовая страница /About/Index.cshtml не создана или ни одна из версий страницы не является актуальной");

                _logger.Trace("Переход по маршруту /About/Sorry.\n");
                return View("Sorry");
            }

            _logger.Trace("Переход по маршруту /About/Index.\n");
            return View(model);
		}


        /// <summary>
        /// Метод вывода меню редактирования страницы About
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        public IActionResult EditMenuAboutPage()
        {
            var allPages = _AboutUtils.GetAllModels();
            _logger.Debug("Получена модель IEnumerable<AboutPageVersion>. ");

            _logger.Trace("Переход по маршруту /About/EditMenuAboutPage.\n");
            return View(allPages);
        }


        /// <summary>
        /// Метод создания новой версии страницы About
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        public IActionResult CreateAboutPage()
        {
            var newViewModel = _AboutUtils.CreateModel();
            _logger.Debug("Создана модель AboutPageVersion. ");

            _logger.Trace("Переход по маршруту /About/EditAboutPage.\n");
            return View("EditAboutPage", newViewModel);
        }


        /// <summary>
        /// Метод вывода на редактирование страницы About по ее id
        /// </summary>
        /// <param name="id">идентификатор страницы About</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        public IActionResult EditAboutPage(int id)
        {
            var editModel = _AboutUtils.GetModel(id);
            _logger.Debug($"Получена модель AboutPageVersion по id={id}. ");

            _logger.Trace("Переход по маршруту /About/EditAboutPage.\n");
            return View(editModel);
        }


        /// <summary>
        /// Метод удаления страницы About по ее id
        /// </summary>
        /// <param name="id">идентификатор страницы About</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        public IActionResult DeleteAboutPage(int id)
        {
            var deleteModel = _AboutUtils.GetModel(id);
            _AboutUtils.DeleteModel(id);
            _logger.Debug($"Удалена модель AboutPageVersion по id={id}. ");

            _logger.Trace("Возвращено ../Shared/SuccessForDelete.cshtml\n");
            return View("SuccessForDelete", deleteModel);
        }


        [Authorize(Roles = "superadmin,editor")]
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
        /// <param name="model">Модель AboutPageVersion</param>
        /// <param name="MainImagePath">Путь к главной картинке</param>
        /// <param name="AboutImagePath">Путь к картинке About</param>
        /// <param name="DetailsImagePath">Путь к картинке деталей</param>
        /// <param name="OperationModeImagePath">Путь к картинке операций</param>
        /// <param name="PhotoGalleryImagePath">Путь к картинке фотогалереи</param>
        /// <param name="FeedbackImagePath">Путь к картинке формы обратной связи</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpPost]
        public async Task<IActionResult> SaveAboutPage(AboutPageVersion model, IFormFile? MainImagePath, IFormFile? AboutImagePath, IFormFile? DetailsImagePath, IFormFile? OperationModeImagePath, IFormFile? PhotoGalleryImagePath, IFormFile? FeedbackImagePath)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Debug("Модель AboutPageVersion прошла валидацию. ");
                    
                    var newModel = await _AboutUtils.SetEditAboutViewModelAndSaveAsync
                        (model, MainImagePath, AboutImagePath, DetailsImagePath, OperationModeImagePath, PhotoGalleryImagePath, FeedbackImagePath);
                    _logger.Debug("Модель AboutPageVersion заполнена данными из формыи успешно сохранена в БД. ");
           
                    _logger.Trace("Возвращено ../Shared/Success.cshtml\n");
                    return View("Success", newModel);
                }
                else
                {
                    _logger.Warn("Модель AboutPageVersion не прошла валидацию.");

                    foreach (var modelState in ModelState)
                    {
                        foreach (var error in modelState.Value.Errors)
                        {
                            _logger.Warn($"Ошибка поля {modelState.Key}: {error.ErrorMessage}");
                        }
                    }

                    return View("EditAboutPage", model);
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
        /// <returns></returns>
        [HttpPost]
		public async Task<IActionResult> FeedBackForm(CorrespondenceViewModel model)
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
                        

                        /*model.Question = textAreaForm["textArea"].ToString();*/
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

                    /*model.Question = textAreaForm["textArea"].ToString();*/
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
