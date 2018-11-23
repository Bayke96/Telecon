using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Telecon.Models;

namespace Telecon.Models
{
    [Table("AccRecovery")]
    public class PassRecovery
    {
        [Key]
        public int ID { get; set; }
        [Column("Request_Email"), Index(IsUnique = true), ConcurrencyCheck, Required(ErrorMessage = "You must type a valid email.")]
        [ForeignKey("User")]
        public int userEmail { get; set; }
        public virtual User User { get; set; }
        [Column("Request_Date"), Required(ErrorMessage = "A date is necessary for the request")]
        public DateTime requestDate { get; set; }
        [Column("Request_Counter")]
        public int requestAmmount { get; set; }

        public PassRecovery()
        {
            requestDate = DateTime.Today;
        }
    }

    

}