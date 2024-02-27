namespace ToursWebAppEXAMProject.Models
{
    public partial class Offer
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SallerId { get; set; }
        public int TourId { get; set; }

        public virtual Customer? Customer { get; set; } = null!;
        public virtual Saller? Saller { get; set; } = null!;
        public virtual Tour? Tour { get; set; } = null!;
    }
}
