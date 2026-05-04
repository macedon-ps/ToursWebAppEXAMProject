using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.ViewModels
{
	public class MyErrorViewModel
	{
		/// <summary>
		/// Сообщение об ошибке
		/// </summary>
		[Display(Name = "Сообщение об ошибке")]
		public string? ErrorMessage { get; set; }

		/// <summary>
		/// Дата и время возникновния ошибки
		/// </summary>
		[Display(Name = "Дата и время ошибки")]
		public DateTime DateTimeError { get; set; }

		public MyErrorViewModel(string? errorMessage)
		{
			DateTimeError = DateTime.Now;
			ErrorMessage = errorMessage;
		}
	}
}
