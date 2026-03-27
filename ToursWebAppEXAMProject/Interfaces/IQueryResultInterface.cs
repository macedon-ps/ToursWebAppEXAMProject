using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface IQueryResultInterface
	{
		IEnumerable<Product> GetProductsByCountryIdAndCityId(int? countryId, int? cityId);

        /// <summary>
        /// Получение словаря, где ключ - название страны, а значение - список городов в этой стране
        /// </summary>
        /// <returns></returns>
        Dictionary<string, List<string>> GetAllCountriesAndCities();

        /// <summary>
        /// Получение словаря, где ключ - название страны, а значение - ссылка на карту этой страны
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetAllCountriesAndMaps();

        int? GetIdByCountryName(string countryName);

        int? GetIdByCityName(string cityName);

    }
}
