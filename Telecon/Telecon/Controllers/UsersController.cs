using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Telecon.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Crear()
        {
            return View("CrearUsuario");
        }

        public ActionResult Visualizar()
        {
            return View("VisualizarUsuarios");
        }

        public ActionResult Editar()
        {
            return View("EditarUsuario");
        }

        public ActionResult Eliminar()
        {
            return View("EliminarUsuario");
        }
        public ActionResult Olvidar()
        {
            return View("OlvidarContraseña");
        }

        public ActionResult Perfil()
        {
            return View();
        }

        public ActionResult ActualizarPerfil()
        {
            return View();
        }

        public ActionResult CambiarContraseña()
        {
            return View();
        }

        public ActionResult MisClientes()
        {
            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }

        /* ENGLISH VERSION */

        public ActionResult Create()
        {
            return View("CreateUser");
        }

        public ActionResult ViewUsers()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View("EditUser");
        }
        public ActionResult Delete()
        {
            return View("DeleteUser");
        }
        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }
        public ActionResult UserProfile()
        {
            return View("Profile");
        }
        public ActionResult UpdateProfile()
        {
            return View("UpdateProfile");
        }
        public ActionResult ChangePassword()
        {
            return View("ChangePassword");
        }
        public ActionResult Customers()
        {
            return View();
        }
        public ActionResult OnlineChat()
        {
            return View();
        }

    }
}