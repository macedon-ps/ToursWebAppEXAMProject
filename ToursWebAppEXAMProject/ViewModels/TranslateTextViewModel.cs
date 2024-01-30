using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToursWebAppEXAMProject.ViewModels
{
    public class TranslateTextViewModel
    {
        public string TextOrigin { get; set; } = "";

        public string TextTranslated { get; set; } = "";

        public string LanguageFrom { get; set; } = "en";

        public string LanguageTo { get; set; } = "ru";

        public SelectList LanguagesList { get; set; } = null!;
    }
}