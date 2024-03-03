using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Controllers
{
	public partial class SearchController : Controller
	{
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
                    searchViewModel.CountryDescription = country.ShortDescription;

                    var allCountries = GetAllCountries();
                    searchViewModel.Countries = allCountries;
                    searchViewModel.CountriesList = GetAllCountriesSelectList(countryName);
                }
                else
                {
                    WriteLogs($"Ошибка поиска страны. Страна с названием {countryName} отсутствует в БД ", NLogsModeEnum.Warn);
                }

                if (city != null)
                {
                    searchViewModel.CityNameSelected = city.Name;
                    searchViewModel.CityDescrition = city.ShortDescription;

                    var allCities = GetAllCities(countryName);
                    searchViewModel.Cities = allCities;
                    searchViewModel.CitiesList = GetAllCitiesSelectList(countryName);
                    
					// TODO: определить данные для LocalDescrition
                    //searchViewModel.LocalDescrition = 
                }
                else
                {
                    WriteLogs($"Ошибка поиске города. Нет ни одного города для страны {countryName} в БД ", NLogsModeEnum.Warn);
                }

                searchViewModel.AllCountriesAndCitiesByString = _QueryResult
					.GetAllCountriesAndCitiesByString();
                searchViewModel.AllCountriesAndMapsByString = _QueryResult
					.GetAllCountryMapsByString();

            }
            catch (Exception error)
			{
				WriteLogs($"Ошибка создания SearchProductViewModel: {error.Message}", NLogsModeEnum.Error);
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
			var searchViewModel = viewModel as SearchProductViewModel;

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
                    searchViewModel.CountryDescription = country.ShortDescription;

                    // передаваемые данные через элементы форм в строковом формате
                    searchViewModel.Countries = GetAllCountries();
                    searchViewModel.CountriesList = GetAllCountriesSelectList(countryName);
				}
                else
                {
                    WriteLogs($"Ошибка поиска страны. Страна с названием {countryName} отсутствует в БД ", NLogsModeEnum.Warn);
                }

                if (city != null)
                {
                    searchViewModel.CityNameSelected = city.Name;
                    searchViewModel.CityDescrition = city.ShortDescription;

                    var allCities = GetAllCities(countryName);
                    searchViewModel.Cities = allCities;
                    searchViewModel.CitiesList = GetAllCitiesSelectList(countryName);

                    // TODO: определить данные для LocalDescrition
                    //searchViewModel.LocalDescrition = 
                }
                else
                {
                    WriteLogs($"Ошибка поиске города. Нет ни одного города для страны {countryName} в БД ", NLogsModeEnum.Warn);
                }

                searchViewModel.AllCountriesAndCitiesByString = _QueryResult
					.GetAllCountriesAndCitiesByString();
                searchViewModel.AllCountriesAndMapsByString = _QueryResult
					.GetAllCountryMapsByString();

            }
            catch (Exception error)
            {
                WriteLogs($"Ошибка создания SearchProductViewModel: {error.Message}", NLogsModeEnum.Error);
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
			var city = _QueryResult.GetCitiesByCountryName(countryNameSelected).First();

			if(city == null)
			{
				city = new City();
			}

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
				(n => n.Name == cityNameSelected);

            if (city == null)
            {
                city = new City();
            }

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
        public IActionResult GetQueryResultProductsByCountryAndCityName(string countryName, string cityName)
        {
            var products = _QueryResult.GetProductsByCountryNameAndCityName(countryName, cityName);

            return View("GetAllProducts", products);
        }

        /// <summary>
        /// Метод преобразования List<string> в строку
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
		private List<string>? ParseStringToListOfStrings(string? allItemsOneString)
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
		private SelectList? ParseStringToSelectList(string? allItemsOneString, string selectedItemName)
		{
			var allItemsListOfString = ParseStringToListOfStrings(allItemsOneString);
			var selectList = new SelectList(allItemsListOfString, selectedItemName);

			return selectList;
		}
	}
}
