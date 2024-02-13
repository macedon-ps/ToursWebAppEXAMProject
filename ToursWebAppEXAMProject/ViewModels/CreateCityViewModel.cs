﻿using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.ViewModels
{
    public class CreateCityViewModel
    {
        /// <summary>
        /// Коллекция стран в БД
        /// </summary>
        public IEnumerable<Country> Countries { get; set; } = null!;

        /// <summary>
        /// Экземпляр города
        /// </summary>
        public City City { get; set; } = null!;

    }
}