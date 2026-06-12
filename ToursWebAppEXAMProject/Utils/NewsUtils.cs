using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Services.CloudineryImageStorageService;

namespace ToursWebAppEXAMProject.Utils
{
    public class NewsUtils
    {
        private readonly IBaseInterface<New> _AllNews;
        private readonly CloudinaryImageStorageService _CloudinaryImageStorageService;


        public NewsUtils(IBaseInterface<New> News, CloudinaryImageStorageService CloudinaryImageStorageService) 
        {
            _AllNews = News;
            _CloudinaryImageStorageService = CloudinaryImageStorageService;
        }


        public IEnumerable<New> GetNews()
        {
            return _AllNews.GetAllItems();
        }


        public New GetNewsById(int id)
        {
            return _AllNews.GetItemById(id);
        }


        public IEnumerable<New> QueryResult(bool isFullName, string insertedText)
        {
            return _AllNews.GetQueryResultItemsAfterFullName(insertedText, isFullName);
        }


        public void DeleteNewsById(New newsItem)
        {
            _AllNews.DeleteItem(newsItem, newsItem.Id);
        }


        public async Task<(string Url, string PublicId)>SaveNewImageByFileNameAsync(IFormFile? imageFileName, int newsId)
        {
            var folder = ImageFolder.News;
            var publicId = $"news_{newsId}_{Path.GetFileNameWithoutExtension(imageFileName.FileName)}";
            return await _CloudinaryImageStorageService.UploadAsync(folder, imageFileName, publicId);
        }


        public void SaveNewsModel(New newsModel)
        {
            if (newsModel != null)
            {
                _AllNews.SaveItem(newsModel, newsModel.Id);
            }
        }
    }
}
