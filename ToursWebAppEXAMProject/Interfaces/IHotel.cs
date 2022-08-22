using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface IHotel
	{
		IEnumerable<Hotel> GetAllHotels();

		Hotel GetHotelById(int id);

		void SaveHotel(Hotel hotel);

		void DeleteHotel(int id);
	}
}