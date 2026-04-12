using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Services.ImageStorage;

namespace ToursWebAppEXAMProject.Utils
{
    public class CountryUtils
    {
        private readonly IBaseInterface<Country> _AllCountries;
        private readonly IBaseInterface<City> _AllCities;
        private readonly IQueryResultInterface _QueryResult;
        private readonly ImageStorageService _ImageStorageService;


        public CountryUtils(IBaseInterface<Country> AllCountries, IBaseInterface<City> AllCities, IQueryResultInterface QueryResult, ImageStorageService ImageStorageService)
        {
            _AllCountries = AllCountries;
            _AllCities = AllCities;
            _QueryResult = QueryResult;
            _ImageStorageService = ImageStorageService;
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


        public async Task<string?> SaveImagePathAsync(IFormFile? imageFileName)
        {
            var folder = ImageFolder.Countries;
            return await _ImageStorageService.SaveAsync(folder, imageFileName);
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
