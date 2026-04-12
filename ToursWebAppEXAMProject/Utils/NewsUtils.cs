using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Services.ImageStorage;

namespace ToursWebAppEXAMProject.Utils
{
    public class NewsUtils
    {
        private readonly IBaseInterface<New> _AllNews;
        private readonly ImageStorageService _ImageStorageService;
        

        public NewsUtils(IBaseInterface<New> News, ImageStorageService ImageStorageService) 
        {
            _AllNews = News;
            _ImageStorageService = ImageStorageService;
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


        public async Task<string?> SaveNewImageByFileNameAsync(IFormFile? imageFileName)
        {
            var folder = ImageFolder.News;
            return await _ImageStorageService.SaveAsync(folder, imageFileName);
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
