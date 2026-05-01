using Microsoft.EntityFrameworkCore;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Interfaces;
using NLog;
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
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

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
		}
		
		/// <summary>
		/// Метод GetAllItems(), кот. используется для возврата коллекции сущностей из БД
		/// </summary>
		/// <returns></returns>
		public IEnumerable<T> GetAllItems()
		{
			_logger.Debug($"Произведено подключение к БД. Запрашиваются все модели {itemTypeName}. ");
			
			try
			{
				var items = dbSetEntityItems.ToList();

				if (items == null)
				{
					_logger.Warn($"В БД нет моделей {itemTypeName}\n");
					
					return items;
				}
				else
				{
                    _logger.Debug("Выборка осуществлена успешно\n");
                    
					return items ;
				}
            }
			catch (Exception ex)
			{
                _logger.Error($"Выборка моделей {itemTypeName} не осуществлена. \nКод ошибки: {ex.Message}\n");
                
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
            _logger.Debug($"Произведено подключение к БД. Запрашивается модель {itemTypeName} с id = {id}. ");
            
			try
			{
				var item = dbSetEntityItems.Find(id);

				if (item == null)
				{
                    _logger.Warn($"В БД отсутствует модель {itemTypeName} с Id = {id}.\n");
                    
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
                _logger.Error($"Выборка модели {itemTypeName} с Id = {id} не осуществлена. \nКод ошибки: {ex.Message}\n");

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
            _logger.Debug($"Произведено подключение к БД. Запрашиваются модели {itemTypeName} по ключевому слову \"{keyword}\". ");
            
			try
			{
				// предполагается возможность поиска коллекции сущностей:
				// по полному названию						keyword - полное название, IsFullName = true
				// по ключевому слову в названии			keyword - ключевое слово,  IsFullName = false
				// p.s. предполагается, что одинаковых названий м.б. несколько, т.к. нет ограничения уникальности для названия объекта сущности

				var items = new List<T>();
				
				if (isFullName)
				{
					if (typeof(T) == typeof(TechTaskItem))
					{
						items = dbSetEntityItems
						.FromSqlRaw($"Select * from {itemTypeName} where Description = '{keyword}'")
						.ToList();
					}
					else 
					{
                        items = dbSetEntityItems
                        .FromSqlRaw($"Select * from {itemTypeName} where Name = '{keyword}'")
                        .ToList();
                    }
				}
				else
				{
					if(typeof(T) == typeof(TechTaskItem))
					{
                        items = dbSetEntityItems
						.FromSqlRaw($"Select * from {itemTypeName} where Description like '%{keyword}%'")
						.ToList();
					}
					else
					{
                        items = dbSetEntityItems
                        .FromSqlRaw($"Select * from {itemTypeName} where Name like '%{keyword}%'")
                        .ToList();
                    }
				}
												
				if (items == null)
				{
                    _logger.Warn($"Выборка моделей {itemTypeName} по названию / ключевому слову \"{keyword}\" не осуществлена.\n");
                   
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
                _logger.Error($"Выборка моделей {itemTypeName} по названию / ключевому слову \"{keyword}\" не осуществлена. \nКод ошибки: {ex.Message}\n");

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
                    _logger.Debug($"Создание новой модели {itemTypeName}.\n");
                    
					dbSetEntityItems.Add(item);
					context.SaveChanges();
					return;
				}
				if(item != null && id != 0)
				{
                    _logger.Debug($"Обновление существующеей модели {itemTypeName}.\n");
                    
					context.Entry(item).State = EntityState.Modified;
					context.SaveChanges();
					return;
				}
			}
			catch (Exception ex)
			{
                _logger.Error($"Создание/обновление модели {itemTypeName} не осуществлено.\nКод ошибки: {ex.Message}\n");
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
                    _logger.Debug($"Удаление модели {itemTypeName} с Id = {id}.\n");
                    
					dbSetEntityItems.Remove(tItem);
					context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
                _logger.Error($"Удаление модели {itemTypeName} с Id = {id} не осуществлено.\nКод ошибки: {ex.Message}\n");
            }
		}
	}
}
