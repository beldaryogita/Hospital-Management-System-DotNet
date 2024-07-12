using System;
using System.ComponentModel.DataAnnotations;
using MedicalRecordsManagement.Models;

public class Treatment
{
    [Key]
    public int TreatmentId { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    [Required]
    public string Diagnosis { get; set; }

    [Required]
    public string Prescription { get; set; }

    [DataType(DataType.Date)]
    public DateTime TreatmentDate { get; set; }

    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
}
