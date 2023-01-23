using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.ViewModels
{
	public class ModelsErrorViewModel
	{
		/// <summary>
		/// Передаваемый в GET запросе id сущности, вызвавшей ошибку
		/// </summary> 
		[Display(Name = "id сущности, вызвавшей ошибку")]
		public int? RequestId { get; set; }

		/// <summary>
		/// Сообщение об ошибке, передаваемое во вью ошибки
		/// </summary>
		[Display(Name = "Сообщение об ошибке")]
		public string? ErrorMessage { get; set; }

		/// <summary>
		/// Тип данных (класс), вызвавший ошибку
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
		/// <param name="requestId">id сущности, вызвавшей ошибку</param>
		/// <returns></returns>
		public bool IsRequestId(int? requestId)
		{
			if (requestId == null) return false;
			return true;
		}

		/// <summary>
		/// Метод IsErrorMessage, кот. возвращает булевое значение, было ли сообщение об ошибке
		/// </summary>
		public bool IsErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

		/// <summary>
		/// Конструктор с праметрами (Type, int, string)
		/// </summary>
		/// <param name="modelType">Тип данных (класс), вызвавший ошибку</param>
		/// <param name="id">id сущности, вызвавшей ошибку</param>
		/// <param name="message">Сообщение об ошибке</param>
		public ModelsErrorViewModel(Type modelType, int id, string? message = "")
		{
			DateTimeError = DateTime.Now;
			ModelTypeCalledError = modelType.ToString();
			RequestId = id;
			if (message != null && id != 0) ErrorMessage = message ?? $"вывод значения сущности с id = {id} из БД (или другого источника данных) невозможен";
		}

		/// <summary>
		/// Конструктор с праметрами (Type, string)
		/// </summary>
		/// <param name="modelType">Тип данных (класс), вызвавший ошибку</param>
		/// <param name="message">Сообщение об ошибке</param>
		public ModelsErrorViewModel(Type modelType, string? message = "") : this(modelType, 0, message)
		{
			if (message != null) ErrorMessage = message ?? "вывод значений сущностей из БД (или другого источника данных) невозможен";
		}
	}
}