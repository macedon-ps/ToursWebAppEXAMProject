using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface ILocation
	{
		IEnumerable<Location> GetAllLocation();

		Location GetLocationById(int id);

		void SaveLocation(Location location);

		void DeleteLocation(int id);
	}
}