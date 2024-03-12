using Google.Cloud.Translation.V2;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToursWebAppEXAMProject.ViewModels
{
    public class TranslateTextViewModel
    {
        public string TextOrigin { get; set; } = "";

        public string? TextTranslated { get; set; } = "";

        public string? LanguageFrom { get; set; } = "en";

        public string? LanguageTo { get; set; } = "ru";

        public IList<Language>? Languages { get; set; } = null!;

        public SelectList? LanguagesList { get; set; }

        public string? LanguagesListJson { get; set; } 
    }
}