using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using ToursWebAppEXAMProject.Utils;

namespace ToursWebAppEXAMProject.Controllers
{
    public class NewsController : Controller
    {
        
        private readonly NewsUtils _NewsUtils;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public NewsController(NewsUtils NewsUtils)
        {
            _NewsUtils = NewsUtils;
        }

        /// <summary>
        /// Метод вывода всех новостей
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllNews()
        {
            var newsItems = _NewsUtils.GetNews();
            _logger.Debug("Получена модель IEnumerable<New>. ");

            if (newsItems == null)
            {
                _logger.Warn("В БД нет ни одной новости. "); 

                _logger.Trace("Возвращено ../Shared/Nothing.cshtml.\n");
                return View("Nothing", new NothingViewModel("В БД нет ни одной новости."));
            }
            else
            {
                _logger.Debug("Выводятся все новости. ");

                _logger.Trace("Переход по маршруту /News/GetAllNews.\n");
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
            _logger.Debug($"Получена модель New по id = {id}. ");

            if (newsItem.Id == 0)
            {
                _logger.Warn($"В БД нет новости с id = {id}. ");

                _logger.Trace("Возвращено ../Shared//Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет новости с id = {id}.\n"));
            }
            else
            {
                _logger.Debug($"Выводится новость с id = {id}. ");

                _logger.Trace($"Переход по маршруту /News/GetNesw?id={id}.\n");
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
            _logger.Debug("Создается модель New. ");

            _logger.Trace("Переход по маршруту /News/EditNews.cshtml\n");
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
            var newsItem = _NewsUtils.GetNewsForEdit(id);
            _logger.Debug($"Получена модель New по id={id}. ");

            _logger.Trace("Переход по маршруту /News/EditNews.\n");
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
            _logger.Debug("Получена модель IEnumerable<New>. ");
                     
            if (newsItems == null)
            {
                _logger.Warn($"По результатам запроса получен пустой список новостей по запросу \"...{insertedText}...\". ");

                _logger.Trace("Возвращено ../Shared//Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет новостей по запросу \"...{insertedText}...\"."));
            }
            else
            {
                _logger.Debug("Получен список новостей по результатам запроса. ");
                _logger.Debug($"Выводятся все новости по запросу. ");

                _logger.Trace("Переход по маршруту /News/GetQueryResultNews.\n");
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
            _logger.Debug($"Удалена новость по id={id}. ");

            _logger.Trace("Переход по маршруту ../Shared/SuccessForDelete.cshtml\n");
            return View("SuccessForDelete", newsItem);
        }

        /// <summary>
        /// Метод сохранения новости с данными, введенными пользователем
        /// </summary>
        /// <param name="newsItem">Модель новости</param>
        /// <param name="formValues">Данные формы ввода типа IFormCollection</param>
        /// <param name="changeTitleImagePath">Данные формы ввода типа IFormFile</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpPost]
        public async Task<IActionResult> SaveNews(New newsItem, IFormCollection formValues, IFormFile? changeTitleImagePath)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Debug("Модель New прошла валидацию. ");

                    // если мы хотим поменять картинку
                    if (changeTitleImagePath != null)
                    {
                        await _NewsUtils.SaveImagePathAsync(changeTitleImagePath);
                    }

                    newsItem = _NewsUtils.SetNewsModel(newsItem, formValues, changeTitleImagePath);
                    _NewsUtils.SaveNews(newsItem);
                    _logger.Debug("Новость успешно сохранена в БД. ");
                    
                    _logger.Trace("Переход по маршруту ../Shared/Success.cshtml\n");
                    return View("Success", newsItem);
                }
                else
                {
                    _logger.Warn("Модель New не прошла валидацию. ");
                    newsItem = _NewsUtils.SetNewsModelByFormValues(newsItem, formValues);
                    
                    _logger.Trace("Возвращено /News/EditNews.cshtml\n");
                    return View("EditNews", newsItem);
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
