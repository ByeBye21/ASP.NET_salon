using Microsoft.AspNetCore.Mvc;
using WEB_Project.Models;

namespace WEB_Project.Controllers
{

	public class AccountController : Controller
	{
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(User user)
		{
			if (ModelState.IsValid)
			{
				// Kullanıcıyı veritabanına kaydetme işlemi burada yapılır.
				return RedirectToAction("Index", "Home");
			}
			return View(user);
		}
			[HttpGet]
			public IActionResult Login()
			{
				return View();
			}

			[HttpPost]
			public IActionResult Login(LoginModel model)
			{
				if (ModelState.IsValid)
				{
					// Kullanıcı doğrulama işlemi (örneğin veritabanında kontrol etme)
					bool isValidUser = (model.Username == "test" && model.Password == "password"); // Örnek doğrulama

					if (isValidUser)
					{
						// Giriş başarılıysa yönlendirme yapılır.
						return RedirectToAction("Index", "Home");
					}
					else
					{
						ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre.");
					}
				}
				return View(model);
			}
		}

	}
