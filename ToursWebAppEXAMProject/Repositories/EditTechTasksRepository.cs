using Microsoft.EntityFrameworkCore;
using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.DBContext;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.ViewModels;
using NLog;
using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Repositories
{
    public class EditTechTasksRepository : IEditTechTaskInterface
	{
		/// <summary>
		/// Количество показателей технического задания
		/// </summary>
		double TechTasksCount { get; set; }


        /// <summary>
        /// Количество выполненных показателей технического задания
        /// </summary>
        double TechTasksTrueCount { get; set; }


		/// <summary>
		/// Прогресс выполнения технического задания, в %
		/// </summary>
		double TechTasksProgress { get; set; }


		/// <summary>
		/// Контекс БД
		/// </summary>
		private readonly TourFirmaDBContext _context;


        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();


        public EditTechTasksRepository(TourFirmaDBContext context)
		{
			_context = context;
		}


		/// <summary>
		/// Метод вывода показателей технического задания и прогресса его выполнения
		/// </summary>
		/// <param name="pageName">Название страницы сайта (и контроллера)</param>
		/// <returns></returns>
		public TechTaskViewModel GetTechTasksForPage(string pageName)			
		{
			_logger.Debug($"Произведено подключение к БД. Запрашиваются показатели выполнения ТЗ для страницы \"{pageName}\". ");
			
			try
			{
				var techTasksOfPage = _context.TechTaskViewModels.FirstOrDefault(p => p.PageName == pageName);

				if (techTasksOfPage == null)
				{
                    _logger.Warn($"Выборка показателей выполнения ТЗ для страницы \"{pageName}\" не осуществлена.\n");
                   
					return new TechTaskViewModel();
				}
				else
				{
                    _logger.Debug("Выборка осуществлена успешно. \n");
                    
					return techTasksOfPage;
				}
			}
			catch (Exception ex)
			{
                _logger.Error($"Выборка показателей выполнения ТЗ для страницы \"{pageName}\" не осуществлена.\nКод ошибки: {ex.Message}\n");
                
				return new TechTaskViewModel();
			}
		}


		/// <summary>
		/// Метод расчета прогресса выполнения технического задания
		/// </summary>
		/// <param name="techTasks">Данные из БД о состоянии выполнения ТЗ</param>
		/// <returns></returns>
		public double GetProgressTechTasks(TechTaskViewModel techTasks)
		{
            _logger.Debug($"Запрашиваются прогресс выполнения ТЗ для страницы \"{techTasks.PageName}\". ");
            
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
            _logger.Debug("Произведено подключение к БД. ");
            
			try
			{
				if (techTasks == null)
				{
                    _logger.Warn("Модель не существует.\n");
                    
					return;
				}
				else if (techTasks != null)
				{
                    _logger.Debug($"Обновление показателей выполнения ТЗ для страницы {techTasks.PageName}", NLogsModeEnum.Debug);
                    
					_context.Entry(techTasks).State = EntityState.Modified;
					_context.SaveChanges();
					return;
				}
			}
			catch (Exception ex)
			{
                _logger.Error($"Обновление показателей выполнения ТЗ для страницы {techTasks.PageName} не осуществлено.\nКод ошибки: {ex.Message}");
            }
		}


        public TechTaskPage GetPageWithTasks(string pageName)
        {
            var page = _context.TechTaskPages
				.Include(p => p.Tasks)
				.FirstOrDefault(p => p.PageName == pageName);

            if (page == null) throw new Exception($"Страница ТЗ '{pageName}' не найдена.");

            // сортировка заданий
            page.Tasks = page.Tasks
                .OrderBy(t => t.OrderNumber)
                .ToList();

            return page;
        }


        public void Save(TechTaskPage techTasks)
        {
            // Получаем страницу из БД
            var pageFromDb = _context.TechTaskPages
                .Include(p => p.Tasks)
                .FirstOrDefault(p => p.Id == techTasks.Id);

            if (pageFromDb == null) throw new Exception($"Страница ТЗ с Id={techTasks.Id} не найдена.");

            // Обновляем состояние заданий
            foreach (var task in techTasks.Tasks)
            {
                var taskFromDb = pageFromDb.Tasks
									.FirstOrDefault(t => t.Id == task.Id);

                if (taskFromDb != null)
                {
                    taskFromDb.IsCompleted = task.IsCompleted;
                }
            }

            _context.SaveChanges();
        }
    }
}
