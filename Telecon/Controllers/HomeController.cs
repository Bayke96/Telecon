using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telecon.Models;
using Telecon.CRUD_Operations;

namespace Telecon.Controllers
{
    public class HomeController : Controller
    {

        AppSettings settings = new AppSettings();
        // GET: Home
        public ActionResult Redireccion()
        {
            return RedirectToAction("Principal");
        }

        [HttpGet]
        public ActionResult Principal()
        {
            using(var context = new DataContext())
            {
                var search = (from s in context.Productos orderby s.ID descending select s).Take(5).ToList();
                return View("Inicio", search);
            }
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        [OutputCache(Duration = 1200)]
        public ActionResult PAdminEsp()
        {
            return View("PanelAdmin");
        }

        [OutputCache(Duration = 1200)]
        public ActionResult Contacto()
        {
            return View("InformacionContacto");
        }

        [OutputCache(Duration = 1200)]
        public ActionResult General()
        {
            return View("InformacionGeneral");
        }

        [OutputCache(Duration = 1200)]
        public ActionResult Servicios()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Privilegios()
        {
            using (var context = new DataContext())
            {
                var selection = (from s in context.appSettings where s.ID == 1 select s).FirstOrDefault();
                return View(selection);
            }
          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Privilegios(bool profilePics = false, bool passwordChange = false, bool emailChange = false,
            bool userAccess = false)
        {
            settings.UpdateAccountSettings(profilePics, passwordChange, emailChange, userAccess);
            using (var context = new DataContext())
            {
                var selection = (from s in context.appSettings where s.ID == 1 select s).FirstOrDefault();
                return View(selection);
            }
        }

        [OutputCache(Duration = 1200)]
        public ActionResult Cercos()
        {
            return View("CercosElectricos");
        }

        [OutputCache(Duration = 1200)]
        public ActionResult EquiposAcceso()
        {
            return View();
        }

        [OutputCache(Duration = 1200)]
        public ActionResult Portones()
        {
            return View();
        }

        [OutputCache(Duration = 1200)]
        public ActionResult Centrales()
        {
            return View("CentralesTelefonicas");
        }

        /* ENGLISH VERSION */

        public ActionResult MainPage()
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.Productos orderby s.ID descending select s).Take(5).ToList();
                return View("Home", search);
            }
        }
        public ActionResult UserLogin()
        {
            return View();
        }

        [OutputCache(Duration = 1200)]
        public ActionResult PAdminEng()
        {
            return View("AdminPanel");
        }

        [OutputCache(Duration = 1200)]
        public ActionResult Contact()
        {
            return View("ContactInfo");
        }

        [OutputCache(Duration = 1200)]
        public ActionResult GeneralInfo()
        {
            return View("GeneralInfo");
        }

        [OutputCache(Duration = 1200)]
        public ActionResult Services()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Privileges()
        {
            using (var context = new DataContext())
            {
                var selection = (from s in context.appSettings where s.ID == 1 select s).FirstOrDefault();
                return View("AdminPrivileges", selection);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Privileges(bool profilePics = false, bool passwordChange = false, bool emailChange = false,
            bool userAccess = false)
        {
            settings.UpdateAccountSettings(profilePics, passwordChange, emailChange, userAccess);
            using (var context = new DataContext())
            {
                var selection = (from s in context.appSettings where s.ID == 1 select s).FirstOrDefault();
                return View("AdminPrivileges", selection);
            }
        }

        [OutputCache(Duration = 1200)]
        public ActionResult ElectricFences()
        {
            return View();
        }

        [OutputCache(Duration = 1200)]
        public ActionResult CCTV()
        {
            return View();
        }

        [OutputCache(Duration = 1200)]
        public ActionResult SlidingGates()
        {
            return View("Sliding-Gates");
        }

        [OutputCache(Duration = 1200)]
        public ActionResult PhoneExchange()
        {
            return View("Phone-Exchange");
        }
            
        public ActionResult PageNotFound()
        {
            return View();
        }

    }
}