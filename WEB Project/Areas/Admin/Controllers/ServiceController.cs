using Microsoft.AspNetCore.Mvc;
using WEB_Project.Data;
using WEB_Project.Models;

namespace WEB_Project.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ServiceController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ServiceController(ApplicationDbContext context)
		{
			_context = context;
		}

		// Servisleri listele
		public IActionResult Index()
		{
			var services = _context.Services.ToList(); // Tüm işlemleri al
			return View(services); // View'a model olarak gönder
		}
	}
}
