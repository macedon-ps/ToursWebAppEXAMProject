using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using ToursWebAppEXAMProject.Utils;

namespace ToursWebAppEXAMProject.Controllers
{
    public class CitiesController : Controller
    {
        private readonly IBaseInterface<City> _AllCities;
        private readonly IBaseInterface<Country> _AllCountries;
        private readonly FileUtils _FileUtils;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public CitiesController(IBaseInterface<City> Cities, IBaseInterface<Country> Countries, FileUtils FileUtils)
        {
            _AllCities = Cities;
            _AllCountries = Countries;
            _FileUtils = FileUtils;
        }

        /// <summary>
        /// Метод вывода всех городов
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllCities()
        {
            _logger.Trace("Переход по маршруту /Cities/GetAllCities. ");

            var cities = _AllCities.GetAllItems();

            if (cities == null)
            {
                var errorMessage = "В БД нет ни одного города";
                var errorInfo = new ErrorViewModel(errorMessage);

                _logger.Warn($"{errorMessage}. Возвращено ../Shared/Error.cshtml\n");

                return View("Error", errorInfo);
            }

            _logger.Debug("Выводятся все города\n");

            return View(cities);
        }

        /// <summary>
        /// Метод вывода города по его id
        /// </summary>
        /// <param name="id">уникальный идентификатор города</param>
        /// <returns></returns>
        public IActionResult GetCity(int id)
        {
            _logger.Trace($"Переход по маршруту /Cities/GetCity?id={id}. ");

            var city = _AllCities.GetItemById(id);

            if (city.Id == 0)
            {
                var errorMessage = $"В БД нет города с id = {id}";
                var errorInfo = new ErrorViewModel(errorMessage);

                _logger.Warn($"{errorMessage}. Возвращено ../Shared/Error.cshtml\n");

                return View("Error", errorInfo);
            }

            _logger.Debug($"Выводится город с id = {id}.\n");

            return View(city);
        }

        /// <summary>
        /// Метод создания города
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        public IActionResult CreateCity()
        {
            _logger.Trace("Выполняется действие /Cities/CreateCity. ");

            var cityViewModel = new CreateCityViewModel();
            var city = new City();
            var countries = _AllCountries.GetAllItems();
            cityViewModel.City = city;
            cityViewModel.Countries = countries;

            _logger.Trace("Возвращено /Cities/CreateCity.cshtml\n");

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
            _logger.Trace("Переход по маршруту /Cities/EditCity. ");

            // TODO: вставить индекс страны при редактировании

            var city = _AllCities.GetItemById(id);
            
            city.DateAdded = DateTime.Now;

            return View(city);
        }

        /// <summary>
        /// Метод вывода результатов выборки городов по тому, что ищем - полное название или ключевое слово (букву)
        /// </summary>
        /// <param name="isFullName">полное название - true, ключевое слово (буква) - false</param>
        /// <param name="InsertedText">текст для поиска</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult GetQueryResultCities(bool isFullName, string InsertedText)
        {
            _logger.Trace("Переход по маршруту /Cities/GetQueryResultCities. ");

            var cities = _AllCities.GetQueryResultItemsAfterFullName(InsertedText, isFullName);
            var numberCities = cities.Count();

            if (numberCities == 0)
            {
                var message = $"Нет городов по запросу \"{InsertedText}\". Возвращено ../Shared/Nothing.cshtml\n";

                _logger.Warn(message);

                var nothingInfo = new NothingViewModel(message);
                return View("Nothing", nothingInfo);
            }

            _logger.Debug($"Выводятся все города по запросу \"{InsertedText}\".\n");

            return View(cities);
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
            var city = _AllCities.GetItemById(id);
            _AllCities.DeleteItem(city, id);

            _logger.Trace("Возвращено ../Shared/SuccessForDelete.cshtml\n");

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
                _logger.Debug("Запущен процесс сохранения города в БД. ");

                if (ModelState.IsValid)
                {
                    _logger.Debug("Модель City прошла валидацию. ");

                    // если мы хотим поменять картинку
                    if (changeTitleImagePath != null)
                    {
                        var folder = "/images/CitiesTitleImages/";
                        await _FileUtils.SaveImageToFolder(folder, changeTitleImagePath);
                        city.TitleImagePath = $"{folder}{changeTitleImagePath.FileName}";
                    }
                    if (formValues["checkIsCapital"] == "on")
                    {
                        city.isCapital = true;
                    }

                    // TODO: проверка city.CountryId на null

                    city.CountryId = Int32.Parse(formValues["CountryId"]);
                    city.FullDescription = formValues["fullInfoAboutCity"];
                    city.DateAdded = DateTime.Now;

                    _AllCities.SaveItem(city, city.Id);

                    _logger.Debug("Город успешно сохранен в БД. ");
                    _logger.Trace("Возвращено ../Shared/Success.cshtml\n");

                    return View("Success", city);
                }
                else
                {
                    _logger.Warn("Модель City не прошла валидацию. ");
                    _logger.Trace("Возвращено /Cities/EditCity.cshtml\n");

                    city.FullDescription = formValues["fullInfoAboutCity"];

                    return View("EditCity", city);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}");
                return View("Error", ex.Message);
            }
         }
    }
}
