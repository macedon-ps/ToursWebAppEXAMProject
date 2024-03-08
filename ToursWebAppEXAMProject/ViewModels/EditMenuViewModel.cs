namespace ToursWebAppEXAMProject.ViewModels
{
	/// <summary>
	/// Класс EditMenuViewModel для отображения меню создания/редактирования/удаления новости/блога/турпродукта
	/// </summary>
	public class EditMenuViewModel
	{
		/// <summary>
		/// Свойство IsFullName, содержит true, если нужно ввести полное название, false - если нужно ввести ключевое слово (символ)
		/// </summary>
		public bool IsFullName { get; set; }

		/// <summary>
		/// свойство InsertedText, содержит полное название или ключевое слово (символ)
		/// </summary>
		public string InsertedText { get; set; }

		/// <summary>
		/// тип данных (новость, блог или турпродукт)
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// конструктор класса задает параметры для отображения меню создания/редактирования/удаления новости/блога/турпродукта
		/// </summary>
		/// <param name="isFullName">свойство, кот. указывает, что вводится - полное название (true) или ключевое слово (символ) (false)</param>
		/// <param name="fullNameOrKeywordOfItem">текст для поиска</param>
		/// <param name="type">тип данных (новость, блог или турпродукт)</param>
		public EditMenuViewModel(bool isFullName = false, string insertedText = "", string type = "")
		{
			IsFullName = isFullName;
			InsertedText = insertedText;
			Type = type;
		}

	}
}
