using System.ComponentModel.DataAnnotations;

namespace WEB_Project.Models
{
    public class Expertise
    {
        public Expertise()
        {
            Employees = new List<Employee>(); // Initialize the list
        }

        [Key]
        public int ExpertiseId { get; set; }

        [Required(ErrorMessage = "Expertise title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Time duration is required.")]
        public TimeSpan Time { get; set; }

        [Required(ErrorMessage = "Cost is required.")]
        public decimal Cost { get; set; }

        public List<Employee> Employees { get; set; }
    }
}