using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telecon.Models;
using Telecon.Data_Formatting;
using Telecon.Encryption;


namespace Telecon.Controllers
{   
    public class UsersController : Controller
    {
        DataFormats df = new DataFormats();
        Security sec = new Security();

        // GET: Users

        [HttpGet]
        public ActionResult Crear()
        {
            return View("CrearUsuario");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(User modelo, bool IsAdmin = false)
        {

            TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;

            var usuario = char.ToUpper(modelo.username.First()) + modelo.username.Substring(1).ToLower().Trim();
            var contraseña = df.GenerateString(12);
            var nombres = cultInfo.ToTitleCase(modelo.firstnames).Trim();
            var apellidos = cultInfo.ToTitleCase(modelo.lastnames).Trim();
            var direccion = df.AddressCorrector(modelo.address).Trim();
            var correo = modelo.email.ToLower().Trim();
            var edad = modelo.age;
            var telefono = modelo.number.Trim();
            
            using (var context = new DataContext())
                {
                    var empleado = new User
                    {
                        username = usuario,
                        password = sec.EncryptPassword(contraseña),
                        firstnames = nombres,
                        lastnames = apellidos,
                        address = direccion,
                        age = edad,
                        email = correo,
                        number = telefono,
                        admin = IsAdmin
                    };

                    context.Usuarios.Add(empleado);
                    context.SaveChanges();

                }

            ModelState.Clear();
            return View("CrearUsuario");            
        }

        [HttpGet]
        public ActionResult Visualizar()
        {
            return View("VisualizarUsuarios");
        }

        [HttpGet]
        public ActionResult Editar()
        {
            return View("EditarUsuario");
        }

        [HttpGet]
        public ActionResult Eliminar()
        {
            return View("EliminarUsuario");
        }

        [HttpGet]
        public ActionResult Olvidar()
        {
            return View("OlvidarContraseña");
        }

      
        public ActionResult Perfil()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ActualizarPerfil()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CambiarContraseña()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MisClientes()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Chat()
        {
            return View();
        }

        /* ENGLISH VERSION */

        [HttpGet]
        public ActionResult Create()
        {
            return View("CreateUser");
        }

        [HttpGet]
        public ActionResult ViewUsers()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View("EditUser");
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return View("DeleteUser");
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        [HttpGet]
        public ActionResult UserProfile()
        {
            return View("Profile");
        }

        [HttpGet]
        public ActionResult UpdateProfile()
        {
            return View("UpdateProfile");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View("ChangePassword");
        }

        [HttpGet]
        public ActionResult Customers()
        {
            return View();
        }

        [HttpGet]
        public ActionResult OnlineChat()
        {
            return View();
        }

    }




    
}