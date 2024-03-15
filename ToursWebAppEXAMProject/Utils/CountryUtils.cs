using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Utils
{
    public class CountryUtils
    {
        private readonly IBaseInterface<Country> _AllCountries;
        private readonly IBaseInterface<City> _AllCities;
        private readonly FileUtils _FileUtils;

        public CountryUtils(IBaseInterface<Country> AllCountries, IBaseInterface<City> AllCities, FileUtils FileUtils)
        {
            _AllCountries = AllCountries;
            _AllCities = AllCities;
            _FileUtils = FileUtils;
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

        public async Task SaveImagePathAsync(IFormFile changeTitleImagePath)
        {
            var folder = "/images/CountriesTitleImages/";
            await _FileUtils.SaveImageToFolder(folder, changeTitleImagePath);
        }

        public Country SetCountryModel(Country country, IFormCollection formValues, IFormFile? changeTitleImagePath)
        {
            var fullInfoCountry = formValues["fullInfoAboutCountry"].ToString();

            if (fullInfoCountry != null) country.FullDescription = fullInfoCountry;
            country.DateAdded = DateTime.Now;

            if (changeTitleImagePath != null)
            {
                var folder = "/images/CountriesTitleImages/";
                country.TitleImagePath = $"{folder}{changeTitleImagePath.FileName}";
            }

            return country;
        }

        public void SaveCountry(Country country)
        {
            if (country != null)
            {
                _AllCountries.SaveItem(country, country.Id);
            }
        }

        public Country SetCountryModelByFormValues(Country country, IFormCollection formValues)
        {
            var fullInfoCountry = formValues["fullInfoAboutCountry"].ToString();
            if (fullInfoCountry != null) country.FullDescription = fullInfoCountry;

            return country;
        }
    }
}
