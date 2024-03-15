
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
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public CityUtils(IBaseInterface<City> AllCities, IBaseInterface<Country> AllCountries, FileUtils FileUtils)
        {
            _AllCities = AllCities;
            _AllCountries = AllCountries;
            _FileUtils = FileUtils;
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

        public City SetCityModel(City city, IFormCollection formValues, IFormFile? changeTitleImagePath)
        {
            var fullInfoCity = formValues["fullInfoAboutCity"].ToString();
            var isCapitalInfo = formValues["checkIsCapital"].ToString();
            var countryIdInfo = formValues["CountryId"].ToString();

            if (fullInfoCity != null) city.FullDescription = fullInfoCity;
            if (isCapitalInfo == "on") city.isCapital = true;
            if(countryIdInfo != "") city.CountryId = Int32.Parse(countryIdInfo);
            
            if (changeTitleImagePath != null)
            {
                var folder = "/images/CitiesTitleImages/";
                city.TitleImagePath = $"{folder}{changeTitleImagePath.FileName}";
            }

            city.DateAdded = DateTime.Now;

            return city;
        }

        public void SaveCity(City city)
        {
            if (city != null)
            {
                _AllCities.SaveItem(city, city.Id);
            }
        }

        public City SetCityModelByFormValues(City city, IFormCollection formValues)
        {
            var fullInfoCity = formValues["fullInfoAboutCity"].ToString();
            if (fullInfoCity != null) city.FullDescription = fullInfoCity;

            return city;
        }
    }
}
