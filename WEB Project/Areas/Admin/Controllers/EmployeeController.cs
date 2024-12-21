using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_Project.Data;
using WEB_Project.Models;
using System.Threading.Tasks;

namespace WEB_Project.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class EmployeeController : Controller
	{
		private readonly ApplicationDbContext _context;

		public EmployeeController(ApplicationDbContext context)
		{
			_context = context;
		}

		// Çalışanları listeleme
		public async Task<IActionResult> Index()
		{
			var employees = await _context.Employees.ToListAsync(); // Asenkron kullanım
			return View(employees);
		}

		// Yeni çalışan ekleme
		public IActionResult Create()
		{
			var employee = new Employee(); // Yeni çalışan nesnesi oluşturuluyor
			return View("CreateOrEdit", employee); // "CreateOrEdit" görünümüne yönlendirme
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Employee employee)
		{
			// ModelState geçerli mi kontrolü
			if (ModelState.IsValid)
			{
				try
				{
					_context.Employees.Add(employee); // Çalışan ekle
					await _context.SaveChangesAsync(); // Veritabanına kaydet
					return RedirectToAction(nameof(Index)); // Başarıyla kaydedildiğinde çalışanlar listesine dön
				}
				catch (Exception ex)
				{
					// Eğer bir hata oluşursa, hatayı ekle ve kullanıcıya bildirilmesini sağla
					ModelState.AddModelError("", "Veri kaydedilirken bir hata oluştu: " + ex.Message);
				}
			}
			else
			{
				// ModelState geçersizse, hata mesajlarını göster
				ModelState.AddModelError("", "Formdaki veriler geçerli değil.");
			}

			// Eğer ModelState geçerli değilse ya da bir hata oluştuysa, aynı sayfaya geri dön
			return View("CreateOrEdit", employee);
		}

		// Çalışan düzenleme
		public async Task<IActionResult> Edit(int id)
		{
			var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id); // Asenkron sorgu
			if (employee == null)
			{
				return NotFound(); // Çalışan bulunamadıysa 404 döner
			}

			return View("CreateOrEdit", employee); // Düzenleme sayfasına yönlendir
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Employee employee)
		{
			if (id != employee.Id) // İd kontrolü
			{
				return NotFound(); // Geçersiz id
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(employee); // Çalışan bilgilerini güncelle
					await _context.SaveChangesAsync(); // Veritabanına kaydet
					return RedirectToAction(nameof(Index)); // Başarıyla kaydedildiğinde çalışanlar listesine yönlendir
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", "Veri güncellenirken bir hata oluştu: " + ex.Message);
				}
			}

			// Eğer model geçerli değilse, aynı sayfaya geri dön
			return View("CreateOrEdit", employee);
		}

		// Çalışan silme
		public async Task<IActionResult> Delete(int id)
		{
			var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id); // Asenkron sorgu
			if (employee == null)
			{
				return NotFound(); // Çalışan bulunamazsa 404 döner
			}

			return View(employee); // Silme sayfasını göster
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var employee = await _context.Employees.FindAsync(id); // Asenkron sorgu
			if (employee == null)
			{
				return NotFound(); // Eğer çalışan bulunamazsa 404 döner
			}

			try
			{
				_context.Employees.Remove(employee); // Çalışanı sil
				await _context.SaveChangesAsync(); // Veritabanına kaydet
				return RedirectToAction(nameof(Index)); // Başarılı ise çalışanlar listesine yönlendir
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Çalışan silinirken bir hata oluştu: " + ex.Message);
				return View(employee); // Hata durumunda aynı sayfaya geri dön
			}
		}
	}
}
