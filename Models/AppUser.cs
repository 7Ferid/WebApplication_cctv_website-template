using Microsoft.AspNetCore.Identity;

namespace WebApplication_cctv_website_template.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
