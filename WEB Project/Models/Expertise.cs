using System.ComponentModel.DataAnnotations;

namespace WEB_Project.Models
{
    public class Expertise
    {
        public Expertise()
        {
            Employees = new List<Employee>(); //Initialize the list
        }

        [Key]
        public int ExpertiseId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        [Required]
        public decimal Cost { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
