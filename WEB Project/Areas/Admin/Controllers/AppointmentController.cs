using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_Project.Data;
using WEB_Project.Models;

namespace WEB_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var appointments = _context.Appointments
                .Include(a => a.Employees)
                .Include(a => a.Expertises)
                .ToList();

            return View(appointments);
        }

        [HttpPost]
        public IActionResult Approve(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                appointment.IsApproved = true;
                _context.SaveChanges();
                TempData["Message"] = "Appointment approved successfully.";
            }
            else
            {
                TempData["Error"] = "Appointment not found.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
                TempData["Message"] = "Appointment deleted successfully.";
            }
            else
            {
                TempData["Error"] = "Appointment not found.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
