using System.ComponentModel.DataAnnotations;
using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.ViewModels
{
    public class CreateCityViewModel
    {
        /// <summary>
        /// Коллекция стран в БД
        /// </summary>
        public IEnumerable<Country> Countries { get; set; } = null!;

        [Required(ErrorMessage ="Введите необходимые данные о городе")]
        /// <summary>
        /// Экземпляр города
        /// </summary>
        public City City { get; set; } = null!;

    }
}
