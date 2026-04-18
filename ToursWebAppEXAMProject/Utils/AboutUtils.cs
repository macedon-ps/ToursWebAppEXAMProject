using Microsoft.CodeAnalysis;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.ViewModels;

namespace ToursWebAppEXAMProject.Utils
{
    public class AboutUtils
    {
        private readonly IBaseInterface<EditAboutPageViewModel> _AboutPage;
        private readonly IBaseInterface<AboutPageVersion> _AboutVersion;
        private readonly FeedbackUtils _Feedback;
        private readonly FileUtils _FileUtils;

        public AboutUtils(IBaseInterface<EditAboutPageViewModel> AboutPage, IBaseInterface<AboutPageVersion> AboutVersion, FeedbackUtils Feedback, FileUtils FileUtils)
        {
            _AboutPage = AboutPage;
            _AboutVersion = AboutVersion;
            _Feedback = Feedback;
            _FileUtils = FileUtils;
        }

        /// <summary>
        /// Метод получения вью-модели EditAboutPageViewModel
        /// </summary>
        /// <returns></returns>
        public EditAboutPageViewModel GetModel()
        {
            var version = _AboutVersion
                .GetAllItems()
                .FirstOrDefault(v => v.IsActual);

            if (version == null)
                return new EditAboutPageViewModel();

            var viewModel = new EditAboutPageViewModel
            {
                // TODO: Временно Id, IsActual берется из вью-модели
                AboutPageVersion = version,
                DateAdded = version.DateAdded,
                IsActual = version.IsActual
            };

            return viewModel;
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


        public async Task<EditAboutPageViewModel> SetEditAboutViewModelAndSaveAsync(EditAboutPageViewModel viewModel, IFormFile? MainImagePath, IFormFile? AboutImagePath, IFormFile? DetailsImagePath, IFormFile? OperationModeImagePath, IFormFile? PhotoGalleryImagePath, IFormFile? FeedbackImagePath)
        {
            // Main
            if (MainImagePath != null)
            {
                var folder = "/images/AboutPage/Main/";
                await _FileUtils.SaveImageToFolder(folder, MainImagePath);
                viewModel.MainImagePath = $"{folder}{MainImagePath.FileName}";
            }

            // About
            if (AboutImagePath != null)
            {
                var folder = "/images/AboutPage/About/";
                await _FileUtils.SaveImageToFolder(folder, AboutImagePath);
                viewModel.AboutImagePath = $"{folder}{AboutImagePath.FileName}";
            }

            // Details
            if (DetailsImagePath != null)
            {
                var folder = "/images/AboutPage/Details/";
                await _FileUtils.SaveImageToFolder(folder, DetailsImagePath);
                viewModel.DetailsImagePath = $"{folder}{DetailsImagePath.FileName}";
            }
            // OperationMode
            if (OperationModeImagePath != null)
            {
                var folder = "/images/AboutPage/OperationMode/";
                await _FileUtils.SaveImageToFolder(folder, OperationModeImagePath);
                viewModel.OperationModeImagePath = $"{folder}{OperationModeImagePath.FileName}";
            }
            // PhotoGallery
            if (PhotoGalleryImagePath != null)
            {
                var folder = "/images/AboutPage/PhotoGallery/";
                await _FileUtils.SaveImageToFolder(folder, PhotoGalleryImagePath);
                viewModel.PhotoGalleryImagePath = $"{folder}{PhotoGalleryImagePath.FileName}";
            }
            // Feedback
            if (FeedbackImagePath != null)
            {
                var folder = "/images/AboutPage/Feedback/";
                await _FileUtils.SaveImageToFolder(folder, FeedbackImagePath);
                viewModel.FeedbackImagePath = $"{folder}{FeedbackImagePath.FileName}";
            }

            viewModel.DateAdded = DateTime.Now;

            _AboutPage.SaveItem(viewModel, viewModel.Id);

            return viewModel;
        }
    }
}
