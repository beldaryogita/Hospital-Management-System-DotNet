using MedicalRecordsManagement.Models;
using Microsoft.EntityFrameworkCore;

public class HospitalContext : DbContext
{
    public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Treatment> Treatments { get; set; }

    public DbSet<Admin> Admins { get; set; }
}
