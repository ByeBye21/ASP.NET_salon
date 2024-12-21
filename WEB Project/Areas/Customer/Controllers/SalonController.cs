using Microsoft.AspNetCore.Mvc;
using WEB_Project.Data;

namespace WEB_Project.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class SalonController : Controller
	{
		private readonly ApplicationDbContext _context;

		public SalonController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Customer/Salon
		public IActionResult Index()
		{
			var salon = _context.Salons.FirstOrDefault(); // İlk salon bilgisini al
			if (salon == null)
			{
				return NotFound(); // Eğer veri yoksa 404 döndür
			}
			return View(salon); // View'e model gönder
		}
	}
}
