﻿using System;
using System.Collections.Generic;

namespace ToursWebAppEXAMProject.Models
{
    public partial class City
    {
        public City()
        {
            Hotels = new HashSet<Hotel>();
            Locations = new HashSet<Location>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CountryId { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<Hotel> Hotels { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}
