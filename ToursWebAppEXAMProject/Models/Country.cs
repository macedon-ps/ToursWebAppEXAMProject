using System;
using System.Collections.Generic;

namespace ToursWebAppEXAMProject.Models
{
	public partial class Country
	{
		public Country()
		{
			Cities = new HashSet<City>();
			Locations = new HashSet<Location>();
		}

		public int Id { get; set; }
		public string Name { get; set; } = null!;

		public virtual ICollection<City> Cities { get; set; }
		public virtual ICollection<Location> Locations { get; set; }
	}
}
