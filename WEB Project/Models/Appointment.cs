using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WEB_Project.Models;

namespace WEB_Project.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }

        [Display(Name = "Appointment Data")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "Appointment Time")]
        [DataType(DataType.Time)]
        [Required]
        public TimeOnly Hour { get; set; }

        public bool IsApproved { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [ForeignKey("Employees")]
        public int EmployeeId { get; set; }
        public virtual Employee? Employees { get; set; }

        [ForeignKey("Expertises")]
        public int ExpertiseId { get; set; }
        public virtual Expertise? Expertises { get; set; }
    }
}
