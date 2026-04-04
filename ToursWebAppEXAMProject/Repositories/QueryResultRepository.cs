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

        /// <summary>
        /// Возвращает турпродукты по Id страны и Id города.
        /// </summary>
        /// <param name="countryId">Идентификатор страны.</param>
        /// <param name="cityId">Идентификатор города.</param>
        /// <returns>Коллекция турпродуктов, отобранная по Id страны и Id города. Возвращается пустая коллекция, если
        /// нет турпродуктов или возникает ошибка.</returns>
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

        /// <summary>
        /// Возвращает список городов, отобранных по Id страны, упорядоченных по имени города.
        /// </summary>
        /// <param name="countryId">Уникальный Id страны.</param>
        /// <returns>Список городов страны.</returns>
        public List<City> GetCitiesByCountryId(int countryId)
        {
           return _context.Cities
                .Where(c => c.CountryId == countryId)
                .OrderBy(c => c.Name)
                .ToList();
        }

        /// <summary>
        /// Возвращает карту по Id .
        /// </summary>
        /// <param name="countryId">Уникальный Id страны.</param>
        /// <returns>Карта страны.</returns>
        public string GetMapByCountryId(int countryId)
        {
            return _context.Countries
                .Where(c => c.Id == countryId)
                .Select(c => c.CountryMapPath)
                .FirstOrDefault() ?? "";
        }
    }
}
