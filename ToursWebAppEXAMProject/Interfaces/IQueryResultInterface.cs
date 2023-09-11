using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface IQueryResultInterface
	{
		IEnumerable<City> GetCitiesByCountryForeignKeyId(int countryForeignKeyId);

		IEnumerable<City> GetCitiesByCountryName(string countryName);

        IEnumerable<Product> GetProductsByCountryName(string countryName);

        string GetAllCountriesAndCitiesByString();

		string GetAllCountryShortDescriptionsByString();

        string GetAllCityShortDescriptionsByString();

		string GetAllCountryMapsByString();

    }
}
