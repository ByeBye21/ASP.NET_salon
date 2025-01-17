﻿using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Expertises)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null) return NotFound();

            ViewBag.Expertises = await _context.Expertises.ToListAsync();
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee, string[] selectedExpertises)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Expertises = await _context.Expertises.ToListAsync();
                return View(employee);
            }

            var existingEmployee = await _context.Employees
                .Include(e => e.Expertises)
                .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.Name = employee.Name;
            existingEmployee.StartTime = employee.StartTime;
            existingEmployee.EndTime = employee.EndTime;

            existingEmployee.Expertises.Clear();
            if (selectedExpertises != null)
            {
                foreach (var expertiseId in selectedExpertises)
                {
                    var expertise = await _context.Expertises.FindAsync(int.Parse(expertiseId));
                    if (expertise != null)
                    {
                        existingEmployee.Expertises.Add(expertise);
                    }
                }
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Employee updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Expertises)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Employee deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Employee not found.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
