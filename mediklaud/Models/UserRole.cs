using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mediklaud.Models
{
    public class UserRole
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public bool Status;

        public UserInfo UserInfo { get; set; } // Navigation property for UserInfo


    }
}
