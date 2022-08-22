using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface ISaller
	{
		IEnumerable<Saller> GetAllSallers();

		Saller GetSallerById(int id);

		void SaveSaller(Saller saller);

		void DeleteSaller(int id);
	}
}