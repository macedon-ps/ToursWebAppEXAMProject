using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using NLog;
using System.Collections.Generic;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Services;

namespace ToursWebAppEXAMProject.Repositories
{
	public class ProductsRepository : IProduct
	{
		/// <summary>
		/// Контекст подключения к MS SQL Server, к БД TourFirmaDB
		/// </summary>
		private readonly TourFirmaDBContext context;

		/// <summary>
		/// Статическое сойство для логирования событий
		/// </summary>
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

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
		/// Метод GetAllProducts(), кот. используется для возврата коллекции турпродуктов из БД
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Product> GetAllProducts()
		{
			logger.Trace("Запрашиваются все туристические продукты");
			Console.WriteLine("Запрашиваются все туристические продукты");

			try
			{
				var products = context.Products;

				if (products == null)
				{
					logger.Warn($"Выборка туристических продуктов не осуществлена. Они не существуют");
					Console.WriteLine($"Выборка туристических продуктов не осуществлена. Они не существуют");

					return new List<Product>();
				}
				else
				{
					logger.Debug("Выборка осуществлена успешно");
					Console.WriteLine("Выборка осуществлена успешно");

					return products;
				}

			}
			catch (Exception ex)
			{
				logger.Error("Выборка не осуществлена");
				logger.Error($"Код ошибки: {ex.Message}");
				Console.WriteLine("Выборка не осуществлена");
				Console.WriteLine($"Код ошибки: {ex.Message}");

				return new List<Product>();
			}
		}

		/// <summary>
		/// Метод GetProduct(int id), кот. используется для возврата конкретного турпродукта по его Id
		/// </summary>
		/// <param name="id">идентификатор турпродукта</param>
		/// <returns></returns>
		public Product GetProduct(int id)
		{
			logger.Trace($"Запрашивваемый id туристического продукта: {id}");
			Console.WriteLine($"Запрашивваемый id туристического продукта: {id}");

			try
			{
				var product = context.Products.FirstOrDefault(x => x.Id == id);
				
				if (product == null)
				{
					logger.Warn($"Выборка турпродукта не осуществлена. Турпродукта с Id = {id} не существует");
					Console.WriteLine($"Выборка турпродукта не осуществлена. Турпродукта с Id = {id} не существует");

					return new Product();
				}
				else
				{
					logger.Debug("Выборка осуществлена успешно");
					Console.WriteLine("Выборка осуществлена успешно");

					return product;
				}

			}
			catch (Exception ex)
			{
				logger.Error($"Выборка турпродукта с Id = {id} не осуществлена");
				logger.Error($"Код ошибки: {ex.Message}");
				Console.WriteLine($"Выборка турпродукта с Id = {id} не осуществлена");
				Console.WriteLine($"Код ошибки: {ex.Message}");
				return new Product();
			}
		}

		/// <summary>
		/// Метод SaveProduct(Product product), кот. используется для создания нового/изменения существующего турпродукта по его объекту
		/// </summary>
		/// <param name="product">объект турпродукта</param>
		/// <exception cref="NotImplementedException"></exception>
		public void SaveProduct(Product product)
		{
			try
			{
				if (product.Id == 0)
				{
					logger.Trace($"Создание нового туристического продукта");
					Console.WriteLine($"Создание нового туристического продукта");
					context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Added;
				}
				else
				{
					logger.Trace($"Обновление существующего туристического продукта с id = {product.Id}");
					Console.WriteLine($"Обновление существующего туристического продукта с id = {product.Id}");
					context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				}
				context.SaveChanges();
			}
			catch (Exception ex)
			{
				var str = "с id = ";
				var empty = "";
				logger.Error($"Создание/обновление турпродукта {(product.Id != 0 ? str + product.Id : empty)} не осуществлено");
				logger.Error($"Код ошибки: {ex.Message}");
				Console.WriteLine($"Создание/обновление турпродукта {(product.Id != 0 ? str + product.Id : empty)} не осуществлено");
				Console.WriteLine($"Код ошибки: {ex.Message}");
			}
		}

		/// <summary>
		/// Метод DeleteProduct(int id), кот. используется для удаления турпродукта по его id
		/// </summary>
		/// <param name="id">идентификатор турпродукта</param>
		/// <exception cref="NotImplementedException"></exception>
		public void DeleteProduct(int id)
		{
			try
			{
				if (context.Products.Any(x => x.Id == id))
				{
					logger.Trace($"Удаление туристического продукта с id = {id}");
					Console.WriteLine($"Удаление туристического продукта с id = {id}");
					context.Products.Remove(new Product() { Id = id });
					context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				logger.Error($"Удаление турпродукта с id = {id} не осуществлено");
				logger.Error($"Код ошибки: {ex.Message}");
				Console.WriteLine($"Удаление турпродукта с id = {id} не осуществлено");
				Console.WriteLine($"Код ошибки: {ex.Message}");
			}
		}
	}
}
