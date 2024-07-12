using Microsoft.AspNetCore.Mvc;
using MedicalRecordsManagement.Models;
using System.Linq;

namespace MedicalRecordsManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly HospitalContext _context;

        public AdminController(HospitalContext context)
        {
            _context = context;
        }

        // GET: Admin/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Admin admin)
        {
            var adminInDb = _context.Admins.SingleOrDefault(a => a.Username == admin.Username && a.Password == admin.Password);
            if (adminInDb != null)
            {
                //// Set session
                //HttpContext.Session.SetInt32("AdminId", adminInDb.AdminId);

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(admin);
        }

        // GET: Admin/AddDoctor
        public IActionResult AddDoctor()
        {
            return View();
        }

        // POST: Admin/AddDoctor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDoctor(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(doctor);
        }
    }
}
