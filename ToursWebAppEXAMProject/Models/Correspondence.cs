using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.Models
{
    public class Correspondence
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите вопрос к компании")]
        [Display(Name = "Вопрос к компании")]
        [StringLength(400, ErrorMessage = "Вопрос к компании не должен содержать более 400 символов")]
        public string Question { get; set; } = "Вопрос к компании";

        [Display(Name = "Время получения сообщения")]
        [DataType(DataType.Time)]
        public DateTime? QuestionDate { get; set; }

        [Display(Name = "Ответ компании")]
        [StringLength(400)]
        public string? Answer { get; set; } = "Ответ компании";

        [Display(Name = "Время ответа на сообщение")]
        [DataType(DataType.Time)]
        public DateTime? AnswerDate { get; set; }

        public bool IsCustomer { get; set; } = false;

        public int AskerId { get; set; }
        
        public virtual Asker? Asker { get; set; }

    }
}
