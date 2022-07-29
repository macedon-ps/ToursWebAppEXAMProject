using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToursWebAppEXAMProject.Services
{
	public class ConfigData : PageModel
	{
		/// <summary>
		/// Строка подключения к БД
		/// </summary>
		public static string ConnectionString { get; set; }
		
		/// <summary>
		/// Название компании
		/// </summary>
		public static string CompanyName { get; set; }
		
		/// <summary>
		/// Контактный телефон компании
		/// </summary>
		public static string CompanyPhone { get; set; }
		
		/// <summary>
		/// Электронная почта компании
		/// </summary>
		public static string CompanyEmail { get; set; }

	}
}
