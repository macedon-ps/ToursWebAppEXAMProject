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
        /// <param name="city">Модель города</param>
        /// <param name="formValues">Данные формы ввода типа IFormCollection</param>
        /// <param name="changeTitleImagePath">Данные формы ввода типа IFormFile</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpPost]
        public async Task<IActionResult> SaveCity(City city, IFormCollection formValues, IFormFile? changeTitleImagePath)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Debug("Модель City прошла валидацию. ");

                    // если мы хотим поменять картинку
                    if (changeTitleImagePath != null)
                    {
                        await _CityUtils.SaveImagePathAsync(changeTitleImagePath);
                    }

                    city = _CityUtils.SetCityModel(city, formValues, changeTitleImagePath);

                    if (city.CountryId !=0)
                    {
                        _CityUtils.SaveCity(city);
                        _logger.Debug("Город успешно сохранен в БД. ");

                        _logger.Trace("Переход по маршруту ../Shared/Success.cshtml\n");
                        return View("Success", city);
                    }
                    else
                    {
                        _logger.Warn("Модель City не прошла валидацию. Не задана страна. ");
                        city = _CityUtils.SetCityModelByFormValues(city, formValues);

                        _logger.Trace("Возвращено /Cities/EditCity.cshtml\n");
                        return View("EditCity", city);
                    }
                }
                else
                {
                    _logger.Warn("Модель City не прошла валидацию. ");
                    city = _CityUtils.SetCityModelByFormValues(city, formValues);

                    _logger.Trace("Возвращено /Cities/EditCity.cshtml\n");
                    return View("EditCity", city);
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
