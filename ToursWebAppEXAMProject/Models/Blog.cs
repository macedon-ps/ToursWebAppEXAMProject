using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.Models
{
	public partial class Blog
	{
		public Blog() { DateAdded = DateTime.Now; }

		[Required]
		public int Id { get; set; }

		[Required]
		[Display(Name = "Заголовок блога")]
		public string Name { get; set; } = "Заголовок блога";

		[Display(Name = "Сообщение")]
		public string Message { get; set; } = "Сообщение";

		[Display(Name = "Вся строка сообщений")]
		public string FullMessageLine { get; set; } = "Вся строка сообщений";

		[Display(Name = "Короткое описание темы блога")]
		public string ShortDescription { get; set; } = "Краткое описание темы блога";

		[Display(Name = "Полное описание темы блога")]
		public string FullDescription { get; set; } = "Полное описание темы блога";

		[Display(Name = "Титульная картинка")]
		public string? TitleImagePath { get; set; }

		[Display(Name = "Время создания")]
		[DataType(DataType.Time)]
		public DateTime? DateAdded { get; set; } 
	}
}