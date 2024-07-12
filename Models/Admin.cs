using System.ComponentModel.DataAnnotations;

namespace MedicalRecordsManagement.Models
{
    public class Admin
    {
        public int AdminId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public int DoctorId { get; set; }

        // Navigation property
        public Doctor Doctor { get; set; }
    }
}
