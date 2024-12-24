using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_Project.Data;
using WEB_Project.Models;

namespace WEB_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees
                .Include(e => e.Expertises)
                .ToListAsync();
            ViewBag.Expertises = await _context.Expertises.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Expertises = await _context.Expertises.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee, string[] selectedExpertises)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Expertises = await _context.Expertises.ToListAsync();
                return View(employee);
            }

            if (selectedExpertises != null)
            {
                foreach (var expertiseId in selectedExpertises)
                {
                    var expertise = await _context.Expertises.FindAsync(int.Parse(expertiseId));
                    if (expertise != null)
                    {
                        employee.Expertises.Add(expertise);
                    }
                }
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Employee registered successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}