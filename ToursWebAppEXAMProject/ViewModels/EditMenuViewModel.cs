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
		/// свойство fullNameOrKeywordOfItem, содержит полное название или ключевое слово
		/// </summary>
		public string fullNameOrKeywordOfItem { get; set; }

		/// <summary>
		/// тип данных
		/// </summary>
		public string type { get; set; }

		/// <summary>
		/// конструктор класса EditMenuViewModel(), кот. задает параметры для отображения меню создания/редактирования/удаления новости/блога/турпродукта
		/// </summary>
		/// <param name="isFullName">свойство, кот. указывает, что вводится - полное название (true) или ключевое слово (false)</param>
		/// <param name="fullNameOrKeywordOfItem">ввод для поиска полного названия или ключевого слова</param>
		/// <param name="type">тип данных</param>
		public EditMenuViewModel(bool isFullName, string fullNameOrKeywordOfItem, string type)
		{
			this.isFullName = isFullName;
			this.fullNameOrKeywordOfItem = fullNameOrKeywordOfItem;
			this.type = type;
		}

	}
}
