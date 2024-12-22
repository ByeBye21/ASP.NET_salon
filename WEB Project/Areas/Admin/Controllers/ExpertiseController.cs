using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_Project.Data;
using WEB_Project.Models;

namespace WEB_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ExpertiseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpertiseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Expertise/Index
        public IActionResult Index()
        {
            var expertiseList = _context.Expertises.ToList();
            return View(expertiseList);
        }

        // POST: Expertise/Add
        [HttpPost]
        public IActionResult Add(string area, decimal cost, TimeSpan time)
        {
            if (!string.IsNullOrEmpty(area))
            {
                var expertise = new Expertise { Area = area, Cost = cost, Time = time };
                _context.Expertises.Add(expertise);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Expertise added successfully!";
            }
            return RedirectToAction("Index");
        }

        // POST: Expertise/Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var expertise = _context.Expertises.Find(id);
            if (expertise != null)
            {
                _context.Expertises.Remove(expertise);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Expertise deleted successfully!";
            }
            return RedirectToAction("Index");
        }

        // GET: Expertise/Edit/{id}
        public IActionResult Edit(int id)
        {
            var expertise = _context.Expertises.Find(id);
            if (expertise == null)
            {
                return NotFound();
            }
            return View(expertise);
        }

        // POST: Expertise/Edit
        [HttpPost]
        public IActionResult Edit(Expertise updatedExpertise)
        {
            if (ModelState.IsValid)
            {
                var existingExpertise = _context.Expertises.Find(updatedExpertise.Id);
                if (existingExpertise != null)
                {
                    existingExpertise.Area = updatedExpertise.Area;
                    existingExpertise.Cost = updatedExpertise.Cost;
                    existingExpertise.Time = updatedExpertise.Time;

                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Expertise updated successfully!";
                    return RedirectToAction("Index");
                }
            }
            return View(updatedExpertise);
        }

    }
}