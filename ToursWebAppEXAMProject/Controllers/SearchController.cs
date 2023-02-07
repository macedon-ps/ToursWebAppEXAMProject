using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLog;
using System.Collections;
using System.Diagnostics;
using System.Text.Json;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Repositories;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Controllers
{
	public class SearchController: Controller
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		private readonly DataManager DataManager;

		public SearchController(DataManager DataManager)
		{
			this.DataManager = DataManager;
		}

		/// <summary>
		/// Метод Index(), кот. выводит дефолтный вид страницы Search
		/// </summary>
		/// <returns></returns>
		[HttpGet]		
		public IActionResult Index()
		{
			var searchViewModel = new SearchProductViewModel();

			// задаем значение по умолчанию для countryNameSelected, для стартовой страницы Index.cshtml
			var countryNameSelected = "Турция";
			
			searchViewModel.CountryId = GetIdOfSelectedCountry(countryNameSelected);
			searchViewModel.CountryNameSelected = countryNameSelected;
			var allCountries = GetAllCountries();
			searchViewModel.Countries = allCountries;
			searchViewModel.CountriesList = GetAllCountriesSelectList(countryNameSelected);
			
			var cityNameSelected = GetCityNameSelected(countryNameSelected);
			searchViewModel.CityNameSelected = cityNameSelected;
			var allCities = GetAllCities(countryNameSelected);
			searchViewModel.Cities = allCities;
			searchViewModel.CitiesList = GetAllCitiesSelectList(countryNameSelected);

			searchViewModel.AllCountriesWithCitiesListByOneString = DataManager.CollectionOfCitiesAfterParamsInterface.
				GetAllCountriesWithCitiesListByOneString();


			logger.Trace("Переход по маршруту /Search/Index. Возвращено представление Search/Index.cshtml\n");
			Console.WriteLine("Переход по маршруту /Search/Index. Возвращено представление Search/Index.cshtml\n");
			return View(searchViewModel);
		}

		/// <summary>
		/// POST версия метода Index(SearchProductViewModel searchProductViewModel), кот. принимает введенную модель
		/// </summary>
		/// <param name="searchProductViewModel"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult Index(SearchProductViewModel searchViewModel, IFormCollection formValues)
		{
			var countryName = searchViewModel.CountryNameSelected;
			var cityName = searchViewModel.CityNameSelected;

			// передаваемые данные через элементы форм в строковом формате
			var allCountries = formValues["CountriesOneStringFormValue"];
			var allCities = formValues["CitiesOneStringFormValue"];
			
			// преобразование данных из строки в List<string>
			searchViewModel.Countries = ParseStringToListOfStrings(allCountries);
			searchViewModel.Cities = ParseStringToListOfStrings(allCities);
			
			// создание элементов SelectList
			searchViewModel.CountriesList = ParseStringToSelectList(allCountries, countryName);
			searchViewModel.CitiesList = ParseStringToSelectList(allCities, cityName);

			if (ModelState.IsValid)
			{
				logger.Debug("Модель SearchProductViewModel успешно прошла валидацию");
				Console.WriteLine("Модель SearchProductViewModel успешно прошла валидацию");
				logger.Trace("Переход по маршруту /Search/Index. Возвращено представление Search/Index.cshtml\n");
				Console.WriteLine("Переход по маршруту /Search/Index. Возвращено представление Search/Index.cshtml\n");
				return View(searchViewModel);
			}
			logger.Debug("Модель SearchProductViewModel не прошла валидацию");
			Console.WriteLine("Модель SearchProductViewModel не прошла валидацию");
			logger.Trace("Переход по маршруту /Search/Index. Возвращено представление Search/Index.cshtml\n");
			Console.WriteLine("Переход по маршруту /Search/Index. Возвращено представление Search/Index.cshtml\n");
			return View();
		}

		
		/// <summary>
		/// Метод GetAllCountriesSelectList(string countryNameSelected) для создания списка стран в формате SelectList
		/// </summary>
		/// <param name="countryNameSelected">Выбранная страна</param>
		/// <returns></returns>
		private SelectList GetAllCountriesSelectList(string countryNameSelected)
		{
			return new SelectList(GetAllCountries(), countryNameSelected);
		}

		/// <summary>
		/// Метод GetAllCountries() для создания списка стран в формате List<string>
		/// </summary>
		/// <returns></returns>
		private List<string> GetAllCountries()
		{
			var countriesList = new List<string>();
			var countriesFromDB = DataManager.CountryBaseInterface.GetAllItems();

			foreach (var country in countriesFromDB)
			{
				countriesList.Add(country.Name);
			}

			return countriesList;
		}

		/// <summary>
		/// Метод GetIdOfSelectedCountry(string countryNameSelected) для определения Id выбранной страны
		/// </summary>
		/// <param name="countryNameSelected">Выбранная страна</param>
		/// <returns></returns>
		private int GetIdOfSelectedCountry(string countryNameSelected)
        {
			var countryId = DataManager.CountryBaseInterface.GetAllItems()
				.FirstOrDefault(c=> c.Name == countryNameSelected).Id;

			return countryId;
        }

		/// <summary>
		/// Метод GetCityNameSelected(string countryNameSelected) для поиска первого из городов страны по ее названию
		/// </summary>
		/// <param name="countryNameSelected">Выбранная страна</param>
		/// <returns></returns>
		private string? GetCityNameSelected(string countryNameSelected)
		{
			var cityNameSelected = DataManager.CollectionOfCitiesAfterParamsInterface.GetQueryResultItemsAfterCountryName(countryNameSelected).First().Name;

			return cityNameSelected;
		}

		/// <summary>
		/// Метод GetAllCitiesSelectList(string countryNameSelected) для создания списка городов выбранной страны в формате SelectList
		/// </summary>
		/// <param name="countryNameSelected">Выбранная страна</param>
		/// <returns></returns>
		private SelectList? GetAllCitiesSelectList(string countryNameSelected)
		{
			return new SelectList(GetAllCities(countryNameSelected), countryNameSelected);
		}

		/// <summary>
		/// Метод GetAllCities(string countryNameSelected) для создания списка городов выбранной страны в формате List<string>
		/// </summary>
		/// <param name="countryNameSelected">Выбранная страна</param>
		/// <returns></returns>
		private List<string> GetAllCities(string countryNameSelected)
		{
			var citiesList = new List<string>();
			var citiesFromDB = DataManager.CollectionOfCitiesAfterParamsInterface.GetQueryResultItemsAfterCountryName(countryNameSelected);

			foreach (var city in citiesFromDB)
			{
				citiesList.Add(city.Name);
			}

			return citiesList;
		}

		/// <summary>
		/// Метод ParseListOfStringsToString(List<string> listOfStrinfs) для пробразования List<string> в строку
		/// </summary>
		/// <param name="listOfStrings">коллекция строк</param>
		/// <returns></returns>
		private string ParseListOfStringsToString(List<string> allItemsListOfString)
		{
			var allItemsOneString = "";
			allItemsOneString = String.Join(",", allItemsListOfString);

			return allItemsOneString;
		}

		/// <summary>
		/// Метод ParseSelectListToString(List<string> listOfStrinfs, string countryName) для пробразования SelectList в строку по названию страны
		/// </summary>
		/// <param name="listOfStrings">Коллекция строк</param>
		/// <param name="countryName">Выбранная страна</param>
		/// <returns></returns>
		public string ParseSelectListToString(List<string> allItemsListOfString, string selectedItemName)
		{
			var selectListOneString = "";
			var selectList = new SelectList(allItemsListOfString, selectedItemName);
			selectListOneString = JsonSerializer.Serialize<SelectList>(selectList);

			return selectListOneString;
		}

		/// <summary>
		/// Метод ParseStringToListOfStrings(string? allItems) для пробразования строки в List<string>
		/// </summary>
		/// <param name="allItems">Все страны/города</param>
		/// <returns></returns>
		private List<string>? ParseStringToListOfStrings(string? allItemsOneString)
		{
			var allItemsListOfString = new List<string>();

			allItemsListOfString = allItemsOneString.Split(",").ToList();

			return allItemsListOfString;
		}

		/// <summary>
		/// Метод ParseStringToSelectList(string? allCountries, string? countryName) для пробразования строки в SelectList
		/// </summary>
		/// <param name="allItems">Все страны/города</param>
		/// <param name="selectedItemName">Название выбранной страны/города</param>
		/// <returns></returns>
		private SelectList? ParseStringToSelectList(string? allItemsOneString, string selectedItemName)
		{
			var allItemsListOfString = ParseStringToListOfStrings(allItemsOneString);
			var selectList = new SelectList(allItemsListOfString, selectedItemName);

			return selectList;
		}



		/// <summary>
		/// Метод GetProduct(), кот. возвращает данные турпродукта по его id
		/// </summary>
		/// <param name="id">уникальный идентификатор турпродукта</param>
		/// <returns></returns>
		public IActionResult GetProduct(int id)
		{
			logger.Trace($"Переход по маршруту /Search/GetProduct?id={id}");
			Console.WriteLine($"Переход по маршруту /Search/GetProduct?id={id}");

			var product = DataManager.ProductBaseInterface.GetItemById(id);
			
			if (product.Id == 0)
			{
				logger.Warn($"Возвращено представление /Error.cshtml\n");
				Console.WriteLine($"Возвращеноя представление /Error.cshtml\n");

				// message = "";
				var errorInfo = new ModelsErrorViewModel(typeof(Product), id);
				return View("Error", errorInfo);
			}

			logger.Debug($"Возвращено представление /Search/GetProduct.cshtml\n");
			Console.WriteLine($"Возвращено представление /Search/GetProduct.cshtml\n");
			return View(product);
		}

		/// <summary>
		/// Метод GetAllProducts(), кот. возвращает данные всех туристических продуктов из БД
		/// </summary>
		/// <returns></returns>
		public IActionResult GetAllProducts()
		{
			logger.Trace("Переход по маршруту /Search/GetAllProducts");
			Console.WriteLine("Переход по маршруту /Search/GetAllProducts");

			var products = DataManager.ProductBaseInterface.GetAllItems();
			
			if (products == null)
			{
				logger.Warn("Возвращено представление /Error.cshtml\n");
				Console.WriteLine("Возвращено представление /Error.cshtml\n");

				// message = "";
				var errorInfo = new ModelsErrorViewModel(typeof(List<Product>));
				return View("Error", errorInfo);
			}
			
			logger.Debug("Возвращено представление /Search/GetAllProducts.cshtml\n");
			Console.WriteLine("Возвращено представление /Search/GetAllProducts.cshtml\n");
			return View(products);
		}

		/// <summary>
		/// Метод GetQueryResultProducts(), кот. возвращает данные некоторых туристических продуктов из БД по имени / ключевому слову
		/// </summary>
		/// <returns></returns>
		public IActionResult GetQueryResultProducts(string keyword, bool isFullName)
		{
			logger.Trace("Переход по маршруту /Search/GetAllProducts");
			Console.WriteLine("Переход по маршруту /Search/GetAllProducts");

			var products = DataManager.ProductBaseInterface.GetQueryResultItemsAfterFullName(keyword, isFullName);

			if (products == null)
			{
				logger.Warn("Возвращено представление /Error.cshtml\n");
				Console.WriteLine("Возвращено представление /Error.cshtml\n");

				// message = "";
				var errorInfo = new ModelsErrorViewModel(typeof(List<Product>));
				return View("Error", errorInfo);
			}

			logger.Debug("Возвращено представление /Search/GetAllProducts.cshtml\n");
			Console.WriteLine("Возвращено представление /Search/GetAllProducts.cshtml\n");
			return View(products);
		}

		/// <summary>
		/// Метод TechTaskSearch() для отображения данных о выполнении ТЗ на странице Search
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IActionResult TechTaskSearch()
		{
			logger.Trace("Переход по маршруту Search/TechTaskSearch. Возвращаено представление Search/TechTaskSearch.cshtml\n");
			Console.WriteLine("Переход по маршруту Search/TechTaskSearch. Возвращаено представление Search/TechTaskSearch.cshtml\n");

			var pageName = "Search";
			var model = DataManager.TechTaskInterface.GetTechTasksForPage(pageName);

			return View(model);
		}

		/// <summary>
		/// Метод TechTaskSearch(TechTaskViewModel model) для редактирования и сохранения данных о выполнении ТЗ на странице Search
		/// </summary>
		/// <param name="model">Сохраняемая вью-модель</param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult TechTaskSearch(TechTaskViewModel model)
		{
			logger.Debug("Запущен процесс сохранения показателей выполнения тех. задания в БД");
			Console.WriteLine("Запущен процесс сохранения показателей выполнения тех. задания в БД");

			if (ModelState.IsValid)
			{
				logger.Debug("Модель TechTaskViewModel успешно прошла валидацию");
				Console.WriteLine("Модель TechTaskViewModel успешно прошла валидацию");

				double TechTasksCount = 6;
				double TechTasksTrueCount = 0;
				if (model.IsExecuteTechTask1 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask2 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask3 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask4 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask5 == true) TechTasksTrueCount++;
				if (model.IsExecuteTechTask6 == true) TechTasksTrueCount++;

				double ExecuteTechTasksProgress = Math.Round((TechTasksTrueCount / TechTasksCount) * 100);
				model.ExecuteTechTasksProgress = ExecuteTechTasksProgress;

				DataManager.TechTaskInterface.SaveProgressTechTasks(model);
				logger.Debug("Показатели выполнения тех. задания успешно сохранены в БД");
				Console.WriteLine("Показатели выполнения тех. задания успешно сохранены в БД");
				logger.Debug("Возвращено представление /Search/TechTaskSearch.cshtml\n");
				Console.WriteLine("Возвращено представление /Search/TechTaskSearch.cshtml\n");
				
				return View(model);
			}
			logger.Debug("Модель TechTaskViewModel не прошла валидацию");
			Console.WriteLine("Модель TechTaskViewModel не прошла валидацию");
			logger.Debug("Возвращено представление /Search/TechTaskSearch.cshtml\n");
			Console.WriteLine("Возвращено представление /Search/TechTaskSearch.cshtml\n");

			return View(model);
		}
	}
}
