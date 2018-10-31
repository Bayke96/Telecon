using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Telecon.Models
{
    [Table("Usuarios")]
    public partial class User
    {
        [Key]
        public int ID { get; set; }
        [Column("u_username"), Required(ErrorMessage = "You must type an username."), ConcurrencyCheck, MinLength(3), MaxLength(128)]
        public string username { get; set; }
        [Column("u_password"), Required(ErrorMessage = "You must type a password."), MinLength(12), MaxLength(128), DataType(DataType.Password)]
        public string password { get; set; }
        [Column("u_firstnames"), Required(ErrorMessage = "You must type a firstname."), MinLength(3), MaxLength(72)]
        public string firstnames { get; set; }
        [Column("u_lastnames"), Required(ErrorMessage = "You must type a lastname."), MinLength(2), MaxLength(72)]
        public string lastnames { get; set; }
        [Column("u_email"), Required(ErrorMessage = "You must type an email."), MaxLength(50), DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Column("u_age"), Required(ErrorMessage = "You must type an age between 15 - 100 years old."), Range(15, 100)]
        public int age { get; set; }
        [Column("u_address"), MaxLength(128)]
        public string address { get; set; }
        [Column("u_number"), MaxLength(72), DataType(DataType.PhoneNumber)]
        public string number { get; set; }
        [Column("u_admin"), Required(ErrorMessage = "You must choose the user's privileges.")]
        public bool admin { get; set; }
    }
}