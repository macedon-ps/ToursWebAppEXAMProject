using ToursWebAppEXAMProject.Repositories;

namespace ToursWebAppEXAMProject.ViewModels
{
	public class TechTaskViewModel
	{
		public int Id { get; set; }

		/// <summary>
		/// Название страницы сайта (и контроллера)
		/// </summary>
		public string PageName { get; set; } = null!;

		/// <summary>
		/// Показатель выполнения ТЗ_1
		/// </summary>
		public bool? IsExecuteTechTask1 { get; set; }

        /// <summary>
        /// Показатель выполнения ТЗ_2
        /// </summary>
        public bool? IsExecuteTechTask2 { get; set; }

        /// <summary>
        /// Показатель выполнения ТЗ_3
        /// </summary>
        public bool? IsExecuteTechTask3 { get; set; }

        /// <summary>
        /// Показатель выполнения ТЗ_4
        /// </summary>
        public bool? IsExecuteTechTask4 { get; set; }

        /// <summary>
        /// Показатель выполнения ТЗ_5
        /// </summary>
        public bool? IsExecuteTechTask5 { get; set; }

        /// <summary>
        /// Показатель выполнения ТЗ_6
        /// </summary>
		public bool? IsExecuteTechTask6 { get; set; }

        /// <summary>
        /// Прогресс выполнения ТЗ
        /// </summary>
		public double? ExecuteTechTasksProgress { get; set; } = 0.0;
	}
}
