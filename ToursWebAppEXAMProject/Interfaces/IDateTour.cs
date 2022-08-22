using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface IDateTour
	{
		IEnumerable<DateTour> GetAllDateTours();

		DateTour GetDateTourById(int id);

		void SaveDateTour(DateTour dateTour);

		void DeleteDateTour(int id);
	}
}