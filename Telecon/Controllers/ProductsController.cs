using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Telecon.Controllers
{


    public class ProductsController : Controller
    {
        // GET: Products
       public ActionResult Administrar()
        {
            return View("AdministrarProductos");
        }
        public ActionResult Admin()
        {
            return View("AdminProducts");
        }
        public ActionResult Main()
        {
            return View("Productos");
        }
        public ActionResult MainPage()
        {
            return View("Products");
        }
        
        public ActionResult Agregar()
        {
            return View("NuevoProducto");
        }

        public ActionResult Modificar()
        {
            return View("ModificarProducto");
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
            return View("EditProduct");
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