using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Services.TechTasks
{
    public class TechTaskService : ITechTaskService
    {
        private readonly IEditTechTaskInterface _repo;

        public TechTaskService(IEditTechTaskInterface repo)
        {
            _repo = repo;
        }

        public TechTaskPage GetPage(string pageName)
        {
            return _repo.GetPageWithTasks(pageName);
        }


        public void Save(TechTaskPage page)
        {
            CalculateProgress(page);

            _repo.Save(page);
        }


        public double CalculateProgress(TechTaskPage page)
        {
            var progress = 0.0;

            if (!page.Tasks.Any())
            {
               return progress;
            }

            int completed = page.Tasks.Count(t => t.IsCompleted);

            progress = Math.Round((double)completed / page.Tasks.Count * 100);

            return progress;
        }
    }
}
