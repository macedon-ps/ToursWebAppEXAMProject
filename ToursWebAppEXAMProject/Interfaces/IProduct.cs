using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	/// <summary>
	/// Интерфейс ITourProduct
	/// </summary>
	public interface IProduct
	{
		/// <summary>
		/// Метод интерфейса GetAllProducts(), кот. возвращает список всех существующих в базе туристических продуктов
		/// </summary>
		/// <returns></returns>
		IEnumerable<Product> GetAllProducts();

		/// <summary>
		/// Метод интерфейса GetProduct(int id), кот. возвращает из базы один турпродукт по его id
		/// </summary>
		/// <param name="id">id турпродукта</param>
		/// <returns></returns>
		Product GetProduct(int id);

		/// <summary>
		/// Метод интерфейса SaveProduct(Product product), кот. создает/или изменяет турподукт по его объекту и добавляет в БД
		/// </summary>
		/// <param name="product">объект турпродукта</param>
		void SaveProduct(Product product);

		/// <summary>
		/// Метод DeleteProduct(int id), кот. удаляет мз БД турпродукт по его id
		/// </summary>
		/// <param name="id">id турпродукта</param>
		void DeleteProduct(int id);


	}
}
