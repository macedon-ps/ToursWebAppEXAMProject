using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Utils
{
    public class NewsUtils
    {
        private readonly IBaseInterface<New> _AllNews;
        private readonly FileUtils _FileUtils;
        
        public NewsUtils(IBaseInterface<New> News, FileUtils FileUtils) 
        {
            _AllNews = News;
            _FileUtils = FileUtils;
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

        public async Task SaveNewImageByFileNameAsync(IFormFile imageFileName)
        {
            var folder = "/images/NewsTitleImages/";
            await _FileUtils.SaveImageToFolder(folder, imageFileName);
        }

        public New SetNewsModel(New newsModel, IFormFile? imageFileName)
        {
            // добавляем дату добавления новости (текущую дату)
            newsModel.DateAdded = DateTime.Now;

            if (imageFileName != null)
            {
                newsModel.TitleImagePath = $"/images/NewsTitleImages/{imageFileName.FileName}";
            }
            
            return newsModel;    
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
