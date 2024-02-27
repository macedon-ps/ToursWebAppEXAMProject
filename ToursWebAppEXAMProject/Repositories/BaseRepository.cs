using Microsoft.EntityFrameworkCore;
using ToursWebAppEXAMProject.EnumsDictionaries;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Interfaces;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

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
                case "Asker":
                    itemKeyword[0] = "спрашивающий"; itemKeyword[1] = "спрашивающего"; itemKeyword[2] = "спрашивающие"; itemKeyword[3] = "спрашивающих";
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
				case "EditAboutPage":
					itemKeyword[0] = "страница About"; itemKeyword[1] = "страницы About"; itemKeyword[2] = "страницы About"; itemKeyword[3] = "страниц About";
					break;
			}
		}
		
		/// <summary>
		/// Метод GetAllItems(), кот. используется для возврата коллекции сущностей из БД
		/// </summary>
		/// <returns></returns>
		public IEnumerable<T> GetAllItems()
		{
			WriteLogs($"Произведено подключение к БД. Запрашиваются все {itemKeyword[2]}. ", NLogsModeEnum.Debug);
			
			try
			{
				var items = dbSetEntityItems.AsNoTracking().ToList();

				if (items == null)
				{
					WriteLogs($"В БД нет {itemKeyword[3]}\n", NLogsModeEnum.Warn);
					
					return new List<T>();
				}
				else
				{
                    WriteLogs("Выборка осуществлена успешно\n", NLogsModeEnum.Debug);
                    
					return items ;
				}
			}
			catch (Exception ex)
			{
                WriteLogs($"Выборка {itemKeyword[3]} не осуществлена. \nКод ошибки: {ex.Message}\n", NLogsModeEnum.Error);
                
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
            WriteLogs($"Произведено подключение к БД. Запрашивается {itemKeyword[1]} с id = {id}. ", NLogsModeEnum.Debug);
            
			try
			{
				var item = dbSetEntityItems.Find(id);

				if (item == null)
				{
                    WriteLogs($"В БД отсутствует {itemKeyword[1]} с Id = {id}.\n", NLogsModeEnum.Warn);
                    
					return new T();
				}
				else
				{
                    WriteLogs("Выборка осуществлена успешно\n", NLogsModeEnum.Debug);
                    
					return item;
				}
			}
			catch (Exception ex)
			{
                WriteLogs($"Выборка {itemKeyword[1]} с Id = {id} не осуществлена. \nКод ошибки: {ex.Message}\n", NLogsModeEnum.Error);

                return new T();
			}
		}

		/// <summary>
		/// Метод GetQueryResultItemsAfterFullName(string keyword), кот. используется для возврата результатов выборки сущностей из БД по ключевому слову
		/// </summary>
		/// <param name="keyword">ключевое слово для поиска</param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public IEnumerable<T> GetQueryResultItemsAfterFullName(string keyword, bool isFullName)
		{
            WriteLogs($"Произведено подключение к БД. Запрашиваются {itemKeyword[2]} по ключевому слову \"{keyword}\". ", NLogsModeEnum.Debug);
            
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
                    WriteLogs($"Выборка {itemKeyword[3]} по названию / ключевому слову \"{keyword}\" не осуществлена.\n", NLogsModeEnum.Warn);
                   
					return new List<T>();
				}
				else
				{
                    WriteLogs("Выборка осуществлена успешно.\n", NLogsModeEnum.Debug);
                    
					return items;
				}
			}
			catch (Exception ex)
			{
                WriteLogs($"Выборка {itemKeyword[3]} по названию / ключевому слову \"{keyword}\" не осуществлена. \nКод ошибки: {ex.Message}\n", NLogsModeEnum.Error);

                return new List<T>();
			}
		}

		/// <summary>
		/// Метод SaveItem(T item), кот. используется для создания новой/изменения существующей сущности по ее объекту
		/// </summary>
		/// <param name="item">объект сущности</param>
		public void SaveItem(T item, int id)
		{
            WriteLogs("Произведено подключение к БД. ", NLogsModeEnum.Debug);
            
			try
			{
				if (item == null)
				{
                    WriteLogs("Модель не существует.\n", NLogsModeEnum.Warn);
                    
					return;
				}
				if(item != null && id==0)
				{
                    WriteLogs($"Создание нового(ой) {itemKeyword[1]}.\n", NLogsModeEnum.Debug);
                    
					dbSetEntityItems.Add(item);
					context.SaveChanges();
					return;
				}
				if(item != null && id != 0)
				{
                    WriteLogs($"Обновление существующего(ей) {itemKeyword[1]}.\n", NLogsModeEnum.Debug);
                    
					context.Entry(item).State = EntityState.Modified;
					context.SaveChanges();
					return;
				}
			}
			catch (Exception ex)
			{
                WriteLogs($"Создание/обновление {itemKeyword[1]} не осуществлено.\nКод ошибки: {ex.Message}\n", NLogsModeEnum.Error);
            }
		}

		/// <summary>
		/// Метод DeleteItem(int id), кот. используется для удаления сущности по ее id
		/// </summary>
		/// <param name="id">идентификатор сущности</param>
		public void DeleteItem(T tItem, int id)
		{
            WriteLogs("Произведено подключение к БД. ", NLogsModeEnum.Debug);
            
			try
			{
				if (dbSetEntityItems.Find(id)!=null)
				{
                    WriteLogs($"Удаление {itemKeyword[1]}", NLogsModeEnum.Debug);
                    
					dbSetEntityItems.Remove(tItem);
					context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
                WriteLogs($"Удаление {itemKeyword[1]} не осуществлено.\nКод ошибки: {ex.Message}\n", NLogsModeEnum.Error);
            }
		}
	}
}
