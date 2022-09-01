using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.Models
{
	public class Blog
	{
		public Blog() { }

		[Required]
		public int Id { get; set; }

		[Required]
		[Display(Name = "Заголовок блога")]
		public string Title { get; set; } = "Заголовок блога";

		[Display(Name = "Сообщение")]
		public string Message { get; set; } = "Сообщение";

		[Display(Name = "Полная строка сообщений")]
		public string FullMessageLine { get; set; } = "Полная строка сообщений";

		[Display(Name = "Титульная картинка")]
		public string TitleImagePath { get; set; } = null!;
	}
}