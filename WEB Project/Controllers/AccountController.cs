using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using WEB_Project.Data;
using WEB_Project.Models;
using System.Linq;

namespace WEB_Project.Controllers
{
	public class AccountController : Controller
	{
		private readonly ApplicationDbContext _context;

		// Constructor: DbContext'e bağlanmak için
		public AccountController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Register
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		// POST: Register
		[HttpPost]
		public IActionResult Register(User user)
		{
			if (ModelState.IsValid)
			{
				// Şifreyi hashleme (bcrypt ile güvenli hale getiriyoruz)
				user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

				// Kullanıcıyı veritabanına ekleyelim
				_context.Users.Add(user);
				_context.SaveChanges();

				// Kayıt başarılıysa, anasayfaya yönlendir
				return RedirectToAction("Index", "Home");
			}

			return View(user);
		}

		// GET: Login
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		// POST: Login
		[HttpPost]
		public IActionResult Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				// Kullanıcıyı veritabanında bulma
				var userFromDb = _context.Users.FirstOrDefault(u => u.Username == model.Username);

				if (userFromDb != null)
				{
					// Şifreyi doğrulama (bcrypt ile şifreyi kontrol etme)
					if (BCrypt.Net.BCrypt.Verify(model.Password, userFromDb.Password))
					{
						// Giriş başarılıysa, kullanıcıyı anasayfaya yönlendir
						return RedirectToAction("Index", "Home");
					}
					else
					{
						// Şifre yanlışsa hata mesajı
						ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre.");
					}
				}
				else
				{
					// Kullanıcı adı bulunamadıysa hata mesajı
					ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre.");
				}
			}
			return View(model);
		}
	}
}
