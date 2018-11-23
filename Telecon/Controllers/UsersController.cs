using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telecon.Models;
using Telecon.Model_Operations;
using System.IO;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using Telecon.Encryption;

namespace Telecon.Controllers
{   
    
    public class UsersController : Controller
    {
        UserCRUD operations = new UserCRUD();
        
        // GET: Users

        [HttpGet]
        public ActionResult Crear()
        {
            if(User.Identity.IsAuthenticated == true && User.IsInRole("Admin"))
            {
                return View("CrearUsuario");
            }
            else
            {
                return RedirectToAction("Perfil");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(User modelo, HttpPostedFileBase file, bool IsAdmin = false)
        {
            string path = "";

            if (file != null)
            {
                string pic = Path.GetFileName(file.FileName);
                path = Path.Combine(
                                       Server.MapPath("~/Style/Media/User_Images"), pic);
                file.SaveAs(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
            operations.AddUser(modelo, IsAdmin, path);
            ModelState.Clear();

            return View("CrearUsuario");              
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            if (User.Identity.IsAuthenticated == true && User.IsInRole("Admin"))
            {
                using (var context = new DataContext())
                {
                    var search = (from s in context.Usuarios where s.ID == id select s).SingleOrDefault();
                    return View("EditarUsuario", search);
                }
            }
            else
            {
                return RedirectToAction("Perfil");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(User modelo, HttpPostedFileBase uploadFile, bool IsAdmin = false)
        {
            int id = modelo.ID;
            string path = "";

            if (uploadFile != null)
            {
                string pic = Path.GetFileName(uploadFile.FileName);
                path = Path.Combine(
                                       Server.MapPath("~/Style/Media/User_Images"), pic);
                uploadFile.SaveAs(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    uploadFile.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }

            var userName = User.Identity.GetUserId();
            int userID = 0;
            bool userRank = false;
            using (var context = new DataContext())
            {
                userID = (from s in context.Usuarios where s.username == userName select s.ID).Single();
                userRank = (from s in context.Usuarios where s.username == userName select s.admin).Single();

            if (uploadFile == null) operations.UpdateUser(modelo, IsAdmin);
            if (uploadFile != null) operations.UpdateUser(modelo, IsAdmin, path);

            if (userID == modelo.ID && userName.ToUpper() != modelo.username.ToUpper())
            {
                var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
                AuthenticationManager.SignOut();
                return RedirectToAction("Login", "Home");
            }
            if (userID == modelo.ID && userRank != IsAdmin)
            {
                var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
                AuthenticationManager.SignOut();
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var search = (from s in context.Usuarios where s.ID == id select s).FirstOrDefault();
                return View("EditarUsuario", search);
            }
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            if (User.Identity.IsAuthenticated == true && User.IsInRole("Admin"))
            {
                using (var context = new DataContext())
                {
                    var search = (from s in context.Usuarios where s.ID == id select s).SingleOrDefault();
                    return View("EliminarUsuario", search);
                }
            }
            else
            {
                return RedirectToAction("Perfil");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(User modelo)
        {
            var userName = User.Identity.GetUserId();
            int userID = 0;
            using (var context = new DataContext())
            {
                userID = (from s in context.Usuarios where s.username == userName select s.ID).Single();
            
            operations.DeleteUser(modelo.ID);

            if (userID == modelo.ID)
            {
                var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
                AuthenticationManager.SignOut();
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var search = (from s in context.Usuarios select s).ToList();
                return View("VisualizarUsuarios", search);
            }
            }
        }

        [HttpGet]
        public ActionResult Visualizar()
        {
            if (User.Identity.IsAuthenticated == true && User.IsInRole("Admin"))
            {
                using (var context = new DataContext())
                {
                    var search = (from s in context.Usuarios select s).ToList();
                    return View("VisualizarUsuarios", search);
                }
            }
            else
            {
                return RedirectToAction("Perfil");
            }
        }

        [HttpGet]
        public ActionResult Olvidar()
        { 
            if(User.Identity.IsAuthenticated == false)
            {
                return View("OlvidarContraseña");
            }
            else
            {
                return RedirectToAction("MainPage", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Olvidar(User modelo)
        {
            operations.SendPasswordRecovery(modelo.username);
            ModelState.Clear();
            return View("OlvidarContraseña");
        }

        [HttpGet]
        public ActionResult Perfil()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var userName = User.Identity.GetUserId();
                using(var context = new DataContext())
                {
                    var search = (from s in context.Usuarios where s.username == userName select s).FirstOrDefault();
                    return View(search);
                }
            }
        }

        [HttpGet]
        public ActionResult ActualizarPerfil()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string userName = User.Identity.GetUserId();
                using(var context = new DataContext())
                {
                    var profile = (from s in context.Usuarios where s.username == userName select s).FirstOrDefault();
                    return View(profile);
                }
                
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarPerfil(User model)
        {
            string userName = User.Identity.GetUserId();
            operations.UpdateProfile(userName, model);
            using (var context = new DataContext())
            {
                var profile = (from s in context.Usuarios where s.username == userName select s).FirstOrDefault();
                return View(profile);
            }
        }

        [HttpGet]
        public ActionResult CambiarContraseña()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiarContraseña(string vieja_contraseña, string nueva_contraseña, string repetir_contraseña)
        {
            Security sec = new Security();
            if (vieja_contraseña == nueva_contraseña)
            {
                return View();
            }
            if(nueva_contraseña != repetir_contraseña)
            {
                return View();
            }

            string userName = User.Identity.GetUserId();
            bool passwordMatches = sec.PasswordMatch(userName, vieja_contraseña);

            if(passwordMatches == true)
            {
                operations.ChangePassword(userName, nueva_contraseña);
                var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
                AuthenticationManager.SignOut();
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
           
        }

        [HttpGet]
        public ActionResult MisClientes()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string userName = User.Identity.GetUserId();
                using(var context = new DataContext())
                {
                    var userID = (from s in context.Usuarios where s.username == userName select s.ID).SingleOrDefault();
                    var customers = (from s in context.Clientes where s.employeeID == userID select s).ToList();
                    return View(customers);
                } 
            }
        }

        [HttpGet]
        public ActionResult Chat()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }

        /* ENGLISH VERSION */

        [HttpGet]
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated == true && User.IsInRole("Admin"))
            {
                return View("CreateUser");
            }
            else
            {
                return RedirectToAction("UserProfile");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User modelo, HttpPostedFileBase file, bool IsAdmin = false)
        {
            TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
            string path = "";

            if (file != null)
            {
                string pic = Path.GetFileName(file.FileName);
                path = Path.Combine(
                                       Server.MapPath("~/Style/Media/User_Images"), pic);
                file.SaveAs(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
            operations.AddUser(modelo, IsAdmin, path);
            ModelState.Clear();

            return View("CreateUser");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (User.Identity.IsAuthenticated == true && User.IsInRole("Admin"))
            {
                using (var context = new DataContext())
                {
                    var search = (from s in context.Usuarios where s.ID == id select s).SingleOrDefault();
                    return View("EditUser", search);
                }
            }
            else
            {
                return RedirectToAction("UserProfile");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User modelo, HttpPostedFileBase uploadFile, bool IsAdmin = false)
        {
            int id = modelo.ID;
            string path = "";

            if (uploadFile != null)
            {
                string pic = Path.GetFileName(uploadFile.FileName);
                path = Path.Combine(
                                       Server.MapPath("~/Style/Media/User_Images"), pic);
                uploadFile.SaveAs(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    uploadFile.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }

            var userName = User.Identity.GetUserId();
            int userID = 0;
            bool userRank = false;
            using (var context = new DataContext())
            {
                userID = (from s in context.Usuarios where s.username == userName select s.ID).Single();
                userRank = (from s in context.Usuarios where s.username == userName select s.admin).Single();

            if (uploadFile == null) operations.UpdateUser(modelo, IsAdmin);
            if (uploadFile != null) operations.UpdateUser(modelo, IsAdmin, path);

            if (userID == modelo.ID && userName.ToUpper() != modelo.username.ToUpper())
            {
                var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
                AuthenticationManager.SignOut();
                return RedirectToAction("UserLogin", "Home");
            }
            if (userID == modelo.ID && userRank != IsAdmin)
            {
                var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
                AuthenticationManager.SignOut();
                return RedirectToAction("UserLogin", "Home");
            }
            else
            {
                var search = (from s in context.Usuarios where s.ID == id select s).FirstOrDefault();
                return View("EditUser", search);
            }
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (User.Identity.IsAuthenticated == true && User.IsInRole("Admin"))
            {
                using (var context = new DataContext())
                {
                    var search = (from s in context.Usuarios where s.ID == id select s).SingleOrDefault();
                    return View("DeleteUser", search);
                }
            }
            else
            {
                return RedirectToAction("UserProfile");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(User modelo)
        {
            var userName = User.Identity.GetUserId();
            int userID = 0;
            using (var context = new DataContext())
            {
                userID = (from s in context.Usuarios where s.username == userName select s.ID).Single();
            
            operations.DeleteUser(modelo.ID);

            if (userID == modelo.ID)
            {
                var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
                AuthenticationManager.SignOut();
                return RedirectToAction("UserLogin", "Home");
            }
            else
            {
                var search = (from s in context.Usuarios select s).ToList();
                return View("ViewUsers", search);
            }
            }
        }

        [HttpGet]
        public ActionResult ViewUsers()
        {
            if (User.Identity.IsAuthenticated == true && User.IsInRole("Admin"))
            {
                using (var context = new DataContext())
                {
                    var search = (from s in context.Usuarios select s).ToList();
                    return View(search);
                }
            }
            else
            {
                return RedirectToAction("UserProfile");
            }
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            if(User.Identity.IsAuthenticated == false)
            {
                return View("ForgotPassword");
            }
            else
            {
                return RedirectToAction("Principal", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(User model)
        {
            operations.SendPasswordRecovery(model.username);
            ModelState.Clear();
            return View("ForgotPassword");
        }

        [HttpGet]
        public ActionResult UserProfile()
        {
            if(User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("UserLogin", "Home");
            }
            else
            {
                var userName = User.Identity.GetUserId();
                using (var context = new DataContext())
                {
                    var search = (from s in context.Usuarios where s.username == userName select s).FirstOrDefault();
                    return View("Profile", search);
                }
            }
        }

        [HttpGet]
        public ActionResult UpdateProfile()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("UserLogin", "Home");
            }
            else
            {
                string userName = User.Identity.GetUserId();
                using (var context = new DataContext())
                {
                    var profile = (from s in context.Usuarios where s.username == userName select s).FirstOrDefault();
                    return View(profile);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(User model)
        {
            string userName = User.Identity.GetUserId();
            operations.UpdateProfile(userName, model);
            using (var context = new DataContext())
            {
                var profile = (from s in context.Usuarios where s.username == userName select s).FirstOrDefault();
                return View(profile);
            }
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("UserLogin", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string vieja_contraseña, string nueva_contraseña, string repetir_contraseña)
        {
            Security sec = new Security();
            if (vieja_contraseña == nueva_contraseña)
            {
                return View();
            }
            if (nueva_contraseña != repetir_contraseña)
            {
                return View();
            }
            string userName = User.Identity.GetUserId();
            bool passwordMatches = sec.PasswordMatch(userName, vieja_contraseña);

            if (passwordMatches == true)
            {
                operations.ChangePassword(userName, nueva_contraseña);
                var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
                AuthenticationManager.SignOut();
                return RedirectToAction("UserLogin", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Customers()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("UserLogin", "Home");
            }
            else
            {
                string userName = User.Identity.GetUserId();
                using (var context = new DataContext())
                {
                    var userID = (from s in context.Usuarios where s.username == userName select s.ID).SingleOrDefault();
                    var customers = (from s in context.Clientes where s.employeeID == userID select s).ToList();
                    return View(customers);
                }
            }
        }

        [HttpGet]
        public ActionResult OnlineChat()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("UserLogin", "Home");
            }
            else
            {
                return View();
            }
        }

        // Invalid Login Attempts

        public ActionResult LoginInvalido()
        {
            return View();
        }

        public ActionResult InvalidLogin()
        {
            return View();
        }

        // User Log outs

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Perfil")]
        public ActionResult Desconectar()
        {
            var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignOut();
            return RedirectToAction("Principal", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("UserProfile")]
        public ActionResult LogOut()
        {
            var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignOut();
            return RedirectToAction("MainPage", "Home");
        }

    }
    
}