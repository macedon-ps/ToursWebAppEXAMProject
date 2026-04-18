using Microsoft.CodeAnalysis;
using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Services.ImageStorage;

namespace ToursWebAppEXAMProject.Utils
{
    public class AboutUtils
    {
        private readonly IBaseInterface<AboutPageVersion> _AboutPageVersion;
        private readonly IBaseInterface<PhotoGalleryImage> _PhotoGalleryImages;
        private readonly FileUtils _FileUtils;
        private readonly ImageStorageService _ImageStorageService;

        public AboutUtils(IBaseInterface<AboutPageVersion> AboutPageVersion, IBaseInterface<PhotoGalleryImage> PhotoGalleryImages, FileUtils FileUtils, ImageStorageService ImageStorageService)
        {
            _AboutPageVersion = AboutPageVersion;
            _PhotoGalleryImages = PhotoGalleryImages;
            _FileUtils = FileUtils;
            _ImageStorageService = ImageStorageService;
        }

        /// <summary>
        /// Метод получения модели AboutPageVersion
        /// </summary>
        /// <returns></returns>
        public AboutPageVersion GetModel()
        {
            var model = _AboutPageVersion
                .GetAllItems()
                .FirstOrDefault(v => v.IsActual);

            if (model == null)
                return new AboutPageVersion();

            var photoGalleryImages = _PhotoGalleryImages
                .GetAllItems()
                .Where(img => img.AboutPageVersionId == model.Id)
                .ToList();

            model.CollectionImages = photoGalleryImages;
           
            return model;
        }


        public AboutPageVersion GetModel(int id)
        {
            var editModel = _AboutPageVersion.GetItemById(id);

            var photoGalleryImages = _PhotoGalleryImages
                .GetAllItems()
                .Where(img => img.AboutPageVersionId == id)
                .ToList();

            editModel.CollectionImages = photoGalleryImages;

            return editModel;
        }


        public IEnumerable<AboutPageVersion> GetAllModels()
        {
            var allModels = _AboutPageVersion.GetAllItems();

            foreach (var model in allModels)
            {
                var photoGalleryImages = _PhotoGalleryImages
                    .GetAllItems()
                    .Where(img => img.AboutPageVersionId == model.Id)
                    .ToList();

                model.CollectionImages = photoGalleryImages;
            }

            return allModels;
        }


        public AboutPageVersion CreateModel()
        {
            var newModel = new AboutPageVersion();
            newModel.IsActual = true;

            return newModel;
        }


        public void DeleteModel(int id)
        {
            var aboutPage = _AboutPageVersion.GetItemById(id);

            // если есть коллекция изображений, то удаляем сперва удаляем их
            if (aboutPage.CollectionImages != null && aboutPage.CollectionImages.Any())
            {
                foreach (var image in aboutPage.CollectionImages)
                {
                    _PhotoGalleryImages.DeleteItem(image, image.Id);
                }
            }
            // затем удаляем саму версию страницы "О нас"
            _AboutPageVersion.DeleteItem(aboutPage, id);
        }


        public async Task<AboutPageVersion> SetEditAboutViewModelAndSaveAsync(AboutPageVersion model, IFormFile? MainImagePath, IFormFile? AboutImagePath, IFormFile? DetailsImagePath, IFormFile? OperationModeImagePath, IFormFile? PhotoGalleryImagePath, IFormFile? FeedbackImagePath)
        {
            // Main
            if (MainImagePath != null)
            {
                var folder = ImageFolder.About_Main;
                await _ImageStorageService.SaveAsync(folder, MainImagePath);
                model.MainImagePath = $"{folder}{MainImagePath.FileName}";
            }

            // About
            if (AboutImagePath != null)
            {
                var folder = ImageFolder.About_About;
                await _ImageStorageService.SaveAsync(folder, AboutImagePath);
                model.AboutImagePath = $"{folder}{AboutImagePath.FileName}";
            }

            // Details
            if (DetailsImagePath != null)
            {
                var folder = ImageFolder.About_Details;
                await _ImageStorageService.SaveAsync(folder, DetailsImagePath);
                model.DetailsImagePath = $"{folder}{DetailsImagePath.FileName}";
            }
            // OperationMode
            if (OperationModeImagePath != null)
            {
                var folder = ImageFolder.About_OperationMode;
                await _ImageStorageService.SaveAsync(folder, OperationModeImagePath);
                model.OperationModeImagePath = $"{folder}{OperationModeImagePath.FileName}";
            }
            // PhotoGallery
            if (PhotoGalleryImagePath != null)
            {
                var folder = ImageFolder.About_PhotoGallery;
                await _ImageStorageService.SaveAsync(folder, PhotoGalleryImagePath);
                model.PhotoGalleryImagePath = $"{folder}{PhotoGalleryImagePath.FileName}";
            }
            // Feedback
            if (FeedbackImagePath != null)
            {
                var folder = ImageFolder.About_Feedback;
                await _ImageStorageService.SaveAsync(folder, FeedbackImagePath);
                model.FeedbackImagePath = $"{folder}{FeedbackImagePath.FileName}";
            }

            model.DateAdded = DateTime.Now;

            _AboutPageVersion.SaveItem(model, model.Id);

            return model;
        }
    }
}
