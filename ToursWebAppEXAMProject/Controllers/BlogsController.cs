using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Utils;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Controllers
{
    public class BlogsController : Controller
    {
        private readonly BlogUtils _BlogUtils;
        private readonly ILogger<BlogsController> _logger;

        public BlogsController(BlogUtils BlogUtils, ILogger<BlogsController> logger)
        {
            _BlogUtils = BlogUtils;
            _logger = logger;
        }

        /// <summary>
        /// Метод вывода всех блогов
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllBlogs()
        {
            var blogs = _BlogUtils.GetBlogs();
            _logger.LogDebug("Получена модель IEnumerable<Blog>. ");

            if (blogs == null)
            {
                _logger.LogWarning("В БД нет ни одного блога. ");
                _logger.LogTrace("Возвращено ../Shared/Nothing.cshtml.\n");
                return View("Nothing", new NothingViewModel("В БД нет ни одного блога."));
            }
            else
            {
                _logger.LogDebug("Выводятся все блоги. ");

                _logger.LogTrace("Переход по маршруту /Blogs/GetBlogs.\n");
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
            _logger.LogDebug($"Получена модель Blog по id = {id}. ");

            if (blog.Id == 0)
            {
                _logger.LogWarning($"В БД нет блога с id = {id}. ");
                _logger.LogTrace("Возвращено ../Shared//Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет блога с id = {id}.\n"));
            }
            else
            {
                _logger.LogDebug($"Выводится новость с id = {id}. ");

                _logger.LogTrace($"Переход по маршруту /News/GetBlog?id={id}.\n");
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
            _logger.LogDebug("Создается модель Blog. ");

            _logger.LogTrace("Переход по маршруту /Blogs/EditBlog.cshtml\n");
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
            var blog = _BlogUtils.GetBlogById(id);
            _logger.LogDebug($"Получена модель Blog по id={id}. ");

            _logger.LogTrace("Переход по маршруту /Blogs/EditBlog.\n");
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
            _logger.LogTrace("Переход по маршруту /Blogs/GetQueryResultBlogs. ");

            var blogs = _BlogUtils.QueryResult(isFullName, insertedText);
            _logger.LogDebug("Получена модель IEnumerable<Blog>. ");

            if (blogs == null)
            {
                _logger.LogWarning($"По результатам запроса получен пустой список блогов по запросу \"...{insertedText}...\". ");
                _logger.LogTrace("Возвращено ../Shared//Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет блогов по запросу \"...{insertedText}...\"."));
            }
            else
            {
                _logger.LogDebug("Получен список блогов по результатам запроса. ");
                _logger.LogDebug($"Выводятся все блоги по запросу. ");

                _logger.LogTrace("Переход по маршруту /Blogs/GetQueryResultBlogs.\n");
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
            _logger.LogDebug($"Удален блог по id={id}. ");

            _logger.LogTrace("Переход по маршруту ../Shared/SuccessForDelete.cshtml\n");
            return View("SuccessForDelete", blog);
        }

        /// <summary>
        /// Метод сохранения блога с данными, введенными пользователем
        /// </summary>
        /// <param name="blogModel">Модель блога</param>
        /// <param name="titleImagePath">Данные формы ввода типа IFormFile</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpPost]
        public async Task<IActionResult> SaveBlog(Blog blogModel, IFormFile? titleImagePath)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogDebug("Модель Blog прошла валидацию. ");

                    // если мы хотим поменять картинку
                    if (titleImagePath != null)
                    {
                        var uploadImage = await _BlogUtils.SaveBlogImageByFileNameAsync(titleImagePath, blogModel.Id);
                        blogModel.TitleImagePath = uploadImage.Url;
                        blogModel.ImagePublicId = uploadImage.PublicId;
                    }

                    blogModel.DateAdded = DateTime.Now;
                    _BlogUtils.SaveBlogModel(blogModel);
                    _logger.LogDebug("Блог успешно сохранен в БД. ");

                    _logger.LogTrace("Переход по маршруту ../Shared/Success.cshtml\n");
                    return View("Success", blogModel);
                }
                else
                {
                    _logger.LogWarning("Модель Blog не прошла валидацию. ");
                                        
                    _logger.LogTrace("Возвращено /Blogs/EditBlog.cshtml\n");
                    return View("EditBlog", blogModel);
                }
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Ошибка при обработке модели Blog.");
                _logger.LogTrace("Возвращено ../Shared/Error.cshtml\n");

                var errorInfo = new ErrorViewModel()
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };

                return View("Error", errorInfo);
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
            _logger.LogDebug($"Получена модель Blog по id={blogId}. ");

            blog = _BlogUtils.SetBlogModelWithChatDataAndSave(blog, userName, message);
            _logger.LogDebug("Модель Blog заполнена данными из чата и сохранена. ");

            _logger.LogTrace("Переход по маршруту /Blogs/GetBlog.\n");
            return RedirectToAction("GetBlog", "Blogs", new { id = blogId } );
        }
    }
}
