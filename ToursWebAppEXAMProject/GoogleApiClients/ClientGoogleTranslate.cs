using Google.Cloud.Translation.V2;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToursWebAppEXAMProject.GoogleApiClients
{
    public static class ClientGoogleTranslate
    {
        /// <summary>
        /// Метод перевода текста с одного языка на другой
        /// </summary>
        /// <param name="text">оригинальный текст, кот. нужно перевести</param>
        /// <param name="langTo">язык текста перевода</param>
        /// <param name="langFrom">язык источника</param>
        /// <returns></returns>
        public static string TranslateText(string text, string langTo, string langFrom)
        {
            var client = TranslationClient.Create();

            // перевод оригинального текста
            var response = client.TranslateText(text, langTo, langFrom);

            // вывод переведенного текста
            return response.TranslatedText;
        }

        public static IList<Language> GetAllLanguages()
        {
            var client = TranslationClient.Create();
            var languages = client.ListLanguages(LanguageCodes.Russian);
            
            // вывод списка языков
            return languages;
        }
    }
}