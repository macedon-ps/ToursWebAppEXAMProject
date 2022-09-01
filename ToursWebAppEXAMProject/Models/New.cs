using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.Models
{
	public class New
	{
		public New() { }

		[Required]
		public int Id { get; set; }

		[Display(Name = "Заголовок новости")]
		public string Title { get; set; } = "Заголовок новости";

		[Display(Name = "Краткое орисание новости")]
		public string ShortDescription { get; set; } = "Краткое орисание новости";

		[Display(Name = "Полное описание новости")]
		public string FullDescription { get; set; } = "Полное описание новости";
		
		[Display(Name = "Титульная картинка")]
		public string TitleImagePath { get; set; } = null!;
	}
}
