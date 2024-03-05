using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToursWebAppEXAMProject.ConfigFiles
    {
    public class ConfigEmail : PageModel
    {
        public static string CompanyName { get; set; } = null!;
        public static string Email { get; set; } = null!;
        public static string Password { get; set; } = null!;
        public static string SmtpServer { get; set; } = null!;
        public static int PortSmtp { get; set; }
        public static bool UseSslSmtp { get; set; }
        public static string ImapServer { get; set; } = null!;
        public static int PortImap { get; set; }
        public static bool UseSslImap { get; set; }
        public static bool Adress { get; set; }

    }
}
