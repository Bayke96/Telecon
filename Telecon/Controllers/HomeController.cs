using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telecon.Models;
using Telecon.CRUD_Operations;
using Telecon.Encryption;
using Telecon.Model_Operations;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Telecon.Data_Formatting;

namespace Telecon.Controllers
{
    public class HomeController : Controller
    {
        Security sec = new Security();
        AppSettings settings = new AppSettings();
        UserCRUD uoperations = new UserCRUD();
        DataFormats df = new DataFormats();

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

        [HttpGet]
        public ActionResult Login()
        {
            if(User.Identity.IsAuthenticated == true)
            {
                return RedirectToAction("Perfil", "Users");
            }
            else
            {
                return View("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User modelo)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                bool loginValidation = sec.PasswordMatch(modelo.username, modelo.password);
                string userName = df.FirstLetterToUpper(modelo.username);
                bool userRole = uoperations.LoadUserRole(userName);
                string roleName = null;

                if (userRole == true)
                {
                    roleName = "Admin";
                }
                else
                {
                    roleName = "User";
                }
                if (loginValidation == true)
                {

                    var ident = new ClaimsIdentity(
                    new[]
                    {
            // adding following 2 claim just for supporting default antiforgery provider
                new Claim(ClaimTypes.NameIdentifier, userName),
                new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

            // an optional claim you could omit this 
                new Claim(ClaimTypes.Name, userName),

            // you could even add some role
                new Claim(ClaimTypes.Role, roleName),
                        // and so on
                    },
                    DefaultAuthenticationTypes.ApplicationCookie);

                    // Identity is sign in user based on claim don't matter 
                    // how you generated it Identity 
                    HttpContext.GetOwinContext().Authentication.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);

                    ModelState.Clear();
                    string userIP = Request.UserHostAddress;
                    sec.ResetAttempts(userIP);
                    return RedirectToAction("Perfil", "Users");
                }
                else
                {
                    ModelState.Clear();
                    string userIP = Request.UserHostAddress;
                    sec.RegisterLoginAttempt(userIP);
                    return RedirectToAction("LoginInvalido", "Users");
                }
            }
            else
            {
                return RedirectToAction("Perfil", "Users");
            }
        }

        [OutputCache(Duration = 1200)]
        public ActionResult PAdminEsp()
        {
            if (User.Identity.IsAuthenticated == true && User.IsInRole("Admin"))
            {
                return View("PanelAdmin");
            }
            else
            {
                return RedirectToAction("Perfil", "Users");
            }
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
            if (User.Identity.IsAuthenticated == true && User.IsInRole("Admin"))
            {
                using (var context = new DataContext())
                {
                    var selection = (from s in context.appSettings where s.ID == 1 select s).FirstOrDefault();
                    return View(selection);
                }
            }
            else
            {
                return RedirectToAction("Perfil", "Users");
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

        [HttpGet]
        public ActionResult UserLogin()
        {
            if(User.Identity.IsAuthenticated == true)
            {
                return RedirectToAction("UserProfile", "Users");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(User model)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                bool loginValidation = sec.PasswordMatch(model.username, model.password);
                string userName = df.FirstLetterToUpper(model.username);
                bool userRole = uoperations.LoadUserRole(userName);
                string roleName = null;
                if(userRole == true)
                {
                    roleName = "Admin";
                }
                else
                {
                    roleName = "User";
                }

                if (loginValidation == true)
                {

                    var ident = new ClaimsIdentity(
                    new[]
                    {
            // adding following 2 claim just for supporting default antiforgery provider
                new Claim(ClaimTypes.NameIdentifier, userName),
                new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

            // an optional claim you could omit this 
                new Claim(ClaimTypes.Name, userName),

            // you could even add some role
                new Claim(ClaimTypes.Role, roleName),
                        // and so on
                    },
                    DefaultAuthenticationTypes.ApplicationCookie);

                    // Identity is sign in user based on claim don't matter 
                    // how you generated it Identity 
                    HttpContext.GetOwinContext().Authentication.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);

                    ModelState.Clear();
                    string userIP = Request.UserHostAddress;
                    sec.ResetAttempts(userIP);
                    return RedirectToAction("UserProfile", "Users");
                }
                else
                {
                    ModelState.Clear();
                    string userIP = Request.UserHostAddress;
                    sec.RegisterLoginAttempt(userIP);
                    return RedirectToAction("InvalidLogin", "Users");
                }
            }
            else
            {
                return RedirectToAction("UserProfile", "Users");
            }
        }

        [OutputCache(Duration = 1200)]
        public ActionResult PAdminEng()
        {
            if (User.Identity.IsAuthenticated == true && User.IsInRole("Admin"))
            {
                return View("AdminPanel");
            }
            else
            {
                return RedirectToAction("UserProfile", "Users");
            }
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
            if (User.Identity.IsAuthenticated == true && User.IsInRole("Admin"))
            {
                using (var context = new DataContext())
                {
                    var selection = (from s in context.appSettings where s.ID == 1 select s).FirstOrDefault();
                    return View("AdminPrivileges", selection);
                }
            }
            else
            {
                return RedirectToAction("UserProfile", "Users");
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