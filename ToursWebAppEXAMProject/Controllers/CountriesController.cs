using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using ToursWebAppEXAMProject.Utils;

namespace ToursWebAppEXAMProject.Controllers
{
    public class CountriesController : Controller
    {
        private readonly IBaseInterface<Country> _AllCountries;
        private readonly IBaseInterface<City> _AllCities;
        private readonly FileUtils _FileUtils;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public CountriesController(IBaseInterface<Country> Countries, IBaseInterface<City> Cities, FileUtils FileUtils)
        {
            _AllCountries = Countries;
            _AllCities = Cities;
            _FileUtils = FileUtils;
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
            _logger.Trace("Переход по маршруту /Countries/GetAllCountries. ");
            
            var countries = _AllCountries.GetAllItems();

            if(countries == null)
            {
                var errorMessage = "В БД нет ни одной страны";
                var errorInfo = new ErrorViewModel(errorMessage);

                _logger.Warn($"{errorMessage}. Возвращено ../Shared/Error.cshtml\n");

                return View("Error", errorInfo);
            }

            _logger.Debug("Выводятся все страны\n");

            return View(countries);
        }

        /// <summary>
        /// Метод вывода страны по ее id
        /// </summary>
        /// <param name="id">уникальный идентификатор страны</param>
        /// <returns></returns>
        public IActionResult GetCountry(int id)
        {
            _logger.Trace($"Переход по маршруту /Countries/GetCountry?id={id}. ");

            var country = _AllCountries.GetItemById(id);
            
            var cities = _AllCities.GetAllItems().Where(c => c.CountryId == id);
            country.Cities = cities;

            if (country.Id == 0)
            {
                var errorMessage = $"В БД нет страны с id = {id}";
                var errorInfo = new ErrorViewModel(errorMessage);

                _logger.Warn($"{errorMessage}. Возвращено ../Shared/Error.cshtml\n");

                return View("Error", errorInfo);
            }

            _logger.Debug($"Выводится страна с id = {id}.\n");

            return View(country);
        }

        /// <summary>
        /// Метод создания страны
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        public IActionResult CreateCountry()
        {
            _logger.Trace("Выполняется действие /Countries/CreateCountry. ");

            var country = new Country();

            _logger.Trace("Возвращено /Countries/EditCountry.cshtml\n");

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
            _logger.Trace("Переход по маршруту /Countries/EditCountry. ");

            var country = _AllCountries.GetItemById(id);

            var cities = _AllCities.GetAllItems().Where(c => c.CountryId == id);
            country.Cities = cities;
            country.DateAdded = DateTime.Now;

            return View(country);
        }

        /// <summary>
        /// Метод вывода результатов выборки стран по тому, что ищем - полное название или ключевое слово (букву)
        /// </summary>
        /// <param name="isFullName">полное название - true, ключевое слово (буква) - false</param>
        /// <param name="fullNameOrKeywordOfItem">текст для поиска</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult GetQueryResultCountries(bool isFullName, string fullNameOrKeywordOfItem)
        {
            _logger.Trace("Переход по маршруту /Countries/GetQueryResultCountries. ");

            var countries = _AllCountries.GetQueryResultItemsAfterFullName(fullNameOrKeywordOfItem, isFullName);
            var numberCountries = countries.Count();

            if (numberCountries == 0)
            {
                var message = $"Нет стран по запросу \"{fullNameOrKeywordOfItem}\". Возвращено ../Shared/Nothing.cshtml\n";

                _logger.Warn(message);

                var nothingInfo = new ErrorViewModel(message);
                return View("Nothing", nothingInfo);
            }

            _logger.Debug($"Выводятся все страны по запросу \"{fullNameOrKeywordOfItem}\".\n");

            return View(countries);
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
            var country = _AllCountries.GetItemById(id);
            _AllCountries.DeleteItem(country, id);

            _logger.Trace("Возвращено ../Shared/SuccessForDelete.cshtml\n");

            return View("SuccessForDelete", country);
        }

        /// <summary>
        /// Метод сохранения страны с данными, введенными пользователем
        /// </summary>
        /// <param name="country">Модель страны</param>
        /// <param name="formValues">Данные формы ввода типа IFormCollection</param>
        /// <param name="changeTitleImagePath">Данные формы ввода типа IFormFile</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpPost]
        public async Task<IActionResult> SaveCountry(Country country, IFormCollection formValues, IFormFile? changeTitleImagePath)
        {
            _logger.Debug("Запущен процесс сохранения страны в БД. ");

            if (ModelState.IsValid)
            {
                _logger.Debug("Модель Country прошла валидацию. ");

                // если мы хотим поменять картинку
                if (changeTitleImagePath != null)
                {
                    var folder = "/images/CountriesTitleImages/";
                    await _FileUtils.SaveImageToFolder(folder, changeTitleImagePath);
                    country.TitleImagePath = $"{folder}{changeTitleImagePath.FileName}";
                }

                country.FullDescription = formValues["fullInfoAboutCountry"];
                country.DateAdded = DateTime.Now;

                _AllCountries.SaveItem(country, country.Id);

                _logger.Debug("Страна успешно сохранена в БД. ");
                _logger.Trace("Возвращено ../Shared/Success.cshtml\n");

                return View("Success", country);
            }
            else
            {
                _logger.Warn("Модель Country не прошла валидацию. ");
                _logger.Trace("Возвращено /Countries/EditCountry.cshtml\n");

                country.FullDescription = formValues["fullInfoAboutCountry"];

                return View("EditCountry", country);
            }
        }
    }
}
