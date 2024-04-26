using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mediklaud.Models
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string MobileNo { get; set; }
        public string Email { get; set; }

        [Required]
        public string Specialization { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ConsultationFee { get; set; }

       


        public virtual ICollection<Appointment> Appointments { get; set; }

    }

}
