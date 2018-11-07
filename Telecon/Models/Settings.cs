using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Telecon.Models
{
    [Table("App_Settings")]
    public class Settings
    {
        [Key]
        public int ID { get; set; }
        [NotMapped]
        public int? aumentarPrecios { get; set; }
        public double? precioMinimo { get; set; }
        public double? precioMaximo { get; set; }

        public bool? adicionProductos { get; set; }
        public bool? modificacionProductos { get; set; }
        public bool? eliminarProductos { get; set; }
   
        public bool? fotosPerfil { get; set; }
        public bool? cambioContraseña { get; set; }
        public bool? cambioCorreo { get; set; }
        public bool? accesoUsuario { get; set; }

    }
}