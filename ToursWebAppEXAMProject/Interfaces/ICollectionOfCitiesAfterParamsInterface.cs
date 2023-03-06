using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface ICollectionOfCitiesAfterParamsInterface
	{
		IEnumerable<City> GetQueryResultItemsAfterCountryForeignKeyId(int countryForeignKeyId);

		IEnumerable<City> GetQueryResultItemsAfterCountryName(string countryName);

		string GetAllCountriesWithCitiesListByOneString();
	}
}
