using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WEB_Project.Data;
using WEB_Project.Models;
using System.Linq;
using System.Diagnostics;

namespace WEB_Project.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;

		public HomeController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			// Veritabanından ilk berber dükkanını alıyoruz
			var barberShopInfo = _context.BarberShopInfos.FirstOrDefault();

			// Modeli view'a gönderiyoruz
			return View(barberShopInfo);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
