using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.Models
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
            Products = new HashSet<Product>();
            DateAdded = DateTime.Now;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название страны")]
        [Display(Name = "Название страны")]
        [StringLength(50, ErrorMessage = "Название страны не должно содержать более 50 символов")]
        public string Name { get; set; } = "Название страны";

        [Required(ErrorMessage = "Введите краткое описание страны")]
        [Display(Name = "Краткое описание страны")]
        [StringLength(400, ErrorMessage = "Краткое описание страны не должно содержать более 400 символов")]
        public string ShortDescription { get; set; } = "Краткое описание страны";

        [Required(ErrorMessage = "Введите полное описание страны")]
        [Display(Name = "Полное описание страны")]
        public string FullDescription { get; set; } = "Полное описание страны";

        [Required(ErrorMessage = "Введите название солицы страны")]
        [Display(Name = "Название столицы страны")]
        [StringLength(50, ErrorMessage = "Название столицы страны не должно содержать более 50 символов")]
        public string Capital { get; set; } = "Столица страны";

        [Required(ErrorMessage = "Выберите титульную картинку страны")]
        [Display(Name = "Титульная картинка страны")]
        [StringLength(100, ErrorMessage = "Путь к титульной картинке страны не должен содержать более 100 символов")]
        public string TitleImagePath { get; set; } = "Нет титульной картинки страны";

        [Required(ErrorMessage = "Выберите ссылку на карту страны в GoogleMaps")]
        [Display(Name = "Ссылка на карту страны в GoogleMaps")]
        [StringLength(200, ErrorMessage = "Ссылка на карту страны в GoogleMaps не должна содержать более 200 символов")]
        public string CountryMapPath { get; set; } = "Нет ссылки на карту страны в GoogleMaps";

        public DateTime? DateAdded { get; set; }
        public virtual IEnumerable<City> Cities { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
