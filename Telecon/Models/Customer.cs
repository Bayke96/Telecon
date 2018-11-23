using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Telecon.Models
{
    [Table("Clientes")]
    public class Customer
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("User")]
        public int employeeID { get; set; }
        public virtual User User { get; set; }
        [Column("c_razonsocial"), ConcurrencyCheck, Required(ErrorMessage = "You must type the customer's name."), DataType(DataType.Text), MaxLength(128)]
        public string razonsocial { get; set; }
        [Column("c_nombres"), MinLength(3), MaxLength(72)]
        public string nombres { get; set; }
        [Column("c_apellidos"), MinLength(2), MaxLength(72)]
        public string apellidos { get; set; }
        [Column("c_telefono"), Required(ErrorMessage = "You must type the customer's phone number."), MaxLength(72), DataType(DataType.PhoneNumber)]
        public string telefono { get; set; }
        [Column("c_direccion"), Required(ErrorMessage = "You must type the customer's address."), MaxLength(128)]
        public string direccion { get; set; }
        [Column("c_comentarios"), MaxLength(128)]
        public string comentarios { get; set; }

        [NotMapped]
        public IEnumerable<Customer> customersList { get; set; }
    }
}