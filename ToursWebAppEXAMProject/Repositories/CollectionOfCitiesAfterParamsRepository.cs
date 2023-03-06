using Microsoft.EntityFrameworkCore;
using NLog;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Repositories
{
	public class CollectionOfCitiesAfterParamsRepository : ICollectionOfCitiesAfterParamsInterface
	{
		private readonly TourFirmaDBContext context;

		/// <summary>
		/// Статическое сойство для логирования событий
		/// </summary>
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		public string[] itemKeyword = new string[4];
		
		/// <summary>
		/// DI. Подключение зависимости. Связывание с комнтекстом
		/// </summary>
		/// <param name="_context">контекст подключения к БД</param>
		public CollectionOfCitiesAfterParamsRepository(TourFirmaDBContext _context)
		{
			context = _context;
			
			itemKeyword[0] = "город"; 
			itemKeyword[1] = "города"; 
			itemKeyword[2] = "города"; 
			itemKeyword[3] = "городов";
		}


		/// <summary>
		/// Метод GetQueryResultItemsAfterCountryForeignKeyId(int foreignKeyId), кот. используется для возврата результатов выборки сущностей из БД по внешнему ключу Id
		/// </summary>
		/// <param name="foreignKeyId">внешний ключ - Id другой сущности </param>
		/// <returns></returns>

		public IEnumerable<City> GetQueryResultItemsAfterCountryForeignKeyId(int foreignKeyId)
		{
			logger.Debug("Произведено подключение к базе данных");
			Console.WriteLine("Произведено подключение к базе данных");
			logger.Trace($"Запрашиваются {itemKeyword[2]} по внешнему ключу Id = {foreignKeyId}");
			Console.WriteLine($"Запрашиваются {itemKeyword[2]} по внешнему ключу Id = {foreignKeyId}");

			try
			{
				var items = new List<City>();
				
				// для городов
				items = context.Cities
				.FromSqlRaw($"Select * from City where CountryId = '{foreignKeyId}'")
				.ToList();

				if (items == null)
				{
					logger.Warn($"Выборка {itemKeyword[3]} по внешнему ключу Id = {foreignKeyId} не осуществлена.");
					Console.WriteLine($"Выборка {itemKeyword[3]} по внешнему ключу Id = {foreignKeyId} не осуществлена.");

					return new List<City>();
				}
				else
				{
					logger.Debug("Выборка осуществлена успешно");
					Console.WriteLine("Выборка осуществлена успешно");

					return items;
				}
			}
			catch (Exception ex)
			{
				logger.Error("Выборка не осуществлена");
				logger.Error($"Код ошибки: {ex.Message}");
				Console.WriteLine("Выборка не осуществлена");
				Console.WriteLine($"Код ошибки: {ex.Message}");

				return new List<City>();
			}
		}

		public IEnumerable<City> GetQueryResultItemsAfterCountryName(string countryName)
		{
			logger.Debug("Произведено подключение к базе данных");
			Console.WriteLine("Произведено подключение к базе данных");
			logger.Trace($"Запрашиваются {itemKeyword[2]} по названию Country.Name = {countryName}");
			Console.WriteLine($"Запрашиваются {itemKeyword[2]} по названию Country.Name = {countryName}");

			try
			{
				var items = new List<City>();

				// для городов
				items = context.Cities
				.FromSqlRaw($"select City.Id, City.Name, City.CountryId from City, Country where City.CountryId = Country.Id and Country.Name = '{countryName}'")
				.ToList();

				if (items == null)
				{
					logger.Warn($"Выборка {itemKeyword[3]} по названию Country.Name = {countryName} не осуществлена.");
					Console.WriteLine($"Выборка {itemKeyword[3]} по названию Country.Name = {countryName} не осуществлена.");

					return new List<City>();
				}
				else
				{
					logger.Debug("Выборка осуществлена успешно");
					Console.WriteLine("Выборка осуществлена успешно");

					return items;
				}
			}
			catch (Exception ex)
			{
				logger.Error("Выборка не осуществлена");
				logger.Error($"Код ошибки: {ex.Message}");
				Console.WriteLine("Выборка не осуществлена");
				Console.WriteLine($"Код ошибки: {ex.Message}");

				return new List<City>();
			}
		}

		public string GetAllCountriesWithCitiesListByOneString()
		{
			logger.Debug("Произведено подключение к базе данных");
			Console.WriteLine("Произведено подключение к базе данных");
			logger.Trace("Запрашиваются список всех сторан и городов одной строкой");
			Console.WriteLine("Запрашиваются список всех сторан и городов одной строкой");

			try
			{
				// объявляем и инициализируем переменные
				var cities = new List<City>();
				string countriesWithSitiesOneString = "";
				string citiesOfOneCountryListOneString = "";
				string allInfo = "";

				// запрос к БД дать список стран в формате List<Country>, страны не повторяются
				var countries = context.Countries
				.FromSqlRaw($"select distinct Country.Id, Country.Name from Country, City where Country.Id = City.CountryId")
				.ToList();

				// для каждой страны - в цикле - новый запрос к БД - дать список городов для каждой страныб города не повторяются
				foreach (Country country in countries)
				{
					cities = context.Cities
					.FromSqlRaw($"select distinct City.Id, City.Name, City.CountryId from City where City.CountryId = {country.Id}")
					.ToList();

					// заполняем countriesWithSitiesOneString, citiesOfOneCountryListOneString и allInfo
					countriesWithSitiesOneString += $"{country.Name}:";

					foreach (City city in cities)
					{
						citiesOfOneCountryListOneString += city.Name + ",";
					}

					countriesWithSitiesOneString += citiesOfOneCountryListOneString + "\n";
					allInfo += countriesWithSitiesOneString;

					// очищаем countriesWithSitiesOneString и citiesOfOneCountryListOneString для нового элемента коллекции
					citiesOfOneCountryListOneString = "";
					countriesWithSitiesOneString = "";
				}

				// итоговый вариант // для редактирования
				// Console.WriteLine(allInfo);

				if (allInfo == null)
				{
					logger.Warn("Выборка списка всех сторан и городов одной строкой не осуществлена.");
					Console.WriteLine("Выборка списка всех сторан и городов одной строкой не осуществлена.");

					return "В БД нет записей о странах и городах";
				}
				else
				{
					logger.Debug("Выборка осуществлена успешно");
					Console.WriteLine("Выборка осуществлена успешно");

					return allInfo;
				}
			}
			catch (Exception ex)
			{
				logger.Error("Выборка не осуществлена");
				logger.Error($"Код ошибки: {ex.Message}");
				Console.WriteLine("Выборка не осуществлена");
				Console.WriteLine($"Код ошибки: {ex.Message}");

				return $"Вызвано исключение: {ex.Message}";
			}
		}
	}
}
