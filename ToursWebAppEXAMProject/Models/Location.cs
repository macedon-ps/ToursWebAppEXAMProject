using System;
using System.Collections.Generic;

namespace ToursWebAppEXAMProject.Models
{
    public partial class Location
    {
        public Location()
        {
            Tours = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int HotelId { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual Country Country { get; set; } = null!;
        public virtual Hotel Hotel { get; set; } = null!;
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
