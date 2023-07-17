namespace ToursWebAppEXAMProject.ViewModels
{
	/// <summary>
	/// Класс EditMenuViewModel для отображения меню создания/редактирования/удаления новости/блога/турпродукта
	/// </summary>
	public class EditMenuViewModel
	{
		/// <summary>
		/// Свойство isFullName, содержит true, если нужно ввести полное название, false - если нужно ввести ключевое слово (символ)
		/// </summary>
		public bool isFullName { get; set; }

		/// <summary>
		/// свойство fullNameOrKeywordOfItem, содержит полное название или ключевое слово (символ)
		/// </summary>
		public string fullNameOrKeywordOfItem { get; set; }

		/// <summary>
		/// тип данных (новость, блог или турпродукт)
		/// </summary>
		public string type { get; set; }

		/// <summary>
		/// конструктор класса задает параметры для отображения меню создания/редактирования/удаления новости/блога/турпродукта
		/// </summary>
		/// <param name="isFullName">свойство, кот. указывает, что вводится - полное название (true) или ключевое слово (символ) (false)</param>
		/// <param name="fullNameOrKeywordOfItem">текст для поиска</param>
		/// <param name="type">тип данных (новость, блог или турпродукт)</param>
		public EditMenuViewModel(bool isFullName = false, string fullNameOrKeywordOfItem = "", string type = "")
		{
			this.isFullName = isFullName;
			this.fullNameOrKeywordOfItem = fullNameOrKeywordOfItem;
			this.type = type;
		}

	}
}
