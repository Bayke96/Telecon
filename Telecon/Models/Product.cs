using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Telecon.Models
{
    [Table("Productos")]
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [ConcurrencyCheck, Column("p_name"), Required( ErrorMessage = "You must type a name for the product."), Index(IsUnique = true), MinLength(3), MaxLength(128)]
        public string name { get; set; }
        [Column("p_description"), Required(ErrorMessage = "You must type a description for the product."), MaxLength(128)]
        public string description { get; set; }
        [Column("p_price"), Required(ErrorMessage = "You must type a price for the product."), Range(0, 100000000)]
        public double price { get; set; }
        [Column("p_mainimage"), Required(ErrorMessage = "You must insert a main image for the product."), MaxLength(128)]
        public string mainImage{ get; set; }
        [Column("p_secondimageA"), MaxLength(128)]
        public string secondaryImageA { get; set; }
        [Column("p_secondimageB"), MaxLength(128)]
        public string secondaryImageB { get; set; }
        [Column("p_secondimageC"), MaxLength(128)]
        public string secondaryImageC { get; set; }

        [NotMapped]
        public IEnumerable<Product> productsList { get; set; }

    }
}