using Microsoft.AspNetCore.Identity;

namespace SolvintechTestApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Token { get; set; }
    }
}
