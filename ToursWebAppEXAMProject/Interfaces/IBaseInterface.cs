﻿namespace ToursWebAppEXAMProject.Interfaces
{
	public interface IBaseInterface<T> where T : class
	{
		IEnumerable<T> GetAllItems();

		IEnumerable<T> GetQueryResultItemsAfterFullName(string keyword, bool isFullName);

		T GetItemById(int id);

		void SaveItem(T tItem, int id);

		void DeleteItem(T tItem, int id);
	}
}
