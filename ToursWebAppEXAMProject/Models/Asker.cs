using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.Models
{
    public class Asker
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите Ваше имя")]
        [Display(Name = "Имя клиента")]
        [MaxLength(50, ErrorMessage = "Имя не должно быть длиннее 50 символов")]
        public string Name { get; set; } = "Имя не указано";

        [Required(ErrorMessage = "Введите Вашу фимилию")]
        [Display(Name = "Фамилия клиента")]
        [MaxLength(50, ErrorMessage = "Фамилия не должна быть длиннее 50 символов")]
        public string Surname { get; set; } = "Фамилия не указана";

        [Required(ErrorMessage = "Введите Ваш email")]
        [Display(Name = "email клиента")]
        [MaxLength(50, ErrorMessage = "Email не должна быть длиннее 50 символов")]
        public string Email { get; set; } = "email@gmail.com";

        [Display(Name = "Пол")]
        [MaxLength(10, ErrorMessage = "Пол не должен быть длиннее 10 символов")]
        public string Gender { get; set; } = "Пол не указан";

        [Required(ErrorMessage = "Введите Вашу дату рождения")]
        [Display(Name = "Дата рождения (формат: ГГГГ-ММ-ДД)")]
        public DateTime BirthDay { get; set; }

        public int? CustomerId { get; set; }

        public Customer? Customer { get; set; }
    }
}
