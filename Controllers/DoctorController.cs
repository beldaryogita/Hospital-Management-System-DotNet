
using Microsoft.AspNetCore.Mvc;
using MedicalRecordsManagement.Models;


namespace MedicalRecordsManagement.Controllers;
public class DoctorController : Controller
    {
        private readonly HospitalContext _context;

        public DoctorController(HospitalContext context)
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
        var user = _context.Doctors.FirstOrDefault(u => u.Email == email && u.Password == password);

        if (user != null)
        {
            // Implement session management or authentication mechanism here
            // For simplicity, you can use ASP.NET Core Identity or implement your own session handling
            //HttpContext.Session.SetString("Username", username);
            return RedirectToAction("Treatments", "Treatments");
        }

        // Handle invalid login
        ViewBag.ErrorMessage = "Invalid username or password.";
        return View("Login");
    }

    [HttpPost]
        public IActionResult Signup(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(doctor);
        }



    //public IActionResult Treatments()
    //{
    //    var treatments = _context.Treatments.ToList(); // Fetch all treatments from database
    //    return View(treatments);
    //}
    


}

