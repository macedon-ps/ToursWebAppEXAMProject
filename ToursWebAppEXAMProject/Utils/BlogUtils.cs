using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Utils
{
    public class BlogUtils
    {
        private readonly IBaseInterface<Blog> _AllBlogs;
        private readonly FileUtils _FileUtils;

        public BlogUtils(IBaseInterface<Blog> AllBlogs, FileUtils FileUtils)
        {
             _AllBlogs = AllBlogs;
            _FileUtils = FileUtils;
        }

        public IEnumerable<Blog> GetBlogs()
        {
            return _AllBlogs.GetAllItems();
        }

        public Blog GetBlogById(int id)
        {
            return _AllBlogs.GetItemById(id);
        }

        public IEnumerable<Blog> QueryResult(bool isFullName, string insertedText)
        {
            return _AllBlogs.GetQueryResultItemsAfterFullName(insertedText, isFullName);
        }

        public void DeleteBlogById(Blog blog)
        {
            _AllBlogs.DeleteItem(blog, blog.Id);
        }

        public async Task SaveBlogImageByFileNameAsync(IFormFile changeTitleImagePath)
        {
            var folder = "/images/BlogsTitleImages/";
            await _FileUtils.SaveImageToFolder(folder, changeTitleImagePath);
        }

        public Blog SetBlogModel(Blog blogModel, IFormFile? imageFileName)
        {
            // добавляем дату добавления блога (текущую дату)            
            blogModel.DateAdded = DateTime.Now;

            if (imageFileName != null)
            {
                var folder = "/images/BlogsTitleImages/";
                blogModel.TitleImagePath = $"{folder}{imageFileName.FileName}";
            }

            return blogModel;
        }

        public void SaveBlogModel(Blog blogModel)
        {
            if (blogModel != null)
            {
                _AllBlogs.SaveItem(blogModel, blogModel.Id);
            }
        }

        public Blog SetBlogModelWithChatDataAndSave(Blog blog, string userName, string message)
        {
            var timeMessage = $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";
            var allMessageText = $"<p>{timeMessage}: <b>{userName}:</b><br/> {message}</p><br/>";

            // если чат пустой, т.е. с дефолтной строкой, то заменяем дефолтную строку пустой строкой и сохраняем
            if (blog.FullMessageLine == "Вся строка сообщений")
            {
                blog.FullMessageLine = "";
                SaveBlogModel(blog);
            }

            blog.Message = $"В {timeMessage} пользователь {userName} прислал сообщение";
            blog.FullMessageLine += allMessageText;

            // сохраняем сообщения чата в БД
            SaveBlogModel(blog);

            return blog;
        }
    }
}
