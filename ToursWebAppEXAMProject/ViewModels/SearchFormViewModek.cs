using System.ComponentModel.DataAnnotations;
using ToursWebAppEXAMProject.Enums;

namespace ToursWebAppEXAMProject.ViewModels
{
    public class SearchFormViewModel
    {
        /// <summary>
		/// Название выбранной страны
		/// </summary>
		[Display(Name = "Страна:")]
        public string? CountryNameSelected { get; set; }

        /// <summary>
		/// Название выбранного города
		/// </summary>
		[Display(Name = "Город:")]
        public string? CityNameSelected { get; set; }

        /// <summary>
        /// Выбранные даты отдыха
        /// </summary>
        [Display(Name = "Даты поездки:")]
        public DateTime DateFrom { get; set; } = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        // по умолчанию - интервал в 5 дней
        public DateTime DateTo { get; set; } = Convert.ToDateTime(DateTime.Now.AddDays(5).ToShortDateString());

        /// <summary>
        /// Количество дней отдыха
        /// </summary>
        [Display(Name = "Дней поездки:")]
        public NumberOfDaysEnum NumberOfDaysFromSelectList { get; set; } = NumberOfDaysEnum.Четыре_Пять;

        /// <summary>
        /// Количество взрослых и детей
        /// </summary>
        [Display(Name = "Взрослых и детей:")]
        public NumberOfPeopleEnum NumberOfPeopleFromSelectList { get; set; } = NumberOfPeopleEnum.Два_взрослых;
    }
}
