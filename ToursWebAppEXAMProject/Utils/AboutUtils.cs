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
        private readonly ImageStorageService _ImageStorageService;

        public AboutUtils(IBaseInterface<AboutPageVersion> AboutPageVersion, IBaseInterface<PhotoGalleryImage> PhotoGalleryImages, ImageStorageService ImageStorageService)
        {
            _AboutPageVersion = AboutPageVersion;
            _PhotoGalleryImages = PhotoGalleryImages;
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


        public async Task<AboutPageVersion> SetAboutPageVersionAndSaveAsync(AboutPageVersion model, IFormFile? MainImageFileName, IFormFile? AboutImageFileName, IFormFile? DetailsImageFileName, IFormFile? OperationModeImageFileName, IFormFile? PhotoGalleryImageFileName, IFormFile? CollectionImagesFileName, IFormFile? FeedbackImageFileName)
        {
            // Main
            if (MainImageFileName != null)
            {
                var folder = ImageFolder.About_Main;
                model.MainImagePath = await _ImageStorageService.SaveAsync(folder, MainImageFileName);
            }

            // About
            if (AboutImageFileName != null)
            {
                var folder = ImageFolder.About_About;
                model.AboutImagePath = await _ImageStorageService.SaveAsync(folder, AboutImageFileName);
            }

            // Details
            if (DetailsImageFileName != null)
            {
                var folder = ImageFolder.About_Details;
                model.DetailsImagePath = await _ImageStorageService.SaveAsync(folder, DetailsImageFileName);
            }
            // OperationMode
            if (OperationModeImageFileName != null)
            {
                var folder = ImageFolder.About_OperationMode;
                model.OperationModeImagePath = await _ImageStorageService.SaveAsync(folder, OperationModeImageFileName);
            }
            // PhotoGallery
            if (PhotoGalleryImageFileName != null)
            {
                var folder = ImageFolder.About_PhotoGallery;
                model.PhotoGalleryImagePath = await _ImageStorageService.SaveAsync(folder, PhotoGalleryImageFileName);
            }
            // CollectionImagesFileName
            if (CollectionImagesFileName != null) 
            {
                var folder = ImageFolder.About_PhotoGallery_Collection;
                var collectionImagePath = await _ImageStorageService.SaveAsync(folder, CollectionImagesFileName);
                
                var photoGalleryImageModel = new PhotoGalleryImage
                {
                    AboutPageVersionId = model.Id,
                    ImagePath = collectionImagePath
                };
                
                _PhotoGalleryImages.SaveItem(photoGalleryImageModel, photoGalleryImageModel.Id);
            }
            // Feedback
            if (FeedbackImageFileName != null)
            {
                var folder = ImageFolder.About_Feedback;
                model.FeedbackImagePath = await _ImageStorageService.SaveAsync(folder, FeedbackImageFileName);
            }

            model.DateAdded = DateTime.Now;

            _AboutPageVersion.SaveItem(model, model.Id);

            return model;
        }

    }
}
