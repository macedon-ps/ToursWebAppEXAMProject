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
