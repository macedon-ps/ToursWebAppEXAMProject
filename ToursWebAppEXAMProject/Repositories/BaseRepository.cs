using Microsoft.EntityFrameworkCore;
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
        /// Свойство для логирования событий
        /// </summary>
        private readonly ILogger<BaseRepository<T>> _logger;

        private readonly string itemTypeName;
        
        /// <summary>
        /// DI. Подключение зависимости. Связывание с комнтекстом
        /// </summary>
        /// <param name="_context">контекст подключения к БД</param>
        public BaseRepository(TourFirmaDBContext _context, ILogger<BaseRepository<T>> logger)
		{
			context = _context;
			dbSetEntityItems = _context.Set<T>();		
			itemTypeName = typeof(T).Name;
			_logger = logger;
        }
		
		/// <summary>
		/// Метод GetAllItems(), кот. используется для возврата коллекции сущностей из БД
		/// </summary>
		/// <returns></returns>
		public IEnumerable<T> GetAllItems()
		{
			_logger.LogDebug($"Произведено подключение к БД. Запрашиваются все модели {itemTypeName}. ");
			
			try
			{
				var items = dbSetEntityItems.ToList();

				if (items == null)
				{
					_logger.LogWarning($"В БД нет моделей {itemTypeName}\n");
					
					return items;
				}
				else
				{
                    _logger.LogDebug("Выборка осуществлена успешно\n");
                    
					return items ;
				}
            }
			catch (Exception ex)
			{
                _logger.LogError($"Выборка моделей {itemTypeName} не осуществлена. \nКод ошибки: {ex.Message}\n");
                
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
            _logger.LogDebug($"Произведено подключение к БД. Запрашивается модель {itemTypeName} с id = {id}. ");
            
			try
			{
				var item = dbSetEntityItems.Find(id);

				if (item == null)
				{
                    _logger.LogWarning($"В БД отсутствует модель {itemTypeName} с Id = {id}.\n");
                    
					return new T();
				}
				else
				{
                    _logger.LogDebug("Выборка осуществлена успешно\n");
                    
					return item;
				}
			}
			catch (Exception ex)
			{
                _logger.LogError($"Выборка модели {itemTypeName} с Id = {id} не осуществлена. \nКод ошибки: {ex.Message}\n");

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
            _logger.LogDebug($"Произведено подключение к БД. Запрашиваются модели {itemTypeName} по ключевому слову \"{keyword}\". ");
            
			try
			{
                // предполагается возможность поиска коллекции сущностей:
                // по полному названию						keyword - полное название, IsFullName = true
                // по ключевому слову в названии			keyword - ключевое слово,  IsFullName = false
                // p.s. предполагается, что одинаковых названий м.б. несколько, т.к. нет ограничения уникальности для названия объекта сущности

                IQueryable<T> query = dbSetEntityItems;

                if (typeof(T) == typeof(TechTaskItem))
                {
                    if (isFullName)
                    {
                        query = query.Where(x =>
                            EF.Property<string>(x, "Description") == keyword);
                    }
                    else
                    {
                        query = query.Where(x =>
                            EF.Property<string>(x, "Description").Contains(keyword));
                    }
                }
                else
                {
                    if (isFullName)
                    {
                        query = query.Where(x =>
                            EF.Property<string>(x, "Name") == keyword);
                    }
                    else
                    {
                        query = query.Where(x =>
                            EF.Property<string>(x, "Name").Contains(keyword));
                    }
                }

                var items = query.ToList();

                if (items == null)
				{
                    _logger.LogWarning($"Выборка моделей {itemTypeName} по названию / ключевому слову \"{keyword}\" не осуществлена.\n");
                   
					return new List<T>();
				}
				else
				{
                    _logger.LogDebug("Выборка осуществлена успешно.\n");
                    
					return items;
				}
			}
			catch (Exception ex)
			{
                _logger.LogError($"Выборка моделей {itemTypeName} по названию / ключевому слову \"{keyword}\" не осуществлена. \nКод ошибки: {ex.Message}\n");

                return new List<T>();
			}
		}


		/// <summary>
		/// Метод SaveItem(T item), кот. используется для создания новой/изменения существующей сущности по ее объекту
		/// </summary>
		/// <param name="item">объект сущности</param>
		public void SaveItem(T item, int id)
		{
            _logger.LogDebug("Произведено подключение к БД. ");
            
			try
			{
				if (item == null)
				{
                    _logger.LogWarning("Модель не существует.\n");
                    
					return;
				}
				if(item != null && id==0)
				{
                    _logger.LogDebug($"Создание новой модели {itemTypeName}.\n");
                    
					dbSetEntityItems.Add(item);
					context.SaveChanges();
					return;
				}
				if(item != null && id != 0)
				{
                    _logger.LogDebug($"Обновление существующеей модели {itemTypeName}.\n");
                    
					context.Entry(item).State = EntityState.Modified;
					context.SaveChanges();
					return;
				}
			}
			catch (Exception ex)
			{
                _logger.LogError($"Создание/обновление модели {itemTypeName} не осуществлено.\nКод ошибки: {ex.Message}\n");
            }
		}


		/// <summary>
		/// Метод DeleteItem(int id), кот. используется для удаления сущности по ее id
		/// </summary>
		/// <param name="id">идентификатор сущности</param>
		public void DeleteItem(T tItem, int id)
		{
            _logger.LogDebug("Произведено подключение к БД. ");
            
			try
			{
				if (dbSetEntityItems.Find(id)!=null)
				{
                    _logger.LogDebug($"Удаление модели {itemTypeName} с Id = {id}.\n");
                    
					dbSetEntityItems.Remove(tItem);
					context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
                _logger.LogError($"Удаление модели {itemTypeName} с Id = {id} не осуществлено.\nКод ошибки: {ex.Message}\n");
            }
		}
	}
}
