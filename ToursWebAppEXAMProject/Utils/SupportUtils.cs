using Google.Cloud.Translation.V2;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using ToursWebAppEXAMProject.ViewModels;
using ToursWebAppEXAMProject.Services.GoogleApiClients;

namespace ToursWebAppEXAMProject.Utils
{
    public class SupportUtils
    {
        private readonly IMemoryCache _MemoryCache;

        public SupportUtils(IMemoryCache MemoryCache)
        {
            _MemoryCache = MemoryCache;
        }

        /// <summary>
        /// Метод получения вью-модели TranslateTextViewModel
        /// </summary>
        /// <returns></returns>
        public TranslateTextViewModel? GetModel()
        {
            var viewModel = new TranslateTextViewModel();
            
            // проверка, использование и создание кешированных данных
            _MemoryCache.TryGetValue("allLanguagesKey", out IList<Language>? languages);

            if (languages == null)
            {
                // вывод поддерживаемых языков для перевода через Google Translate API
                languages = ClientGoogleTranslate.GetAllLanguages();

                if (languages != null)
                {
                    _MemoryCache.Set("allLanguagesKey", languages, TimeSpan.FromMinutes(30));
                }
            }

            viewModel.LanguagesList = new SelectList(languages, "Code", "Name");
            viewModel.LanguagesListJson = JsonSerializer.Serialize(languages);

            return viewModel;
        }

        /// <summary>
        /// Метод получения вью-модели TranslateTextViewModel для Post формы
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="formValues"></param>
        /// <returns></returns>
        public TranslateTextViewModel GetModel(TranslateTextViewModel viewModel, IFormCollection formValues)
        {
            if (viewModel.TextOrigin != null)
            {
                viewModel.LanguageFrom = formValues["langFromSelect"];
                viewModel.LanguageTo = formValues["langToSelect"];

                viewModel.Languages = JsonSerializer.Deserialize<IList<Language>>(viewModel.LanguagesListJson);
                viewModel.LanguagesList = new SelectList(viewModel.Languages, "Code", "Name");

                // проверка, использование и создание кешированных данных
                if (viewModel.LanguagesList == null)
                {
                    _MemoryCache.TryGetValue("allLanguagesKey", out IList<Language>? languages);

                    if (languages == null)
                    {
                        // вывод поддерживаемых языков для перевода через Google Translate API
                        languages = ClientGoogleTranslate.GetAllLanguages();

                        if (languages != null)
                        {
                            _MemoryCache.Set("allLanguagesKey", languages, TimeSpan.FromMinutes(30));
                        }
                    }
                    viewModel.LanguagesList = new SelectList(viewModel.Languages, "Code", "Name");
                }

                // перевод текста через Google Translate API
                var translateText = ClientGoogleTranslate.TranslateText(viewModel.TextOrigin, viewModel.LanguageTo, viewModel.LanguageFrom);
                if (translateText != null)
                {
                    viewModel.TextTranslated = translateText;
                }
                return viewModel;
            }
            return viewModel;
        }
    }
}
