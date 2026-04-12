using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.ViewModels
{
    public class EditAboutPageViewModel
    {
        
        public AboutPageVersion AboutPageVersion { get; set; } = new AboutPageVersion();


        public List<string>? CollectionPathForPhotoGalleryImages { get; set; }


        public DateTime? DateAdded { get; set; }

    }
}
