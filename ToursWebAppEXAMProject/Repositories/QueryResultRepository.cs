using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using System.Diagnostics.Metrics;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Repositories
{
    public class QueryResultRepository : IQueryResultInterface
	{
		private readonly TourFirmaDBContext _context;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public string[] itemKeyword = new string[4];
		
		/// <summary>
		/// DI. Подключение зависимости. Связывание с комнтекстом
		/// </summary>
		/// <param name="_context">контекст подключения к БД</param>
		public QueryResultRepository(TourFirmaDBContext context)
		{
			_context = context;
			
			itemKeyword[0] = "город"; 
			itemKeyword[1] = "города"; 
			itemKeyword[2] = "города"; 
			itemKeyword[3] = "городов";
		}

        // старая реализация
        /// <summary>
        /// Метод вывода списка турпродуктов по названию страны и города (для сервера)
        /// </summary>
        /// <param name="countryName">название страны</param>
        /// <param name="cityName">название города</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /*
        public IEnumerable<Product> GetProductsByCountryIdAndCityId(string countryName, string cityName)
        {
            _logger.Debug($"Произведено подключение к БД. Запрашиваются турпродукты для страны \"{countryName}\" и для города \"{cityName}\". ");

            try
            {
                var products = _context.Products
                    .Where(p => p.Country.Name == countryName &&
                                p.City.Name == cityName)
                    .ToList();

                if (products == null)
                {
                    _logger.Warn($"Выборка турпородуктов по названию страны \"{countryName}\" и названию города \"{cityName}\" не осуществлена.\n");

                    return new List<Product>();
                }
                else
                {
                    _logger.Debug($"Выборка турпородуктов по названию страны \"{countryName}\" и названию города \"{cityName}\" осуществлена успешно.\n");

                    return products;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Выборка турпородуктов по названию страны \"{countryName}\" и названию города \"{cityName}\" не осуществлена. \nКод ошибки: {ex.Message}\n");

                return new List<Product>();
            }
        }*/

        /// <summary>
        /// Метод создания словаря (страна, список городов) в js (для фронта)
        /// </summary>
        /// <returns>Словарь (страна, список городов)</returns>
        public Dictionary<string, List<string>> GetAllCountriesAndCities()
        {
            var countriesCities = new Dictionary<string, List<string>>();

            // легкий запрос, но страны без городов не выводятся
            countriesCities = _context.Cities
                                .AsNoTracking()
                                .OrderBy(c => c.Country.Name)
                                .ThenBy(c => c.Name)
                                .GroupBy(c => c.Country.Name)
                                .ToDictionary(
                                    g => g.Key,
                                    g => g.Select(c => c.Name).ToList()
                                );

            // дорогой запрос, но все страны, даже без городов, выводятся
            /*countriesCities = _context.Countries
                                .Include(c => c.Cities)
                                .ToDictionary(
                                    c => c.Name,
                                    c => c.Cities.Select(city => city.Name).ToList()
                                );*/
            
            return countriesCities;
        }

        /// <summary>
        /// Метод создания словаря (страна, карта) в js (для фронта)
        /// </summary>
        /// <returns>Словарь (страна, карта)</returns>
        public Dictionary<string, string> GetAllCountriesAndMaps()
        {
            var countriesMaps = new Dictionary<string, string>();

            countriesMaps = _context.Countries
                                        .ToDictionary(
                                            c => c.Name,
                                            c => c.TitleImagePath
                                        );
            return countriesMaps;
        }

        public IEnumerable<Product> GetProductsByCountryIdAndCityId(int? countryId, int? cityId)
        {
            _logger.Debug($"Произведено подключение к БД. Запрашиваются турпродукты для Id страны \"{countryId}\" и для Id города \"{cityId}\". ");

            try
            {
                if(countryId == 0 || cityId == 0)
                {
                    return new List<Product>();
                }
                
                var products = _context.Products
                    .Where(p => p.CountryId == countryId &&
                                p.CityId == cityId)
                    .ToList();

                if (products == null)
                {
                    _logger.Warn($"Выборка турпородуктов по Id страны \"{countryId}\" и Id города \"{cityId}\" не осуществлена.\n");

                    return new List<Product>();
                }
                else
                {
                    _logger.Debug($"Выборка турпородуктов по Id страны \"{countryId}\" и Id города \"{cityId}\" осуществлена успешно.\n");

                    return products;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Выборка турпородуктов по Id страны \"{countryId}\" и Id города \"{cityId}\" не осуществлена. \nКод ошибки: {ex.Message}\n");

                return new List<Product>();
            }
        }

        public int? GetIdByCountryName(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                return null;

            countryName = countryName.Trim();

            return _context.Countries
                .Where(x => x.Name.ToLower() == countryName.ToLower())
                .Select(x => (int?)x.Id)
                .FirstOrDefault();
        }

        public int? GetIdByCityName(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
                return null;

            cityName = cityName.Trim();

            return _context.Cities
                .Where(x => x.Name.ToLower() == cityName.ToLower())
                .Select(x => (int?)x.Id)
                .FirstOrDefault();
        }
    }
}
