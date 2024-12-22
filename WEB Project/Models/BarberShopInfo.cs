using System.ComponentModel.DataAnnotations;

namespace WEB_Project.Models
{
	public class BarberShopInfo
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public string WorkingHours { get; set; }
	}

}
