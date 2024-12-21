namespace WEB_Project.Models
{
	public class Service
	{
			public int Id { get; set; } // İşlemin benzersiz kimliği
			public string Name { get; set; } // İşlem adı (örneğin: Saç Kesimi)
			public decimal Price { get; set; } // Ücret
			public int Duration { get; set; } // Süre (dakika cinsinden)

			// Salonla ilişki
			public int SalonId { get; set; }
			public Salon Salon { get; set; }
		}
	}


