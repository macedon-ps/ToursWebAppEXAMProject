using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	/// <summary>
	/// Интерфейс ITourProduct
	/// </summary>
	public interface IProduct
	{
		/// <summary>
		/// Метод интерфейса GetAllTourProducts(), кот. возвращает список всех существующих в базе туров
		/// </summary>
		/// <returns></returns>
		IEnumerable<Product> GetAllTourProducts();

		/// <summary>
		/// Метод интерфейса GetTourProduct(int id), кот. возвращает из базы одного стдента по его id
		/// </summary>
		/// <param name="id">id тура</param>
		/// <returns></returns>
		Product GetTourProduct(int id);

	}
}
