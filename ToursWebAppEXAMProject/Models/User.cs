using Microsoft.AspNetCore.Identity;

namespace ToursWebAppEXAMProject.Models
{
    public class User: IdentityUser
    {
        public int BirthYear { get; set; }
    }
}
