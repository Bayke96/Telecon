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
                if(search == null)
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
    }
}