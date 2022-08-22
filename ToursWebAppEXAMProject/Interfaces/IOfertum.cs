using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface IOfertum
	{
		IEnumerable<Ofertum> GetAllOfertums();

		Ofertum GetOfertumById(int id);

		void SaveOfertum(Ofertum ofertum);

		void DeleteOfertum(int id);
	}
}