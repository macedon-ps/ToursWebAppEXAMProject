using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Html;

namespace ToursWebAppEXAMProject.Models
{
	public class SearchProductViewModel
	{
		[Display(Name = "Страна:")]
		public CountriesList? CountryNameFromSelectList { get; set; } 

		[Display(Name = "Даты поездки:")]
		public DateTime? DaysInterval { get; set; } 

		[Display(Name = "Дней поездки:")]
		public NumberOfDays? NumberOfDaysFromSelectList { get; set; } 
				
		[Display(Name = "Взрослых и детей:")]
		public NumberOfPeople? NumberOfPeopleFromSelectList { get; set; }

		[Display(Name = "Города:")]
		public List<string>? CitiesFromSelectList { get; set; } 

		[Display(Name = "Карта / фотографии:")]
		public string? MapImagePath { get; set; } 

		[Display(Name = "Информация о стране:")]
		public string? CountryDescription { get; set; }

		[Display(Name = "Информация о городе:")]
		public string? CityDescrition { get; set; } 

		[Display(Name = "Достопримечательность:")]
		public string? LocalDescrition { get; set; } 

		public List<string>? PicturesImagePath { get; set; } 

	}
}
