using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Utils;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Controllers
{
    public class CitiesController : Controller
    {
        private readonly CityUtils _CityUtils;
        private readonly ILogger<CitiesController> _logger;

        public CitiesController(CityUtils CityUtils, ILogger<CitiesController> logger)
        {
            _CityUtils = CityUtils;
            _logger = logger;
        }


        /// <summary>
        /// Метод вывода всех городов
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllCities()
        {
            var cities = _CityUtils.GetCities();
            _logger.LogDebug("Получена модель IEnumerable<City>. ");

            if (cities == null)
            {
                _logger.LogWarning("В БД нет ни одного города. ");
                _logger.LogTrace("Возвращено ../Shared/Nothing.cshtml.\n");
                return View("Nothing", new NothingViewModel("В БД нет ни одного города."));
            }
            else
            {
                _logger.LogDebug("Выводятся все города. ");

                _logger.LogTrace("Переход по маршруту /Cities/GetAllCities.\n");
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
            _logger.LogDebug($"Получена модель City по id = {id}. ");

            if (city.Id == 0)
            {
                _logger.LogWarning($"В БД нет города с id = {id}. ");
                _logger.LogTrace("Возвращено ../Shared/Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет страны с id = {id}.\n"));
            }
            else
            {
                _logger.LogDebug($"Выводится город с id = {id}. ");

                _logger.LogTrace($"Переход по маршруту /Cities/GetCity?id={id}.\n");
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
            _logger.LogDebug("Создается вью-модель CreateCityViewModel. ");

            _logger.LogTrace("Переход по маршруту /Cities/CreateCity.cshtml\n");
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
            _logger.LogDebug($"Получена модель City по id={id}. ");

            _logger.LogTrace("Переход по маршруту /Cities/EditCity.\n");
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
            _logger.LogDebug("Получена модель IEnumerable<City>. ");

            if (cities == null)
            {
                _logger.LogWarning($"По результатам запроса получен пустой список городов по запросу \"...{insertedText}...\". ");
                _logger.LogTrace("Возвращено ../Shared/Nothing.cshtml\n");
                return View("Nothing", new NothingViewModel($"В БД нет городов по запросу \"...{insertedText}...\"."));
            }
            else
            {
                _logger.LogDebug("Получен список городов по результатам запроса. ");
                _logger.LogDebug($"Выводятся все города по запросу. ");

                _logger.LogTrace("Переход по маршруту /Cities/GetQueryResultCities.\n");
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
            _logger.LogDebug($"Удален город по id={id}. ");

            _logger.LogTrace("Переход по маршруту ../Shared/SuccessForDelete.cshtml\n");
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
                    _logger.LogDebug("Модель City прошла валидацию. ");

                    // если мы хотим поменять картинку
                    if (titleImagePath != null)
                    {
                        var uploadImage = await _CityUtils.SaveCityImageByFileNameAsync(titleImagePath, cityModel.Id);
                        cityModel.TitleImagePath = uploadImage.Url;
                        cityModel.ImagePublicId = uploadImage.PublicId;
                    }

                    if (cityModel.CountryId !=0)
                    {
                        _CityUtils.SaveCity(cityModel);
                        _logger.LogDebug("Город успешно сохранен в БД. ");
                    }

                    cityModel.DateAdded = DateTime.Now;


                    _logger.LogTrace("Переход по маршруту ../Shared/Success.cshtml\n");
                    return View("Success", cityModel);
                }
                else
                {
                    _logger.LogWarning("Модель City не прошла валидацию. ");
                    
                    _logger.LogTrace("Возвращено /Cities/EditCity.cshtml\n");
                    return View("EditCity", cityModel);
                }
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Ошибка при обработке модели City.");
                _logger.LogTrace("Возвращено ../Shared/Error.cshtml\n");

                var errorInfo = new ErrorViewModel()
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };

                return View("Error", errorInfo);
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
