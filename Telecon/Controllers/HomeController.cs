﻿using System;
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

        public ActionResult Privilegios()
        {
            return View();
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
            return View("Home");
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


        public ActionResult Privileges()
        {
            return View("AdminPrivileges");
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