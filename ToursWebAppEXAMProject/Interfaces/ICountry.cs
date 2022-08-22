using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface ICountry
	{
		IEnumerable<Country> GetAllCountries();
		Country GetCountryById(int id);

		void SaveCountry(Country country);

		void DeleteCountry(int id);
	}
}