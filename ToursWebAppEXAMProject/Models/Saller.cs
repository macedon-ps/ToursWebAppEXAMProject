using System;
using System.Collections.Generic;

namespace ToursWebAppEXAMProject.Models
{
    public partial class Saller
    {
        public Saller()
        {
            Oferta = new HashSet<Offer>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Position { get; set; } = null!;

        public virtual ICollection<Offer> Oferta { get; set; }
    }
}
