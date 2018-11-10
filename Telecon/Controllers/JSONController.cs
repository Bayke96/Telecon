using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telecon.Models;
using Telecon.Data_Formatting;
using Telecon.CRUD_Operations;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace Telecon.Controllers
{
    public class JSONController : Controller
    {

        ProductCRUD poperations = new ProductCRUD();
        DataFormats df = new DataFormats();

        // ------------------------------------- USERS JSON AJAX CALLS ----------------------------------------------- //

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UserExists(string userName)
        {
            bool found = false;
            string user = char.ToUpper(userName.First()) + userName.Substring(1).ToLower().Trim();
            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios where s.username == user select s.username).FirstOrDefault();
                if (search == null) found = false;
                if (search != null) found = true;
            }
            return Json(found, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EmailRegistered(string email)
        {
            string mail = email.ToLower();
            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios where s.email == mail select new { emailname = s.email, emailid = s.ID }).FirstOrDefault();
                if (search == null)
                {
                    var jsonresult = new[]
                    {
                    new { emailname = "NaN", emailid = "NaN"}
                    };
                    var jsonData = JsonConvert.SerializeObject(jsonresult);
                    return Json(jsonData, JsonRequestBehavior.AllowGet);
                }
                return Json(search, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UserID(string userName)
        {
            int id = 0;
            string user = char.ToUpper(userName.First()) + userName.Substring(1).ToLower().Trim();
            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios where s.username == user select s.ID).SingleOrDefault();
                if (search != 0) id = search;
            }
            return Json(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SearchUserByID(int id = 0)
        {
            using (var context = new DataContext())
            {
                var all = (from s in context.Usuarios
                           select new
                           {
                               userID = s.ID,
                               userName = s.username,
                               firstName = s.firstnames,
                               lastName = s.lastnames,
                               s.email,
                               s.number
                           }
                ).ToList();

                var search = (from s in context.Usuarios
                              where s.ID == id
                              select new
                              {
                                  userName = s.username,
                                  firstName = s.firstnames,
                                  lastName = s.lastnames,
                                  s.email,
                                  s.number
                              }
                                  ).FirstOrDefault();

                if (id != 0) return Json(search);
                if (id == 0) return Json(all);
            }
            return Json(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SearchUserByNames(string names = "")
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios
                              where s.firstnames.Contains(names.ToLower()) || s.lastnames.Contains(names.ToLower())
                              select new
                              {
                                  userID = s.ID,
                                  userName = s.username,
                                  firstName = s.firstnames,
                                  lastName = s.lastnames,
                                  s.email,
                                  s.number
                              }
                                  ).ToList();

                return Json(search);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SearchUserByEmail(string email = "")
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios
                              where s.email.StartsWith(email.ToLower())
                              select new
                              {
                                  userID = s.ID,
                                  userName = s.username,
                                  firstName = s.firstnames,
                                  lastName = s.lastnames,
                                  s.email,
                                  s.number
                              }
                                  ).ToList();

                return Json(search);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SearchUserByPhone(string phone = "")
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios
                              where s.number.Contains(phone)
                              select new
                              {
                                  userID = s.ID,
                                  userName = s.username,
                                  firstName = s.firstnames,
                                  lastName = s.lastnames,
                                  s.email,
                                  s.number
                              }
                                  ).ToList();

                return Json(search);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OrderUsers(string orderBy)
        {
            using (var context = new DataContext())
            {
                if (orderBy.Contains("Código".Trim()) || orderBy.Contains("ID".Trim()))
                {
                    var search = (from s in context.Usuarios
                                  orderby s.ID ascending
                                  select new
                                  {
                                      userID = s.ID,
                                      userName = s.username,
                                      firstName = s.firstnames,
                                      lastName = s.lastnames,
                                      s.email,
                                      s.number
                                  }
                                 ).ToList();

                    return Json(search);
                }

                if (orderBy.Contains("Nombre".Trim()) || orderBy.Contains("Name".Trim()))
                {
                    var search = (from s in context.Usuarios
                                  orderby s.username ascending
                                  select new
                                  {
                                      userID = s.ID,
                                      userName = s.username,
                                      firstName = s.firstnames,
                                      lastName = s.lastnames,
                                      s.email,
                                      s.number
                                  }
                                 ).ToList();

                    return Json(search);
                }

                if (orderBy.Contains("Correo".Trim()) || orderBy.Contains("E-mail".Trim()))
                {
                    var search = (from s in context.Usuarios
                                  orderby s.email ascending
                                  select new
                                  {
                                      userID = s.ID,
                                      userName = s.username,
                                      firstName = s.firstnames,
                                      lastName = s.lastnames,
                                      s.email,
                                      s.number
                                  }
                                 ).ToList();

                    return Json(search);
                }

                if (orderBy.Contains("Teléfono".Trim()) || orderBy.Contains("Phone".Trim()))
                {
                    var search = (from s in context.Usuarios
                                  orderby s.email ascending
                                  select new
                                  {
                                      userID = s.ID,
                                      userName = s.username,
                                      firstName = s.firstnames,
                                      lastName = s.lastnames,
                                      s.email,
                                      s.number
                                  }
                                 ).ToList();

                    return Json(search);
                }
                return Json(null);
            }

        }

        // ------------------------------------- PRODUCTS JSON AJAX CALLS ----------------------------------------------- //

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ProductValidations(string productName)
        {
            bool found = false;
            string product = df.AddressCorrector(productName);
            string json = "";
            using (var context = new DataContext())
            {
                var search = (from s in context.Productos where s.name == product select s.name).FirstOrDefault();
                
                if (search == null) found = false;
                if (search != null) found = true;
                var prices = (from s in context.appSettings select new { minimo = s.precioMinimo, maximo = s.precioMaximo }).FirstOrDefault();
                if (prices == null) json = JsonConvert.SerializeObject(new { name = found, minimo = "", maximo = "" });
                if (prices != null) json = JsonConvert.SerializeObject(new { name = found, prices.minimo, prices.maximo });
             
               
                
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AgregarProducto(Product modelo, HttpPostedFileBase PImg1, HttpPostedFileBase PImg2 = null,
            HttpPostedFileBase PImg3 = null, HttpPostedFileBase PImg4 = null)
        {

            string path = "", path2 = null, path3 = null, path4 = null;

            if (PImg1 != null)
            {
                string pic = Path.GetFileName(PImg1.FileName);
                path = Path.Combine(
                                       Server.MapPath("~/Style/Media/Product_Images"), pic);
                PImg1.SaveAs(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    PImg1.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
            if (PImg2 != null)
            {
                string pic = Path.GetFileName(PImg2.FileName);
                path2 = Path.Combine(
                                       Server.MapPath("~/Style/Media/Product_Images"), pic);
                PImg2.SaveAs(path2);
                using (MemoryStream ms = new MemoryStream())
                {
                    PImg2.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
            if (PImg3 != null)
            {
                string pic = Path.GetFileName(PImg3.FileName);
                path3 = Path.Combine(
                                      Server.MapPath("~/Style/Media/Product_Images"), pic);
                PImg3.SaveAs(path3);
                using (MemoryStream ms = new MemoryStream())
                {
                    PImg3.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
            if (PImg4 != null)
            {
                string pic = Path.GetFileName(PImg4.FileName);
                path4 = Path.Combine(
                                      Server.MapPath("~/Style/Media/Product_Images"), pic);
                PImg4.SaveAs(path4);
                using (MemoryStream ms = new MemoryStream())
                {
                    PImg4.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }

            poperations.AddProduct(modelo, path, path2, path3, path4);
            ModelState.Clear();

            return Json("true");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CargarProducto(string productName)
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.Productos where s.name == productName select s).FirstOrDefault();
                return Json(search);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditarProducto(Object[] productData, HttpPostedFileBase PImg1, HttpPostedFileBase PImg2 = null,
                HttpPostedFileBase PImg3 = null, HttpPostedFileBase PImg4 = null)
        {
            object[] productUpdate = new object[3];
            for(int i = 0; i < productData.Length; i++)
            {
                productUpdate[i] = productData[i].ToString().Trim();
            }

            string values = productUpdate[0].ToString();
            object[] productValues = new object[5];
            int counter = 0;
            var regex = new Regex(Regex.Escape(","));
            foreach (var letters in values.Split('~'))
            {
                productValues[counter] = letters.Replace(",", "").Trim();
                if (counter == 2)
                {
                    productValues[counter] = regex.Replace(letters, "", 1).Trim();
                }
                counter++;
            }
            
            string path = null, path2 = null, path3 = null, path4 = null;

            if (PImg1 != null)
            {
                string pic = Path.GetFileName(PImg1.FileName);
                path = Path.Combine(
                                       Server.MapPath("~/Style/Media/Product_Images"), pic);
                PImg1.SaveAs(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    PImg1.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
            if (PImg2 != null)
            {
                string pic = Path.GetFileName(PImg2.FileName);
                path2 = Path.Combine(
                                       Server.MapPath("~/Style/Media/Product_Images"), pic);
                PImg2.SaveAs(path2);
                using (MemoryStream ms = new MemoryStream())
                {
                    PImg2.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
            if (PImg3 != null)
            {
                string pic = Path.GetFileName(PImg3.FileName);
                path3 = Path.Combine(
                                      Server.MapPath("~/Style/Media/Product_Images"), pic);
                PImg3.SaveAs(path3);
                using (MemoryStream ms = new MemoryStream())
                {
                    PImg3.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
            if (PImg4 != null)
            {
                string pic = Path.GetFileName(PImg4.FileName);
                path4 = Path.Combine(
                                      Server.MapPath("~/Style/Media/Product_Images"), pic);
                PImg4.SaveAs(path4);
                using (MemoryStream ms = new MemoryStream())
                {
                    PImg4.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
     
            poperations.EditProduct(productValues, path, path2, path3, path4);
            return Json("true");
        }

        [HttpGet]
        public JsonResult ListaProductos()
        {
            using(var context = new DataContext())
            {
                var search = (from s in context.Productos select s.name).ToList();
                return Json(search, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
