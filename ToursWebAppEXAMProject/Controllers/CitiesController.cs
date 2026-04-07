using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using ToursWebAppEXAMProject.Utils;
using System.Diagnostics.Metrics;

namespace ToursWebAppEXAMProject.Controllers
{
    public class CitiesController : Controller
    {
        private readonly CityUtils _CityUtils;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public CitiesController(CityUtils CityUtils)
        {
            _CityUtils = CityUtils;
        }

        /// <summary>
        /// Метод вывода всех городов
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllCities()
        {
            var cities = _CityUtils.GetCities();
            _logger.Debug("Получена модель IEnumerable<City>. ");

            if (cities == null)
            {
                _logger.Warn("В БД нет ни одного города. ");

                _logger.Trace("Возвращено ../Shared/Nothing.cshtml.\n");
                return View("Nothing", new NothingViewModel("В БД нет ни одного города."));
            }
            else
            {
                _logger.Debug("Выводятся все города. ");

                _logger.Trace("Переход по маршруту /Cities/GetAllCities.\n");
                return View(cities);
            }
        }

        /// <summary>
        /// Метод вывода города по его id
        /// </summary>
        /// <param name="id">уникальный идентификатор города</param>
        /// <returns></returns>
        public IActionResult GetCity(int id)
        {
            var city = _CityUtils.GetCityById(id);
            _logger.Debug($"Получена модель City по id = {id}. ");

            if (city.Id == 0)
            {
                _logger.Warn($"В БД нет города с id = {id}. ");

                _logger.Trace("Возвращено ../Shared/Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет страны с id = {id}.\n"));
            }
            else
            {
                _logger.Debug($"Выводится город с id = {id}. ");

                _logger.Trace($"Переход по маршруту /Cities/GetCity?id={id}.\n");
                return View(city);
            }
        }

        /// <summary>
        /// Метод создания города
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        public IActionResult CreateCity()
        {
            var cityViewModel = _CityUtils.GetCreateCityViewModel();
            _logger.Debug("Создается вью-модель CreateCityViewModel. ");

            _logger.Trace("Переход по маршруту /Cities/CreateCity.cshtml\n");
            return View(cityViewModel);
        }

        /// <summary>
        /// Метод редактирования города по его id
        /// </summary>
        /// <param name="id">универсальный идентификатор города</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult EditCity(int id)
        {
            var city = _CityUtils.GetCityForEdit(id);
            _logger.Debug($"Получена модель City по id={id}. ");

            _logger.Trace("Переход по маршруту /Cities/EditCity.\n");
            return View(city);
        }

        /// <summary>
        /// Метод вывода результатов выборки городов по тому, что ищем - полное название или ключевое слово (букву)
        /// </summary>
        /// <param name="isFullName">полное название - true, ключевое слово (буква) - false</param>
        /// <param name="insertedText">текст для поиска</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult GetQueryResultCities(bool isFullName, string insertedText)
        {
            var cities = _CityUtils.QueryResult(isFullName, insertedText);
            _logger.Debug("Получена модель IEnumerable<City>. ");

            if (cities == null)
            {
                _logger.Warn($"По результатам запроса получен пустой список городов по запросу \"...{insertedText}...\". ");

                _logger.Trace("Возвращено ../Shared/Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет городов по запросу \"...{insertedText}...\"."));
            }
            else
            {
                _logger.Debug("Получен список городов по результатам запроса. ");
                _logger.Debug($"Выводятся все города по запросу. ");

                _logger.Trace("Переход по маршруту /Cities/GetQueryResultCities.\n");
                return View(cities);
            }
        }

        /// <summary>
        /// Метод удаления отдельного города по его id
        /// </summary>
        /// <param name="id">универсальный идентификатор города</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult DeleteCity(int id)
        {
            var city = _CityUtils.GetCityById(id);
            if (city != null)
            {
                _CityUtils.DeleteCityById(city);
            }
            _logger.Debug($"Удален город по id={id}. ");

            _logger.Trace("Переход по маршруту ../Shared/SuccessForDelete.cshtml\n");
            return View("SuccessForDelete", city);
        }

        /// <summary>
        /// Метод сохранения города с данными, введенными пользователем
        /// </summary>
        /// <param name="cityModel">Модель города</param>
        /// <param name="titleImagePath">Данные формы ввода типа IFormFile</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpPost]
        public async Task<IActionResult> SaveCity(City cityModel, IFormFile? titleImagePath)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Debug("Модель City прошла валидацию. ");

                    // если мы хотим поменять картинку
                    if (titleImagePath != null)
                    {
                        await _CityUtils.SaveImagePathAsync(titleImagePath);
                    }

                    cityModel = _CityUtils.SetCityModel(cityModel, titleImagePath);

                    if (cityModel.CountryId !=0)
                    {
                        _CityUtils.SaveCity(cityModel);
                        _logger.Debug("Город успешно сохранен в БД. ");

                        _logger.Trace("Переход по маршруту ../Shared/Success.cshtml\n");
                        return View("Success", cityModel);
                    }
                    else
                    {
                        _logger.Warn("Модель City не прошла валидацию. Не задана страна. ");
                        
                        _logger.Trace("Возвращено /Cities/EditCity.cshtml\n");
                        return View("EditCity", cityModel);
                    }
                }
                else
                {
                    _logger.Warn("Модель City не прошла валидацию. ");
                    
                    _logger.Trace("Возвращено /Cities/EditCity.cshtml\n");
                    return View("EditCity", cityModel);
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
        /// API-метод для получения списка городов по id страны, который используется для динамического заполнения выпадающего списка городов на странице Search при выборе страны.
        /// </summary>
        /// <param name="countryId">Id страны</param>
        /// <returns>список городов выбранной страны</returns>
        [HttpGet]
        public IActionResult GetCities(int countryId)
        {
            var cities = _CityUtils.GetCitiesByCountryId(countryId);

            var result = cities.Select(c => new
            {
                id = c.Id,
                name = c.Name
            });

            return Json(result);
        }
    }
}
