namespace ToursWebAppEXAMProject.Models
{
	public class ErrorViewModel
	{
		/// <summary>
		/// ������������ � GET ������� id �����-�� �������� 
		/// </summary> 
		public int? RequestId { get; set; }

		/// <summary>
		/// ���������, ������������ �� ��� ������
		/// </summary>
		
		public string? MessageToErrorViewModel { get; set; }

		/// <summary>
		/// ��� ������, ���. �������� ������
		/// </summary>
		public string ModelTypeCalledError { get; set; } = null!;

		/// <summary>
		/// ���� � ����� ������������� ������
		/// </summary>
		public DateTime DateTimeError { get; set; }

		/// <summary>
		/// ����� IsRequestId(int? requestId), ���. ���������� ������� ��������, ���� �� id �����-�� ��������
		/// </summary>
		/// <param name="requestId">id �����-�� ��������</param>
		/// <returns></returns>
		public bool IsRequestId(int? requestId)
		{
			if(requestId == null) return false;
			return true;
		}
		
		/// <summary>
		/// ����� IsErrorMessage, ���. ���������� ������� ��������, ���� �� ��������� �� ������
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
			
			if(message != null) errorInfo.MessageToErrorViewModel = message ?? $"����� �������� �������� � id = {id} �� �� ����������";

			errorInfo.RequestId = id;

			return errorInfo;
		}
		public static ErrorViewModel GetErrorInfo(Type modelType, string? message = "")
		{
			var errorInfo = new ErrorViewModel();
			errorInfo.ModelTypeCalledError = modelType.ToString();

			if (message != null) errorInfo.MessageToErrorViewModel = message ?? "����� �������� ��������� �� �� ����������";
			
			return errorInfo;
		}
	}
}