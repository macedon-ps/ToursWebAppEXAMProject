using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.ViewModels
{
	public class ErrorViewModel
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

		public ErrorViewModel(string? errorMessage)
		{
			DateTimeError = DateTime.Now;
			ErrorMessage = errorMessage;
		}
	}
}
