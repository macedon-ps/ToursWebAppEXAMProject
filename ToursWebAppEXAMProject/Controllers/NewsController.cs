using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Utils;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Controllers
{
    public class NewsController : Controller
    {
        
        private readonly NewsUtils _NewsUtils;
        private readonly ILogger<NewsController> _logger;

        public NewsController(NewsUtils NewsUtils, ILogger<NewsController> logger)
        {
            _NewsUtils = NewsUtils;
            _logger = logger;
        }


        /// <summary>
        /// Метод вывода всех новостей
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllNews()
        {
            var newsItems = _NewsUtils.GetNews();
            _logger.LogDebug("Получена модель IEnumerable<New>. ");

            if (newsItems == null)
            {
                _logger.LogWarning("В БД нет ни одной новости. "); 
                _logger.LogTrace("Возвращено ../Shared/Nothing.cshtml.\n");
                return View("Nothing", new NothingViewModel("В БД нет ни одной новости."));
            }
            else
            {
                _logger.LogDebug("Выводятся все новости. ");

                _logger.LogTrace("Переход по маршруту /News/GetAllNews.\n");
                return View(newsItems);
            }
        }


        /// <summary>
        /// Метод вывода новости по ее id
        /// </summary>
        /// <param name="id">уникальный идентификатор новости</param>
        /// <returns></returns>
        public IActionResult GetNews(int id)
        {
            var newsItem = _NewsUtils.GetNewsById(id);
            _logger.LogDebug($"Получена модель New по id = {id}. ");

            if (newsItem.Id == 0)
            {
                _logger.LogWarning($"В БД нет новости с id = {id}. ");
                _logger.LogTrace("Возвращено ../Shared//Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет новости с id = {id}.\n"));
            }
            else
            {
                _logger.LogDebug($"Выводится новость с id = {id}. ");

                _logger.LogTrace($"Переход по маршруту /News/GetNews?id={id}.\n");
                return View(newsItem);
            }
        }


        /// <summary>
        /// Метод создания новости
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        public IActionResult CreateNews()
        {
            var newsItem = new New();
            _logger.LogDebug("Создается модель New. ");

            _logger.LogTrace("Переход по маршруту /News/EditNews.cshtml\n");
            return View("EditNews", newsItem);
        }


        /// <summary>
        /// Метод редактирования новости по ее id
        /// </summary>
        /// <param name="id">универсальный идентификатор новости</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult EditNews(int id)
        {
            var newsItem = _NewsUtils.GetNewsById(id);
            _logger.LogDebug($"Получена модель New по id={id}. ");

            _logger.LogTrace("Переход по маршруту /News/EditNews.\n");
            return View(newsItem);
        }


        /// <summary>
        /// Метод вывода результатов выборки новостей по тому, что ищем - полное название или ключевое слово (букву)
        /// </summary>
        /// <param name="isFullName">полное название - true, ключевое слово (буква) - false</param>
        /// <param name="insertedText">текст для поиска</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult GetQueryResultNews(bool isFullName, string insertedText)
        {
            var newsItems = _NewsUtils.QueryResult(isFullName, insertedText);
            _logger.LogDebug("Получена модель IEnumerable<New>. ");
                     
            if (newsItems == null)
            {
                _logger.LogWarning($"По результатам запроса получен пустой список новостей по запросу \"...{insertedText}...\". ");
                _logger.LogTrace("Возвращено ../Shared//Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет новостей по запросу \"...{insertedText}...\"."));
            }
            else
            {
                _logger.LogDebug("Получен список новостей по результатам запроса. ");
                _logger.LogDebug($"Выводятся все новости по запросу. ");

                _logger.LogTrace("Переход по маршруту /News/GetQueryResultNews.\n");
                return View(newsItems);
            }
        }


        /// <summary>
        /// Метод удаления отдельной новости по ее id
        /// </summary>
        /// <param name="id">универсальный идентификатор новости</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult DeleteNews(int id)
        {
            var newsItem = _NewsUtils.GetNewsById(id);
            if(newsItem!=null)
            {
                _NewsUtils.DeleteNewsById(newsItem);
            }
            _logger.LogDebug($"Удалена новость по id={id}. ");

            _logger.LogTrace("Переход по маршруту ../Shared/SuccessForDelete.cshtml\n");
            return View("SuccessForDelete", newsItem);
        }


        /// <summary>
        /// Метод сохранения новости с данными, введенными пользователем
        /// </summary>
        /// <param name="newsModel">Модель новости</param>
        /// <param name="titleImagePath">Данные формы ввода типа IFormFile</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpPost]
        public async Task<IActionResult> SaveNews(New newsModel, IFormFile? titleImagePath)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogDebug("Модель News прошла валидацию. ");

                    // если мы хотим поменять картинку
                    if (titleImagePath != null)
                    {
                        var uploadImage = await _NewsUtils.SaveNewImageByFileNameAsync(titleImagePath, newsModel.Id);
                        newsModel.TitleImagePath = uploadImage.Url;
                        newsModel.ImagePublicId = uploadImage.PublicId;
                    }

                    newsModel.DateAdded = DateTime.Now;
                    _NewsUtils.SaveNewsModel(newsModel);
                    _logger.LogDebug("Новость успешно сохранена в БД. ");
                    
                    _logger.LogTrace("Переход по маршруту ../Shared/Success.cshtml\n");
                    return View("Success", newsModel);
                }
                else
                {
                    _logger.LogWarning("Модель New не прошла валидацию. ");
                                        
                    _logger.LogTrace("Возвращено /News/EditNews.cshtml\n");
                    return View("EditNews", newsModel);
                }
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Ошибка при обработке модели New.");
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
