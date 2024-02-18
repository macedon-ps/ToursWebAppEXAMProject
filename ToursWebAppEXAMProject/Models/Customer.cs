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
        public string Name { get; set; } = "Имя не указано";

        [Required(ErrorMessage = "Введите фимилию клиента")]
        [Display(Name = "Фамилия клиента")]
        public string Surname { get; set; } = "Фамилия не указана";

        public string Gender { get; set; } = "Пол не указан";

        [Required(ErrorMessage = "Введите дату рождения")]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDay { get; set; }

        public virtual ICollection<Offer> Offer { get; set; }
    }
}
