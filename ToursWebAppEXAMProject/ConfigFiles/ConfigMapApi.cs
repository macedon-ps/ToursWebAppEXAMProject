using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToursWebAppEXAMProject.ConfigFiles
{
    public class ConfigMapApi : PageModel
    {
        static string MapApiKey { get; set; } = "AIzaSyBEVmfZAyKdq0DmdqKx-08Hnatd9hibv6A&callback=console.debug&libraries=maps,marker&v=beta";

        public static string GetMapApiKey() { return MapApiKey; }
    }
}
