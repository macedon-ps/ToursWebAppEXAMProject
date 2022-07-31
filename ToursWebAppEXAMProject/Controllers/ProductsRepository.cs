using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using NLog;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Services;

namespace ToursWebAppEXAMProject.Controllers
{
	public class ProductsRepository : IProduct
	{
		//public List<Product> Products { get; set; } = null!;
		public bool IsConnected { get; set; } = false;

		

		// создаем подключение к БД
		// private readonly SqlConnection _connection;

		/// <summary>
		/// Контекст подключения к MS SQL Server, к БД TourFirmaDB
		/// </summary>
		private readonly TourFirmaDBContext context;

		/// <summary>
		/// DI. Подключение зависимости. Связывание с комнтекстом
		/// </summary>
		/// <param name="_context">контекст подключения к БД</param>
		public ProductsRepository(TourFirmaDBContext _context) 
		{
			context = _context;
			logger.Debug("Произведено подключение к базе данных");
			Console.WriteLine("Произведено подключение к базе данных");
		}

		/// <summary>
		/// Статическое сойство для логирования событий
		/// </summary>
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// Метод по возврату коллекции турпродуктов из БД
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Product> GetAllTours()
		{
			logger.Debug("Отправлен запрос к БД по выборке всех туров");
			Console.WriteLine("Отправлен запрос к БД по выборке всех туров");

			try
            {
				logger.Debug("Выборка осуществлена успешно");
				Console.WriteLine("Выборка осуществлена успешно");

				return context.Products;
			}
			catch(Exception ex)
            {
				logger.Debug("Выборка не осуществлена");
				logger.Error($"Код ошибки: {ex.Message}");
                Console.WriteLine("Выборка не осуществлена");
				Console.WriteLine($"Код ошибки: {ex.Message}");
				
				return Enumerable.Empty<Product>();
			}
		}
		/// <summary>
		/// Метод по возврату конкретного турпродукта по его Id
		/// </summary>
		/// <param name="id">идентификатор турпродукта</param>
		/// <returns></returns>
		public Product GetTour(int id)
		{
			logger.Trace($"Запрашивваемый id туристического продукта: {id}");
			Console.WriteLine($"Запрашивваемый id туристического продукта: {id}");

			try
			{
				logger.Debug("Выборка осуществлена успешно");
				Console.WriteLine("Выборка осуществлена успешно");

				return context.Products.FirstOrDefault(x => x.Id == id);
			}
			catch(Exception ex)
            {
				logger.Debug($"Выборка турпродукта с Id = {id} не осуществлена");
				logger.Error($"Код ошибки: {ex.Message}");
				Console.WriteLine($"Выборка турпродукта с Id = {id} не осуществлена");
				Console.WriteLine($"Код ошибки: {ex.Message}");
				return new Product();
			}
		}
	}
}
