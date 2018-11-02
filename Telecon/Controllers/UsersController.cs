using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telecon.Models;
using Telecon.Data_Formatting;
using Telecon.Model_Operations;
using System.Threading.Tasks;
using System.IO;

namespace Telecon.Controllers
{   
    public class UsersController : Controller
    {
        DataFormats df = new DataFormats();
        UserCRUD operations = new UserCRUD();

        // GET: Users

        [HttpGet]
        public ActionResult Crear()
        {
            return View("CrearUsuario");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(User modelo, HttpPostedFileBase file, bool IsAdmin = false)
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

            operations.AddUser(usuario, contraseña, nombres, apellidos, direccion, edad, correo, telefono, IsAdmin, path);
            ModelState.Clear();

            return View("CrearUsuario");              
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            using(var context = new DataContext())
            {
                var search = (from s in context.Usuarios where s.ID == id select s).SingleOrDefault();
                return View("EditarUsuario", search);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(User modelo, HttpPostedFileBase uploadFile, bool IsAdmin = false)
        {
            TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;

            int id = modelo.ID;
            var usuario = char.ToUpper(modelo.username.First()) + modelo.username.Substring(1).ToLower().Trim();
            var nombres = cultInfo.ToTitleCase(modelo.firstnames).Trim();
            var apellidos = cultInfo.ToTitleCase(modelo.lastnames).Trim();
            var correo = modelo.email.ToLower().Trim();
            var edad = modelo.age;
            var telefono = modelo.number.Trim();
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

            if (uploadFile == null) operations.UpdateUser(id, usuario, nombres, apellidos, edad, correo, telefono, IsAdmin);
            if (uploadFile != null) operations.UpdateUser(id, usuario, nombres, apellidos, edad, correo, telefono, IsAdmin, path);

            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios where s.ID == id select s).FirstOrDefault();
                return View("EditarUsuario", search);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios where s.ID == id select s).SingleOrDefault();
                return View("EliminarUsuario", search);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(User modelo)
        {
            operations.DeleteUser(modelo.ID);
            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios select s).ToList();
                return View("VisualizarUsuarios", search);
            }
        }

        [HttpGet]
        public ActionResult Visualizar()
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios select s).ToList();
                return View("VisualizarUsuarios", search);
            }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User modelo, HttpPostedFileBase file, bool IsAdmin = false)
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

            operations.AddUser(usuario, contraseña, nombres, apellidos, direccion, edad, correo, telefono, IsAdmin, path);
            ModelState.Clear();

            return View("CreateUser");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios where s.ID == id select s).SingleOrDefault();
                return View("EditUser", search);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User modelo, HttpPostedFileBase uploadFile, bool IsAdmin = false)
        {
            TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;

            int id = modelo.ID;
            var usuario = char.ToUpper(modelo.username.First()) + modelo.username.Substring(1).ToLower().Trim();
            var nombres = cultInfo.ToTitleCase(modelo.firstnames).Trim();
            var apellidos = cultInfo.ToTitleCase(modelo.lastnames).Trim();
            var correo = modelo.email.ToLower().Trim();
            var edad = modelo.age;
            var telefono = modelo.number.Trim();
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

            if (uploadFile == null) operations.UpdateUser(id, usuario, nombres, apellidos, edad, correo, telefono, IsAdmin);
            if (uploadFile != null) operations.UpdateUser(id, usuario, nombres, apellidos, edad, correo, telefono, IsAdmin, path);

            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios where s.ID == id select s).FirstOrDefault();
                return View("EditUser", search);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios where s.ID == id select s).SingleOrDefault();
                return View("DeleteUser", search);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(User modelo)
        {
            operations.DeleteUser(modelo.ID);
            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios select s).ToList();
                return View("ViewUsers", search);
            }
        }

        [HttpGet]
        public ActionResult ViewUsers()
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios select s).ToList();
                return View(search);
            }
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