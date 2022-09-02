using System;
using System.Collections.Generic;

namespace ToursWebAppEXAMProject.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Oferta = new HashSet<Ofertum>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public int Age { get; set; }

        public virtual ICollection<Ofertum> Oferta { get; set; }
    }
}
