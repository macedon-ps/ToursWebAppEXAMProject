using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;

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


        public TechTaskPage GetPageFromViewModel(TechTaskPageViewModel viewModel)
        {
            
            var page = new TechTaskPage
            {
                Id = viewModel.Id,
                PageName = viewModel.PageName,
                Tasks = viewModel.Tasks.Select(t => new TechTaskItem
                {
                    Id = t.Id,
                    OrderNumber = t.OrderNumber,
                    TechTaskPageId = t.TechTaskPageId,
                    Description = t.Description,
                    IsCompleted = t.IsCompleted
                }).ToList()
            };

            return page;
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

        public TechTaskPageViewModel GetPageViewModel(string pageName)
        {
            var page = GetPage(pageName);
            
            var viewModel = new TechTaskPageViewModel
            {
                Id = page.Id,
                PageName = page.PageName,
                
                Tasks = page.Tasks.Select(t => new TechTaskItem
                {
                    Id = t.Id,
                    OrderNumber = t.OrderNumber,
                    TechTaskPageId = t.TechTaskPageId,
                    Description = t.Description,
                    IsCompleted = t.IsCompleted
                }).ToList(),

                Progress = CalculateProgress(page),
            };

            return viewModel;
        }
    }
}
