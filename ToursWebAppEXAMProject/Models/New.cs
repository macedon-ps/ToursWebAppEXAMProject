using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.Models
{
    public partial class New
    {
        public New() { DateAdded = DateTime.Now; }

        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите заголовок новости")]
        [Display(Name = "Заголовок новости")]
        [StringLength(200, ErrorMessage = "Заголовок новости не должен содержать более 200 символов")]
        public string Name { get; set; } = "Заголовок новости";

        [Required(ErrorMessage = "Введите краткое описание новости")]
        [Display(Name = "Краткое описание новости")]
        [StringLength(400, ErrorMessage = "Краткое описание новости не должно содержать более 400 символов")]
        public string ShortDescription { get; set; } = "Краткое описание новости";

        [Required(ErrorMessage = "Введите полное описание новости")]
        [Display(Name = "Полное описание новости")]
        public string FullDescription { get; set; } = "Полное описание новости";

        [Required(ErrorMessage = "Выберите титульную картинку новости")]
        [Display(Name = "Титульная картинка")]
        [StringLength(100, ErrorMessage = "Путь к титульной картинке не должен содержать более 100 символов")]
        public string? TitleImagePath { get; set; } = "Нет титульной картинки";

        [Display(Name = "Время создания")]
        [DataType(DataType.Time)]
        public DateTime? DateAdded { get; set; }
    }
}
