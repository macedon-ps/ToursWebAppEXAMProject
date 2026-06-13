using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.Models
{
    public partial class Hotel
    {
        public Hotel()
        {
            Tours = new HashSet<Tour>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название гостинницы")]
        [Display(Name = "Название гостинницы")]
        [StringLength(200, ErrorMessage = "Название гостинницы не должно содержать более 200 символов")]
        public string Name { get; set; } = null!;
                
        public int LevelHotel { get; set; }
                
        public int CityId { get; set; }

        public virtual City City { get; set; } = null!;

        public virtual ICollection<Tour> Tours { get; set; }
    }
}
