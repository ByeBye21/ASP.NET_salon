using System.ComponentModel.DataAnnotations;

namespace WEB_Project.Models
{
    public class Employee
    {
        public Employee()
        {
            Expertises = new List<Expertise>(); // Initialize the list
        }

        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Start time is required.")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        public TimeSpan EndTime { get; set; }

        public List<Expertise> Expertises { get; set; }
    }
}