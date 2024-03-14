using System.ComponentModel.DataAnnotations;
using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.ViewModels
{
    public class QueryResultProductViewModel
    {
        /// <summary>
		/// Турпродукты - результаты выборки из БД
		/// </summary>
		public List<Product> Products { get; set; } = null!;

        /// <summary>
        /// Выбранная страна
        /// </summary>
        public Country Country { get; set; } = null!;

        /// <summary>
        /// Выбранный город
        /// </summary>
        public City City { get; set; } = null!;

        /// <summary>
        /// Выбранные даты отдыха
        /// </summary>
        [Display(Name = "Даты поездки:")]
        public DateTime DateFrom { get; set; } 

        public DateTime DateTo { get; set; } 

        /// <summary>
        /// Количество дней отдыха
        /// </summary>
        [Display(Name = "Дней поездки:")]
        public NumberOfDaysEnum NumberOfDaysFromSelectList { get; set; } 

        /// <summary>
        /// Количество взрослых и детей
        /// </summary>
        [Display(Name = "Взрослых и детей:")]
        public NumberOfPeopleEnum NumberOfPeopleFromSelectList { get; set; } 
    }
}
