using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.Models
{
    public class TechTaskItem
    {
        public int Id { get; set; }


        [Display(Name = "Id страницы проекта")]
        public int TechTaskPageId { get; set; }


        /// <summary>
        /// Текст задания
        /// </summary>
        [Required(ErrorMessage = "Введите полное описание задания")]
        [Display(Name = "Полное описание задания")]
        public string Description { get; set; } = null!;


        /// <summary>
        /// Выполнено или нет
        /// </summary>
        [Display(Name = "Выполнено или нет")]
        public bool IsCompleted { get; set; }


        /// <summary>
        /// Порядок вывода
        /// </summary>
        [Display(Name = "Порядок вывода")]
        public int OrderNumber { get; set; }


        public TechTaskPage? TechTaskPage { get; set; }
    }
}
