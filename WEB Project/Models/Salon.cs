using WEB_Project.Migrations;

namespace WEB_Project.Models
{
	public class Salon
	{
		
			public int Id { get; set; } // Salonun benzersiz kimliği
			public string Name { get; set; } // Salon adı
			public string Address { get; set; } // Salon adresi
			public string PhoneNumber { get; set; } // Salon telefon numarası
			public string WorkingHours { get; set; } // Çalışma saatleri (örneğin: "09:00-18:00")

			// Salonun sunduğu hizmetler (işlemler) ile ilişki
			public List<Service> Services { get; set; }
		}
}
