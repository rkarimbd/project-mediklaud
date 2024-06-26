﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mediklaud.Models
{
    public class HospitalService
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }

        [Required]
        public string ServiceName { get; set; }

        [Required]
        public int HospitalId { get; set; }
        [Required]
        public bool Status { get; set; }


      

        public virtual ICollection<Billing> Billings { get; set; }

    }
}
