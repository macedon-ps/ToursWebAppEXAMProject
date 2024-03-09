using Microsoft.CodeAnalysis;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.ViewModels;

namespace TourWebAppEXAMProject.Utils
{
    public class AboutUtils
    {
        private readonly IBaseInterface<EditAboutPageViewModel> _AboutPage;
        private readonly FeedbackUtils _Feedback;
        private readonly FileUtils _FileUtils;

        public AboutUtils(IBaseInterface<EditAboutPageViewModel> AboutPage, FeedbackUtils Feedback, FileUtils FileUtils)
        {
            _AboutPage = AboutPage;
            _Feedback = Feedback;
            _FileUtils = FileUtils;
        }

        /// <summary>
        /// Метод получения вью-модели EditAboutPageViewModel
        /// </summary>
        /// <returns></returns>
        public EditAboutPageViewModel GetModel()
        {
            // выводим всегда актуальную на данный момент версию страницы About
            var isActualVersion = _AboutPage.GetAllItems().FirstOrDefault(v => v.IsActual == true);

            return isActualVersion ?? new EditAboutPageViewModel();
        }

        public EditAboutPageViewModel GetModel(int id)
        {
            var editViewModel = _AboutPage.GetItemById(id);

            return editViewModel;
        }

        public IEnumerable<EditAboutPageViewModel> GetAllModel()
        {
            var allModels = _AboutPage.GetAllItems();
            
            return allModels;
        }

        public EditAboutPageViewModel CreateModel()
        {
            var newViewModel = new EditAboutPageViewModel();
            newViewModel.IsActual = true;

            return newViewModel;
        }

        public void DeleteModel(int id)
        {
            var aboutPage = _AboutPage.GetItemById(id);
            _AboutPage.DeleteItem(aboutPage, id);
        }

        public EditAboutPageViewModel SetEditAboutViewModelAndSave(EditAboutPageViewModel viewModel, IFormCollection formValues, IFormFile? changeMainImagePath, IFormFile? changeAboutImagePath, IFormFile? changeDetailsImagePath, IFormFile? changeOperationModeImagePath, IFormFile? changePhotoGalleryImagePath, IFormFile? changeFeedbackImagePath)
        {
            // Main
            if (changeMainImagePath != null)
            {
                var folder = "/images/AboutPage/Main/";
                _FileUtils.SaveImageToFolder(folder, changeMainImagePath);
                viewModel.MainImagePath = $"{folder}{changeMainImagePath.FileName}";
            }

            // About
            if (changeAboutImagePath != null)
            {
                var folder = "/images/AboutPage/About/";
                _FileUtils.SaveImageToFolder(folder, changeAboutImagePath);
                viewModel.AboutImagePath = $"{folder}{changeAboutImagePath.FileName}";
            }

            // Details
            if (changeDetailsImagePath != null)
            {
                var folder = "/images/AboutPage/Details/";
                _FileUtils.SaveImageToFolder(folder, changeDetailsImagePath);
                viewModel.DetailsImagePath = $"{folder}{changeDetailsImagePath.FileName}";
            }
            // OperationMode
            if (changeOperationModeImagePath != null)
            {
                var folder = "/images/AboutPage/OperationMode/";
                _FileUtils.SaveImageToFolder(folder, changeOperationModeImagePath);
                viewModel.OperationModeImagePath = $"{folder}{changeOperationModeImagePath.FileName}";
            }
            // PhotoGallery
            if (changePhotoGalleryImagePath != null)
            {
                var folder = "/images/AboutPage/PhotoGallery/";
                _FileUtils.SaveImageToFolder(folder, changePhotoGalleryImagePath);
                viewModel.PhotoGalleryImagePath = $"{folder}{changePhotoGalleryImagePath.FileName}";
            }
            // Feedback
            if (changeFeedbackImagePath != null)
            {
                var folder = "/images/AboutPage/Feedback/";
                _FileUtils.SaveImageToFolder(folder, changeFeedbackImagePath);
                viewModel.FeedbackImagePath = $"{folder}{changeFeedbackImagePath.FileName}";
            }

            viewModel.MainFullDescription = formValues["fullInfoMain"];
            viewModel.AboutFullDescription = formValues["fullInfoAbout"];
            viewModel.DetailsFullDescription = formValues["fullInfoDetails"];
            viewModel.OperationModeFullDescription = formValues["fullInfoOperationMode"];
            viewModel.PhotoGalleryFullDescription = formValues["fullInfoPhotoGallery"];
            viewModel.FeedbackFullDescription = formValues["fullInfoFeedback"];
            viewModel.DateAdded = DateTime.Now;

            _AboutPage.SaveItem(viewModel, viewModel.Id);

            return viewModel;
        }

        public EditAboutPageViewModel SetEditAboutViewByFormValues(EditAboutPageViewModel viewModel, IFormCollection formValues)
        {
            viewModel.MainFullDescription = formValues["fullInfoMain"];
            viewModel.AboutFullDescription = formValues["fullInfoAbout"];
            viewModel.DetailsFullDescription = formValues["fullInfoDetails"];
            viewModel.OperationModeFullDescription = formValues["fullInfoOperationMode"];
            viewModel.PhotoGalleryFullDescription = formValues["fullInfoPhotoGallery"];
            viewModel.FeedbackFullDescription = formValues["fullInfoFeedback"];

            return viewModel;
        }
    }
}
