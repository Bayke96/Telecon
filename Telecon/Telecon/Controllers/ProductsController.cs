using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}