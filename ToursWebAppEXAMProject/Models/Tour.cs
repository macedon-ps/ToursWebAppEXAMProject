using System;
using System.Collections.Generic;

namespace ToursWebAppEXAMProject.Models
{
    public partial class Tour
    {
        public Tour()
        {
            Offer = new HashSet<Offer>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int DateTourId { get; set; }
        public int HotelId { get; set; }
        public int FoodId { get; set; }
        public int ProductId { get; set; }
        public virtual DateTour DateTour { get; set; } = null!;
        public virtual Hotel Hotel { get; set; } = null!;
        public virtual Food Food { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<Offer> Offer { get; set; }
    }
}
