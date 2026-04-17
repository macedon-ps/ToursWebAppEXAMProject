using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Utils
{
    public class AboutMigrationUtils
    {
        private readonly IBaseInterface<EditAboutPageViewModel> _oldRepo;
        private readonly IBaseInterface<AboutPageVersion> _newRepo;

        public AboutMigrationUtils(
            IBaseInterface<EditAboutPageViewModel> oldRepo,
            IBaseInterface<AboutPageVersion> newRepo)
        {
            _oldRepo = oldRepo;
            _newRepo = newRepo;
        }

        public void MigrateOldData()
        {
            var oldItems = _oldRepo.GetAllItems();

            foreach (var old in oldItems)
            {
                var newVersion = new AboutPageVersion
                {
                    Keyword = old.Keyword,
                    IsActual = old.IsActual,

                    MainTitle = old.MainTitle,
                    AboutTitle = old.AboutTitle,
                    DetailsTitle = old.DetailsTitle,
                    OperationModeTitle = old.OperationModeTitle,
                    PhotoGalleryTitle = old.PhotoGalleryTitle,
                    FeedbackTitle = old.FeedbackTitle,

                    MainShortDescription = old.MainShortDescription,
                    AboutShortDescription = old.AboutShortDescription,
                    DetailsShortDescription = old.DetailsShortDescription,
                    OperationModeShortDescription = old.OperationModeShortDescription,
                    PhotoGalleryShortDescription = old.PhotoGalleryShortDescription,
                    FeedbackShortDescription = old.FeedbackShortDescription,

                    MainFullDescription = old.MainFullDescription,
                    AboutFullDescription = old.AboutFullDescription,
                    DetailsFullDescription = old.DetailsFullDescription,
                    OperationModeFullDescription = old.OperationModeFullDescription,
                    PhotoGalleryFullDescription = old.PhotoGalleryFullDescription,
                    FeedbackFullDescription = old.FeedbackFullDescription,

                    MainImagePath = old.MainImagePath,
                    AboutImagePath = old.AboutImagePath,
                    DetailsImagePath = old.DetailsImagePath,
                    OperationModeImagePath = old.OperationModeImagePath,
                    FeedbackImagePath = old.FeedbackImagePath,

                    DateAdded = old.DateAdded
                };

                _newRepo.SaveItem(newVersion, 0);
            }
        }
    }
}
