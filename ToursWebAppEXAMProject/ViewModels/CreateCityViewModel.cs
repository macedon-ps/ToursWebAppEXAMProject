using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.ViewModels
{
    public class CreateCityViewModel
    {
        /// <summary>
        /// Идентификатор страны
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Название страны
        /// </summary>
        public string CountryName { get; set; } = null!;

        /// <summary>
        /// Коллекция стран в БД
        /// </summary>
        public IEnumerable<Country> Countries { get; set; } = null!;

        /// <summary>
        /// Экземпляр города
        /// </summary>
        public City City { get; set; }

    }
}
