using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace LibraryApp.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public int StudentNo { get; set; }
        public string? Adress { get; set; }
        public string? Faculty { get; set; }
    }
}
