using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.ViewModels
{
	public class ErrorViewModel
	{
		/// <summary>
		/// Передаваемый в GET запросе id какой-то сущности 
		/// </summary> 
		[Display(Name = "id сущности, вызвавшей ошибку")]
		public int? RequestId { get; set; }

		/// <summary>
		/// Сообщение, передаваемое во вью ошибки
		/// </summary>

		[Display(Name = "Сообщение об ошибке")]
		public string? MessageToErrorViewModel { get; set; }

		/// <summary>
		/// Тип модели, кот. вызывает ошибку
		/// </summary>
		[Display(Name = "Тип данных (класс), вызвавший ошибку")]
		public string ModelTypeCalledError { get; set; } = null!;

		/// <summary>
		/// Дата и время возникновения ошибки
		/// </summary>
		[Display(Name = "Дата и время ошибки")]
		public DateTime DateTimeError { get; set; }

		/// <summary>
		/// Метод IsRequestId(int? requestId), кот. возвращает булевое значение, было ли id какой-то сущности
		/// </summary>
		/// <param name="requestId">id какой-то сущности</param>
		/// <returns></returns>
		public bool IsRequestId(int? requestId)
		{
			if (requestId == null) return false;
			return true;
		}

		/// <summary>
		/// Метод IsErrorMessage, кот. возвращает булевое значение, было ли сообщение об ошибке
		/// </summary>
		public bool IsErrorMessage => !string.IsNullOrEmpty(MessageToErrorViewModel);

		public ErrorViewModel(Type modelType, int id, string? message = "")
		{
			DateTimeError = DateTime.Now;
			ModelTypeCalledError = modelType.ToString();
			RequestId = id;
			if (message != null && id != 0) MessageToErrorViewModel = message ?? $"вывод значения сущности с id = {id} из БД невозможен";
		}

		public ErrorViewModel(Type modelType, string? message = "") : this(modelType, 0, message)
		{
			if (message != null) MessageToErrorViewModel = message ?? "вывод значений сущностей из БД невозможен";
		}
	}
}