namespace ToursWebAppEXAMProject.ViewModels
{
	public class EditMenuViewModel
	{
		public bool isFullName { get; set; }

		public string fullNameOrKeywordOfItem { get; set; }

		public string type { get; set; }

		public EditMenuViewModel(bool isFullName, string fullNameOrKeywordOfItem, string type)
		{
			this.isFullName = isFullName;
			this.fullNameOrKeywordOfItem = fullNameOrKeywordOfItem;
			this.type = type;
		}

	}
}
