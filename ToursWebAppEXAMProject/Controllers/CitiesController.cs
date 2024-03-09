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
    public class CitiesController : Controller
    {
        private readonly IBaseInterface<City> _AllCities;
        private readonly IBaseInterface<Country> _AllCountries;
        private readonly FileUtils _FileUtils;

       
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
            WriteLogs("Переход по маршруту /Cities/GetAllCities. ", NLogsModeEnum.Trace);

            var cities = _AllCities.GetAllItems();

            if (cities == null)
            {
                var errorMessage = "В БД нет ни одного города";
                var errorInfo = new ErrorViewModel(errorMessage);

                WriteLogs($"{errorMessage}. Возвращено ../Shared/Error.cshtml\n", NLogsModeEnum.Warn);

                return View("../Shared/Error", errorInfo);
            }

            WriteLogs("Выводятся все города\n", NLogsModeEnum.Debug);

            return View(cities);
        }

        /// <summary>
        /// Метод вывода города по его id
        /// </summary>
        /// <param name="id">уникальный идентификатор города</param>
        /// <returns></returns>
        public IActionResult GetCity(int id)
        {
            WriteLogs($"Переход по маршруту /Cities/GetCity?id={id}. ", NLogsModeEnum.Trace);

            var city = _AllCities.GetItemById(id);

            if (city.Id == 0)
            {
                var errorMessage = $"В БД нет города с id = {id}";
                var errorInfo = new ErrorViewModel(errorMessage);

                WriteLogs($"{errorMessage}. Возвращено ../Shared/Error.cshtml\n", NLogsModeEnum.Warn);

                return View("../Shared/Error", errorInfo);
            }

            WriteLogs($"Выводится город с id = {id}.\n", NLogsModeEnum.Debug);

            return View(city);
        }

        /// <summary>
        /// Метод создания города
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        public IActionResult CreateCity()
        {
            WriteLogs("Выполняется действие /Cities/CreateCity. ", NLogsModeEnum.Trace);

            var cityViewModel = new CreateCityViewModel();
            var city = new City();
            var countries = _AllCountries.GetAllItems();
            cityViewModel.City = city;
            cityViewModel.Countries = countries;

            WriteLogs("Возвращено /Cities/CreateCity.cshtml\n", NLogsModeEnum.Trace);

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
            WriteLogs("Переход по маршруту /Cities/EditCity. ", NLogsModeEnum.Trace);

            // TODO: вставить индекс страны при редактировании

            var city = _AllCities.GetItemById(id);
            
            city.DateAdded = DateTime.Now;

            return View(city);
        }

        /// <summary>
        /// Метод вывода результатов выборки городов по тому, что ищем - полное название или ключевое слово (букву)
        /// </summary>
        /// <param name="isFullName">полное название - true, ключевое слово (буква) - false</param>
        /// <param name="fullNameOrKeywordOfItem">текст для поиска</param>
        /// <returns></returns>
        [Authorize(Roles = "superadmin,editor")]
        [HttpGet]
        public IActionResult GetQueryResultCities(bool isFullName, string fullNameOrKeywordOfItem)
        {
            WriteLogs("Переход по маршруту /Cities/GetQueryResultCities. ", NLogsModeEnum.Trace);

            var cities = _AllCities.GetQueryResultItemsAfterFullName(fullNameOrKeywordOfItem, isFullName);
            var numberCities = cities.Count();

            if (numberCities == 0)
            {
                var message = $"Нет городов по запросу \"{fullNameOrKeywordOfItem}\". Возвращено ../Shared/Nothing.cshtml\n";

                WriteLogs(message, NLogsModeEnum.Warn);

                var nothingInfo = new ErrorViewModel(message);
                return View("../Shared/Nothing", nothingInfo);
            }

            WriteLogs($"Выводятся все города по запросу \"{fullNameOrKeywordOfItem}\".\n", NLogsModeEnum.Debug);

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

            WriteLogs("Возвращено ../Shared/SuccessForDelete.cshtml\n", NLogsModeEnum.Trace);

            return View("../Shared/SuccessForDelete", city);
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
                WriteLogs("Запущен процесс сохранения города в БД. ", NLogsModeEnum.Debug);

                if (ModelState.IsValid)
                {
                    WriteLogs("Модель City прошла валидацию. ", NLogsModeEnum.Debug);

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

                    WriteLogs("Город успешно сохранен в БД. ", NLogsModeEnum.Debug);
                    WriteLogs("Возвращено ../Shared/Success.cshtml\n", NLogsModeEnum.Trace);

                    return View("../Shared/Success", city);
                }
                else
                {
                    WriteLogs("Модель City не прошла валидацию. ", NLogsModeEnum.Warn);
                    WriteLogs("Возвращено /Cities/EditCity.cshtml\n", NLogsModeEnum.Trace);

                    city.FullDescription = formValues["fullInfoAboutCity"];

                    return View("EditCity", city);
                }
            }
            catch (Exception ex)
            {
                WriteLogs($"{ex.Message}", NLogsModeEnum.Error);
                return View("Error", ex.Message);
            }
         }
    }
}
