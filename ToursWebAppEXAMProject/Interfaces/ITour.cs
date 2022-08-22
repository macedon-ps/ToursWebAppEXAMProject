using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface ITour
	{
		IEnumerable<Tour> GetAllTours();

		Tour GetTourById(int id);

		void SaveTour(Tour tour);

		void DeleteTour(int id);
	}
}