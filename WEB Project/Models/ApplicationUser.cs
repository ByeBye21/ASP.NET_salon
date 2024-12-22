using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WEB_Project.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string? Expertise { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
    }
}