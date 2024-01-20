using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.ViewModels
{
    public class CreateProductViewModel
    {
        /// <summary>
        /// Коллекция стран в БД
        /// </summary>
        public IEnumerable<Country> Countries { get; set; } = null!;

        /// <summary>
        /// Коллекция городов в БД
        /// </summary>
        public IEnumerable<City> Cities { get; set; } = null!;

        /// <summary>
        /// Экземпляр турпродукта
        /// </summary>
        public Product Product { get; set; } = null!;
    }
}
