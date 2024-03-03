using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using TourWebAppEXAMProject.Utils;
using static TourWebAppEXAMProject.Services.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
    public class CountriesController : Controller
    {
        private readonly IBaseInterface<Country> _AllCountries;
        private readonly IBaseInterface<City> _AllCities;
        
        public CountriesController(IBaseInterface<Country> Countries, IBaseInterface<City> Cities)
        {
            this._AllCountries = Countries;
            this._AllCities = Cities;
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
            WriteLogs("Переход по маршруту /Countries/GetAllCountries. ", NLogsModeEnum.Trace);
            
            var countries = _AllCountries.GetAllItems();

            if(countries == null)
            {
                var errorMessage = "В БД нет ни одной страны";
                var errorInfo = new ErrorViewModel(errorMessage);

                WriteLogs($"{errorMessage}. Возвращено ../Shared/Error.cshtml\n", NLogsModeEnum.Warn);

                return View("../Shared/Error", errorInfo);
            }

            WriteLogs("Выводятся все страны\n", NLogsModeEnum.Debug);

            return View(countries);
        }

        /// <summary>
        /// Метод вывода страны по ее id
        /// </summary>
        /// <param name="id">уникальный идентификатор страны</param>
        /// <returns></returns>
        public IActionResult GetCountry(int id)
        {
            WriteLogs($"Переход по маршруту /Countries/GetCountry?id={id}. ", NLogsModeEnum.Trace);

            var country = _AllCountries.GetItemById(id);
            
            var cities = _AllCities.GetAllItems().Where(c => c.CountryId == id);
            country.Cities = cities;

            if (country.Id == 0)
            {
                var errorMessage = $"В БД нет страны с id = {id}";
                var errorInfo = new ErrorViewModel(errorMessage);

                WriteLogs($"{errorMessage}. Возвращено ../Shared/Error.cshtml\n", NLogsModeEnum.Warn);

                return View("../Shared/Error", errorInfo);
            }

            WriteLogs($"Выводится страна с id = {id}.\n", NLogsModeEnum.Debug);

            return View(country);
        }

        /// <summary>
        /// Метод создания страны
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        public IActionResult CreateCountry()
        {
            WriteLogs("Выполняется действие /Countries/CreateCountry. ", NLogsModeEnum.Trace);

            var country = new Country();

            WriteLogs("Возвращено /Countries/EditCountry.cshtml\n", NLogsModeEnum.Trace);

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
            WriteLogs("Переход по маршруту /Countries/EditCountry. ", NLogsModeEnum.Trace);

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
            WriteLogs("Переход по маршруту /Countries/GetQueryResultCountries. ", NLogsModeEnum.Trace);

            var countries = _AllCountries.GetQueryResultItemsAfterFullName(fullNameOrKeywordOfItem, isFullName);
            var numberCountries = countries.Count();

            if (numberCountries == 0)
            {
                var message = $"Нет стран по запросу \"{fullNameOrKeywordOfItem}\". Возвращено ../Shared/Nothing.cshtml\n";

                WriteLogs(message, NLogsModeEnum.Warn);

                var nothingInfo = new ErrorViewModel(message);
                return View("../Shared/Nothing", nothingInfo);
            }

            WriteLogs($"Выводятся все страны по запросу \"{fullNameOrKeywordOfItem}\".\n", NLogsModeEnum.Debug);

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

            WriteLogs("Возвращено ../Shared/SuccessForDelete.cshtml\n", NLogsModeEnum.Trace);

            return View("../Shared/SuccessForDelete", country);
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
            WriteLogs("Запущен процесс сохранения страны в БД. ", NLogsModeEnum.Debug);

            if (ModelState.IsValid)
            {
                WriteLogs("Модель Country прошла валидацию. ", NLogsModeEnum.Debug);

                // если мы хотим поменять картинку
                if (changeTitleImagePath != null)
                {
                    var folder = "/images/CountriesTitleImages/";
                    await FileUtils.SaveFileIfExistPath(folder, changeTitleImagePath);
                    country.TitleImagePath = $"{folder}{changeTitleImagePath.FileName}";
                }

                country.FullDescription = formValues["fullInfoAboutCountry"];
                country.DateAdded = DateTime.Now;

                _AllCountries.SaveItem(country, country.Id);

                WriteLogs("Страна успешно сохранена в БД. ", NLogsModeEnum.Debug);
                WriteLogs("Возвращено ../Shared/Success.cshtml\n", NLogsModeEnum.Trace);

                return View("../Shared/Success", country);
            }
            else
            {
                WriteLogs("Модель Country не прошла валидацию. ", NLogsModeEnum.Warn);
                WriteLogs("Возвращено /Countries/EditCountry.cshtml\n", NLogsModeEnum.Trace);

                country.FullDescription = formValues["fullInfoAboutCountry"];

                return View("EditCountry", country);
            }
        }


    }
}
