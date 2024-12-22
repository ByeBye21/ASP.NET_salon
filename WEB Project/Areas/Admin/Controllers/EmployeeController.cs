using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEB_Project.Data;
using WEB_Project.Models;

namespace WEB_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmployeeController(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Main page showing the employee list
        public IActionResult Index(string expertise)
        {
            var employeeRoleId = _db.Roles
                .Where(r => r.Name == SD.Role_Employee)
                .Select(r => r.Id)
                .FirstOrDefault();

            var employeesQuery = _db.UserRoles
                .Where(ur => ur.RoleId == employeeRoleId)
                .Join(_db.ApplicationUsers,
                      ur => ur.UserId,
                      user => user.Id,
                      (ur, user) => new RegisterEmployee
                      {
                          Name = user.Name,
                          Email = user.Email,
                          Expertise = user.Expertise,
                          StartTime = (TimeSpan)user.StartTime,
                          EndTime = (TimeSpan)user.EndTime
                      });

            if (!string.IsNullOrEmpty(expertise))
            {
                employeesQuery = employeesQuery.Where(e => e.Expertise == expertise);
            }

            var employees = employeesQuery.ToList();
            return View(employees);
        }

        // Display the register form
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterEmployee model)
        {
            if (ModelState.IsValid)
            {
                var employee = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true,
                    Name = model.Name,
                    Expertise = model.Expertise,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime
                };

                var result = await _userManager.CreateAsync(employee, model.Password);

                if (result.Succeeded)
                {
                    if (await _roleManager.RoleExistsAsync(SD.Role_Employee))
                    {
                        await _userManager.AddToRoleAsync(employee, SD.Role_Employee);
                    }

                    TempData["SuccessMessage"] = "Employee registered successfully!";
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
    }
}
