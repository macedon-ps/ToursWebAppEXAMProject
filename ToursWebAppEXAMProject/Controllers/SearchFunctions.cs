using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Controllers
{
	public partial class SearchController : Controller
	{
		
		public SearchProductViewModel GetModel()
		{
			var searchViewModel = new SearchProductViewModel();

			// задаем значение по умолчанию для defoultCountryNameSelected, для стартовой страницы Index.cshtml
			var defoultCountryNameSelected = "Турция";

			searchViewModel.CountryId = GetIdOfSelectedCountry(defoultCountryNameSelected);
			searchViewModel.CountryNameSelected = defoultCountryNameSelected;
			var allCountries = GetAllCountries();
			searchViewModel.Countries = allCountries;
			searchViewModel.CountriesList = GetAllCountriesSelectList(defoultCountryNameSelected);

			var cityNameSelected = GetCityNameSelected(defoultCountryNameSelected);
			searchViewModel.CityNameSelected = cityNameSelected;
			var allCities = GetAllCities(defoultCountryNameSelected);
			searchViewModel.Cities = allCities;
			searchViewModel.CitiesList = GetAllCitiesSelectList(defoultCountryNameSelected);

			searchViewModel.AllCountriesWithCitiesListByOneString = DataManager.CollectionOfCitiesAfterParamsInterface.
				GetAllCountriesWithCitiesListByOneString();

			return searchViewModel;
		}

		public SearchProductViewModel GetModel(SearchProductViewModel viewModel, IFormCollection formValues)
		{
			var searchViewModel = viewModel as SearchProductViewModel;

			var countryName = formValues["countriesSelect"].ToString();

			
				searchViewModel.CountryId = GetIdOfSelectedCountry(countryName);
				searchViewModel.CountryNameSelected = countryName;
				// var cityName = formValues["citiesSelect"];

				// передаваемые данные через элементы форм в строковом формате
				searchViewModel.Countries = GetAllCountries();
				searchViewModel.CountriesList = GetAllCountriesSelectList(countryName);

				var cityNameSelected = GetCityNameSelected(countryName);
				searchViewModel.CityNameSelected = cityNameSelected;
				var allCities = GetAllCities(countryName);
				searchViewModel.Cities = allCities;
				searchViewModel.CitiesList = GetAllCitiesSelectList(countryName);

				/*var allCities = formValues["CitiesOneStringFormValue"];

				// преобразование данных из строки в List<string>
				searchViewModel.Countries = ParseStringToListOfStrings(allCountries);
				searchViewModel.Cities = ParseStringToListOfStrings(allCities);

				// создание элементов SelectList
				searchViewModel.CountriesList = ParseStringToSelectList(allCountries, countryName);
				searchViewModel.CitiesList = ParseStringToSelectList(allCities, cityName);*/
			



			return searchViewModel;
		}



		/// <summary>
		/// Метод GetAllCountriesSelectList(string defoultCountryNameSelected) для создания списка стран в формате SelectList
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
		/// Метод GetIdOfSelectedCountry(string defoultCountryNameSelected) для определения Id выбранной страны
		/// </summary>
		/// <param name="countryNameSelected">Выбранная страна</param>
		/// <returns></returns>
		private int GetIdOfSelectedCountry(string countryNameSelected)
		{
			var countryId = DataManager.CountryBaseInterface.GetAllItems()
				.FirstOrDefault(c => c.Name == countryNameSelected).Id;

			return countryId;
		}

		/// <summary>
		/// Метод GetCityNameSelected(string defoultCountryNameSelected) для поиска первого из городов страны по ее названию
		/// </summary>
		/// <param name="countryNameSelected">Выбранная страна</param>
		/// <returns></returns>
		private string? GetCityNameSelected(string countryNameSelected)
		{
			var cityNameSelected = DataManager.CollectionOfCitiesAfterParamsInterface.GetQueryResultItemsAfterCountryName(countryNameSelected).First().Name;

			return cityNameSelected;
		}

		/// <summary>
		/// Метод GetAllCitiesSelectList(string defoultCountryNameSelected) для создания списка городов выбранной страны в формате SelectList
		/// </summary>
		/// <param name="countryNameSelected">Выбранная страна</param>
		/// <returns></returns>
		private SelectList? GetAllCitiesSelectList(string countryNameSelected)
		{
			return new SelectList(GetAllCities(countryNameSelected), countryNameSelected);
		}

		/// <summary>
		/// Метод GetAllCities(string defoultCountryNameSelected) для создания списка городов выбранной страны в формате List<string>
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
	}
}
