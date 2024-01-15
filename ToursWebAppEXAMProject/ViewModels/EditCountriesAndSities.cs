namespace ToursWebAppEXAMProject.ViewModels
{
    public class EditCountriesAndSities
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
        public IEnumerable<string> Countries { get; set; } = null!;

        /// <summary>
        /// Идентификатор города
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Название города
        /// </summary>
        public string CityName { get; set; } = null!;

        /// <summary>
        /// Коллекция городов в БД
        /// </summary>
        public IEnumerable<string> Sities { get; set; } = null!;
    }
}
