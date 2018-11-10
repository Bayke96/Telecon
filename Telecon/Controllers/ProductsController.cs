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

        public ActionResult Main()
        {
            return View("Productos");
        }
        public ActionResult MainPage()
        {
            return View("Products");
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

        public ActionResult Eliminar()
        {
            return View("EliminarProducto");
        }
        public ActionResult Producto()
        {
            return View();
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
        public ActionResult Delete()
        {
            return View("DeleteProduct");
        }
        public ActionResult Product()
        {
            return View();
        }
    }
}