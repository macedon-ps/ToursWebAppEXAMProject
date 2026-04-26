using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.ViewModels
{
    public class TechTaskPageViewModel
    {
        public int Id { get; set; }

        public string PageName { get; set; }

        public List<TechTaskItem> Tasks { get; set; }

        public double Progress { get; set; }
    }
}
