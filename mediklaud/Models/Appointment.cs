using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mediklaud.Models
{
    public class Appointment
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }

        [Required]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        [Required]
        public DateTime AppointmentDateTime { get; set; }

        [Required]
        
        
        [Column(TypeName = "decimal(18, 2)")]

        public double ConsultationFee { get; set; }

        [Required]
        public bool Status { get; set; } 

        public string  Comments {  get; set; }


        //[Required]
        //public int UserId { get; set; }
        //public UserInfo UserInfo { get; set; } // Navigation property for UserInfo

        // One-to-one relationship with Billing
        public Billing Billing { get; set; } // Navigation property for Billing



    }

}
