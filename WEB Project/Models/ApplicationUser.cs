using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WEB_Project.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
    }
}