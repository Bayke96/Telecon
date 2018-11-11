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
            using(var context = new DataContext())
            {
                var selection = (from s in context.appSettings where s.ID == 1 select s).FirstOrDefault();
                return View("AdministrarProductos", selection);
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
            using (var context = new DataContext())
            {
                var selection = (from s in context.appSettings where s.ID == 1 select s).FirstOrDefault();
                return View("AdminProducts", selection);
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
                var search = (from s in context.Productos orderby s.ID ascending select s).Take(12).ToList();
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
            return View("NuevoProducto");
        }

        [HttpGet]
        public ActionResult Modificar()
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.Productos select s).ToList();
                return View("ModificarProducto", search);
            }
        }

        [HttpGet]
        public ActionResult Eliminar()
        {
            using(var context = new DataContext())
            {
                var search = (from s in context.Productos select s).ToList();
                return View("EliminarProducto", search);
            }
        }

        public ActionResult MainPage()
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.Productos orderby s.ID ascending select s).Take(12).ToList();
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

        public ActionResult Add()
        {
            return View("AddProduct");
        }
        public ActionResult Edit()
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.Productos select s).ToList();
                return View("EditProduct", search);
            }
        }

        [HttpGet]
        public ActionResult Delete()
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.Productos select s).ToList();
                return View("DeleteProduct", search);
            }
        }
    }
}