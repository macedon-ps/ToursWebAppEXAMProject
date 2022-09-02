using System;
using System.Collections.Generic;

namespace ToursWebAppEXAMProject.Models
{
    public partial class New
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string ShortDescription { get; set; } = null!;
        public string FullDescription { get; set; } = null!;
        public string? TitleImagePath { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}
