using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Html;
using ToursWebAppEXAMProject.EnumsDictionaries;

namespace ToursWebAppEXAMProject.ViewModels
{
	public class SearchProductViewModel
	{
		[Display(Name = "Страна:")]
		public CountriesListEnum? CountryNameFromSelectList { get; set; } = CountriesListEnum.Украина;
		
		[Display(Name = "Даты поездки:")]
		public DateTime? DaysInterval { get; set; } = DateTime.Now;

		[Display(Name = "Дней поездки:")]
		public NumberOfDaysEnum? NumberOfDaysFromSelectList { get; set; } = NumberOfDaysEnum.Шесть_Восемь;

		[Display(Name = "Взрослых и детей:")]
		public NumberOfPeopleEnum? NumberOfPeopleFromSelectList { get; set; } = NumberOfPeopleEnum.Два_взрослых;

		[Display(Name = "Города:")]
		public CitiesListEnum? CityFromSelectList { get; set; } = CitiesListEnum.Киев;

		[Display(Name = "Карта / фотографии:")]
		public string? MapImagePath { get; set; } = "/images/Maps/UkraineMap.jpg";
		
		[Display(Name = "Информация о стране:")]
		public string? CountryDescription { get; set; } = "Информация о стране:";

		[Display(Name = "Информация о городе:")]
		public string? CityDescrition { get; set; } = "Информация о городе:";

		[Display(Name = "Достопримечательность:")]
		public string? LocalDescrition { get; set; } = "Достопримечательность:";

		public List<string>? PicturesImagePath { get; set; }

	}
}
