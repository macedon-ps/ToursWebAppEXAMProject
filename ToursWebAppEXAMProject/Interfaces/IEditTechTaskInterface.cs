using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface IEditTechTaskInterface
	{
        // старые методы
        TechTaskViewModel GetTechTasksForPage(string pageName);


        void SaveProgressTechTasks(TechTaskViewModel techTasks);


        // новые методы
        TechTaskPage GetPageWithTasks(string pageName);


		void Save(TechTaskPage techTasks);
	}
}
