
using Microsoft.Extensions.Caching.Memory;
using NLog;
using System.Diagnostics.Metrics;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Utils
{
    public class CityUtils
    {
        private readonly IBaseInterface<City> _AllCities;
        private readonly IBaseInterface<Country> _AllCountries;
        private readonly FileUtils _FileUtils;
        private readonly IQueryResultInterface _QueryResult;
        private readonly IMemoryCache _Cache;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public CityUtils(IBaseInterface<City> AllCities, IBaseInterface<Country> AllCountries, FileUtils FileUtils, IQueryResultInterface QueryResult, IMemoryCache Cache)
        {
            _AllCities = AllCities;
            _AllCountries = AllCountries;
            _FileUtils = FileUtils;
            _QueryResult = QueryResult;
            _Cache = Cache;
        }

        public IEnumerable<City> GetCities()
        {
            return _AllCities.GetAllItems();
        }

        public City GetCityById(int id)
        {
            return _AllCities.GetItemById(id);
        }

        public CreateCityViewModel GetCreateCityViewModel()
        {
            var cityViewModel = new CreateCityViewModel();
            var city = new City();
            var countries = _AllCountries.GetAllItems();
            cityViewModel.City = city;
            cityViewModel.Countries = countries;

            return cityViewModel;
        }

        public City GetCityForEdit(int id)
        {
            var city = _AllCities.GetItemById(id);
            city.DateAdded = DateTime.Now;

            return city;
        }

        public IEnumerable<City> QueryResult(bool isFullName, string insertedText)
        {
            var cities = _AllCities.GetQueryResultItemsAfterFullName(insertedText, isFullName);

            return cities;
        }

        public void DeleteCityById(City city)
        {
            _AllCities.DeleteItem(city, city.Id);
        }

        public async Task SaveImagePathAsync(IFormFile changeTitleImagePath)
        {
            var folder = "/images/CitiesTitleImages/";
            await _FileUtils.SaveImageToFolder(folder, changeTitleImagePath);
        }

        public City SetCityModel(City cityModel, IFormFile? imageFileName)
        {

            // если мы хотим поменять картинку
            cityModel.DateAdded = DateTime.Now;

            if (imageFileName != null)
            {
                var folder = "/images/CitiesTitleImages/";
                cityModel.TitleImagePath = $"{folder}{imageFileName.FileName}";
            }

            return cityModel;
        }

        public void SaveCity(City cityModel)
        {
            if (cityModel != null)
            {
                _AllCities.SaveItem(cityModel, cityModel.Id);
            }
        }

        public List<City> GetCitiesByCountryId(int countryId)
        {
            var cacheKey = $"cities_{countryId}";

            if (!_Cache.TryGetValue(cacheKey, out List<City> cities))
            {
                cities = _QueryResult.GetCitiesByCountryId(countryId);
                    
                _Cache.Set(cacheKey, cities, TimeSpan.FromMinutes(30));
            }

            return cities;
        }
    }
}
