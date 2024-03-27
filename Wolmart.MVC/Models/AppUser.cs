using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Wolmart.MVC.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
    }
}
