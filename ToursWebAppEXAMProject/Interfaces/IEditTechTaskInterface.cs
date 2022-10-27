using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface IEditTechTaskInterface
	{
		TechTaskViewModel GetTechTasksForPage(string pageName);

		void SaveProgressTechTasks(TechTaskViewModel techTasks);
	}
}
