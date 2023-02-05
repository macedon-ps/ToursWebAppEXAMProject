using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToursWebAppEXAMProject.Controllers;
using ToursWebAppEXAMProject.EnumsDictionaries;
using ToursWebAppEXAMProject.Repositories;

namespace ToursWebAppEXAMProject.ViewModels
{
	public class SearchProductViewModel
	{
		/// <summary>
		/// Id страны
		/// </summary>
		public int CountryId { get; set; }

		/// <summary>
		/// Название выбранной страны
		/// </summary>
		[Display(Name = "Страна:")]
		public string? CountryNameSelected { get; set; } 

		/// <summary>
		/// Список стран в формате List<string>
		/// </summary>
		public List<string>? Countries { get; set; } = null!;

		/// <summary>
		/// Список стран в формате SelectList
		/// </summary>
		public SelectList? CountriesList { get; set; } = null!;

		/// <summary>
		/// Событие изменения списка стран
		/// </summary>
		//public event Action? SelectCountryChangeEvent;

		/// <summary>
		/// Название выбранного города
		/// </summary>
		[Display(Name = "Город:")]
		public string? CityNameSelected { get; set; } 

		/// <summary>
		/// Список городов выбранной страны в формате List<string>
		/// </summary>
		public List<string>? Cities { get; set; } = null!;

		/// <summary>
		/// Список городов выбранной страны в формате SelectList
		/// </summary>
		public SelectList? CitiesList { get; set; } = null!;

		/// <summary>
		/// Выбранные даты отдыха
		/// </summary>
		[Display(Name = "Даты поездки:")]
		// TODO: нужно доработать данный параметр DaysInterval / возможно это только дата начала ???
		public DateTime DaysInterval { get; set; } = DateTime.Now;

		/// <summary>
		/// Количество дней отдыха
		/// </summary>
		[Display(Name = "Дней поездки:")]
		public NumberOfDaysEnum NumberOfDaysFromSelectList { get; set; } = NumberOfDaysEnum.Шесть_Восемь;

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
		
		/// <summary>
		/// Краткая информация о стране
		/// </summary>
		[Display(Name = "Информация о стране:")]
		public string CountryDescription { get; set; } = "Информация о стране:";

		/// <summary>
		/// Краткая информация о городе
		/// </summary>
		[Display(Name = "Информация о городе:")]
		public string CityDescrition { get; set; } = "Информация о городе:";

		/// <summary>
		/// Краткая информация о достопримечательности
		/// </summary>
		[Display(Name = "Достопримечательность:")]
		public string LocalDescrition { get; set; } = "Достопримечательность:";

		/// <summary>
		/// Список адресов картинок / изображений
		/// </summary>
		public List<string>? PicturesImagePath { get; set; }

	}
}
