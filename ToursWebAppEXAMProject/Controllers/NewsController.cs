using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.FileUtilities;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
    public class NewsController : Controller
    {
        private readonly IBaseInterface<New> _AllNews;
        
        public NewsController(IBaseInterface<New> News)
        {
            this._AllNews = News;
        }

        /// <summary>
        /// Метод вывода всех новостей
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllNews()
        {
            WriteLogs("Переход по маршруту /News/GetAllNews. ", NLogsModeEnum.Trace);

            var newsItems = _AllNews.GetAllItems();

            if (newsItems == null)
            {
                var errorMessage = "В БД нет ни одной новости";
                var errorInfo = new ErrorViewModel(errorMessage);

                WriteLogs($"{errorMessage}. Возвращено ../Shared/Error.cshtml.\n", NLogsModeEnum.Warn);

                return View("../Shared/Error", errorInfo);
            }

            WriteLogs("Выводятся все новости.\n", NLogsModeEnum.Debug);

            return View(newsItems);
        }

        /// <summary>
        /// Метод вывода новости по ее id
        /// </summary>
        /// <param name="id">уникальный идентификатор новости</param>
        /// <returns></returns>
        public IActionResult GetNews(int id)
        {
            WriteLogs($"Переход по маршруту /News/GetNesw?id={id}. ", NLogsModeEnum.Trace);

            var newsItem = _AllNews.GetItemById(id);

            if (newsItem.Id == 0)
            {
                var errorMessage = $"В БД нет новости с id = {id}";
                var errorInfo = new ErrorViewModel(errorMessage);

                WriteLogs($"{errorMessage}. Возвращено ../Shared//Error.cshtml\n", NLogsModeEnum.Warn);

                return View("../Shared/Error", errorInfo);
            }

            WriteLogs($"Выводится новость с id = {id}.\n", NLogsModeEnum.Debug);

            return View(newsItem);
        }

        /// <summary>
        /// Метод создания новости
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        public IActionResult CreateNews()
        {
            WriteLogs("Выполняется действие /News/CreateNews. ", NLogsModeEnum.Trace);

            var newsItem = new New();

            WriteLogs("Возвращено /News/EditNews.cshtml\n", NLogsModeEnum.Trace);

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
            WriteLogs("Переход по маршруту /News/EditNews. ", NLogsModeEnum.Trace);

            var newsItem = _AllNews.GetItemById(id);
            newsItem.DateAdded = DateTime.Now;
                        
           return View(newsItem);
        }

        /// <summary>
        /// Метод вывода результатов выборки новостей по тому, что ищем - полное название или ключевое слово (букву)
        /// </summary>
        /// <param name="isFullName">полное название - true, ключевое слово (буква) - false</param>
        /// <param name="fullNameOrKeywordOfItem">текст для поиска</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult GetQueryResultNews(bool isFullName, string fullNameOrKeywordOfItem)
        {
            WriteLogs("Переход по маршруту /News/GetQueryResultNews. ", NLogsModeEnum.Trace);

            var newsItems = _AllNews.GetQueryResultItemsAfterFullName(fullNameOrKeywordOfItem, isFullName);
            var numberNews = newsItems.Count();
         
            if (numberNews == 0)
            {
                var message = $"Нет новостей по запросу \"{fullNameOrKeywordOfItem}\". Возвращено ../Shared/Nothing.cshtml\n";

                WriteLogs(message, NLogsModeEnum.Warn);

                var nothingInfo = new ErrorViewModel(message);
                return View("../Shared/Nothing", nothingInfo);
            }

            WriteLogs($"Выводятся все новости по запросу \"{fullNameOrKeywordOfItem}\".\n", NLogsModeEnum.Debug);

            return View(newsItems);
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
            var newsItem = _AllNews.GetItemById(id);
            _AllNews.DeleteItem(newsItem, id);

            WriteLogs("Возвращено ../Shared/SuccessForDelete.cshtml\n", NLogsModeEnum.Trace);

            return View("../Shared/SuccessForDelete", newsItem);
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
            WriteLogs("Запущен процесс сохранения новости в БД. ", NLogsModeEnum.Debug);

            if (ModelState.IsValid)
            {
                WriteLogs("Модель New прошла валидацию. ", NLogsModeEnum.Debug);

                // если мы хотим поменять картинку
                if (changeTitleImagePath != null)
                {
                    var folder = "/images/NewsTitleImages/";
                    FileUtils.SaveFileIfExistPath(folder, changeTitleImagePath);
                    newsItem.TitleImagePath = $"{folder}{changeTitleImagePath.FileName}";
                }

                newsItem.FullDescription = formValues["fullInfoAboutNew"];
                newsItem.DateAdded = DateTime.Now;

                _AllNews.SaveItem(newsItem, newsItem.Id);

                WriteLogs("Новость успешно сохранена в БД. ", NLogsModeEnum.Debug);
                WriteLogs("Возвращено ../Shared/Success.cshtml\n", NLogsModeEnum.Trace);

                return View("../Shared/Success", newsItem);
            }
            else
            {
                WriteLogs("Модель New не прошла валидацию. ", NLogsModeEnum.Warn);
                WriteLogs("Возвращено /News/EditNews.cshtml\n", NLogsModeEnum.Trace);

                newsItem.FullDescription = formValues["fullInfoAboutNew"];

                return View("EditNews", newsItem);
            }
        }
    }
}
