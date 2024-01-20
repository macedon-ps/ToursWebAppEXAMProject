using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.Models
{
    public partial class Product
    {
        public Product()
        {
            Tours = new HashSet<Tour>();
            DateAdded = DateTime.Now;
        }

        public int Id { get; set; }

        // устанавливается "под капотом", во вью - скрыто
        public int CountryId { get; set; }

        // устанавливается "под капотом", во вью - скрыто
        public int CityId { get; set; }

        [Required(ErrorMessage = "Введите название туристического продукта")]
        [Display(Name = "Название туристического продукта")]
        [StringLength(200, ErrorMessage = "Название туристического продукта не должно содержать более 200 символов")]
        public string Name { get; set; } = "Название туристического продукта";

        [Required(ErrorMessage = "Введите краткое описание туристического продукта")]
        [Display(Name = "Краткое описание туристического продукта")]
        [StringLength(400, ErrorMessage = "Краткое описание туристического продукта не должно содержать более 400 символов")]
        public string ShortDescription { get; set; } = "Краткое описание туристического продукта";

        [Required(ErrorMessage = "Введите полное описание туристического продукта")]
        [Display(Name = "Полное описание туристического продукта")]
        public string FullDescription { get; set; } = "Полное описание туристического продукта";

        [Display(Name = "Титульная картинка")]
        [StringLength(100, ErrorMessage = "Путь к титульной картинке не должен содержать более 100 символов")]
        public string? TitleImagePath { get; set; } = "Нет титульной картинки";

        [Display(Name = "Время создания")]
        [DataType(DataType.Time)]
        public DateTime? DateAdded { get; set; }

        public virtual Country? Country { get; set; } 

        public virtual City? City { get; set; } 

        public virtual ICollection<Tour> Tours { get; set; }
    }
}
