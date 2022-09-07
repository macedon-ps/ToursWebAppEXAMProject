namespace ToursWebAppEXAMProject.Interfaces
{
	public interface IBaseInterface<T> where T : class
	{
		IEnumerable<T> GetAllItems();

		IEnumerable<T> GetQueryResultItems(string keyword, bool isFullName);

		T GetItemById(int id);

		void SaveItem(T tItem);

		void DeleteItem(T tItem);
	}
}
