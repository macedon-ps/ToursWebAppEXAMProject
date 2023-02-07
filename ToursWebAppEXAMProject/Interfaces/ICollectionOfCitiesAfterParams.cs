using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface ICollectionOfCitiesAfterParams
	{
		IEnumerable<City> GetQueryResultItemsAfterCountryForeignKeyId(int countryForeignKeyId);

		IEnumerable<City> GetQueryResultItemsAfterCountryName(string countryName);

		string GetAllCountriesWithCitiesListByOneString();
	}
}
