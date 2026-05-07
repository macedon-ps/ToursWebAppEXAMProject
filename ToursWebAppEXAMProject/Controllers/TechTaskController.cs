using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
        private readonly ILogger<TechTaskController> _logger;

        public TechTaskController(ITechTaskService service, TechTaskItemUtils techTaskItemUtils, ILogger<TechTaskController> logger)
        {
            _service = service;
            _techTaskItemUtils = techTaskItemUtils;
            _logger = logger;
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
            _logger.LogDebug("Получена модель IEnumerable<TechTaskItem>. ");

            if (techTaskItems == null)
            {
                _logger.LogWarning("В БД нет ни одного ТЗ. ");
                _logger.LogTrace("Возвращено ../Shared/Nothing.cshtml.\n");
                return View("Nothing", new NothingViewModel("В БД нет ни одного ТЗ."));
            }
            else
            {
                _logger.LogDebug("Выводятся все ТЗ. ");
                _logger.LogTrace("Переход по маршруту /TechTask/GetAllTechtaskItems.\n");
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
            _logger.LogDebug("Создается модель TechTaskItem. ");
            
            _logger.LogTrace("Переход по маршруту /TechTask/EditTechTaskItem.cshtml\n");
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
            _logger.LogDebug($"Получена модель TechTaskItem по id={id}. ");

            _logger.LogTrace("Переход по маршруту /TechTask/EditTechTaskItem.cshtml\n");
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
            _logger.LogDebug($"Удален ТЗ по id={id}. ");
            _logger.LogTrace("Переход по маршруту ../Shared/SuccessForDelete.cshtml\n");
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
            _logger.LogDebug("Получена модель IEnumerable<TechTaskItem>. ");

            if (techTaskItems == null)
            {
                _logger.LogWarning($"По результатам запроса получен пустой список ТЗ по запросу \"...{insertedText}...\". ");
                _logger.LogTrace("Возвращено ../Shared/Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет ТЗ по запросу \"...{insertedText}...\"."));
            }
            else
            {
                _logger.LogDebug("Получен список ТЗ по результатам запроса. ");
                _logger.LogDebug($"Выводятся все ТЗ по запросу \"...{insertedText}...\". ");

                _logger.LogTrace("Переход по маршруту /TechTask/GetQueryResultTechTaskItems.\n");
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
                    _logger.LogDebug("Модель TechTaskItem прошла валидацию. ");

                    _techTaskItemUtils.SaveTechTaskItem(techTaskItem);
                    _logger.LogDebug("ТЗ успешно сохранено в БД. ");

                    _logger.LogTrace("Переход по маршруту ../Shared/Success.cshtml\n");
                    return View("Success", techTaskItem);
                }
                else
                {
                    _logger.LogWarning("Модель TechTaskItem не прошла валидацию. ");          
                    _logger.LogTrace("Возвращено /TechTask/EditTechTaskItem.cshtml\n");
                    return View("EditTechTaskItem", techTaskItem);
                }
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Ошибка при обработке модели TechTaskItem.");
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
