using Google.Cloud.Translation.V2;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ToursWebAppEXAMProject.ViewModels
{
    public class TranslateTextViewModel
    {
        public string TextOrigin { get; set; } = "";

        public string? TextTranslated { get; set; } = "";

        [Display(Name = "Перевести с: ")]
        public string? LanguageFrom { get; set; } = "en";

        [Display(Name = "Перевести на: ")]
        public string? LanguageTo { get; set; } = "ru";

        public IList<Language>? Languages { get; set; } = null!;

        public SelectList? LanguagesList { get; set; }

        public string? LanguagesListJson { get; set; } 
    }
}