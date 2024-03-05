using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToursWebAppEXAMProject.ConfigFiles
{
    public class ConfigData : PageModel
    {
        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        public static string ConnectionString { get; set; } = null!;

        /// <summary>
        /// Название компании
        /// </summary>
        public static string CompanyName { get; set; } = null!;

        /// <summary>
        /// Контактный телефон компании
        /// </summary>
        public static string CompanyPhone { get; set; } = null!;

        /// <summary>
        /// Электронная почта компании
        /// </summary>
        public static string CompanyEmail { get; set; } = null!;

        /// <summary>
        /// Ссылка на сайт, где можно посчитать расходы на туристическое путешествие
        /// </summary>
        public static string CalculateTourSite { get; set; } = null!;

        /// <summary>
        /// Ссылка на соц. сеть WhatsApp
        /// </summary>
        public static string WhatsApp { get; set; } = null!;

        /// <summary>
        /// Ссылка на соц. сеть Telegram
        /// </summary>
        public static string Telegram { get; set; } = null!;

        /// <summary>
        /// Ссылка на соц. сеть Viber
        /// </summary>
        public static string Viber { get; set; } = null!;

        public static string Facebook { get; set; } = null!;
        
        public static string Domain { get; set; } = null!;

    }
}
