namespace ToursWebAppEXAMProject.Models
{
    public class TechTaskPage
    {
        public int Id { get; set; }

        /// <summary>
        /// Home, Search, Edit, Support, About, Common
        /// </summary>
        public string PageName { get; set; } = null!;

        public List<TechTaskItem> Tasks { get; set; } = new();
    }
}
