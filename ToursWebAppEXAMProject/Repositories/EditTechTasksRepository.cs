using Microsoft.EntityFrameworkCore;
using NLog;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Repositories
{
	public class EditTechTasksRepository : IEditTechTaskInterface
	{
		double TechTasksCount { get; set; }

		double TechTasksTrueCount { get; set; }

		double TechTasksProgress { get; set; }

		private readonly TourFirmaDBContext context;

		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		public EditTechTasksRepository(TourFirmaDBContext _context)
		{
			this.context = _context;
		}

		public TechTaskViewModel GetTechTasksForPage(string pageName)			
		{
			logger.Debug("Произведено подключение к базе данных");
			Console.WriteLine("Произведено подключение к базе данных");
			logger.Trace($"Запрашиваются показатели выполнения тех. заданий для страницы \"{pageName}\"");
			Console.WriteLine($"Запрашиваются показатели выполнения тех. заданий для страницы \"{pageName}\"");

			try
			{
				var techTasksOfPage = context.TechTaskViewModels.FirstOrDefault(p => p.PageName == pageName);

				if (techTasksOfPage == null)
				{
					logger.Warn($"Выборка показатели выполнения тех. заданий для страницы \"{pageName}\" не осуществлена");
					Console.WriteLine($"Выборка показатели выполнения тех. заданий для страницы \"{pageName}\" не осуществлена");

					return new TechTaskViewModel();
				}
				else
				{
					logger.Debug("Выборка осуществлена успешно");
					Console.WriteLine("Выборка осуществлена успешно");

					return techTasksOfPage;
				}
			}
			catch (Exception ex)
			{
				logger.Error("Выборка не осуществлена");
				logger.Error($"Код ошибки: {ex.Message}");
				Console.WriteLine("Выборка не осуществлена");
				Console.WriteLine($"Код ошибки: {ex.Message}");

				return new TechTaskViewModel();
			}
		}

		public double GetProgressTechTasks(TechTaskViewModel techTasks)
		{
			logger.Trace($"Запрашиваются прогресс выполнения тех. заданий для страницы \"{techTasks.PageName}\"");
			Console.WriteLine($"Запрашиваются прогресс выполнения тех. заданий для страницы \"{techTasks.PageName}\"");

			TechTasksCount = 6;
			if(techTasks.IsExecuteTechTask1 == true) TechTasksTrueCount++;
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
			logger.Debug("Произведено подключение к базе данных");
			Console.WriteLine("Произведено подключение к базе данных");
			try
			{
				if (techTasks == null)
				{
					logger.Trace($"Модель равна null");
					Console.WriteLine($"Модель равна null");
					return;
				}
				else if (techTasks != null)
				{
					logger.Trace($"Обновление существующих показателей выполнения тех. заданий для страницы {techTasks.PageName}");
					Console.WriteLine($"Обновление существующих показателей выполнения тех. заданий для страницы {techTasks.PageName}");
					context.Entry(techTasks).State = EntityState.Modified;
					context.SaveChanges();
					return;
				}
			}
			catch (Exception ex)
			{
				logger.Error($"Обновление показателей выполнения тех. заданий для страницы {techTasks.PageName} не осуществлено");
				logger.Error($"Код ошибки: {ex.Message}");
				Console.WriteLine($"Обновление показателей выполнения тех. заданий для страницы {techTasks.PageName} не осуществлено");
				Console.WriteLine($"Код ошибки: {ex.Message}");
			}
		}
	}
}
