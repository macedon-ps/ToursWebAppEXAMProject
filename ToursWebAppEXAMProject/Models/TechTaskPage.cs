using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.Models
{
    public class TechTaskPage
    {
        public int Id { get; set; }

        /// <summary>
        /// Home, Search, Edit, Support, About, Common
        /// </summary>
        [StringLength(100)]
        public string PageName { get; set; } = null!;

        public List<TechTaskItem> Tasks { get; set; } = new();
    }
}
