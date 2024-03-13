using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.Models
{
    public partial class City
    {
        public City()
        {
            Hotels = new HashSet<Hotel>();
            Products = new HashSet<Product>();
            DateAdded = DateTime.Now;
        }

        public int Id { get; set; }

        // во вью н.б. скрывать, скорее всего
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Введите название города")]
        [Display(Name = "Название города")]
        [StringLength(50, ErrorMessage = "Название города не должно содержать более 50 символов")]
        public string Name { get; set; } = "Название города";

        [Required(ErrorMessage = "Введите краткое описание города")]
        [Display(Name = "Краткое описание города")]
        [StringLength(400, ErrorMessage = "Краткое описание города не должно содержать более 400 символов")]
        public string ShortDescription { get; set; } = "Краткое описание города";

        [Required(ErrorMessage = "Введите краткое описание достопримечательностей")]
        [Display(Name = "Достопримечательности")]
        [StringLength(400, ErrorMessage = "Краткое описание достопримечательностей не должно содержать более 400 символов")]
        public string? LocalDescrition { get; set; } = "Достопримечательности";

        [Required(ErrorMessage = "Введите полное описание города")]
        [Display(Name = "Полное описание города")]
        public string FullDescription { get; set; } = "Полное описание города";

        [Required(ErrorMessage = "Укажите, является ли город солицей страны")]
        [Display(Name = "Является ли столицей страны ?")]
        public bool isCapital { get; set; } = false;

        [Required(ErrorMessage = "Выберите титульную картинку города")]
        [Display(Name = "Титульная картинка города")]
        [StringLength(100, ErrorMessage = "Путь к титульной картинке города не должен содержать более 100 символов")]
        public string TitleImagePath { get; set; } = "Нет титульной картинки города";

        public DateTime? DateAdded { get; set; }
        public virtual Country? Country { get; set; } 
        public virtual IEnumerable<Hotel> Hotels { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
