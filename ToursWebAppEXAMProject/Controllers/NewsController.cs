using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.EnumsDictionaries;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Repositories;
using ToursWebAppEXAMProject.ViewModels;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
    public class NewsController : Controller
    {
        private readonly DataManager DataManager;

        public NewsController(DataManager DataManager)
        {
            this.DataManager = DataManager;
        }

        /// <summary>
        /// Метод вывода всех новостей
        /// </summary>
        /// <returns></returns>
        public IActionResult GetALLNews()
        {
            WriteLogs("Переход по маршруту /News/GetAllNews. ", NLogsModeEnum.Trace);

            var news = DataManager.NewBaseInterface.GetAllItems();

            if (news == null)
            {
                var errorInfo = new ModelsErrorViewModel(typeof(List<New>));

                WriteLogs("Нет новостей. Возвращено /ModelsError.cshtml.\n", NLogsModeEnum.Warn);

                return View("ModelsError", errorInfo);
            }
            WriteLogs("Выводятся все новости.\n", NLogsModeEnum.Debug);

            return View(news);
        }

        /// <summary>
        /// Метод вывода новости по ее id
        /// </summary>
        /// <param name="id">уникальный идентификатор новости</param>
        /// <returns></returns>
        public IActionResult GetNew(int id)
        {
            WriteLogs($"Переход по маршруту /News/GetNew?id={id}. ", NLogsModeEnum.Trace);

            var new_ = DataManager.NewBaseInterface.GetItemById(id);

            if (new_.Id == 0)
            {
                WriteLogs($"Нет новости с id = {id}. Возвращено /ModelsError.cshtml\n", NLogsModeEnum.Warn);

                var errorInfo = new ModelsErrorViewModel(typeof(New), id);

                return View("ModelsError", errorInfo);
            }

            WriteLogs($"Выводится новость с id = {id}.\n", NLogsModeEnum.Debug);

            return View(new_);
        }
    }
}
