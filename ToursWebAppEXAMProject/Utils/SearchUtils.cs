using Microsoft.AspNetCore.Mvc.Rendering;
using NLog;
using System.Text.Json;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Utils
{
    public class SearchUtils
	{
		private readonly IQueryResultInterface _QueryResult;
		private readonly IBaseInterface<Country> _AllCountries;
		private readonly IBaseInterface<City> _AllCities;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public SearchUtils(IQueryResultInterface QueryResult, IBaseInterface<Country> AllCountries, IBaseInterface<City> AllCities) 
		{
			_QueryResult = QueryResult;
			_AllCountries = AllCountries;
			_AllCities = AllCities;
		}
		
		/// <summary>
        /// Метод создания вью-модели для стартовой страницы поиска турпродуктов
        /// </summary>
        /// <param name="countryName">Название страны</param>
        /// <returns></returns>
        public SearchProductViewModel GetModel(string countryName)
		{
            var searchViewModel = new SearchProductViewModel();

            try
			{
                var country = GetCountryBySelectedName(countryName);
                var city = GetCityByCountrySelectedName(countryName);

                if (country != null && country.Name == countryName)
                {
                    searchViewModel.CountryId = country.Id;
                    searchViewModel.CountryNameSelected = country.Name;
                    searchViewModel.MapImagePath = country.CountryMapPath;
                    
                    var allCountries = GetAllCountries();
                    searchViewModel.Countries = allCountries;
                    searchViewModel.CountriesList = GetAllCountriesSelectList(countryName);
                }
                else
                {
                    _logger.Warn($"Ошибка поиска страны. Страна с названием {countryName} отсутствует в БД ");
                }

                if (city != null)
                {
                    searchViewModel.CityNameSelected = city.Name;
                    
                    var allCities = GetAllCities(countryName);
                    searchViewModel.Cities = allCities;
                    searchViewModel.CitiesList = GetAllCitiesSelectList(countryName);
                }
                else
                {
                    _logger.Warn($"Ошибка поиске города. Нет ни одного города для страны {countryName} в БД ");
                }

                searchViewModel.AllCountriesAndCitiesByString = _QueryResult
					.GetAllCountriesAndCitiesByString();
                searchViewModel.AllCountriesAndMapsByString = _QueryResult
					.GetAllCountryMapsByString();

            }
            catch (Exception error)
			{
				_logger.Error($"Ошибка создания SearchProductViewModel: {error.Message}");
            }

			return searchViewModel;
		}

        /// <summary>
        /// Метод создания вью-модели для вывода результатов поиска турпродуктов с данными, введенными пользователями
        /// </summary>
        /// <param name="viewModel">Вью-модель для поиска турпродуктов</param>
        /// <param name="formValues">Данные формы ввода типа IFormCollection</param>
        /// <returns></returns>
        public SearchProductViewModel GetModel(SearchProductViewModel viewModel, IFormCollection formValues)
		{
			var searchViewModel = viewModel;

			try
			{
                var countryName = formValues["CountryNameSelected"].ToString();
                var cityName = formValues["CityNameSelected"].ToString();

                var country = GetCountryBySelectedName(countryName);
                var city = GetCityByCitySelectedName(countryName, cityName);

				if (country != null && country.Name == countryName)
                {
                    searchViewModel.CountryId = country.Id;
                    searchViewModel.CountryNameSelected = country.Name;
                    searchViewModel.MapImagePath = country.CountryMapPath;
                    
                    // передаваемые данные через элементы форм в строковом формате
                    searchViewModel.Countries = GetAllCountries();
                    searchViewModel.CountriesList = GetAllCountriesSelectList(countryName);
				}
                else
                {
                    _logger.Warn($"Ошибка поиска страны. Страна с названием {countryName} отсутствует в БД ");
                }

                if (city != null)
                {
                    searchViewModel.CityNameSelected = city.Name;
                    
                    var allCities = GetAllCities(countryName);
                    searchViewModel.Cities = allCities;
                    searchViewModel.CitiesList = GetAllCitiesSelectList(countryName);
                }
                else
                {
					_logger.Warn($"Ошибка поиске города. Нет ни одного города для страны {countryName} в БД ");
                }

                searchViewModel.AllCountriesAndCitiesByString = _QueryResult
					.GetAllCountriesAndCitiesByString();
                searchViewModel.AllCountriesAndMapsByString = _QueryResult
					.GetAllCountryMapsByString();

            }
            catch (Exception error)
            {
                _logger.Error($"Ошибка создания SearchProductViewModel: {error.Message}");
            }

			return searchViewModel;
		}

		/// <summary>
		/// Метод создания списка стран в формате SelectList
		/// </summary>
		/// <param name="countryNameSelected">Выбранная страна</param>
		/// <returns></returns>
		private SelectList GetAllCountriesSelectList(string countryNameSelected)
		{
			return new SelectList(GetAllCountries(), countryNameSelected);
		}

		/// <summary>
		/// Метод создания списка стран в формате List<string>
		/// </summary>
		/// <returns></returns>
		private List<string> GetAllCountries()
		{
			var countriesList = new List<string>();
			var countriesFromDB = _AllCountries.GetAllItems();

			foreach (var country in countriesFromDB)
			{
				countriesList.Add(country.Name);
			}

			return countriesList;
		}

		/// <summary>
		/// Метод определения Id выбранной страны
		/// </summary>
		/// <param name="countryNameSelected">Выбранная страна</param>
		/// <returns></returns>
		private Country GetCountryBySelectedName(string countryNameSelected)
		{
			var country = _AllCountries.GetAllItems()
				.FirstOrDefault(c => c.Name == countryNameSelected);
			
			if (country == null)
			{
				country = new Country();
			}

			return country;
		}

		/// <summary>
		/// Метод поиска первого из городов страны по ее названию
		/// </summary>
		/// <param name="countryNameSelected">Выбранная страна</param>
		/// <returns></returns>
		private City GetCityByCountrySelectedName(string countryNameSelected)
		{
			var city = _QueryResult.GetCitiesByCountryName(countryNameSelected).First() ?? new City();
            return city;
		}

        /// <summary>
        /// Метод возврата экземпляра города по его названию
        /// </summary>
        /// <param name="countryNameSelected">Выбранная страна</param>
		/// <param name="cityNameSelected">Выбранный город</param>
        /// <returns></returns>
        private City GetCityByCitySelectedName(string countryNameSelected, string cityNameSelected)
        {
            var city = _QueryResult.GetCitiesByCountryName(countryNameSelected).FirstOrDefault
				(n => n.Name == cityNameSelected) ?? new City();
            return city;
        }

        /// <summary>
        /// Метод создания списка городов выбранной страны в формате SelectList
        /// </summary>
        /// <param name="countryNameSelected">Выбранная страна</param>
        /// <returns></returns>
        private SelectList? GetAllCitiesSelectList(string countryNameSelected)
		{
			return new SelectList(GetAllCities(countryNameSelected), countryNameSelected);
		}

		/// <summary>
		/// Метод создания списка городов выбранной страны в формате List<string>
		/// </summary>
		/// <param name="countryNameSelected">Выбранная страна</param>
		/// <returns></returns>
		private List<string> GetAllCities(string countryNameSelected)
		{
			var citiesList = new List<string>();
			var citiesFromDB = _QueryResult.GetCitiesByCountryName(countryNameSelected);

			foreach (var city in citiesFromDB)
			{
				citiesList.Add(city.Name);
			}

			return citiesList;
		}
       

        /// <summary>
        /// Метод преобразования List<string> в строку
        /// </summary>
        /// <param name="listOfStrings">коллекция строк</param>
        /// <returns></returns>
        public string ParseListOfStringsToString(List<string> allItemsListOfString)
		{
			var allItemsOneString = "";
			allItemsOneString = String.Join(",", allItemsListOfString);

			return allItemsOneString;
		}

		/// <summary>
		/// Метод преобразования SelectList в строку по названию страны
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
		/// Метод преобразования строки в List<string>
		/// </summary>
		/// <param name="allItems">Все страны/города</param>
		/// <returns></returns>
		private List<string>? ParseStringToListOfStrings(string allItemsOneString)
		{
			var allItemsListOfString = new List<string>();

			allItemsListOfString = allItemsOneString.Split(",").ToList();

			return allItemsListOfString;
		}
		
		/// <summary>
		/// Метод преобразования строки в SelectList
		/// </summary>
		/// <param name="allItems">Все страны/города</param>
		/// <param name="selectedItemName">Название выбранной страны/города</param>
		/// <returns></returns>
		private SelectList? ParseStringToSelectList(string allItemsOneString, string selectedItemName)
		{
			var allItemsListOfString = ParseStringToListOfStrings(allItemsOneString);
			var selectList = new SelectList(allItemsListOfString, selectedItemName);

			return selectList;
		}
		

        /// <summary>
        /// Метод поиска туристических продуктов по запросу во вью-модели SearchProductViewModel
        /// </summary>
        /// <param name="searchViewModel"></param>
        /// <returns></returns>
        public List<Product> GetProductsQueryResult(string countryName, string cityName)
        {
            var products = new List<Product>();

            if (countryName != "" && cityName != "")
            {
                products = (List<Product>)_QueryResult.GetProductsByCountryNameAndCityName(countryName, cityName);
            }
            return products;
        }

        public QueryResultProductViewModel GetQueryResulpProductsViewModel(SearchProductViewModel searchViewModel, List<Product> products, string countryName, string cityName)
        {
            var viewModel = new QueryResultProductViewModel
            {
                Products = products,
                DateFrom = searchViewModel.DateFrom,
                DateTo = searchViewModel.DateTo,
                NumberOfDaysFromSelectList = searchViewModel.NumberOfDaysFromSelectList,
                NumberOfPeopleFromSelectList = searchViewModel.NumberOfPeopleFromSelectList,
                Country = _AllCountries.GetAllItems().FirstOrDefault(c => c.Name == searchViewModel.CountryNameSelected) ?? new Country(),
                City = _AllCities.GetAllItems().FirstOrDefault(c => c.Name == searchViewModel.CityNameSelected) ?? new City(),
            };
			
			return viewModel;
        }
    }
}
