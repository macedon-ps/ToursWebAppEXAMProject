using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.ViewModels
{
    public class NewsAndBlogsViewModel
    {
        public IEnumerable<Blog> AllBlogs { get; set; } = null!;
        public IEnumerable<New> AllNews { get; set; } = null!;
    }
}
