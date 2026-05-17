using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Utils;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Controllers
{
    public class CountriesController : Controller
    {
        private readonly CountryUtils _CountryUtils;
        private readonly ILogger<CountriesController> _logger;

        public CountriesController(CountryUtils CountryUtils, ILogger<CountriesController> logger)
        {
            _CountryUtils = CountryUtils;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Метод вывода всех стран
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllCountries()
        {
            var countries = _CountryUtils.GetCountries();
            _logger.LogDebug("Получена модель IEnumerable<Country>. ");

            if (countries == null)
            {
                _logger.LogWarning("В БД нет ни одной страны. ");
                _logger.LogTrace("Возвращено ../Shared/Nothing.cshtml.\n");
                return View("Nothing", new NothingViewModel("В БД нет ни одной страны."));
            }
            else
            {
                _logger.LogDebug("Выводятся все страны. ");

                _logger.LogTrace("Переход по маршруту /Countries/GetAllCountries.\n");
                return View(countries);
            }
        }


        /// <summary>
        /// Метод вывода страны по ее id
        /// </summary>
        /// <param name="id">уникальный идентификатор страны</param>
        /// <returns></returns>
        public IActionResult GetCountry(int id)
        {
            var country = _CountryUtils.GetCountryById(id);
            _logger.LogDebug($"Получена модель Country по id = {id}. ");

            if (country.Id == 0)
            {
                _logger.LogWarning($"В БД нет страны с id = {id}. ");
                _logger.LogTrace("Возвращено ../Shared/Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет страны с id = {id}.\n"));
            }
            else
            {
                _logger.LogDebug($"Выводится страна с id = {id}. ");

                _logger.LogTrace($"Переход по маршруту /Countries/GetCountry?id={id}.\n");
                return View(country);
            }
        }


        /// <summary>
        /// Метод создания страны
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        public IActionResult CreateCountry()
        {
            var country = new Country();
            _logger.LogDebug("Создается модель Country. ");

            _logger.LogTrace("Переход по маршруту /Countries/EditCountry.cshtml\n");
            return View("EditCountry", country);
        }


        /// <summary>
        /// Метод редактирования страны по ее id
        /// </summary>
        /// <param name="id">универсальный идентификатор страны</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult EditCountry(int id)
        {
            var country = _CountryUtils.GetCountryForEdit(id);
            _logger.LogDebug($"Получена модель Country по id={id}. ");

            _logger.LogTrace("Переход по маршруту /Countries/EditCountry.\n");
            return View(country);
        }


        /// <summary>
        /// Метод вывода результатов выборки стран по тому, что ищем - полное название или ключевое слово (букву)
        /// </summary>
        /// <param name="isFullName">полное название - true, ключевое слово (буква) - false</param>
        /// <param name="insertedText">текст для поиска</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult GetQueryResultCountries(bool isFullName, string insertedText)
        {
            var countries = _CountryUtils.QueryResult(isFullName, insertedText);
            _logger.LogDebug("Получена модель IEnumerable<Country>. ");

            if (countries == null)
            {
                _logger.LogWarning($"По результатам запроса получен пустой список стран по запросу \"...{insertedText}...\". ");
                _logger.LogTrace("Возвращено ../Shared/Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет стран по запросу \"...{insertedText}...\"."));
            }
            else
            {
                _logger.LogDebug("Получен список стран по результатам запроса. ");
                _logger.LogDebug($"Выводятся все страны по запросу. ");

                _logger.LogTrace("Переход по маршруту /Countries/GetQueryResultCountries.\n");
                return View(countries);
            }
        }


        /// <summary>
        /// Метод удаления отдельной страны по ее id
        /// </summary>
        /// <param name="id">универсальный идентификатор страны</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult DeleteCountry(int id)
        {
            var country = _CountryUtils.GetCountryById(id);
            if (country != null)
            {
                _CountryUtils.DeleteCountryById(country);
            }
            _logger.LogDebug($"Удалена страна по id={id}. ");

            _logger.LogTrace("Переход по маршруту ../Shared/SuccessForDelete.cshtml\n");
            return View("SuccessForDelete", country);
        }


        /// <summary>
        /// Метод сохранения страны с данными, введенными пользователем
        /// </summary>
        /// <param name="countryModel">Модель страны</param>
        /// <param name="titleImagePath">Данные формы ввода типа IFormFile</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpPost]
        public async Task<IActionResult> SaveCountry(Country countryModel, IFormFile? titleImagePath)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogDebug("Модель Country прошла валидацию. ");

                    // если мы хотим поменять картинку
                    if (titleImagePath != null)
                    {
                        countryModel.TitleImagePath = await _CountryUtils.SaveImagePathAsync(titleImagePath);
                    }

                    countryModel.DateAdded = DateTime.Now;
                    _CountryUtils.SaveCountryModel(countryModel);
                    _logger.LogDebug("Страна успешно сохранена в БД. ");

                    _logger.LogTrace("Переход по маршруту ../Shared/Success.cshtml\n");
                    return View("Success", countryModel);
                }
                else
                {
                    _logger.LogWarning("Модель Country не прошла валидацию. ");
                    
                    _logger.LogTrace("Возвращено /Countries/EditCountry.cshtml\n");
                    return View("EditCountry", countryModel);
                }
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Ошибка при обработке модели Country.");
                _logger.LogTrace("Возвращено ../Shared/Error.cshtml\n");

                var errorInfo = new ErrorViewModel()
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };

                return View("Error", errorInfo);
            }
        }


        /// <summary>
        /// API-метод для получения карты страны по id страны.
        /// </summary>
        /// <param name="countryId">Id страны.</param>
        /// <returns>Карта выбранной страны.</returns>
        [HttpGet]
        public IActionResult GetMap(int countryId)
        {
            var map = _CountryUtils.GetMapByCountryId(countryId);

            return Json(map);
        }
    }
}
