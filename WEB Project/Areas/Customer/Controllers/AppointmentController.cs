using Microsoft.AspNetCore.Mvc;
using WEB_Project.Models;
using System.Collections.Generic;
using System.Linq;

namespace WEB_Project.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class AppointmentController : Controller
	{
		private static List<Appointment> _appointments = new List<Appointment>(); // Geçici veritabanı

		[HttpGet]
		public IActionResult Index()
		{
			return View(_appointments); // Tüm randevuları listele
		}

		[HttpGet]
		[HttpGet]
		public IActionResult Create()
		{
			var model = new Appointment
			{
				Employees = new List<Employee>
		{
			new Employee { EmployeeId = 1, Name = "John Doe" },
			new Employee { EmployeeId = 2, Name = "Jane Smith" }
		},
				Expertises = new List<Expertise>
		{
			new Expertise { ExpertiseId = 1, Title = "Haircut" },
			new Expertise { ExpertiseId = 2, Title = "Beard Trim" }
		}
			};

			if (model.Employees == null) model.Employees = new List<Employee>();
			if (model.Expertises == null) model.Expertises = new List<Expertise>();

			return View(model);
		}


		[HttpPost]
		public IActionResult Create(Appointment model)
		{
			if (ModelState.IsValid)
			{
				model.AppointmentId = _appointments.Count + 1; // Yeni ID ata
				_appointments.Add(model);
				return RedirectToAction("Index");
			}

			return View(model); // Hataları göster
		}

		[HttpGet]
		public IActionResult Details(int id)
		{
			var appointment = _appointments.FirstOrDefault(a => a.AppointmentId == id);
			if (appointment == null)
			{
				return NotFound();
			}

			return View(appointment);
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			var appointment = _appointments.FirstOrDefault(a => a.AppointmentId == id);
			if (appointment != null)
			{
				_appointments.Remove(appointment);
			}

			return RedirectToAction("Index");
		}
	}
}
