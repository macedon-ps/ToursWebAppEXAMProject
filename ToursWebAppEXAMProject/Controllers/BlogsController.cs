using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using ToursWebAppEXAMProject.Utils;

namespace ToursWebAppEXAMProject.Controllers
{
    public class BlogsController : Controller
    {
        private readonly BlogUtils _BlogUtils;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public BlogsController(BlogUtils BlogUtils)
        {
            _BlogUtils = BlogUtils;
        }

        /// <summary>
        /// Метод вывода всех блогов
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllBlogs()
        {
            var blogs = _BlogUtils.GetBlogs();
            _logger.Debug("Получена модель IEnumerable<Blog>. ");

            if (blogs == null)
            {
                _logger.Warn("В БД нет ни одного блога. ");

                _logger.Trace("Возвращено ../Shared/Nothing.cshtml.\n");
                return View("Nothing", new NothingViewModel("В БД нет ни одного блога."));
            }
            else
            {
                _logger.Debug("Выводятся все блоги. ");

                _logger.Trace("Переход по маршруту /Blogs/GetBlogs.\n");
                return View(blogs);
            }
        }

        /// <summary>
        /// Метод вывода блога по его id
        /// </summary>
        /// <param name="id">уникальный идентификатор блога</param>
        /// <returns></returns>
        public IActionResult GetBlog(int id)
        {
            var blog = _BlogUtils.GetBlogById(id);
            _logger.Debug($"Получена модель New по id = {id}. ");

            if (blog.Id == 0)
            {
                _logger.Warn($"В БД нет блога с id = {id}. ");

                _logger.Trace("Возвращено ../Shared//Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет блога с id = {id}.\n"));
            }
            else
            {
                _logger.Debug($"Выводится новость с id = {id}. ");

                _logger.Trace($"Переход по маршруту /News/GetNesw?id={id}.\n");
                return View(blog);
            }
        }

        /// <summary>
        /// Метод создания блога
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        public IActionResult CreateBlog()
        {
            var blog = new Blog();
            _logger.Debug("Создается модель Blog. ");

            _logger.Trace("Переход по маршруту /Blogs/EditBlogs.cshtml\n");
            return View("EditBlogs", blog);
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
            var blog = _BlogUtils.GetBlogForEdit(id);
            _logger.Debug($"Получена модель Blog по id={id}. ");

            _logger.Trace("Переход по маршруту /Blogs/EditBlog.\n");
            return View(blog);
        }

        /// <summary>
        /// Метод вывода результатов выборки блогов по тому, что ищем - полное название или ключевое слово (букву)
        /// </summary>
        /// <param name="isFullName">полное название - true, ключевое слово (буква) - false</param>
        /// <param name="insertedText">текст для поиска</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult GetQueryResultBlogs(bool isFullName, string insertedText)
        {
            _logger.Trace("Переход по маршруту /Blogs/GetQueryResultBlogs. ");

            var blogs = _BlogUtils.QueryResult(isFullName, insertedText);
            _logger.Debug("Получена модель IEnumerable<Blog>. ");

            if (blogs == null)
            {
                _logger.Warn($"По результатам запроса получен пустой список блогов по запросу \"...{insertedText}...\". ");

                _logger.Trace("Возвращено ../Shared//Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет блогов по запросу \"...{insertedText}...\"."));
            }
            else
            {
                _logger.Debug("Получен список блогов по результатам запроса. ");
                _logger.Debug($"Выводятся все блоги по запросу. ");

                _logger.Trace("Переход по маршруту /Blogs/GetQueryResultBlogs.\n");
                return View(blogs);
            }
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
            var blog = _BlogUtils.GetBlogById(id);
            if (blog != null)
            {
                _BlogUtils.DeleteBlogById(blog);
            }
            _logger.Debug($"Удален блог по id={id}. ");

            _logger.Trace("Переход по маршруту ../Shared/SuccessForDelete.cshtml\n");
            return View("SuccessForDelete", blog);
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
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Debug("Модель Blog прошла валидацию. ");

                    // если мы хотим поменять картинку
                    if (changeTitleImagePath != null)
                    {
                        await _BlogUtils.SaveImagePathAsync(changeTitleImagePath);
                    }

                    blog = _BlogUtils.SetBlogModel(blog, formValues, changeTitleImagePath);
                    _BlogUtils.SaveBlog(blog);
                    _logger.Debug("Блог успешно сохранен в БД. ");

                    _logger.Trace("Переход по маршруту ../Shared/Success.cshtml\n");
                    return View("Success", blog);
                }
                else
                {
                    _logger.Warn("Модель Blog не прошла валидацию. ");
                    blog = _BlogUtils.SetBlogModelByFormValues(blog, formValues);
                    
                    _logger.Trace("Возвращено /Blogs/EditBlog.cshtml\n");
                    return View("EditBlog", blog);
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
        /// Метод сохранения блога
        /// </summary>
        /// <param name="blogId">идентификатор блога</param>
        /// <param name="userName">имя пользователя</param>
        /// <param name="message">сообщение пользователя</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveBlogMessage(int blogId, string userName, string message)
        {
            var blog = _BlogUtils.GetBlogById(blogId);
            _logger.Debug($"Получена модель Blog по id={blogId}. ");

            blog = _BlogUtils.SetBlogModelWithChatDataAndSave(blog, userName, message);
            _logger.Debug("Модель Blog заполнена данными из чата и сохранена. ");

            _logger.Trace("Переход по маршруту /Blogs/GetBlog.\n");
            return RedirectToAction("GetBlog", "Blogs", new { id = blogId } );
        }
    }
}
