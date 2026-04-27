using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface IEditTechTaskInterface
	{
        TechTaskPage GetPageWithTasks(string pageName);


		void Save(TechTaskPage techTasks);
	}
}
