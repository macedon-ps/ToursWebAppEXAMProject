using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.ViewModels
{
    public class NewsAndBlogsViewModel
    {
        public IEnumerable<Blog>? AllBlogs { get; set; }
        public IEnumerable<New>? AllNews { get; set; }
    }
}
