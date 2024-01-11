using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.EnumsDictionaries;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Repositories;
using ToursWebAppEXAMProject.ViewModels;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
    public class BlogsController : Controller
    {
        private readonly DataManager DataManager;

        public BlogsController(DataManager DataManager)
        {
            this.DataManager = DataManager;
        }

        /// <summary>
        /// Метод вывода всех блогов
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllBlogs()
        {
            WriteLogs("Переход по маршруту /Home/GetAllBlogs. ", NLogsModeEnum.Trace);

            var blogs = DataManager.BlogBaseInterface.GetAllItems();

            if (blogs == null)
            {
                var errorInfo = new ModelsErrorViewModel(typeof(List<Blog>));

                WriteLogs("Нет блогов. Возвращено /ModelsError.cshtml\n", NLogsModeEnum.Warn);

                return View("ModelsError", errorInfo);
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
            WriteLogs($"Переход по маршруту /Home/GetBlog?id={id}. ", NLogsModeEnum.Trace);

            var blog = DataManager.BlogBaseInterface.GetItemById(id);

            if (blog.Id == 0)
            {
                var errorInfo = new ModelsErrorViewModel(typeof(Blog), id);

                WriteLogs($"Нет блога с id = {id}. Возвращено /ModelsError.cshtml\n", NLogsModeEnum.Warn);

                return View("ModelsError", errorInfo);
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
            var blog = DataManager.BlogBaseInterface.GetItemById(blogId);
            var timeMessage = $"{DateTime.Now.ToString("HH:mm:ss")}";
            var allMessageText = $"<p>{timeMessage}: <b>{textUser}:</b><br/> {textMessage}</p><br/>";

            // если чат пустой, т.е. с дефолтной строкой, то заменяем дефолтную строку пустой строкой и сохраняем
            if (blog.FullMessageLine == "Вся строка сообщений")
            {
                blog.FullMessageLine = "";
                DataManager.BlogBaseInterface.SaveItem(blog, blogId);
            }

            blog.Message = $"В {timeMessage} пользователь {textUser} прислал сообщение";
            blog.FullMessageLine += allMessageText;

            // сохраняем сообщения чата в БД
            DataManager.BlogBaseInterface.SaveItem(blog, blogId);

            // логгируем сообщения чата в NLog
            //WriteLogs($"Пользователь {textUser} отправил сообщение в чат. Оно успешно сохраненено в БД", NLogsModeEnum.Debug);

            return RedirectToAction("GetBlog", new { id = blogId });
        }
    }
}
