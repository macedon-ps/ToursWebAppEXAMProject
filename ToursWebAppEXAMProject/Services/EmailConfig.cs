using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToursWebAppEXAMProject.Services
{
    public class EmailConfig: PageModel
    {
        public static string CompanyName { get; set; }
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static string SmtpServer { get; set; }
        public static int Port { get; set; }   
        public static bool UseSsl { get; set; }


    }
}
