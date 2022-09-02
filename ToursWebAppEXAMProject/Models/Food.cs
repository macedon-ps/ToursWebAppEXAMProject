using System;
using System.Collections.Generic;

namespace ToursWebAppEXAMProject.Models
{
    public partial class Food
    {
        public Food()
        {
            Tours = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public string ModeOfEating { get; set; } = null!;

        public virtual ICollection<Tour> Tours { get; set; }
    }
}
