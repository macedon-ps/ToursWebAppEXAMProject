namespace ToursWebAppEXAMProject.Interfaces
{
	public interface IBaseInterface<T> where T : class
	{
		IEnumerable<T> GetAllItems();

		T GetItemById(int id);

		void SaveItem(T tItem);

		void DeleteItem(T tItem);
	}
}
