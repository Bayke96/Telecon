using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Telecon.Models;
using Telecon.Data_Formatting;
using System.Data.Entity.Validation;
using System.Web.ModelBinding;
using System.Diagnostics;

namespace Telecon.CRUD_Operations
{
    public class ProductCRUD
    {
        DataFormats df = new DataFormats();

        public Product LoadProduct(string productName)
        {
            using(var context = new DataContext())
            {
                var search = (from s in context.Productos where s.name == productName select s).FirstOrDefault();
                return search;
            }
        }

        public void AddProduct(Product modelo, string imgPath1, string imgPath2 = null,
           string imgPath3 = null, string imgPath4 = null)
        {
            string productName = df.AddressCorrector(modelo.name);
            string pDescription = df.FirstLetterToUpper(modelo.description);

            using (var context = new DataContext())
            {    
                var producto = new Product
                {
                    name = productName.Trim(),
                    description = pDescription.Trim(),
                    price = Math.Round(modelo.price, 2, MidpointRounding.ToEven),
                    mainImage = imgPath1,
                    secondaryImageA = imgPath2,
                    secondaryImageB = imgPath3,
                    secondaryImageC = imgPath4

                };
                context.Productos.Add(producto);
                context.SaveChanges();
            }
            
        }

        public void EditProduct(Object[] productData, string imgPath1 = null, string imgPath2 = null,
           string imgPath3 = null, string imgPath4 = null)
        {
           
            string oldName = df.AddressCorrector(productData[0].ToString());
            string productName = df.AddressCorrector(productData[1].ToString());
            string pDescription = df.FirstLetterToUpper(productData[2].ToString());

            using (var context = new DataContext())
            {
                var search = (from s in context.Productos where s.name == oldName select s).FirstOrDefault();

                search.name = productName.Trim();
                search.description = pDescription.Trim();
                search.price = Math.Round(Double.Parse(productData[3].ToString()), 2, MidpointRounding.ToEven);

                if (imgPath1 == null) search.mainImage = search.mainImage;
                if (imgPath1 != null) search.mainImage = imgPath1;

                if (imgPath2 == null) search.secondaryImageA = search.secondaryImageA;
                if (imgPath2 != null) search.secondaryImageA = imgPath2;

                if (imgPath3 == null) search.secondaryImageB = search.secondaryImageB;
                if (imgPath3 != null) search.secondaryImageB = imgPath3;

                if (imgPath4 == null) search.secondaryImageC = search.secondaryImageC;
                if (imgPath4 != null) search.secondaryImageC = imgPath4;

                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
                
            }

        }
    }
}