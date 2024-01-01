using Microsoft.EntityFrameworkCore;
using ToursWebAppEXAMProject.EnumsDictionaries;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Repositories
{
    public class QueryResultRepository : IQueryResultInterface
	{
		private readonly TourFirmaDBContext context;

		public string[] itemKeyword = new string[4];
		
		/// <summary>
		/// DI. Подключение зависимости. Связывание с комнтекстом
		/// </summary>
		/// <param name="_context">контекст подключения к БД</param>
		public QueryResultRepository(TourFirmaDBContext _context)
		{
			context = _context;
			
			itemKeyword[0] = "город"; 
			itemKeyword[1] = "города"; 
			itemKeyword[2] = "города"; 
			itemKeyword[3] = "городов";
		}


		/// <summary>
		/// Метод вывода списка городов по внешнему ключу Id страны
		/// </summary>
		/// <param name="foreignKeyId">внешний ключ - Id страны</param>
		/// <returns></returns>
		public IEnumerable<City> GetCitiesByCountryForeignKeyId(int foreignKeyId)
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

        /// <summary>
        /// Метод вывода списка городов по названию страны
        /// </summary>
        /// <param name="countryName">название страны</param>
        /// <returns></returns>
        public IEnumerable<City> GetCitiesByCountryName(string countryName)
		{
            WriteLogs($"Произведено подключение к БД. Запрашиваются {itemKeyword[2]} по названию = {countryName}. ", NLogsModeEnum.Debug);
            
			try
			{
				var items = new List<City>();

				// для городов
				items = context.Cities
				.FromSqlRaw($"select City.Id, City.Name, City.ShortDescription, City.FullDescription, City.isCapital, City.TitleImagePath, City.CountryId from City, Country where City.CountryId = Country.Id and Country.Name = '{countryName}'")
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

        /// <summary>
        /// Метод вывода списка турпродуктов по названию страны и города
        /// </summary>
        /// <param name="countryName">название страны</param>
        /// <param name="cityName">название города</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Product> GetProductsByCountryNameAndCityName(string countryName, string cityName)
        {
            WriteLogs($"Произведено подключение к БД. Запрашиваются турпродукты для страны \"{countryName}\" и для города \"{cityName}\". ", NLogsModeEnum.Debug);

            try
            {
                var products = new List<Product>();

                products = context.Products
                .FromSqlRaw($"select distinct Product.Name, Product.ShortDescription, Product.FullDescription, Product.TitleImagePath, Product.DateAdded, Product.Id, Product.CountryId, Product.CityId from Product, Country, City where Product.CountryId = Country.Id AND Country.Name = '{countryName}' AND Product.CityId = City.Id AND City.Name = '{cityName}';")
                .ToList();
               
                if (products == null)
                {
                    WriteLogs($"Выборка турпородуктов по названию страны \"{countryName}\" и названию города \"{cityName}\" не осуществлена.\n", NLogsModeEnum.Warn);

                    return new List<Product>();
                }
                else
                {
                    WriteLogs($"Выборка турпородуктов по названию страны \"{countryName}\" и названию города \"{cityName}\" осуществлена успешно.\n", NLogsModeEnum.Debug);

                    return products;
                }
            }
            catch (Exception ex)
            {
                WriteLogs($"Выборка турпородуктов по названию страны \"{countryName}\" и названию города \"{cityName}\" не осуществлена. \nКод ошибки: {ex.Message}\n", NLogsModeEnum.Error);

                return new List<Product>();
            }
        }

        /// <summary>
        /// Метод вывода списка турпродуктов по названию страны
        /// </summary>
        /// <param name="countryName">название страны</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Product> GetProductsByCountryName(string countryName)
        {
            throw new NotImplementedException();
        }

		/// <summary>
		/// Метод выборки из БД и преобразования в строку всех стран  городов
		/// </summary>
		/// <returns></returns>
        public string GetAllCountriesAndCitiesByString()
		{
            WriteLogs("Произведено подключение к БД. Запрашивается список всех стран и городов одной строкой. ", NLogsModeEnum.Debug);
            
			try
			{
				// объявляем и инициализируем переменные
				var cities = new List<City>();
				string countriesWithSitiesOneString = "";
				string citiesOfOneCountryListOneString = "";
				string allInfo = "";

				// запрос к БД дать список стран в формате List<Country>, страны не повторяются
				var countries = context.Countries
				.FromSqlRaw($"select distinct Country.Id, Country.Name, Country.ShortDescription, Country.FullDescription, Country.Capital, Country.TitleImagePath, Country.CountryMapPath from Country, City where Country.Id = City.CountryId")
				.ToList();

				// для каждой страны - в цикле - новый запрос к БД - дать список городов для каждой страныб города не повторяются
				foreach (Country country in countries)
				{
					cities = context.Cities
					.FromSqlRaw($"select distinct City.Id, City.Name, City.ShortDescription, City.FullDescription, City.isCapital, City.TitleImagePath, City.CountryId from City where City.CountryId = {country.Id}")
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
                WriteLogs($"Выборка списка всех стран и городов одной строкой не осуществлена.\nКод ошибки: {ex.Message}\n", NLogsModeEnum.Error);
                
				return $"Вызвано исключение: {ex.Message}";
			}
		}


        /// <summary>
        /// Метод выборки из БД и преобразования в строку всех стран и их описаний
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetAllCountryShortDescriptionsByString()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод выборки из БД и преобразования в строку всех городов и их описаний
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetAllCityShortDescriptionsByString()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод выборки из БД и преобразования в строку всех стран и их карт
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetAllCountryMapsByString()
        {
            WriteLogs("Произведено подключение к БД. Запрашивается список всех стран и их карт одной строкой. ", NLogsModeEnum.Debug);

            try
            {
                // объявляем и инициализируем переменные
                var countries = new List<Country>();
                string countriesAndMapsString = "";
                
                // запрос к БД дать список стран в формате List<Country>, страны не повторяются
                countries = context.Countries
                .FromSqlRaw($"select distinct Country.Id, Country.Name, Country.ShortDescription, Country.FullDescription, Country.Capital, Country.TitleImagePath, Country.CountryMapPath from Country, City where Country.Id = City.CountryId")
                .ToList();

                // для каждой страны - в цикле - новый запрос к БД - дать список городов для каждой страныб города не повторяются
                foreach (Country country in countries)
                {
                   countriesAndMapsString += $"{country.Name}#{country.CountryMapPath}\n";
				}

                if (countriesAndMapsString == null)
                {
                    WriteLogs("Выборка списка всех сторан и их карт одной строкой не осуществлена.\n", NLogsModeEnum.Warn);

                    return "В БД нет записей о странах и городах";
                }
                else
                {
                    WriteLogs("Выборка осуществлена успешно\n", NLogsModeEnum.Debug);

                    return countriesAndMapsString;
                }
            }
            catch (Exception ex)
            {
                WriteLogs($"Выборка списка всех стран и их карт одной строкой не осуществлена.\nКод ошибки: {ex.Message}\n", NLogsModeEnum.Error);

                return $"Вызвано исключение: {ex.Message}";
            }
        }
	}
}
