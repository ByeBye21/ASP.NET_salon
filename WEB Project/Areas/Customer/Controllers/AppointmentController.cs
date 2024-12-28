using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_Project.Data;
using WEB_Project.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEB_Project.Areas.Customer.Controllers
{
    [Area("Customer")]
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



        [HttpGet]
        public IActionResult ScheduleAppointment()
        {
            var expertises = _context.Expertises.Select(e => new SelectListItem
            {
                Value = e.ExpertiseId.ToString(),
                Text = $"{e.Title} - {e.Cost} TL"
            }).ToList();

            if (!expertises.Any()) // Check if the list is empty
            {
                ViewData["Message"] = "No expertises available.";
            }

            ViewData["Expertises"] = new SelectList(expertises, "Value", "Text");
            return View();
        }


        [HttpPost]
        public IActionResult ScheduleAppointment(DateTime date, TimeOnly hour, int employeeId, int expertiseId)
        {
            var userName = User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                ViewData["Message"] = "User not logged in.";
                return View();
            }

            // Fetch the selected expertise and its duration
            var expertise = _context.Expertises.FirstOrDefault(e => e.ExpertiseId == expertiseId);
            if (expertise == null)
            {
                ViewData["Message"] = "Invalid expertise selected.";
                return View();
            }

            // Calculate the appointment end time
            var appointmentEndTime = hour.Add(expertise.Time);

            // Debugging: Output key time variables
            Console.WriteLine($"Proposed appointment: {hour} - {appointmentEndTime}");

            // Check for overlapping appointments
            var overlappingAppointments = _context.Appointments
                .Where(a => a.Date == date && a.EmployeeId == employeeId)
                .ToList()
                .Any(a =>
                {
                    // Fetch the expertise duration for the existing appointment
                    var existingExpertise = _context.Expertises.FirstOrDefault(e => e.ExpertiseId == a.ExpertiseId);
                    if (existingExpertise == null) return false;

                    var existingStartTime = a.Hour;
                    var existingEndTime = a.Hour.Add(existingExpertise.Time);

                    // Debugging: Output existing appointment times
                    Console.WriteLine($"Existing appointment: {existingStartTime} - {existingEndTime}");

                    // Check if the proposed appointment overlaps with the existing one
                    return !(appointmentEndTime <= existingStartTime || hour >= existingEndTime);
                });

            if (overlappingAppointments)
            {
                ViewData["Message"] = "This time slot is already booked.";
                return View();
            }

            // Create and save the appointment
            var appointment = new Appointment
            {
                Date = date,
                Hour = hour,
                CustomerName = userName,
                EmployeeId = employeeId,
                ExpertiseId = expertiseId,
                IsApproved = false
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            ViewData["Message"] = "Appointment scheduled successfully.";
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public IActionResult GetEmployees(int expertiseId)
        {
            var employees = _context.Employees
                .Where(e => e.Expertises.Any(ex => ex.ExpertiseId == expertiseId))
                .Select(e => new SelectListItem
                {
                    Value = e.EmployeeId.ToString(),
                    Text = e.Name
                })
                .ToList();

            return Json(employees);
        }

        [HttpPost]
        public IActionResult GetAvailableDates(int employeeId)
        {
            var existingAppointments = _context.Appointments
                .Where(a => a.EmployeeId == employeeId)
                .Select(a => a.Date)
                .ToList();

            var availableDates = Enumerable.Range(0, 30)
                .Select(offset => DateTime.Today.AddDays(offset))
                .Where(date => !existingAppointments.Contains(date))
                .ToList();

            return Json(availableDates.Select(d => d.ToString("yyyy-MM-dd")));
        }
    }
}
