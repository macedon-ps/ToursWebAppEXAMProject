using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using TourWebAppEXAMProject.Utils;
using static TourWebAppEXAMProject.Services.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBaseInterface<Blog> _AllBlogs;
        private readonly FileUtils _FileUtils;
        
        public BlogsController(IBaseInterface<Blog> Blogs, FileUtils FileUtils)
        {
            _AllBlogs = Blogs;
            _FileUtils = FileUtils;
        }

        /// <summary>
        /// Метод вывода всех блогов
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllBlogs()
        {
            WriteLogs("Переход по маршруту /Blogs/GetAllBlogs. ", NLogsModeEnum.Trace);

            var blogs = _AllBlogs.GetAllItems();

            if (blogs == null)
            {
                var errorMessage = "В БД нет ни одного блога";
                var errorInfo = new ErrorViewModel(errorMessage);
                
                WriteLogs($"{errorMessage}. Возвращено ../Shared/Error.cshtml\n", NLogsModeEnum.Warn);

                return View("../Shared/Error", errorInfo);
            }

            WriteLogs("Выводятся все блоги\n", NLogsModeEnum.Debug);

            return View(blogs);
        }

        /// <summary>
        /// Метод вывода блога по его id
        /// </summary>
        /// <param name="id">уникальный идентификатор блога</param>
        /// <returns></returns>
        public IActionResult GetBlog(int id)
        {
            WriteLogs($"Переход по маршруту /Blogs/GetBlog?id={id}. ", NLogsModeEnum.Trace);

            var blog = _AllBlogs.GetItemById(id);

            if (blog.Id == 0)
            {
                var errorMessage = $"В БД нет блога с id = {id}";
                var errorInfo = new ErrorViewModel(errorMessage);

                WriteLogs($"{errorMessage}. Возвращено ../Shared/Error.cshtml\n", NLogsModeEnum.Warn);

                return View("../Shared/Error", errorInfo);
            }

            WriteLogs($"Выводится блог с id = {id}.\n", NLogsModeEnum.Debug);

            return View(blog);
        }

        /// <summary>
        /// Метод сохранения блога
        /// </summary>
        /// <param name="blogId">идентификатор блога</param>
        /// <param name="textUser">имя пользователя</param>
        /// <param name="textMessage">сообщение пользователя</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveBlogMessage(int blogId, string textUser, string textMessage)
        {
            var blog = _AllBlogs.GetItemById(blogId);
            var timeMessage = $"{DateTime.Now.ToString("MM/dd/yyyy HH:mm")}";
            var allMessageText = $"<p>{timeMessage}: <b>{textUser}:</b><br/> {textMessage}</p><br/>";

            // если чат пустой, т.е. с дефолтной строкой, то заменяем дефолтную строку пустой строкой и сохраняем
            if (blog.FullMessageLine == "Вся строка сообщений")
            {
                blog.FullMessageLine = "";
                _AllBlogs.SaveItem(blog, blogId);
            }

            blog.Message = $"В {timeMessage} пользователь {textUser} прислал сообщение";
            blog.FullMessageLine += allMessageText;

            // сохраняем сообщения чата в БД
            _AllBlogs.SaveItem(blog, blogId);

            // логгируем сообщения чата в NLog
            //WriteLogs($"Пользователь {textUser} отправил сообщение в чат. Оно успешно сохраненено в БД", NLogsModeEnum.Debug);

            return RedirectToAction("GetBlog", new { id = blogId });
        }

        /// <summary>
        /// Метод создания блога
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        public IActionResult CreateBlog()
        {
            WriteLogs("Выполняется действие /Blogs/CreateBlog. ", NLogsModeEnum.Trace);

            var blog = new Blog();

            WriteLogs("Возвращено /Blogs/EditBlog.cshtml\n", NLogsModeEnum.Trace);

            return View("EditBlog", blog);
        }

        /// <summary>
        /// Метод редактирования блога по его id
        /// </summary>
        /// <param name="id">универсальный идентификатор блога</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            WriteLogs("Переход по маршруту /Blogs/EditBlog. ", NLogsModeEnum.Trace);

            var blog = _AllBlogs.GetItemById(id);
            blog.DateAdded = DateTime.Now;

            return View(blog);
        }

        /// <summary>
        /// Метод вывода результатов выборки блогов по тому, что ищем - полное название или ключевое слово (букву)
        /// </summary>
        /// <param name="isFullName">полное название - true, ключевое слово (буква) - false</param>
        /// <param name="fullNameOrKeywordOfItem">текст для поиска</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult GetQueryResultBlogs(bool isFullName, string fullNameOrKeywordOfItem)
        {
            WriteLogs("Переход по маршруту /Blogs/GetQueryResultBlogs. ", NLogsModeEnum.Trace);

            var blogs = _AllBlogs.GetQueryResultItemsAfterFullName(fullNameOrKeywordOfItem, isFullName);
            var numberBlogs = blogs.Count();

            if (numberBlogs == 0)
            {
                var message = $"Нет блогов по запросу \"{fullNameOrKeywordOfItem}\". Возвращено ../Shared/Nothing.cshtml\n";

                WriteLogs(message, NLogsModeEnum.Warn);

                var nothingInfo = new ErrorViewModel(message);
                return View("../Shared/Nothing", nothingInfo);
            }

            WriteLogs($"Выводятся все блоги по запросу \"{fullNameOrKeywordOfItem}\".\n", NLogsModeEnum.Debug);

            return View(blogs);
        }

        /// <summary>
        /// Метод удаления отдельного блога по его id
        /// </summary>
        /// <param name="id">универсальный идентификатор блога</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult DeleteBlog(int id)
        {
            var blog = _AllBlogs.GetItemById(id);
            _AllBlogs.DeleteItem(blog, id);

            WriteLogs("Возвращено ../Shared/SuccessForDelete.cshtml\n", NLogsModeEnum.Trace);

            return View("../Shared/SuccessForDelete", blog);
        }

        /// <summary>
        /// Метод сохранения блога с данными, введенными пользователем
        /// </summary>
        /// <param name="blog">Модель блога</param>
        /// <param name="formValues">Данные формы ввода типа IFormCollection</param>
        /// <param name="changeTitleImagePath">Данные формы ввода типа IFormFile</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpPost]
        public async Task<IActionResult> SaveBlog(Blog blog, IFormCollection formValues, IFormFile? changeTitleImagePath)
        {
            WriteLogs("Запущен процесс сохранения блога в БД. ", NLogsModeEnum.Debug);

            if (ModelState.IsValid)
            {
                WriteLogs("Модель Blog прошла валидацию. ", NLogsModeEnum.Debug);

                // если мы хотим поменять картинку
                if (changeTitleImagePath != null)
                {
                    var folder = "/images/BlogsTitleImages/";
                    await _FileUtils.SaveImageToFolder(folder, changeTitleImagePath);
                    blog.TitleImagePath = $"{folder}{changeTitleImagePath.FileName}";
                }

                blog.FullDescription = formValues["fullInfoAboutBlog"];
                blog.FullMessageLine = formValues["fullMessageLine"];
                blog.DateAdded = DateTime.Now;

                _AllBlogs.SaveItem(blog, blog.Id);

                WriteLogs("Блог успешно сохранен в БД. ", NLogsModeEnum.Debug);
                WriteLogs("Возвращено ../Shared/Success.cshtml\n", NLogsModeEnum.Trace);

                return View("../Shared/Success", blog);
            }
            else
            {
                WriteLogs("Модель Blog не прошла валидацию. ", NLogsModeEnum.Warn);
                WriteLogs("Возвращено /Blogs/EditBlog.cshtml\n", NLogsModeEnum.Trace);

                blog.FullDescription = formValues["fullInfoAboutBlog"];

                return View("EditBlog", blog);
            }
        }
    }
}
