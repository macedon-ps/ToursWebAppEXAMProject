using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Services.Email;
using ToursWebAppEXAMProject.Services.ImageStorage;
using ToursWebAppEXAMProject.Utils;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Controllers
{
    public class AboutController : Controller
	{
        private readonly AboutUtils _AboutUtils;
        private readonly ImageStorageService _ImageStorageService;
        private readonly FeedbackUtils _FeedbackUtils;
        private readonly EmailService _EmailService;
        private readonly ILogger<AboutController> _logger;

        public AboutController(AboutUtils AboutUtils, ImageStorageService ImageStorageService, FeedbackUtils FeedbackUtils, EmailService EmailService, ILogger<AboutController> logger)
		{
            _AboutUtils = AboutUtils;
            _ImageStorageService = ImageStorageService;
            _FeedbackUtils = FeedbackUtils;
            _EmailService = EmailService;
            _logger = logger;
        }
        

        /// <summary>
        /// Метод вывода стартовой страницы About
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
		{
			var model = _AboutUtils.GetModel();
            _logger.LogDebug("Получена модель AboutPageVersion. ");

            if (model == null || model.Id == 0)
            { 
                _logger.LogWarning("Стартовая страница /About/Index.cshtml не создана или ни одна из версий страницы не является актуальной");
                _logger.LogTrace("Переход по маршруту /About/Sorry.\n");
                return View("Sorry");
            }

            _logger.LogTrace("Переход по маршруту /About/Index.\n");
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
            _logger.LogDebug("Получена модель IEnumerable<AboutPageVersion>. ");

            _logger.LogTrace("Переход по маршруту /About/EditMenuAboutPage.\n");
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
            _logger.LogDebug("Создана модель AboutPageVersion. ");

            _logger.LogTrace("Переход по маршруту /About/EditAboutPage.\n");
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
            _logger.LogDebug($"Получена модель AboutPageVersion по id={id}. ");

            _logger.LogTrace("Переход по маршруту /About/EditAboutPage.\n");
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
            _logger.LogDebug($"Удалена модель AboutPageVersion по id={id}. ");

            _logger.LogTrace("Возвращено ../Shared/SuccessForDelete.cshtml\n");
            return View("SuccessForDelete", deleteModel);
        }

       
        [Authorize(Roles = "superadmin,editor")]
        public IActionResult DeletePicture(string relativePathToFile)
        {
            _ImageStorageService.DeletePhoto(relativePathToFile);
            _logger.LogDebug($"Удалена картинка по пути: {relativePathToFile}. ");

            _logger.LogTrace("Переход по маршруту /About/DeletePicture.\n");
            return View("DeletePicture", relativePathToFile);
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
        public async Task<IActionResult> SaveAboutPage(AboutPageVersion model, IFormFile? MainImagePath, IFormFile? AboutImagePath, IFormFile? DetailsImagePath, IFormFile? OperationModeImagePath, IFormFile? PhotoGalleryImagePath, IFormFile? CollectionImages, IFormFile? FeedbackImagePath)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogDebug("Модель AboutPageVersion прошла валидацию. ");
                    
                    var newModel = await _AboutUtils.SetAboutPageVersionAndSaveAsync
                        (model, MainImagePath, AboutImagePath, DetailsImagePath, OperationModeImagePath, PhotoGalleryImagePath, CollectionImages, FeedbackImagePath);
                    _logger.LogDebug("Модель AboutPageVersion заполнена данными из формыи успешно сохранена в БД. ");
           
                    _logger.LogTrace("Возвращено ../Shared/Success.cshtml\n");
                    return View("Success", newModel);
                }
                else
                {
                    _logger.LogWarning("Модель AboutPageVersion не прошла валидацию.");

                    foreach (var modelState in ModelState)
                    {
                        foreach (var error in modelState.Value.Errors)
                        {
                            _logger.LogWarning($"Ошибка поля {modelState.Key}: {error.ErrorMessage}");
                        }
                    }

                    return View("EditAboutPage", model);
                }
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Ошибка при получении модели AboutPageVersion.");
                _logger.LogTrace("Возвращено ../Shared/Error.cshtml\n");

                var errorInfo = new ErrorViewModel()
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };

                return View("Error", errorInfo);
            }
        }


        /// <summary>
        /// Метод вывода формы обратной связи с пользователями сайта
        /// </summary>
        /// <returns></returns>
        public IActionResult FeedBackForm()
        {
            var viewModel = new CorrespondenceViewModel();
            _logger.LogDebug("Создаем вью-модель CorrespondenceViewModel");

            _logger.LogTrace("Переход по маршруту /About/FeedBackForm.\n");
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
                    _logger.LogDebug("Вью-модель CorrespondenceViewModel прошла валидацию. ");

                    var user = _FeedbackUtils.GetUser(model);

                    if (user != null)
                    {
                        _logger.LogDebug($"Получен зарегистрированный пользователь {user.UserName}. ");
                        
                        if (!user.EmailConfirmed)
                        {
                            _logger.LogDebug("В доступе отказано, т.к. пользователь не подтвердил свой email");
                            _logger.LogTrace("Переход по маршруту / Account / NotConfirmedEmail.\n");
                            return RedirectToAction("NotConfirmedEmail", "Account");
                        }

                        // находим asker по параметрам, если null, то создаем новый объект
                        var asker = _FeedbackUtils.GetAsker(model);

                        if (asker == null)
                        {
                            asker = _FeedbackUtils.CreateAsker(model);

                            // сохраняем объект типа Asker в БД
                            _FeedbackUtils.SaveAsker(asker);
                            _logger.LogDebug($"Создан новый asker {asker.Name} с email: {asker.Email}");
                        }
                        else
                        {
                            _logger.LogDebug($"Получен asker {asker.Name} с email: {asker.Email}");
                        }
                        

                        /*model.Question = textAreaForm["textArea"].ToString();*/
                        model.QuestionDate = DateTime.Now;

                        var correspondence = _FeedbackUtils.CreateCorrespondence(model.Question, model.QuestionDate, asker.Id, asker.IsCustomer);
                        
                        // сохраняем объект типа correspondence в БД
                        _FeedbackUtils.SaveCorrespondence(correspondence);
                        _logger.LogDebug("Создано новое сообщение и сохранено в БД");

                        // отправляем через форму обратной связи вопрос турфирме от пользователя
                        var isSendMessage = _EmailService.SendEmailFromClientAsync(model.Email, $"{model.Name} {model.Surname}", model.Question);
                        _logger.LogDebug($"Пришло сообщение по email от {model.Email}");
                        if (isSendMessage.Id != 0)
                        {
                            // автоматически формируется ответ турфирмы
                            await _EmailService.SendEmailAsync(model.Email, $"Ответ для {model.Name} {model.Surname}", $"Спасибо, что обратились к нам. На Ваш вопрос:\n{model.Question}\n в ближайшее время будет подготовлен ответ");
                            _logger.LogDebug($"Сформирован и отправлен по email ответ для {model.Email}");
                        }

                        var userRoles = await _FeedbackUtils.GetAllRolesForUser(user);
                        if (!userRoles.Contains("asker"))
                        {
                            // устанавливаем роль "asker"
                            _FeedbackUtils.AddToRole(user, "asker");
                            _logger.LogDebug($"Для {user.UserName} присвоена роль asker");
                        }

                        _logger.LogTrace("Возвращено ../Shared/Success.cshtml\n");
                        return View("Success", model);
                    }
                    else
                    {
                        _logger.LogDebug($"Данный пользователь не зарегистрирован. ");

                        _logger.LogTrace("Перенапраление по маршруту /Account/Register.\n");
                        return RedirectToAction("Register", "Account");
                    }
                }
                else
                {
                    _logger.LogDebug("Вью-модель CorrespondenceViewModel не прошла валидацию. ");

                    model.QuestionDate = DateTime.Now;

                    _logger.LogTrace("Возвращено /About/FeedBackForm.\n");
                    return View(model);
                }
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Ошибка при получении вью-модели CorrespondenceViewModel.");
                _logger.LogTrace("Возвращено ../Shared/Error.cshtml\n");

                var errorInfo = new ErrorViewModel()
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };

                return View("Error", errorInfo);
            }
		}
	}
}
