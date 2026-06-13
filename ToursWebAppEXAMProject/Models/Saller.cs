using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.Models
{
    public partial class Saller
    {
        public Saller()
        {
            Offer = new HashSet<Offer>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя продавца")]
        [Display(Name = "Имя продавца")]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Введите фимилию продавца")]
        [Display(Name = "Фамилия продавца")]
        [StringLength(100)]
        public string Surname { get; set; } = null!;

        [Required(ErrorMessage = "Введите должность")]
        [Display(Name = "Должность продавца")]
        [MaxLength(200, ErrorMessage = "Должность продавца не должна быть длиннее 200 символов")]
        public string Position { get; set; } = null!;

        public virtual ICollection<Offer> Offer { get; set; }
    }
}
