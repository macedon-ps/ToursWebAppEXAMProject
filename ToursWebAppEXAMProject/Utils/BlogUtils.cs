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

        public Blog GetBlogForEdit(int id)
        {
            var blog = _AllBlogs.GetItemById(id);
            blog.DateAdded = DateTime.Now;

            return blog;
        }

        public IEnumerable<Blog> QueryResult(bool isFullName, string insertedText)
        {
            return _AllBlogs.GetQueryResultItemsAfterFullName(insertedText, isFullName);
        }

        public void DeleteBlogById(Blog blog)
        {
            _AllBlogs.DeleteItem(blog, blog.Id);
        }

        public async Task SaveImagePathAsync(IFormFile changeTitleImagePath)
        {
            var folder = "/images/BlogsTitleImages/";
            await _FileUtils.SaveImageToFolder(folder, changeTitleImagePath);
        }

        public Blog SetBlogModel(Blog blog, IFormCollection formValues, IFormFile? changeTitleImagePath)
        {
            var fullInfoBlog = formValues["fullInfoAboutBlog"].ToString();
            var fullMessageLine = formValues["fullMessageLine"].ToString();

            if (fullInfoBlog != null) blog.FullDescription = fullInfoBlog;
            if(fullMessageLine != null) blog.FullMessageLine = fullMessageLine;
            blog.DateAdded = DateTime.Now;

            if (changeTitleImagePath != null)
            {
                var folder = "/images/BlogsTitleImages/";
                blog.TitleImagePath = $"{folder}{changeTitleImagePath.FileName}";
            }

            return blog;
        }

        public void SaveBlog(Blog blog)
        {
            if (blog != null)
            {
                _AllBlogs.SaveItem(blog, blog.Id);
            }
        }

        public Blog SetBlogModelByFormValues(Blog blog, IFormCollection formValues)
        {
            var fullInfoBlog = formValues["fullInfoAboutBlog"].ToString();
            if(fullInfoBlog!=null) blog.FullDescription = fullInfoBlog;

            return blog;
        }

        public Blog SetBlogModelWithChatDataAndSave(Blog blog, string userName, string message)
        {
            var timeMessage = $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";
            var allMessageText = $"<p>{timeMessage}: <b>{userName}:</b><br/> {message}</p><br/>";

            // если чат пустой, т.е. с дефолтной строкой, то заменяем дефолтную строку пустой строкой и сохраняем
            if (blog.FullMessageLine == "Вся строка сообщений")
            {
                blog.FullMessageLine = "";
                SaveBlog(blog);
            }

            blog.Message = $"В {timeMessage} пользователь {userName} прислал сообщение";
            blog.FullMessageLine += allMessageText;

            // сохраняем сообщения чата в БД
            SaveBlog(blog);

            return blog;
        }
    }
}
