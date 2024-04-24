using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mediklaud.Models
{
    public class UserInfo
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string? DisplayName { get; set; }
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? LoginId { get; set; }
        [Required]
        public string? Password { get; set; }


        [Required]
        public int RoleId { get; set; }
        public UserRole Role { get; set; } // Navigation property for UserRole


        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Billing> Billings { get; set; }
        public virtual ICollection<HospitalService> HospitalServices { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }

      




    }
}
