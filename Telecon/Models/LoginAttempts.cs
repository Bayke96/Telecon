using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Telecon.Models
{
    [Table("LoginAttempts")]
    public class LoginAttempts
    {
        [Key]
        public int ID { get; set; }
        [Column("Attempt_IP"), Index(IsUnique = true), Required(ErrorMessage = "The IP is obligatory."), MaxLength(50)]
        public string IP { get; set; }
        [Column("Attempt_Ammounts"), Required]
        public int attemptAmmounts { get; set; }

        public LoginAttempts()
        {
            attemptAmmounts = 1;
        }
    }

    
}