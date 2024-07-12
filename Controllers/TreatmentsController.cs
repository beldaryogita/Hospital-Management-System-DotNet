using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MedicalRecordsManagement.Controllers
{
    public class TreatmentsController : Controller
    {
        private readonly HospitalContext _context;

        public TreatmentsController(HospitalContext context)
        {
            _context = context;
        }

        // GET: AddTreatment
        public IActionResult AddTreatment()
        {
            // Load dropdown data
            ViewBag.Patients = _context.Patients.Select(p => new { p.PatientId, Name = p.FirstName + " " + p.LastName }).ToList();
            ViewBag.Doctors = _context.Doctors.Select(d => new { d.DoctorId, Name = d.FirstName + " " + d.LastName }).ToList();
            return View();
        }
        // POST: AddTreatment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTreatment(Treatment treatment)
        {
            // Retrieve the full Patient and Doctor objects
            treatment.Patient = _context.Patients.FirstOrDefault(p => p.PatientId == treatment.PatientId);
            treatment.Doctor = _context.Doctors.FirstOrDefault(d => d.DoctorId == treatment.DoctorId);


            Console.WriteLine(treatment.Patient.ToString());


            // Validate if the retrieved Patient and Doctor objects are not null
            if (treatment.Patient == null)
            {
                ModelState.AddModelError("PatientId", "Invalid Patient ID.");
            }

            if (treatment.Doctor == null)
            {
                ModelState.AddModelError("DoctorId", "Invalid Doctor ID.");
            }

            if (ModelState!= null)
            {
                _context.Treatments.Add(treatment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Treatments));
            }

            ViewBag.Patients = _context.Patients.Select(p => new { p.PatientId, Name = p.FirstName + " " + p.LastName }).ToList();
            ViewBag.Doctors = _context.Doctors.Select(d => new { d.DoctorId, Name = d.FirstName + " " + d.LastName }).ToList();

            return View(treatment);
        }

        // List of treatments
        public IActionResult Treatments()
        {
            var treatments = _context.Treatments
                                     .Include(t => t.Patient)
                                     .Include(t => t.Doctor)
                                     .ToList();
            return View(treatments);

        }


        // GET: Treatments/Edit/5
        public IActionResult EditTreatment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treatment = _context.Treatments
                                    .Include(t => t.Patient)
                                    .Include(t => t.Doctor)
                                    .FirstOrDefault(t => t.TreatmentId == id);

            if (treatment == null)
            {
                return NotFound();
            }

            // Load dropdown data if needed
            LoadDropdownData();

            return View(treatment);
        }

        // POST: Treatments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTreatment(int id, Treatment treatment)
        {
            if (id != treatment.TreatmentId)
            {
                return NotFound();
            }

            if (ModelState != null)
            {
                try
                {
                    // Ensure the related entities are attached to the context
                    treatment.Patient = _context.Patients.FirstOrDefault(p => p.PatientId == treatment.PatientId);
                    treatment.Doctor = _context.Doctors.FirstOrDefault(d => d.DoctorId == treatment.DoctorId);

                    _context.Update(treatment);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreatmentExists(treatment.TreatmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Treatments));
            }

            // Reload dropdown data in case of validation errors
            LoadDropdownData();

            return View(treatment);
        }

        // GET: Treatments/Delete/5
        public IActionResult DeleteTreatment(int id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTreatment(int id, Treatment treatment)
        {
            var treatmentToDelete = _context.Treatments.Find(id);
            if (treatmentToDelete == null)
            {
                return NotFound();
            }

            _context.Treatments.Remove(treatmentToDelete);
            _context.SaveChanges();
            return RedirectToAction(nameof(Treatments));
        }

        private bool TreatmentExists(int id)
        {
            return _context.Treatments.Any(t => t.TreatmentId == id);
        }
        private void LoadDropdownData()
        {
            ViewBag.Patients = _context.Patients
                                       .Select(p => new { p.PatientId, Name = p.FirstName + " " + p.LastName })
                                       .ToList();
            ViewBag.Doctors = _context.Doctors
                                       .Select(d => new { d.DoctorId, Name = d.FirstName + " " + d.LastName })
                                       .ToList();
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
