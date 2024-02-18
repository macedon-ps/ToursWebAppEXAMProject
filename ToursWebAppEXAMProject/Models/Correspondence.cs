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

        [Display(Name = "Ответ компании")]
        [StringLength(400)]
        public string? Answer { get; set; } = "Ответ компании";

        public bool IsExCustomerOfCompany { get; set; } = false;

        public int? CustomerId { get; set; }

        public int? OfferId { get; set; }

        [Display(Name = "Время получения сообщения")]
        [DataType(DataType.Time)]
        public DateTime MessageDate { get; set; } = DateTime.Now;

        public Customer? Customer { get; set; }

        public Offer? Offer { get; set; }
    }
}
