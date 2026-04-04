using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface IQueryResultInterface
	{
		IEnumerable<Product> GetProductsByCountryIdAndCityId(int? countryId, int? cityId);

        List<City> GetCitiesByCountryId(int countryId);

		string GetMapByCountryId(int countryId);

    }
}
