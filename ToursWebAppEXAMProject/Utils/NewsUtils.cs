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

        public New GetNewsForEdit(int id)
        {
            var newsItem = _AllNews.GetItemById(id);
            newsItem.DateAdded = DateTime.Now;

            return newsItem;
        }

        public IEnumerable<New> QueryResult(bool isFullName, string insertedText)
        {
            return _AllNews.GetQueryResultItemsAfterFullName(insertedText, isFullName);
        }

        public void DeleteNewsById(New newsItem)
        {
            _AllNews.DeleteItem(newsItem, newsItem.Id);
        }

        public async Task SaveImagePathAsync(IFormFile changeTitleImagePath)
        {
            var folder = "/images/NewsTitleImages/";
            await _FileUtils.SaveImageToFolder(folder, changeTitleImagePath);
        }

        public New SetNewsModel(New newsItem, IFormCollection formValues, IFormFile? changeTitleImagePath)
        {
            var fullInfoNews = formValues["fullInfoAboutNew"].ToString();
            
            if (fullInfoNews != null) newsItem.FullDescription = fullInfoNews;
            newsItem.DateAdded = DateTime.Now;

            if (changeTitleImagePath != null)
            {
                var folder = "/images/NewsTitleImages/";
                newsItem.TitleImagePath = $"{folder}{changeTitleImagePath.FileName}";
            }

            return newsItem;    
        }

        public void SaveNews(New newsItem)
        {
            if (newsItem != null)
            {
                _AllNews.SaveItem(newsItem, newsItem.Id);
            }
        }

        public New SetNewsModelByFormValues(New newsItem, IFormCollection formValues)
        {
            var fullInfoNews = formValues["fullInfoAboutNew"].ToString();
            if (fullInfoNews != null) newsItem.FullDescription = fullInfoNews;

            return newsItem;
        }
    }
}
