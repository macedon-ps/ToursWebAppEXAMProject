using Microsoft.EntityFrameworkCore;
using ToursWebAppEXAMProject.EnumsDictionaries;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Repositories
{
	public class CollectionOfCitiesAfterParamsRepository : ICollectionOfCitiesAfterParamsInterface
	{
		private readonly TourFirmaDBContext context;

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
			WriteLogs($"Произведено подключение к БД. Запрашиваются {itemKeyword[2]} по Id = {foreignKeyId}. ", NLogsModeEnum.Debug);
			
			try
			{
				var items = new List<City>();
				
				// для городов
				items = context.Cities
				.FromSqlRaw($"Select * from City where CountryId = '{foreignKeyId}'")
				.ToList();

				if (items == null)
				{
                    WriteLogs($"Выборка {itemKeyword[3]} по Id = {foreignKeyId} не осуществлена.\n", NLogsModeEnum.Warn);
                    
					return new List<City>();
				}
				else
				{
                    WriteLogs("Выборка осуществлена успешно.\n", NLogsModeEnum.Debug);
                    
					return items;
				}
			}
			catch (Exception ex)
			{
                WriteLogs($"Выборка {itemKeyword[3]} по Id = {foreignKeyId} не осуществлена.\n Код ошибки: {ex.Message}\n");
               
				return new List<City>();
			}
		}

		public IEnumerable<City> GetQueryResultItemsAfterCountryName(string countryName)
		{
            WriteLogs($"Произведено подключение к БД. Запрашиваются {itemKeyword[2]} по названию = {countryName}. ", NLogsModeEnum.Debug);
            
			try
			{
				var items = new List<City>();

				// для городов
				items = context.Cities
				.FromSqlRaw($"select City.Id, City.Name, City.CountryId from City, Country where City.CountryId = Country.Id and Country.Name = '{countryName}'")
				.ToList();

				if (items == null)
				{
                    WriteLogs($"В БД отсутствует {itemKeyword[3]} по названию = {countryName}.\n", NLogsModeEnum.Warn);
                    
					return new List<City>();
				}
				else
				{
                    WriteLogs("Выборка осуществлена успешно", NLogsModeEnum.Debug);
                    
					return items;
				}
			}
			catch (Exception ex)
			{
                WriteLogs($"Выборка {itemKeyword[3]} по названию = {countryName} не осуществлена.\nКод ошибки: {ex.Message}", NLogsModeEnum.Error);
                
				return new List<City>();
			}
		}

		public string GetAllCountriesWithCitiesListByOneString()
		{
            WriteLogs("Произведено подключение к БД. Запрашивается список всех сторан и городов одной строкой. ", NLogsModeEnum.Debug);
            
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
                    WriteLogs("Выборка списка всех сторан и городов одной строкой не осуществлена.\n", NLogsModeEnum.Warn);
                    
					return "В БД нет записей о странах и городах";
				}
				else
				{
                    WriteLogs("Выборка осуществлена успешно\n", NLogsModeEnum.Debug);
                    
					return allInfo;
				}
			}
			catch (Exception ex)
			{
                WriteLogs($"Выборка списка всех сторан и городов одной строкой не осуществлена.\nКод ошибки: {ex.Message}\n", NLogsModeEnum.Error);
                
				return $"Вызвано исключение: {ex.Message}";
			}
		}
	}
}
