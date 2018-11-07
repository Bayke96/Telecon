using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telecon.Models;

namespace Telecon.Controllers
{
    public class JSONController : Controller
    {
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
                var all = (from s in context.Usuarios select new
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
                if (orderBy.Contains("Código".Trim()) || orderBy.Contains("ID".Trim())) {
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



    }
}
