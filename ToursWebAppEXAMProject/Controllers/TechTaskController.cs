using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Utils;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Controllers
{
    public class TechTaskController : Controller
    {
        private readonly ITechTaskService _service;
        private readonly TechTaskItemUtils _techTaskItemUtils;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public TechTaskController(ITechTaskService service, TechTaskItemUtils techTaskItemUtils)
        {
            _service = service;
            _techTaskItemUtils = techTaskItemUtils;
        }


        // GET
        public IActionResult Page(string pageName)
        {
            var viewModel = _service.GetPageViewModel(pageName);

            return View("TechTask", viewModel);
        }


        // POST
        [HttpPost]
        public IActionResult Page(TechTaskPageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                
                var model = _service.GetPageFromViewModel(viewModel);
                viewModel.Progress = _service.CalculateProgress(model);
                _service.Save(model);
            }

            return View("TechTask", viewModel);
        }


        public IActionResult GetAllTechTaskItems()
        {
            var techTaskItems = _techTaskItemUtils.GetTechTaskItems();
            _logger.Debug("Получена модель IEnumerable<TechTaskItem>. ");

            if (techTaskItems == null)
            {
                _logger.Warn("В БД нет ни одного ТЗ. ");

                _logger.Trace("Возвращено ../Shared/Nothing.cshtml.\n");
                return View("Nothing", new NothingViewModel("В БД нет ни одного ТЗ."));
            }
            else
            {
                _logger.Debug("Выводятся все ТЗ. ");
                _logger.Trace("Переход по маршруту /TechTask/GetAllTechtaskItems.\n");
                return View(techTaskItems);
            }
        }

        /// <summary>
        /// Метод создания ТЗ.
        /// </summary>
        /// <returns>Вью для создания ТЗ.</returns>
        [Authorize(Roles = "superadmin,admin")]
        public IActionResult CreateTechTaskItem()
        {
            var techTaskItem = new TechTaskItem();
            _logger.Debug("Создается модель TechTaskItem. ");
            
            _logger.Trace("Переход по маршруту /TechTask/EditTechTaskItem.cshtml\n");
            return View("EditTechTaskItem", techTaskItem);
        }


        /// <summary>
        /// Метод редактирования ТЗ по его id.
        /// </summary>
        /// <param name="id">универсальный идентификатор ТЗ.</param>
        /// <returns>Вью для редактирования ТЗ.</returns>
        [Authorize(Roles = "superadmin,admin")]
        [HttpGet]
        public IActionResult EditTechTaskItem(int id)
        {
            var techTaskItem = _techTaskItemUtils.GetTechTaskItemById(id);
            _logger.Debug($"Получена модель TechTaskItem по id={id}. ");

            _logger.Trace("Переход по маршруту /TechTask/EditTechTaskItem.cshtml\n");
            return View(techTaskItem);
        }


        /// <summary>
        /// Метод удаления отдельного ТЗ по его id
        /// </summary>
        /// <param name="id">универсальный идентификатор ТЗ</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        [HttpGet]
        public IActionResult DeleteTechTaskItem(int id)
        {
            var techTaskItem = _techTaskItemUtils.GetTechTaskItemById(id);
            if (techTaskItem != null)
            {
                _techTaskItemUtils.DeleteTechTaskItemById(techTaskItem);
            }
            _logger.Debug($"Удален ТЗ по id={id}. ");
            _logger.Trace("Переход по маршруту ../Shared/SuccessForDelete.cshtml\n");
            return View("SuccessForDelete", techTaskItem);
        }


        /// <summary>
        /// Метод вывода результатов выборки ТЗ по тому, что ищем - полное название или ключевое слово (букву)
        /// </summary>
        /// <param name="isFullName">полное название - true, ключевое слово (буква) - false</param>
        /// <param name="insertedText">текст для поиска</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,admin")]
        [HttpGet]
        public IActionResult GetQueryResultTechTaskItems(bool isFullName, string insertedText)
        {
            var techTaskItems = _techTaskItemUtils.QueryResult(isFullName, insertedText);
            _logger.Debug("Получена модель IEnumerable<TechTaskItem>. ");

            if (techTaskItems == null)
            {
                _logger.Warn($"По результатам запроса получен пустой список ТЗ по запросу \"...{insertedText}...\". ");
                _logger.Trace("Возвращено ../Shared/Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет ТЗ по запросу \"...{insertedText}...\"."));
            }
            else
            {
                _logger.Debug("Получен список ТЗ по результатам запроса. ");
                _logger.Debug($"Выводятся все ТЗ по запросу \"...{insertedText}...\". ");

                _logger.Trace("Переход по маршруту /TechTask/GetQueryResultTechTaskItems.\n");
                return View(techTaskItems);
            }
        }


        /// <summary>
        /// Метод сохранения ТЗ с данными, введенными пользователем
        /// </summary>
        /// <param name="techTaskItem">Модель ТЗ</param>
        /// <param name="titleImagePath">Данные формы ввода типа IFormFile</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpPost]
        public async Task<IActionResult> SaveTechTaskItem(TechTaskItem techTaskItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Debug("Модель TechTaskItem прошла валидацию. ");

                    _techTaskItemUtils.SaveTechTaskItem(techTaskItem);
                    _logger.Debug("ТЗ успешно сохранено в БД. ");

                    _logger.Trace("Переход по маршруту ../Shared/Success.cshtml\n");
                    return View("Success", techTaskItem);
                }
                else
                {
                    _logger.Warn("Модель TechTaskItem не прошла валидацию. ");          
                    _logger.Trace("Возвращено /TechTask/EditTechTaskItem.cshtml\n");
                    return View("EditTechTaskItem", techTaskItem);
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
