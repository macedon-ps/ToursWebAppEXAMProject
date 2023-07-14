using Microsoft.EntityFrameworkCore;
using ToursWebAppEXAMProject.EnumsDictionaries;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.ViewModels;
using static ToursWebAppEXAMProject.LogsMode.LogsMode;

namespace ToursWebAppEXAMProject.Repositories
{
	public class EditTechTasksRepository : IEditTechTaskInterface
	{
		double TechTasksCount { get; set; }

		double TechTasksTrueCount { get; set; }

		double TechTasksProgress { get; set; }

		private readonly TourFirmaDBContext context;

		public EditTechTasksRepository(TourFirmaDBContext _context)
		{
			this.context = _context;
		}

		public TechTaskViewModel GetTechTasksForPage(string pageName)			
		{
			WriteLogs($"Произведено подключение к БД. Запрашиваются показатели выполнения ТЗ для страницы \"{pageName}\". ", NLogsModeEnum.Debug);
			
			try
			{
				var techTasksOfPage = context.TechTaskViewModels.FirstOrDefault(p => p.PageName == pageName);

				if (techTasksOfPage == null)
				{
                    WriteLogs($"Выборка показателей выполнения ТЗ для страницы \"{pageName}\" не осуществлена.\n", NLogsModeEnum.Warn);
                   
					return new TechTaskViewModel();
				}
				else
				{
                    WriteLogs("Выборка осуществлена успешно. \n", NLogsModeEnum.Debug);
                    
					return techTasksOfPage;
				}
			}
			catch (Exception ex)
			{
                WriteLogs($"Выборка показателей выполнения ТЗ для страницы \"{pageName}\" не осуществлена.\nКод ошибки: {ex.Message}\n", NLogsModeEnum.Error);
                
				return new TechTaskViewModel();
			}
		}

		public double GetProgressTechTasks(TechTaskViewModel techTasks)
		{
            WriteLogs($"Запрашиваются прогресс выполнения ТЗ для страницы \"{techTasks.PageName}\". ", NLogsModeEnum.Debug);
            
			TechTasksCount = 6;
			if (techTasks.IsExecuteTechTask1 == true) TechTasksTrueCount++;
			if (techTasks.IsExecuteTechTask2 == true) TechTasksTrueCount++;
			if (techTasks.IsExecuteTechTask1 == true) TechTasksTrueCount++;
			if (techTasks.IsExecuteTechTask2 == true) TechTasksTrueCount++;
			if (techTasks.IsExecuteTechTask1 == true) TechTasksTrueCount++;
			if (techTasks.IsExecuteTechTask2 == true) TechTasksTrueCount++;

			TechTasksProgress = TechTasksTrueCount / TechTasksCount * 100;

			return TechTasksProgress;
		}

		public void SaveProgressTechTasks(TechTaskViewModel techTasks)
		{
            WriteLogs("Произведено подключение к БД. ", NLogsModeEnum.Debug);
            
			try
			{
				if (techTasks == null)
				{
                    WriteLogs("Модель не существует.\n", NLogsModeEnum.Warn);
                    
					return;
				}
				else if (techTasks != null)
				{
                    WriteLogs($"Обновление показателей выполнения ТЗ для страницы {techTasks.PageName}", NLogsModeEnum.Debug);
                    
					context.Entry(techTasks).State = EntityState.Modified;
					context.SaveChanges();
					return;
				}
			}
			catch (Exception ex)
			{
                WriteLogs($"Обновление показателей выполнения ТЗ для страницы {techTasks.PageName} не осуществлено.\nКод ошибки: {ex.Message}", NLogsModeEnum.Error);
            }
		}
	}
}
