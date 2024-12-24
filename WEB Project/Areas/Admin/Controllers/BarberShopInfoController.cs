using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_Project.Data;
using WEB_Project.Models;

namespace WEB_Project.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class BarberShopInfoController : Controller
	{
		private readonly ApplicationDbContext _db;

		public BarberShopInfoController(ApplicationDbContext db)
		{
			_db = db;
		}

		// GET: Edit
		public IActionResult Edit()
		{
			var barberShopInfo = _db.BarberShopInfos.FirstOrDefault();
			if (barberShopInfo == null)
			{
				barberShopInfo = new BarberShopInfo(); // Eğer bilgi yoksa, boş bir model döndür
			}
			return View(barberShopInfo);
		}

		// POST: Edit
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(BarberShopInfo model)
		{
			if (ModelState.IsValid)
			{
				var barberShopInfo = _db.BarberShopInfos.FirstOrDefault();
				if (barberShopInfo == null)
				{
					// Yeni bir kayıt oluştur
					_db.BarberShopInfos.Add(model);
				}
				else
				{
					// Mevcut kaydı güncelle
					barberShopInfo.Name = model.Name;
					barberShopInfo.Address = model.Address;
					barberShopInfo.Phone = model.Phone;
					barberShopInfo.WorkingHours = model.WorkingHours;
					_db.BarberShopInfos.Update(barberShopInfo);
				}
				_db.SaveChanges();
				TempData["Success"] = "Barber Shop Info updated successfully!";
				return RedirectToAction("Index", "Home", new { area = "" });
			}
			return View(model);
		}
	}

}

