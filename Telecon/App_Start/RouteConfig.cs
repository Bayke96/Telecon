using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Telecon
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Users", action = "Crear" }
            );

            routes.MapRoute(
                name: "Inicio",
                url: "es/inicio",
                defaults: new { controller = "Home", action = "Principal" }
            );

            routes.MapRoute(
               name: "Conectar",
               url: "es/conectar",
               defaults: new { controller = "Home", action = "Login" }
           );

            routes.MapRoute(
               name: "CrearUsuario",
               url: "es/admin/usuarios/crearusuario",
               defaults: new { controller = "Users", action = "Crear" }
           );

            routes.MapRoute(
              name: "PanelAdmin",
              url: "es/admin",
              defaults: new { controller = "Home", action = "PAdminEsp" }
            );

            routes.MapRoute(
              name: "VisualizarUsuarios",
              url: "es/admin/usuarios",
              defaults: new { controller = "Users", action = "Visualizar" }
            );

            routes.MapRoute(
             name: "EditarUsuario",
             url: "es/admin/usuarios/editarusuario",
             defaults: new { controller = "Users", action = "Editar" }
            );

            routes.MapRoute(
            name: "EliminarUsuario",
            url: "es/admin/usuarios/eliminarusuario",
            defaults: new { controller = "Users", action = "Eliminar" }
            );

            routes.MapRoute(
            name: "InformacionContacto",
             url: "es/contacto",
            defaults: new { controller = "Home", action = "Contacto" }
            );

            routes.MapRoute(
            name: "InformacionGeneral",
            url: "es/info",
            defaults: new { controller = "Home", action = "General" }
            );

            routes.MapRoute(
            name: "Servicios",
            url: "es/servicios",
            defaults: new { controller = "Home", action = "Servicios" }
            );

            routes.MapRoute(
            name: "ProductosPrincipal",
            url: "es/productos",
            defaults: new { controller = "Products", action = "Main" }
            );

            routes.MapRoute(
            name: "AgregarProducto",
            url: "es/productos/agregar",
            defaults: new { controller = "Products", action = "Agregar" }
            );

            routes.MapRoute(
            name: "ModificarProducto",
            url: "es/productos/modificar",
            defaults: new { controller = "Products", action = "Modificar" }
            );

            routes.MapRoute(
            name: "EliminarProducto",
            url: "es/productos/eliminar",
            defaults: new { controller = "Products", action = "Eliminar" }
            );

            routes.MapRoute(
            name: "DetallesProducto",
            url: "es/producto",
            defaults: new { controller = "Products", action = "Producto" }
            );

            routes.MapRoute(
            name: "AdminProductos",
            url: "es/admin/productos",
            defaults: new { controller = "Products", action = "Administrar" }
            );

            routes.MapRoute(
            name: "Privilegios",
            url: "es/admin/privilegios",
            defaults: new { controller = "Home", action = "Privilegios" }
            );

            routes.MapRoute(
            name: "Cercos",
            url: "es/cercoselectricos",
            defaults: new { controller = "Home", action = "Cercos" }
            );

            routes.MapRoute(
            name: "Acceso",
            url: "es/accesoautorizado",
            defaults: new { controller = "Home", action = "EquiposAcceso" }
            );

            routes.MapRoute(
            name: "Portones",
            url: "es/portones",
            defaults: new { controller = "Home", action = "Portones" }
            );

            routes.MapRoute(
            name: "Centrales",
            url: "es/centralestelefonicas",
            defaults: new { controller = "Home", action = "centrales" }
            );

            routes.MapRoute(
            name: "OlvidarContraseña",
            url: "es/recuperarcontraseña",
            defaults: new { controller = "Users", action = "Olvidar" }
            );

            routes.MapRoute(
            name: "MiPerfil",
            url: "es/perfil",
            defaults: new { controller = "Users", action = "Perfil" }
            );

            routes.MapRoute(
            name: "ActualizarPerfil",
            url: "es/perfil/actualizarperfil",
            defaults: new { controller = "Users", action = "ActualizarPerfil" }
            );

            routes.MapRoute(
            name: "CambiarContraseña",
            url: "es/perfil/cambiarcontraseña",
            defaults: new { controller = "Users", action = "CambiarContraseña" }
            );

            routes.MapRoute(
            name: "MisClientes",
            url: "es/perfil/clientes",
            defaults: new { controller = "Users", action = "MisClientes" }
            );

            routes.MapRoute(
            name: "Chat",
            url: "es/chat",
            defaults: new { controller = "Users", action = "Chat" }
            );




            /* ENGLISH VERSION */

            routes.MapRoute(
                 name: "Home",
                 url: "en/home",
                 defaults: new { controller = "Home", action = "MainPage" }
             );

            routes.MapRoute(
                 name: "UserLogin",
                 url: "en/login",
                 defaults: new { controller = "Home", action = "UserLogin" }
             );

                routes.MapRoute(
              name: "CreateUser",
              url: "en/admin/users/createuser",
              defaults: new { controller = "Users", action = "Create" }
            );

            routes.MapRoute(
              name: "PAdmin",
              url: "en/admin",
              defaults: new { controller = "Home", action = "PAdminEng" }
            );

            routes.MapRoute(
              name: "Users",
              url: "en/admin/users",
              defaults: new { controller = "Users", action = "ViewUsers" }
            );


            routes.MapRoute(
            name: "EditUser",
            url: "en/admin/users/edituser",
            defaults: new { controller = "Users", action = "Edit" }
            );

            routes.MapRoute(
              name: "DeleteUser",
              url: "en/admin/users/deleteuser",
              defaults: new { controller = "Users", action = "Delete" }
            );


            routes.MapRoute(
            name: "ContactInfo",
            url: "en/contact",
            defaults: new { controller = "Home", action = "Contact" }
            );

            routes.MapRoute(
            name: "GeneralInfo",
            url: "en/info",
            defaults: new { controller = "Home", action = "GeneralInfo" }
            );

            routes.MapRoute(
            name: "Services",
            url: "en/services",
            defaults: new { controller = "Home", action = "Services" }
            );

            routes.MapRoute(
            name: "Products",
            url: "en/products",
            defaults: new { controller = "Products", action = "MainPage" }
            );

            routes.MapRoute(
            name: "AddProduct",
            url: "en/products/add",
            defaults: new { controller = "Products", action = "Add" }
            );

            routes.MapRoute(
            name: "EditProduct",
            url: "en/products/edit",
            defaults: new { controller = "Products", action = "Edit" }
            );

            routes.MapRoute(
            name: "DeleteProduct",
            url: "en/products/delete",
            defaults: new { controller = "Products", action = "Delete" }
            );

            routes.MapRoute(
           name: "ProductDetails",
           url: "en/product",
           defaults: new { controller = "Products", action = "Product" }
           );

            routes.MapRoute(
            name: "ProductsAdmin",
            url: "en/admin/products",
            defaults: new { controller = "Products", action = "Admin" }
            );

            routes.MapRoute(
            name: "Privileges",
            url: "en/admin/privileges",
            defaults: new { controller = "Home", action = "Privileges" }
            );

            routes.MapRoute(
            name: "ElectricFences",
            url: "en/electricfences",
            defaults: new { controller = "Home", action = "ElectricFences" }
            );

            routes.MapRoute(
            name: "CCTV",
            url: "en/cctv",
            defaults: new { controller = "Home", action = "CCTV" }
            );

            routes.MapRoute(
            name: "SlidingGates",
            url: "en/sliding-gates",
            defaults: new { controller = "Home", action = "SlidingGates" }
            );

            routes.MapRoute(
            name: "PhoneExchange",
            url: "en/telephone-exchange",
            defaults: new { controller = "Home", action = "PhoneExchange" }
            );

            routes.MapRoute(
            name: "ForgotPassword",
            url: "en/forgotpassword",
            defaults: new { controller = "Users", action = "ForgotPassword" }
            );


            routes.MapRoute(
            name: "Profile",
            url: "en/profile",
            defaults: new { controller = "Users", action = "UserProfile" }
            );

            routes.MapRoute(
            name: "UpdateProfile",
            url: "en/profile/update",
            defaults: new { controller = "Users", action = "UpdateProfile" }
            );

            routes.MapRoute(
            name: "ChangePassword",
            url: "en/profile/changepassword",
            defaults: new { controller = "Users", action = "ChangePassword" }
            );

            routes.MapRoute(
            name: "Customers",
            url: "en/profile/customers",
            defaults: new { controller = "Users", action = "Customers" }
            );

            routes.MapRoute(
            name: "OnlineChat",
            url: "en/chat",
            defaults: new { controller = "Users", action = "OnlineChat" }
            );


        }





    }
}
