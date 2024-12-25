namespace WEB_Project.Models
{
	public class Appointment
	{
		public Appointment()
		{
			Employees = new List<Employee>();
			Expertises = new List<Expertise>();
		}

		public int AppointmentId { get; set; }
		public string CustomerName { get; set; }
		public string CustomerId { get; set; }
		public DateTime AppointmentDate { get; set; }
		public bool Status { get; set; }
		public string BarberShopWorkingHours { get; set; }

		public List<Employee> Employees { get; set; }
		public List<Expertise> Expertises { get; set; }
	}
}
