using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface ICity
	{
		IEnumerable<City> GetAllCities();

		City GetCityById(int id);

		void SaveCity(City city);

		void DeleteCity(int id);

	}
}