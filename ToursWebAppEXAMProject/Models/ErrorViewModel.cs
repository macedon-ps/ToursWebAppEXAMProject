namespace ToursWebAppEXAMProject.Models
{
	public class ErrorViewModel
	{
		/// <summary>
		/// Передаваемый в GET запросе id какой-то сущности 
		/// </summary> 
		public int? RequestId { get; set; }

		/// <summary>
		/// Сообщение, передаваемое во вью ошибки
		/// </summary>
		
		public string? MessageToErrorViewModel { get; set; }

		/// <summary>
		/// Тип модели, кот. вызывает ошибку
		/// </summary>
		public string ModelTypeCalledError { get; set; } = null!;

		/// <summary>
		/// Дата и время возникновения ошибки
		/// </summary>
		public DateTime DateTimeError { get; set; }

		/// <summary>
		/// Метод IsRequestId(int? requestId), кот. возвращает булевое значение, было ли id какой-то сущности
		/// </summary>
		/// <param name="requestId">id какой-то сущности</param>
		/// <returns></returns>
		public bool IsRequestId(int? requestId)
		{
			if(requestId == null) return false;
			return true;
		}
		
		/// <summary>
		/// Метод IsErrorMessage, кот. возвращает булевое значение, было ли сообщение об ошибке
		/// </summary>
		public bool IsErrorMessage => !string.IsNullOrEmpty(MessageToErrorViewModel);

		public ErrorViewModel()
		{
			DateTimeError = DateTime.UtcNow;
		}

		public static ErrorViewModel GetErrorInfo(Type modelType, int id, string ? message = "")
		{
			var errorInfo = new ErrorViewModel();
			errorInfo.ModelTypeCalledError = modelType.ToString();
			
			if(message != null) errorInfo.MessageToErrorViewModel = message ?? $"вывод значения сущности с id = {id} из БД невозможен";

			errorInfo.RequestId = id;

			return errorInfo;
		}
		public static ErrorViewModel GetErrorInfo(Type modelType, string? message = "")
		{
			var errorInfo = new ErrorViewModel();
			errorInfo.ModelTypeCalledError = modelType.ToString();

			if (message != null) errorInfo.MessageToErrorViewModel = message ?? "вывод значений сущностей из БД невозможен";
			
			return errorInfo;
		}
	}
}