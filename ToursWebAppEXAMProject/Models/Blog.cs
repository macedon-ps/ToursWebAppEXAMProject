using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.Models
{
	public partial class Blog
	{
		public Blog() { DateAdded = DateTime.Now; }

		[Required]
		public int Id { get; set; }

		[Required(ErrorMessage = "Введите заголовок блога")]
		[Display(Name = "Заголовок блога")]
		[StringLength(200, ErrorMessage = "Заголовок блога не должен содержать более 200 символов")]
		public string Name { get; set; } = "Заголовок блога";

		[Display(Name = "Сообщение")]
		[StringLength(400, ErrorMessage = "Сообщение не должно содержать более 400 символов")]
		public string Message { get; set; } = "Сообщение";

		[Display(Name = "Вся строка сообщений")]
		public string FullMessageLine { get; set; } = "Вся строка сообщений";

		[Required(ErrorMessage = "Введите краткое описание темы блога")]
		[Display(Name = "Краткое описание темы блога")]
		[StringLength(200, ErrorMessage = "Краткое описание темы блога не должно содержать более 200 символов")]
		public string ShortDescription { get; set; } = "Краткое описание темы блога";

		[Required(ErrorMessage = "Введите полное описание темы блога")]
		[Display(Name = "Полное описание темы блога")]
		public string FullDescription { get; set; } = "Полное описание темы блога";

		[Required(ErrorMessage = "Выберите титульную картинку блога")]
		[Display(Name = "Титульная картинка")]
		[StringLength(100, ErrorMessage = "Путь к титульной картинке не должен содержать более 100 символов")]
		public string TitleImagePath { get; set; } = "Нет титульной картинки";

		[Display(Name = "Время создания")]
		[DataType(DataType.Time)]
		public DateTime? DateAdded { get; set; }
    }
}