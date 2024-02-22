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

		/// <summary>
		/// Ссылка на сайт, где можно посчитать расходы на туристическое путешествие
		/// </summary>
        public static string CalculateTourSite { get; set; }

        /// <summary>
        /// Ссылка на соц. сеть WhatsApp
        /// </summary>
        public static string WhatsApp { get; set; }

        /// <summary>
        /// Ссылка на соц. сеть Telegram
        /// </summary>
        public static string Telegram { get; set; }

        /// <summary>
        /// Ссылка на соц. сеть Viber
        /// </summary>
        public static string Viber { get; set; }

		public static string Facebook { get; set; }
	}
}
