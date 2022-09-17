namespace ToursWebAppEXAMProject.Models
{
	public class EditMenuViewModel
	{
		public bool isFullName { get; set; }

		public string fullNameOrKeywordOfItem { get; set; }	

		public EditMenuViewModel(string fullNameOrKeywordOfItem, bool isFullName)
		{
			this.fullNameOrKeywordOfItem = fullNameOrKeywordOfItem;
			this.isFullName = isFullName;
		}

	}
}
