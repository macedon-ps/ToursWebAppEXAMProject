namespace ToursWebAppEXAMProject.Models
{
    public class TechTaskItem
    {
        public int Id { get; set; }

        public int TechTaskPageId { get; set; }

        public TechTaskPage Page { get; set; }

        /// <summary>
        /// Текст задания
        /// </summary>
        public string Description { get; set; } = null!;

        /// <summary>
        /// Выполнено или нет
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Порядок вывода
        /// </summary>
        public int OrderNumber { get; set; }
    }
}
