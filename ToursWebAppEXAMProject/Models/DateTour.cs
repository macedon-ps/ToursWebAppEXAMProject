using System;
using System.Collections.Generic;

namespace ToursWebAppEXAMProject.Models
{
	public partial class DateTour
	{
		public DateTour()
		{
			Tours = new HashSet<Tour>();
		}

		public int Id { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }
		public int NumberOfDays { get; set; }
		public int NumberOfNights { get; set; }

		public virtual ICollection<Tour> Tours { get; set; }
	}
}
