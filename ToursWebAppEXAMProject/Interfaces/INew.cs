using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface INew
	{
		IEnumerable<New> GetAllNews();

		New GetNewById(int id);

		void SaveNew(New news);

		void DeleteNew(int id);		
	}
}