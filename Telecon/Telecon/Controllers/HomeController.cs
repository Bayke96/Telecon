using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Telecon.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Redireccion()
        {
            return RedirectToAction("Principal");
        }

        public ActionResult Principal()
        {
            return View("Inicio");
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult PAdminEsp()
        {
            return View("PanelAdmin");
        }

        public ActionResult Contacto()
        {
            return View("InformacionContacto");
        }

        public ActionResult General()
        {
            return View("InformacionGeneral");
        }

        public ActionResult Servicios()
        {
            return View();
        }

        public ActionResult Privilegios()
        {
            return View();
        }

        public ActionResult Cercos()
        {
            return View("CercosElectricos");
        }

        public ActionResult EquiposAcceso()
        {
            return View();
        }

        public ActionResult Portones()
        {
            return View();
        }
        public ActionResult Centrales()
        {
            return View("CentralesTelefonicas");
        }

        /* ENGLISH VERSION */

        public ActionResult MainPage()
        {
            return View("Home");
        }
        public ActionResult UserLogin()
        {
            return View();
        }
        public ActionResult PAdminEng()
        {
            return View("AdminPanel");
        }
        public ActionResult Contact()
        {
            return View("ContactInfo");
        }
        public ActionResult GeneralInfo()
        {
            return View("GeneralInfo");
        }
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult Privileges()
        {
            return View("AdminPrivileges");
        }
        public ActionResult ElectricFences()
        {
            return View();
        }
        public ActionResult CCTV()
        {
            return View();
        }
        public ActionResult SlidingGates()
        {
            return View("Sliding-Gates");
        }
        public ActionResult PhoneExchange()
        {
            return View("Phone-Exchange");
        }
            


    }
}