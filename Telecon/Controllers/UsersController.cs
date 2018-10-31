using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telecon.Models;


namespace Telecon.Controllers
{
   
    public class UsersController : Controller
    {
        // GET: Users
        
        public ActionResult Crear()
        {
            return View("CrearUsuario", new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FormCrear(User modelo)
        {

          

            string usuario = modelo.username;
            string nombres = modelo.firstnames;
            string apellidos = modelo.lastnames;
            string correo = modelo.email;
            int edad = modelo.age;
            string direccion = modelo.address;
            string telefono = modelo.number;


            using (var context = new DataContext())
            {
                var user = new User
                {
                    username = usuario,
                    password = "holajohnnycageyolo",
                    firstnames = nombres,
                    lastnames = apellidos,
                    email = correo,
                    age = edad,
                    address = direccion,
                    number = telefono,
                    admin = true
                };

                context.Usuarios.Add(user);
                context.SaveChanges();

            }

            return RedirectToAction("perfil", "users");
            
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