using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Interfaces
{
    public interface ITechTaskService
    {
        TechTaskPage GetPage(string pageName);


        TechTaskPage GetPageFromViewModel(TechTaskPageViewModel viewModel);


        void Save(TechTaskPage page);


        double CalculateProgress(TechTaskPage page);


        TechTaskPageViewModel GetPageViewModel(string pageName);
    }
}
