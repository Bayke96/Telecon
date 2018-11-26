using System.Web.Mvc;
using Telecon.Models;
using System.Linq;
using Telecon.CRUD_Operations;

namespace Telecon.Controllers
{


    public class ProductsController : Controller
    {

       AppSettings settings = new AppSettings();
        // GET: Products
       [HttpGet]
       public ActionResult Administrar()
        {
            if (User.Identity.IsAuthenticated == true && User.IsInRole("Admin"))
            {
                using (var context = new DataContext())
                {
                    var selection = (from s in context.appSettings where s.ID == 1 select s).FirstOrDefault();
                    return View("AdministrarProductos", selection);
                }
            }
            else
            {
                return RedirectToAction("Perfil", "Users");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Administrar(Settings modelo, bool add = false, bool edit = false, bool delete = false)
        {
            if (modelo.aumentarPrecios != null) settings.UpdateProductPrices(modelo.aumentarPrecios);
            settings.UpdateProductSettings(modelo, add, edit, delete);
            using (var context = new DataContext())
            {
                var selection = (from s in context.appSettings where s.ID == 1 select s).FirstOrDefault();
                return View("AdministrarProductos", selection);
            }
        }

        [HttpGet]
        public ActionResult Admin()
        {
            if (User.Identity.IsAuthenticated == true && User.IsInRole("Admin"))
            {
                using (var context = new DataContext())
                {
                    var selection = (from s in context.appSettings where s.ID == 1 select s).FirstOrDefault();
                    return View("AdminProducts", selection);
                }
            }
            else
            {
                return RedirectToAction("UserProfile", "Users");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Admin(Settings modelo, bool add = false, bool edit = false, bool delete = false)
        {
            if (modelo.aumentarPrecios != null) settings.UpdateProductPrices(modelo.aumentarPrecios);
            settings.UpdateProductSettings(modelo, add, edit, delete);
            using (var context = new DataContext())
            {
                var selection = (from s in context.appSettings where s.ID == 1 select s).FirstOrDefault();
                return View("AdminProducts", selection);
            }
        }

        public ActionResult Main(int id)
        {
            using (var context = new DataContext())
            {
                
                var search = (from s in context.Productos orderby s.ID ascending select s).Skip(12 * (id - 1)).Take(20).ToList();
                return View("Productos", search);
            }
        }

        public ActionResult Producto(int id)
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.Productos where s.ID == id select s).SingleOrDefault();
                return View(search);
            }
        }
        

        [HttpGet]
        public ActionResult Agregar()
        {
            if(User.Identity.IsAuthenticated == true)
            {
                return View("NuevoProducto");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public ActionResult Modificar()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                using (var context = new DataContext())
                {
                    var search = (from s in context.Productos select s).ToList();
                    return View("ModificarProducto", search);
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public ActionResult Eliminar()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                using (var context = new DataContext())
                {
                    var search = (from s in context.Productos select s).ToList();
                    return View("EliminarProducto", search);
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult MainPage(int id)
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.Productos orderby s.ID ascending select s).Skip(12 * (id - 1)).Take(20).ToList();
                return View("Products", search);
            }
        }
        public ActionResult Product(int id)
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.Productos where s.ID == id select s).SingleOrDefault();
                return View(search);
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                return View("AddProduct");
            }
            else
            {
                return RedirectToAction("UserLogin", "Home");
            }
        }

        [HttpGet]
        public ActionResult Edit()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                using (var context = new DataContext())
                {
                    var search = (from s in context.Productos select s).ToList();
                    return View("EditProduct", search);
                }
            }
            else
            {
                return RedirectToAction("UserLogin", "Home");
            }
        }

        [HttpGet]
        public ActionResult Delete()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                using (var context = new DataContext())
                {
                    var search = (from s in context.Productos select s).ToList();
                    return View("DeleteProduct", search);
                }
            }
            else
            {
                return RedirectToAction("UserLogin", "Home");
            }
        }
    }
}