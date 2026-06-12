using Microsoft.CodeAnalysis;
using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Services.CloudineryImageStorageService;

namespace ToursWebAppEXAMProject.Utils
{
    public class AboutUtils
    {
        private readonly IBaseInterface<AboutPageVersion> _AboutPageVersion;
        private readonly IBaseInterface<PhotoGalleryImage> _PhotoGalleryImages;
        private readonly CloudinaryImageStorageService _CloudinaryImageStorageService;

        public AboutUtils(IBaseInterface<AboutPageVersion> AboutPageVersion, IBaseInterface<PhotoGalleryImage> PhotoGalleryImages, CloudinaryImageStorageService CloudinaryImageStorageService)
        {
            _AboutPageVersion = AboutPageVersion;
            _PhotoGalleryImages = PhotoGalleryImages;
            _CloudinaryImageStorageService = CloudinaryImageStorageService;
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
                var publicId = $"main_{model.Id}_{Path.GetFileNameWithoutExtension(MainImageFileName.FileName)}";
                var uploadImage = await _CloudinaryImageStorageService.UploadAsync(folder, MainImageFileName, publicId);
                model.MainImagePath = uploadImage.Url;
                model.MainImagePublicId = uploadImage.outPublicId;
            }

            // About
            if (AboutImageFileName != null)
            {
                var folder = ImageFolder.About_About;
                var publicId = $"about_{model.Id}_{Path.GetFileNameWithoutExtension(AboutImageFileName.FileName)}";
                var uploadImage = await _CloudinaryImageStorageService.UploadAsync(folder, AboutImageFileName, publicId);
                model.AboutImagePath = uploadImage.Url;
                model.AboutImagePublicId = uploadImage.outPublicId;
            }

            // Details
            if (DetailsImageFileName != null)
            {
                var folder = ImageFolder.About_Details;
                var publicId = $"details_{model.Id}_{Path.GetFileNameWithoutExtension(DetailsImageFileName.FileName)}";
                var uploadImage = await _CloudinaryImageStorageService.UploadAsync(folder, DetailsImageFileName, publicId);
                model.DetailsImagePath = uploadImage.Url;
                model.DetailsImagePublicId = uploadImage.outPublicId;
            }
            // OperationMode
            if (OperationModeImageFileName != null)
            {
                var folder = ImageFolder.About_OperationMode;
                var publicId = $"operation_mode_{model.Id}_{Path.GetFileNameWithoutExtension(OperationModeImageFileName.FileName)}";
                var uploadImage = await _CloudinaryImageStorageService.UploadAsync(folder, OperationModeImageFileName, publicId);
                model.OperationModeImagePath = uploadImage.Url;
                model.OperationModeImagePublicId = uploadImage.outPublicId;
            }
            // PhotoGallery
            if (PhotoGalleryImageFileName != null)
            {
                var folder = ImageFolder.About_PhotoGallery;
                var publicId = $"photogallery_{model.Id}_{Path.GetFileNameWithoutExtension(PhotoGalleryImageFileName.FileName)}";
                var uploadImage = await _CloudinaryImageStorageService.UploadAsync(folder, PhotoGalleryImageFileName, publicId);
                model.PhotoGalleryImagePath = uploadImage.Url;
                model.PhotoGalleryImagePublicId = uploadImage.outPublicId;
            }
            // CollectionImagesFileName
            if (CollectionImagesFileName != null) 
            {
                var folder = ImageFolder.About_PhotoGallery_Collection;
                // TODO: изменить механизм сохранения фотографий в фотогаллерею
                // var collectionImagePath = await _ImageStorageService.SaveAsync(folder, CollectionImagesFileName);
                
                var photoGalleryImageModel = new PhotoGalleryImage
                {
                    AboutPageVersionId = model.Id,
                    // ImagePath = collectionImagePath
                };
                
                _PhotoGalleryImages.SaveItem(photoGalleryImageModel, photoGalleryImageModel.Id);
            }
            // Feedback
            if (FeedbackImageFileName != null)
            {
                var folder = ImageFolder.About_Feedback;
                var publicId = $"feedback_{model.Id}_{Path.GetFileNameWithoutExtension(FeedbackImageFileName.FileName)}";
                var uploadImage = await _CloudinaryImageStorageService.UploadAsync(folder, FeedbackImageFileName, publicId);
                model.FeedbackImagePath = uploadImage.Url;
                model.FeedbackImagePublicId = uploadImage.outPublicId;
            }

            model.DateAdded = DateTime.UtcNow;

            _AboutPageVersion.SaveItem(model, model.Id);

            return model;
        }

    }
}
