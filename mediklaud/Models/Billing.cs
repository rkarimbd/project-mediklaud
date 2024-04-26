using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mediklaud.Models
{
    public class Billing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillNo { get; set; }
        [Required]
        public DateTime BillDate { get; set; }


        [Required]
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }



        [Required]
        public int ServiceId { get; set; }
        public HospitalService Service { get; set; }


        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BillAmount { get; set; }

        [Required]
        public bool IsPaid { get; set; }

        public string Comments { get; set; }

       

    }
}
