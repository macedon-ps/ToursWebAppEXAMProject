using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.ViewModels
{
	public class ErrorViewModel
	{
		/// <summary>
		/// ������������ � GET ������� id �����-�� �������� 
		/// </summary> 
		[Display(Name = "id ��������, ��������� ������")]
		public int? RequestId { get; set; }

		/// <summary>
		/// ���������, ������������ �� ��� ������
		/// </summary>

		[Display(Name = "��������� �� ������")]
		public string? MessageToErrorViewModel { get; set; }

		/// <summary>
		/// ��� ������, ���. �������� ������
		/// </summary>
		[Display(Name = "��� ������ (�����), ��������� ������")]
		public string ModelTypeCalledError { get; set; } = null!;

		/// <summary>
		/// ���� � ����� ������������� ������
		/// </summary>
		[Display(Name = "���� � ����� ������")]
		public DateTime DateTimeError { get; set; }

		/// <summary>
		/// ����� IsRequestId(int? requestId), ���. ���������� ������� ��������, ���� �� id �����-�� ��������
		/// </summary>
		/// <param name="requestId">id �����-�� ��������</param>
		/// <returns></returns>
		public bool IsRequestId(int? requestId)
		{
			if (requestId == null) return false;
			return true;
		}

		/// <summary>
		/// ����� IsErrorMessage, ���. ���������� ������� ��������, ���� �� ��������� �� ������
		/// </summary>
		public bool IsErrorMessage => !string.IsNullOrEmpty(MessageToErrorViewModel);

		public ErrorViewModel(Type modelType, int id, string? message = "")
		{
			DateTimeError = DateTime.Now;
			ModelTypeCalledError = modelType.ToString();
			RequestId = id;
			if (message != null && id != 0) MessageToErrorViewModel = message ?? $"����� �������� �������� � id = {id} �� �� ����������";
		}

		public ErrorViewModel(Type modelType, string? message = "") : this(modelType, 0, message)
		{
			if (message != null) MessageToErrorViewModel = message ?? "����� �������� ��������� �� �� ����������";
		}
	}
}