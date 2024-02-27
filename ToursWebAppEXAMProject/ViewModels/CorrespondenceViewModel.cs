using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.ViewModels
{
    public class CorrespondenceViewModel
    {
        [Required(ErrorMessage = "Введите Ваше имя")]
        [Display(Name = "Имя клиента")]
        [MaxLength(50, ErrorMessage = "Имя не должно быть длиннее 50 символов")]
        public string Name { get; set; } //= "Имя не указано";

        [Required(ErrorMessage = "Введите Вашу фимилию")]
        [Display(Name = "Фамилия клиента")]
        [MaxLength(50, ErrorMessage = "Фамилия не должна быть длиннее 50 символов")]
        public string Surname { get; set; } //= "Фамилия не указана";

        [Required(ErrorMessage = "Введите Ваш email")]
        [Display(Name = "email клиента")]
        [MaxLength(50, ErrorMessage = "Email не должна быть длиннее 50 символов")]
        public string Email { get; set; } //= "email@gmail.com";

        [Required(ErrorMessage = "Введите Ваш пол")]
        [Display(Name = "Пол")]
        [MaxLength(10, ErrorMessage = "Пол не должен быть длиннее 10 символов")]
        public string Gender { get; set; } //= "Пол не указан";

        [Required(ErrorMessage = "Введите Вашу дату рождения")]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDay { get; set; } = new DateTime(2000, 1, 1);
        
        [Required(ErrorMessage = "Введите вопрос к компании")]
        [Display(Name = "Вопрос к компании")]
        [StringLength(400, ErrorMessage = "Вопрос к компании не должен содержать более 400 символов")]
        public string Question { get; set; } = "Вопрос к компании";

        [Display(Name = "Время получения сообщения")]
        [DataType(DataType.Time)]
        public DateTime? QuestionDate { get; set; }

        [Display(Name = "Ответ компании")]
        [StringLength(400)]
        public string? Answer { get; set; }

        [Display(Name = "Время ответа на сообщение")]
        [DataType(DataType.Time)]
        public DateTime? AnswerDate { get; set; }

    }
}
