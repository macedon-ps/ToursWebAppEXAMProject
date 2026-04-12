using ToursWebAppEXAMProject.Enums;
using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;
using ToursWebAppEXAMProject.Services.ImageStorage;

namespace ToursWebAppEXAMProject.Utils
{
    public class BlogUtils
    {
        private readonly IBaseInterface<Blog> _AllBlogs;
        private readonly ImageStorageService _ImageStorageService;


        public BlogUtils(IBaseInterface<Blog> AllBlogs, ImageStorageService ImageStorageService)
        {
             _AllBlogs = AllBlogs;
            _ImageStorageService = ImageStorageService;
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


        public async Task<string?> SaveBlogImageByFileNameAsync(IFormFile? imageFileName)
        {
            var folder = ImageFolder.Blogs;
            return await _ImageStorageService.SaveAsync(folder, imageFileName);
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
