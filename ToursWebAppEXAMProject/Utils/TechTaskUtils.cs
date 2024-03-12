using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Utils
{
    public class TechTaskUtils
    {
        private readonly IEditTechTaskInterface _AllTasks;

        public TechTaskUtils(IEditTechTaskInterface allTasks)
        {
            _AllTasks = allTasks;
        }
        public void SetTechTaskProgressAndSave(TechTaskViewModel model)
        {
            double TechTasksCount = 6;
            double TechTasksTrueCount = 0;
            if (model.IsExecuteTechTask1 == true) TechTasksTrueCount++;
            if (model.IsExecuteTechTask2 == true) TechTasksTrueCount++;
            if (model.IsExecuteTechTask3 == true) TechTasksTrueCount++;
            if (model.IsExecuteTechTask4 == true) TechTasksTrueCount++;
            if (model.IsExecuteTechTask5 == true) TechTasksTrueCount++;
            if (model.IsExecuteTechTask6 == true) TechTasksTrueCount++;

            double ExecuteTechTasksProgress = Math.Round((TechTasksTrueCount / TechTasksCount) * 100);
            model.ExecuteTechTasksProgress = ExecuteTechTasksProgress;

            _AllTasks.SaveProgressTechTasks(model);
        }
        public TechTaskViewModel GetTechTaskForPage(string pageName)
        {
            var model = _AllTasks.GetTechTasksForPage(pageName);
            return model;
        }

    }
}
