using System;
using System.Collections.Generic;
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

        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название туристического продукта")]
        public string Name { get; set; } = "Название туристического продукта";

        [Display(Name = "Короткое описание туристического продукта")]
        public string ShortDescription { get; set; } = "Краткое описание туристического продукта";

        [Display(Name = "Полное описание туристического продукта")]
        public string FullDescription { get; set; } = "Полное описание туристического продукта";

        [Display(Name = "Титульная картинка")]
        public string? TitleImagePath { get; set; }

        [Display(Name = "Время создания")]
        [DataType(DataType.Time)]
        public DateTime? DateAdded { get; set; } 

        public virtual ICollection<Tour> Tours { get; set; } 
    }
}
