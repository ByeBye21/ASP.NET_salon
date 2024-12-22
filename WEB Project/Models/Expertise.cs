using System.ComponentModel.DataAnnotations;

namespace WEB_Project.Models
{
    public class Expertise
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Area { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        [Required]
        public decimal Cost { get; set; }
    }
}
