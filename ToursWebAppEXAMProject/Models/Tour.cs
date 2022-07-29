using System;
using System.Collections.Generic;

namespace ToursWebAppEXAMProject.Models
{
	public partial class Tour
	{
		public Tour()
		{
			Oferta = new HashSet<Ofertum>();
		}

		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public int ProductId { get; set; }
		public int DateTourId { get; set; }
		public int LocationId { get; set; }
		public int FoodId { get; set; }

		public virtual DateTour DateTour { get; set; } = null!;
		public virtual Food Food { get; set; } = null!;
		public virtual Location Location { get; set; } = null!;
		public virtual Product Product { get; set; } = null!;
		public virtual ICollection<Ofertum> Oferta { get; set; }
	}
}
