using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Offer = new HashSet<Offer>();
        }

        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя клиента")]
        [Display(Name = "Имя клиента")]
        [StringLength(100)]
        public string Name { get; set; } = "Имя не указано";

        [Required(ErrorMessage = "Введите фимилию клиента")]
        [Display(Name = "Фамилия клиента")]
        [StringLength(100)]
        public string Surname { get; set; } = "Фамилия не указана";

        [Required(ErrorMessage = "Введите Ваш email")]
        [Display(Name = "email клиента")]
        [MaxLength(100, ErrorMessage = "Email не должна быть длиннее 100 символов")]
        public string Email { get; set; } = "email@gmail.com";

        [Display(Name = "Пол")]
        [MaxLength(20, ErrorMessage = "Пол не должен быть длиннее 20 символов")]
        public string Gender { get; set; } = "Пол не указан";

        [Required(ErrorMessage = "Введите дату рождения")]
        [Display(Name = "Дата рождения (формат: ГГГГ-ММ-ДД)")]
        public DateTime BirthDay { get; set; }

        public virtual ICollection<Offer> Offer { get; set; }
    }
}
