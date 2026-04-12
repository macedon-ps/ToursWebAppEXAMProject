namespace ToursWebAppEXAMProject.Models
{
    public class PhotoGalleryImage
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        public int AboutPageVersionId { get; set; }

        public virtual AboutPageVersion AboutPageVersion { get; set; }
    }
}
