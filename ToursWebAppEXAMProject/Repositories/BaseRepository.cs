using Microsoft.EntityFrameworkCore;
using NLog;
using System.Linq;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Repositories
{
	public class BaseRepository<T> : IBaseInterface<T> where T : class, new()
	{
		/// <summary>
		/// Контекст подключения к MS SQL Server, к БД TourFirmaDB
		/// </summary>
		private readonly TourFirmaDBContext context;
		
		/// <summary>
		/// Универсальный репозиторий даннных БД
		/// </summary>
		private readonly DbSet<T> dbSetEntityItems;

		/// <summary>
		/// Статическое сойство для логирования событий
		/// </summary>
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		public string[] itemKeyword = new string[4];
		private readonly string itemTypeName;
		
		/// <summary>
		/// DI. Подключение зависимости. Связывание с комнтекстом
		/// </summary>
		/// <param name="_context">контекст подключения к БД</param>
		public BaseRepository(TourFirmaDBContext _context)
		{
			context = _context;
			dbSetEntityItems = _context.Set<T>();		
			itemTypeName = typeof(T).Name;
			switch (itemTypeName)
			{
				case "Product":
					itemKeyword[0] = "турпродукт"; itemKeyword[1] = "турпродукта"; itemKeyword[2] = "турпродукты"; itemKeyword[3]= "турпродуктов";
					break;
				case "City":
					itemKeyword[0] = "город"; itemKeyword[1] = "города"; itemKeyword[2] = "города"; itemKeyword[3] = "городов";
					break;
				case "Country":
					itemKeyword[0] = "страна"; itemKeyword[1] = "страны"; itemKeyword[2] = "страны"; itemKeyword[3] = "стран";
					break;
				case "Hotel":
					itemKeyword[0] = "гостинница"; itemKeyword[1] = "гостинницы"; itemKeyword[2] = "гостинницы"; itemKeyword[3] = "гостинниц";
					break;
				case "Location":
					itemKeyword[0] = "локация"; itemKeyword[1] = "локации"; itemKeyword[2] = "локации"; itemKeyword[3] = "локаций";
					break;
				case "DateTour":
					itemKeyword[0] = "дата тура"; itemKeyword[1] = "даты тура"; itemKeyword[2] = "даты туров"; itemKeyword[3] = "дат туров";
					break;
				case "Food":
					itemKeyword[0] = "режим питания"; itemKeyword[1] = "режима питания"; itemKeyword[2] = "режимы питания"; itemKeyword[3] = "режимов питания";
					break;
				case "Tour":
					itemKeyword[0] = "тур"; itemKeyword[1] = "тура"; itemKeyword[2] = "туры"; itemKeyword[3] = "туров";
					break;
				case "Customer":
					itemKeyword[0] = "покупатель"; itemKeyword[1] = "покупателя"; itemKeyword[2] = "покупатели"; itemKeyword[3] = "покупателей";
					break;
				case "Saller":
					itemKeyword[0] = "продавец"; itemKeyword[1] = "продавца"; itemKeyword[2] = "продавцы"; itemKeyword[3] = "продавецов";
					break;
				case "Ofertum":
					itemKeyword[0] = "оферта"; itemKeyword[1] = "оферты"; itemKeyword[2] = "оферты"; itemKeyword[3] = "оферт";
					break;
				case "Blog":
					itemKeyword[0] = "сообщение блога"; itemKeyword[1] = "сообщения блога"; itemKeyword[2] = "сообщения блогов"; itemKeyword[3] = "сообщений блогов";
					break;
				case "New":
					itemKeyword[0] = "новость"; itemKeyword[1] = "новости"; itemKeyword[2] = "новости"; itemKeyword[3] = "новостей";
					break;
			}
		}
		
		/// <summary>
		/// Метод GetAllItems(), кот. используется для возврата коллекции сущностей из БД
		/// </summary>
		/// <returns></returns>
		public IEnumerable<T> GetAllItems()
		{
			logger.Debug("Произведено подключение к базе данных");
			Console.WriteLine("Произведено подключение к базе данных");
			logger.Trace($"Запрашиваются все {itemKeyword[2]}");
			Console.WriteLine($"Запрашиваются все {itemKeyword[2]}");

			try
			{
				var items = dbSetEntityItems.AsNoTracking().ToList();

				if (items == null)
				{
					logger.Warn($"Выборка всех {itemKeyword[3]} не осуществлена. Они не существуют");
					Console.WriteLine($"Выборка всех {itemKeyword[3]} не осуществлена. Они не существуют");

					return new List<T>();
				}
				else
				{
					logger.Debug("Выборка осуществлена успешно");
					Console.WriteLine("Выборка осуществлена успешно");

					return items ;
				}
			}
			catch (Exception ex)
			{
				logger.Error("Выборка не осуществлена");
				logger.Error($"Код ошибки: {ex.Message}");
				Console.WriteLine("Выборка не осуществлена");
				Console.WriteLine($"Код ошибки: {ex.Message}");

				return new List<T>();
			}
		}

		/// <summary>
		/// Метод GetItemById(int id), кот. используется для возврата конкретной сущности по ее Id
		/// </summary>
		/// <param name="id">идентификатор сущности</param>
		/// <returns></returns>
		public T GetItemById(int id)
		{
			logger.Debug("Произведено подключение к базе данных");
			Console.WriteLine("Произведено подключение к базе данных");
			logger.Trace($"Запрашивваемый id {itemKeyword[1]}: {id}");
			Console.WriteLine($"Запрашивваемый id {itemKeyword[1]}: {id}");

			try
			{
				var item = dbSetEntityItems.Find(id);

				if (item == null)
				{
					logger.Warn($"Выборка {itemKeyword[1]} не осуществлена: {itemKeyword[1]} с Id = {id} не существует");
					Console.WriteLine($"Выборка {itemKeyword[1]} не осуществлена: {itemKeyword[1]} с Id = {id} не существует");

					return new T();
				}
				else
				{
					logger.Debug("Выборка осуществлена успешно");
					Console.WriteLine("Выборка осуществлена успешно");

					return item;
				}
			}
			catch (Exception ex)
			{
				logger.Error($"Выборка {itemKeyword[1]} с Id = {id} не осуществлена");
				logger.Error($"Код ошибки: {ex.Message}");
				Console.WriteLine($"Выборка {itemKeyword[1]} с Id = {id} не осуществлена");
				Console.WriteLine($"Код ошибки: {ex.Message}");
				return new T();
			}
		}

		/// <summary>
		/// Метод GetQueryResultItems(string keyword), кот. используется для возврата результатов выборки сущностей из БД по ключевому слову
		/// </summary>
		/// <param name="keyword">ключевое слово для поиска</param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public IEnumerable<T> GetQueryResultItems(string keyword, bool isFullName)
		{
			logger.Debug("Произведено подключение к базе данных");
			Console.WriteLine("Произведено подключение к базе данных");
			logger.Trace($"Запрашиваются {itemKeyword[2]} по ключевому слову \"{keyword}\"");
			Console.WriteLine($"Запрашиваются {itemKeyword[2]} по ключевому слову \"{keyword}\"");

			try
			{
				// предполагается возможность поиска коллекции сущностей:
				// по полному названию						keyword - полное название, isFullName = true
				// по ключевому слову в названии			keyword - ключевое слово,  isFullName = false
				// p.s. предполагается, что одинаковых названий м.б. несколько, т.к. нет ограничения уникальности для названия объекта сущности

				var items = new List<T>();
				
				if (isFullName)
				{
					items = dbSetEntityItems
					.FromSqlRaw($"Select * from {itemTypeName} where Name = '{keyword}'")
					.ToList();
				}
				else
				{
					items = dbSetEntityItems
					.FromSqlRaw($"Select * from {itemTypeName} where Name like '%{keyword}%'")
					.ToList();
				}
												
				if (items == null)
				{
					logger.Warn($"Выборка {itemKeyword[3]} по названию / ключевому слову \"{keyword}\" не осуществлена.");
					Console.WriteLine($"Выборка {itemKeyword[3]} по названию / ключевому слову \"{keyword}\" не осуществлена.");

					return new List<T>();
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

				return new List<T>();
			}
		}

		/// <summary>
		/// Метод SaveItem(T item), кот. используется для создания новой/изменения существующей сущности по ее объекту
		/// </summary>
		/// <param name="item">объект сущности</param>
		public void SaveItem(T item)
		{
			logger.Debug("Произведено подключение к базе данных");
			Console.WriteLine("Произведено подключение к базе данных");
			try
			{
				if (item == null)
				{
					logger.Trace($"Создание нового(ой) {itemKeyword[1]}");
					Console.WriteLine($"Создание нового(ой) {itemKeyword[1]}");
					dbSetEntityItems.Add(item);
					context.SaveChanges();
				}
				else
				{
					logger.Trace($"Обновление существующего(ей) {itemKeyword[1]}");
					Console.WriteLine($"Обновление существующего(ей) {itemKeyword[1]}");
					context.Entry(item).State = EntityState.Modified;
					context.SaveChanges();
					
				}
			}
			catch (Exception ex)
			{
				logger.Error($"Создание/обновление {itemKeyword[1]} не осуществлено");
				logger.Error($"Код ошибки: {ex.Message}");
				Console.WriteLine($"Создание/обновление {itemKeyword[1]} не осуществлено");
				Console.WriteLine($"Код ошибки: {ex.Message}");
			}
		}

		/// <summary>
		/// Метод DeleteItem(int id), кот. используется для удаления сущности по ее id
		/// </summary>
		/// <param name="id">идентификатор сущности</param>
		public void DeleteItem(T tItem)
		{
			logger.Debug("Произведено подключение к базе данных");
			Console.WriteLine("Произведено подключение к базе данных");
			try
			{
				if (dbSetEntityItems.Find(tItem)!=null)
				{
					logger.Trace($"Удаление {itemKeyword[1]}");
					Console.WriteLine($"Удаление {itemKeyword[1]}");

					dbSetEntityItems.Remove(tItem);
					context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				logger.Error($"Удаление {itemKeyword[1]} не осуществлено");
				logger.Error($"Код ошибки: {ex.Message}");
				Console.WriteLine($"Удаление {itemKeyword[1]} не осуществлено");
				Console.WriteLine($"Код ошибки: {ex.Message}");
			}
		}
    }
}
