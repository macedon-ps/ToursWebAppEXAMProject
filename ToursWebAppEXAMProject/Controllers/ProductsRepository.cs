﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using NLog;
using System.Collections.Generic;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Services;

namespace ToursWebAppEXAMProject.Controllers
{
	public class ProductsRepository : IProduct
	{
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
		public IEnumerable<Product> GetAllProducts()
		{
			logger.Debug("Запрашиваются все туристические продукты");
			Console.WriteLine("Запрашиваются все туристические продукты");

			try
            {
				var products = context.Products;
				
				if(products == null)
				{
					logger.Warn($"Выборка туристических продуктов не осуществлена. Они не существуют\n");
					Console.WriteLine($"Выборка туристических продуктов не осуществлена. Они не существуют\n");

					return new List<Product>();
				}
				else
				{
					logger.Debug("Выборка осуществлена успешно\n");
					Console.WriteLine("Выборка осуществлена успешно\n");

					return products;
				}
				
			}
			catch(Exception ex)
            {
				logger.Error("Выборка не осуществлена");
				logger.Error($"Код ошибки: {ex.Message}\n");
                Console.WriteLine("Выборка не осуществлена");
				Console.WriteLine($"Код ошибки: {ex.Message}\n");
				
				return new List<Product>();
			}
		}
		/// <summary>
		/// Метод по возврату конкретного турпродукта по его Id
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
					logger.Warn($"Выборка турпродукта не осуществлена. Турпродукта с Id = {id} не существует\n");
					Console.WriteLine($"Выборка турпродукта не осуществлена. Турпродукта с Id = {id} не существует\n");

					return new Product();
				}
				else
				{
					logger.Debug("Выборка осуществлена успешно\n");
					Console.WriteLine("Выборка осуществлена успешно\n");

					return product;
				}
				
			}
			catch(Exception ex)
            {
				logger.Error($"Выборка турпродукта с Id = {id} не осуществлена");
				logger.Error($"Код ошибки: {ex.Message}\n");
				Console.WriteLine($"Выборка турпродукта с Id = {id} не осуществлена");
				Console.WriteLine($"Код ошибки: {ex.Message}\n");
				return new Product();
			}
		}
	}
}
