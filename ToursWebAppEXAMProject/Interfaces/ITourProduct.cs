using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	/// <summary>
	/// Интерфейс ITourProduct
	/// </summary>
	public interface ITourProduct
	{
		/// <summary>
		/// Метод интерфейса GetAllTourProducts(), кот. возвращает список всех существующих в базе туров
		/// </summary>
		/// <returns></returns>
		IEnumerable<TourProduct> GetAllTourProducts();

		/// <summary>
		/// Метод интерфейса GetTourProduct(int id), кот. возвращает из базы одного стдента по его id
		/// </summary>
		/// <param name="id">id тура</param>
		/// <returns></returns>
		TourProduct GetTourProduct(int id);

	}
}
