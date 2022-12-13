namespace ToursWebAppEXAMProject.Models
{
	public class SearchProductViewModel
	{
		public string? CountryNameFromSelectList { get; set; } = "Страна";

		public DateTime? DaysInterval { get; set; }

		public NumberOfDays? NumberOfDaysFromSelectList { get; set; } = NumberOfDays.Один;

		public string? NumberOfPeopleFromSelectList { get; set; } = "Количество взрослых и детей";

		public List<string>? CitiesFromSelectList { get; set; } = new List<string>() { "Список городов"};

	public string? MapImagePath { get; set; } = "Расположение карты";

		public string? CountryDescription { get; set; } = "Краткое описание страны";

		public string? CityDescrition { get; set; } = "Краткое описание города";

		public string? LocalDescrition { get; set; } = "Краткое описание местной достопримечательности";

		public List<string>? PicturesImagePath { get; set; } = new List<string>() { "Расположение картинок"};

	}
}
