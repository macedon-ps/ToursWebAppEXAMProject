using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToursWebAppEXAMProject.DTOs;
using ToursWebAppEXAMProject.Enums;

namespace ToursWebAppEXAMProject.ViewModels
{
    public class SearchProductViewModel
	{
		/// <summary>
		/// Id выбранной из списка страны
		/// </summary>
		public int? CountryIdSelected { get; set; }

        /// <summary>
        /// Название выбранной страны
        /// </summary>
        [Display(Name = "Страна:")]
		public string? CountryNameSelected { get; set; } 

		/// <summary>
		/// Список стран в формате SelectList
		/// </summary>
		public SelectList? CountriesList { get; set; } = null!;

        /// <summary>
        /// Id выбранного из списка города
        /// </summary>
        public int? CityIdSelected { get; set; }

        /// <summary>
        /// Название выбранного города
        /// </summary>
        [Display(Name = "Город:")]
		public string? CityNameSelected { get; set; } 

		/// <summary>
		/// Список городов выбранной страны в формате SelectList
		/// </summary>
		public SelectList? CitiesList { get; set; } = null!;

        public List<CountryDto> CountriesData { get; set; } = new();

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
		
		/// <summary>
		/// Адрес карты / или фотографии
		/// </summary>
		[Display(Name = "Карта / фотографии:")]
		public string MapImagePath { get; set; } = "/images/Maps/UkraineMap.jpg";
	}
}
