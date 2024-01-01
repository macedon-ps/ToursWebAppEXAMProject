namespace ToursWebAppEXAMProject.Models
{
    public partial class Saller
    {
        public Saller()
        {
            Offer = new HashSet<Offer>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Position { get; set; } = null!;

        public virtual ICollection<Offer> Offer { get; set; }
    }
}
