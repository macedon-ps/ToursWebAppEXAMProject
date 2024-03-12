using Microsoft.EntityFrameworkCore;
using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Interfaces;
using NLog;

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
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

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
                case "Correspondence":
                    itemKeyword[0] = "сообщение"; itemKeyword[1] = "сообщения"; itemKeyword[2] = "сообщения"; itemKeyword[3] = "сообщений";
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
			_logger.Debug($"Произведено подключение к БД. Запрашиваются все {itemKeyword[2]}. ");
			
			try
			{
				var items = dbSetEntityItems.ToList();

				if (items == null)
				{
					_logger.Warn($"В БД нет {itemKeyword[3]}\n");
					
					return new List<T>();
				}
				else
				{
                    _logger.Debug("Выборка осуществлена успешно\n");
                    
					return items ;
				}
			}
			catch (Exception ex)
			{
                _logger.Error($"Выборка {itemKeyword[3]} не осуществлена. \nКод ошибки: {ex.Message}\n");
                
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
            _logger.Debug($"Произведено подключение к БД. Запрашивается {itemKeyword[1]} с id = {id}. ");
            
			try
			{
				var item = dbSetEntityItems.Find(id);

				if (item == null)
				{
                    _logger.Warn($"В БД отсутствует {itemKeyword[1]} с Id = {id}.\n");
                    
					return new T();
				}
				else
				{
                    _logger.Debug("Выборка осуществлена успешно\n");
                    
					return item;
				}
			}
			catch (Exception ex)
			{
                _logger.Error($"Выборка {itemKeyword[1]} с Id = {id} не осуществлена. \nКод ошибки: {ex.Message}\n");

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
            _logger.Debug($"Произведено подключение к БД. Запрашиваются {itemKeyword[2]} по ключевому слову \"{keyword}\". ");
            
			try
			{
				// предполагается возможность поиска коллекции сущностей:
				// по полному названию						keyword - полное название, IsFullName = true
				// по ключевому слову в названии			keyword - ключевое слово,  IsFullName = false
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
                    _logger.Warn($"Выборка {itemKeyword[3]} по названию / ключевому слову \"{keyword}\" не осуществлена.\n");
                   
					return new List<T>();
				}
				else
				{
                    _logger.Debug("Выборка осуществлена успешно.\n");
                    
					return items;
				}
			}
			catch (Exception ex)
			{
                _logger.Error($"Выборка {itemKeyword[3]} по названию / ключевому слову \"{keyword}\" не осуществлена. \nКод ошибки: {ex.Message}\n");

                return new List<T>();
			}
		}

		/// <summary>
		/// Метод SaveItem(T item), кот. используется для создания новой/изменения существующей сущности по ее объекту
		/// </summary>
		/// <param name="item">объект сущности</param>
		public void SaveItem(T item, int id)
		{
            _logger.Debug("Произведено подключение к БД. ");
            
			try
			{
				if (item == null)
				{
                    _logger.Warn("Модель не существует.\n");
                    
					return;
				}
				if(item != null && id==0)
				{
                    _logger.Debug($"Создание нового(ой) {itemKeyword[1]}.\n");
                    
					dbSetEntityItems.Add(item);
					context.SaveChanges();
					return;
				}
				if(item != null && id != 0)
				{
                    _logger.Debug($"Обновление существующего(ей) {itemKeyword[1]}.\n");
                    
					context.Entry(item).State = EntityState.Modified;
					context.SaveChanges();
					return;
				}
			}
			catch (Exception ex)
			{
                _logger.Error($"Создание/обновление {itemKeyword[1]} не осуществлено.\nКод ошибки: {ex.Message}\n");
            }
		}

		/// <summary>
		/// Метод DeleteItem(int id), кот. используется для удаления сущности по ее id
		/// </summary>
		/// <param name="id">идентификатор сущности</param>
		public void DeleteItem(T tItem, int id)
		{
            _logger.Debug("Произведено подключение к БД. ");
            
			try
			{
				if (dbSetEntityItems.Find(id)!=null)
				{
                    _logger.Debug($"Удаление {itemKeyword[1]}");
                    
					dbSetEntityItems.Remove(tItem);
					context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
                _logger.Error($"Удаление {itemKeyword[1]} не осуществлено.\nКод ошибки: {ex.Message}\n");
            }
		}
	}
}
