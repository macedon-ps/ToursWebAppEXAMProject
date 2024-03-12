using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface IQueryResultInterface
	{
		IEnumerable<City> GetCitiesByCountryName(string countryName);

        IEnumerable<Product> GetProductsByCountryNameAndCityName(string countryName, string cityName);

        string GetAllCountriesAndCitiesByString();

		string GetAllCountryShortDescriptionsByString();

        string GetAllCityShortDescriptionsByString();

		string GetAllCountryMapsByString();

    }
}
