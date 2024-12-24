using System.ComponentModel.DataAnnotations;

namespace WEB_Project.Models
{
    public class Employee
    {
        public Employee()
        {
            Expertises = new List<Expertise>(); //Initialize the list
        }

        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public List<Expertise> Expertises { get; set; }
    }
}
