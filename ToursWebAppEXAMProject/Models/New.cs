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

        [Required]
        [Display(Name = "Заголовок новости")]
        public string Name { get; set; } = "Заголовок новости";

        [Display(Name = "Краткое описание новости")]
        public string ShortDescription { get; set; } = "Краткое описание новости";

        [Display(Name = "Полное описание новости")]
        public string FullDescription { get; set; } = "Полное описание новости";

        [Display(Name = "Титульная картинка")]
        public string? TitleImagePath { get; set; }

        [Display(Name = "Время создания")]
        [DataType(DataType.Time)]
        public DateTime? DateAdded { get; set; }
    }
}
