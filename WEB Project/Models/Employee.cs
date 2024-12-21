namespace WEB_Project.Models
{
	public class Employee
	{
		public int Id { get; set; }
		public string Name { get; set; } // Çalışanın adı
		public string Expertise { get; set; } // Uzmanlık alanı
		public List<Service> Services { get; set; } // Çalışanın yapabildiği işlemler
		public string AvailabilityHours { get; set; } // Uygunluk saatleri (örneğin: "09:00-18:00")
	}
}

