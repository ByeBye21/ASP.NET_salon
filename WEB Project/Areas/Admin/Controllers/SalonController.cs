using Microsoft.AspNetCore.Mvc;
using WEB_Project.Models;
using WEB_Project.Data;
using Microsoft.EntityFrameworkCore;

namespace WEB_Project.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class SalonController : Controller
	{
		private readonly ApplicationDbContext _context;

		public SalonController(ApplicationDbContext context)
		{
			_context = context;
		}

		// Edit sayfasını getirme
		public async Task<IActionResult> Edit()
		{
			var salon = await _context.Salons.FirstOrDefaultAsync();  // Sadece bir salon olduğunu varsayıyoruz
			if (salon == null)
			{
				return NotFound();
			}
			return View(salon);
		}

		// Edit işlemi (POST)
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Salon salon)
		{
			if (id != salon.Id)
			{
				return NotFound(); // Eğer salon id'si uyumsuzsa
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(salon);  // Salon nesnesini güncelle
					await _context.SaveChangesAsync();  // Değişiklikleri kaydet
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!SalonExists(salon.Id))
					{
						return NotFound();  // Salon bulunamadıysa
					}
					else
					{
						throw;
					}
				}

				return RedirectToAction("Index", "Home");  // Başarıyla düzenledikten sonra anasayfaya yönlendir
			}
			return View(salon);  // Eğer model hatalıysa, tekrar düzenleme sayfasını göster
		}

		// Salon mevcut mu kontrol etme
		private bool SalonExists(int id)
		{
			return _context.Salons.Any(e => e.Id == id);  // Salon mevcut mu kontrol eder
		}
	}
}