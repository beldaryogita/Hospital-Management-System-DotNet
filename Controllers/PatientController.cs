using Microsoft.AspNetCore.Mvc;
using MedicalRecordsManagement.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecordsManagement.Controllers
{
    public class PatientController : Controller
    {
        private readonly HospitalContext _context;

        public PatientController(HospitalContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var patient = _context.Patients.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (patient != null)
            {
                // Redirect to the PatientTreatments action with the patient's ID
                return RedirectToAction("PatientTreatments", new { id = patient.PatientId });
            }

            // Handle invalid login
            ViewBag.ErrorMessage = "Invalid username or password.";
            return View("Login");
        }

        [HttpPost]
        public IActionResult Signup(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Patients.Add(patient);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(patient);
        }

        public IActionResult PatientTreatments(int id)
        {
            var treatments = _context.Treatments
    .Include(t => t.Patient) // Include Patient navigation property if needed
    .Include(t => t.Doctor)  // Include Doctor navigation property
    .Where(t => t.PatientId == id)
    .ToList();



            return View(treatments); // Ensure this matches the view filename 'PatientTreatments.cshtml'
        }

        public IActionResult Details(int id)
        {
            var treatment = _context.Treatments
                                    .Include(t => t.Patient)
                                    .Include(t => t.Doctor)
                                    .FirstOrDefault(t => t.TreatmentId == id);

            if (treatment == null)
            {
                return NotFound();
            }

            return View(treatment);
        }

    }
}
