using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Interfaces
{
    public interface ITechTaskService
    {
        TechTaskPage GetPage(string pageName);


        void Save(TechTaskPage page);


        double CalculateProgress(TechTaskPage page);


        TechTaskPageViewModel GetPageViewModel(string pageName);
    }
}
