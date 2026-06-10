using Microsoft.CodeAnalysis;
using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Services.CloudineryImageStorageService;
using ToursWebAppEXAMProject.Services.ImageStorage;

namespace ToursWebAppEXAMProject.Utils
{
    public class CountryUtils
    {
        private readonly IBaseInterface<Country> _AllCountries;
        private readonly IBaseInterface<City> _AllCities;
        private readonly IQueryResultInterface _QueryResult;
        private readonly CloudinaryImageStorageService _CloudinaryImageStorageService;


        public CountryUtils(IBaseInterface<Country> AllCountries, IBaseInterface<City> AllCities, IQueryResultInterface QueryResult, CloudinaryImageStorageService CloudinaryImageStorageService)
        {
            _AllCountries = AllCountries;
            _AllCities = AllCities;
            _QueryResult = QueryResult;
            _CloudinaryImageStorageService = CloudinaryImageStorageService;
        }


        public IEnumerable<Country> GetCountries()
        {
            return _AllCountries.GetAllItems();
        }


        public Country GetCountryById(int id)
        {
            var country = _AllCountries.GetItemById(id);
            var cities = _AllCities.GetAllItems().Where(c => c.CountryId == id);
            country.Cities = cities;

            return country;
        }


        public Country GetCountryForEdit(int id)
        {
            var country = _AllCountries.GetItemById(id);
            var cities = _AllCities.GetAllItems().Where(c => c.CountryId == id);
            country.Cities = cities;
            country.DateAdded = DateTime.Now;

            return country;
        }


        public IEnumerable<Country> QueryResult(bool isFullName, string insertedText)
        {
            var countries = _AllCountries.GetQueryResultItemsAfterFullName(insertedText, isFullName);
            
            return countries;
        }


        public void DeleteCountryById(Country country)
        {
            _AllCountries.DeleteItem(country, country.Id);
        }


        public async Task<(string Url, string PublicId)> SaveCountryImageByFileNameAsync(IFormFile? imageFileName, int countryId)
        {
            var folder = ImageFolder.Countries;
            var publicId = $"country_{countryId}";
            return await _CloudinaryImageStorageService.UploadAsync(folder, imageFileName, publicId);
        }


        public void SaveCountryModel(Country countryModel)
        {
            if (countryModel != null)
            {
                _AllCountries.SaveItem(countryModel, countryModel.Id);
            }
        }


        public string GetMapByCountryId(int countryId)
        {
            return _QueryResult.GetMapByCountryId(countryId);
        }
    }
}
