using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToursWebAppEXAMProject.ConfigFiles
    {
    public class ConfigEmail : PageModel
    {
        public static string CompanyName { get; set; } = null!;
        public static string Email { get; set; } = null!;
        public static string Password { get; set; } = null!;
        public static string SmtpServer { get; set; } = null!;
        public static int Port { get; set; }
        public static bool UseSsl { get; set; }


    }
}
