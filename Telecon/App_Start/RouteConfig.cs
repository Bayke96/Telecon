using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                defaults: new { controller = "Products", action = "Eliminar" }
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
             url: "es/admin/usuarios/editarusuario/{id}",
             defaults: new { controller = "Users", action = "Editar", id = @"[0-9+]" }
            );

            routes.MapRoute(
            name: "EliminarUsuario",
            url: "es/admin/usuarios/eliminarusuario/{id}",
            defaults: new { controller = "Users", action = "Eliminar", id = @"[0-9+]" }
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
            url: "es/productos/{id}",
            defaults: new { controller = "Products", action = "Main", id = @"[0-9+]" }
            );

            routes.MapRoute(
            name: "AgregarProducto",
            url: "es/producto/agregar",
            defaults: new { controller = "Products", action = "Agregar" }
            );

            routes.MapRoute(
            name: "ModificarProducto",
            url: "es/producto/modificar",
            defaults: new { controller = "Products", action = "Modificar" }
            );

            routes.MapRoute(
            name: "EliminarProducto",
            url: "es/producto/eliminar",
            defaults: new { controller = "Products", action = "Eliminar" }
            );

            routes.MapRoute(
            name: "DetallesProducto",
            url: "es/producto/{id}",
            defaults: new { controller = "Products", action = "Producto", id = @"[0-9+]" }
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
            url: "en/admin/users/edituser/{id}",
            defaults: new { controller = "Users", action = "Edit", id = @"[0-9+]" }
            );

            routes.MapRoute(
              name: "DeleteUser",
              url: "en/admin/users/deleteuser/{id}",
              defaults: new { controller = "Users", action = "Delete", id = @"[0-9+]" }
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
            url: "en/products/{id}",
            defaults: new { controller = "Products", action = "MainPage", id = @"[0-9+]" }
            );

            routes.MapRoute(
            name: "AddProduct",
            url: "en/product/add",
            defaults: new { controller = "Products", action = "Add" }
            );

            routes.MapRoute(
            name: "EditProduct",
            url: "en/product/edit",
            defaults: new { controller = "Products", action = "Edit" }
            );

            routes.MapRoute(
            name: "DeleteProduct",
            url: "en/product/delete",
            defaults: new { controller = "Products", action = "Delete" }
            );

            routes.MapRoute(
           name: "ProductDetails",
           url: "en/product/{id}",
           defaults: new { controller = "Products", action = "Product", id = @"[0-9+]" }
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

            routes.MapRoute(
             name: "UsernameJSON",
             url: "json/username",
             defaults: new { controller = "JSON", action = "UserExists" }
            );

            routes.MapRoute(
             name: "EmailJSON",
             url: "json/email",
             defaults: new { controller = "JSON", action = "EmailRegistered" }
            );

            routes.MapRoute(
             name: "idJSON",
             url: "json/id",
             defaults: new { controller = "JSON", action = "UserID" }
            );

            routes.MapRoute(
            name: "OrderUsersJSON",
            url: "json/sort/orderusersby",
            defaults: new { controller = "JSON", action = "OrderUsers" }
           );

            routes.MapRoute(
             name: "SortUsersIDJSON",
             url: "json/sort/userid",
             defaults: new { controller = "JSON", action = "SearchUserByID" }
            );

            routes.MapRoute(
             name: "SortUsersNamesJSON",
             url: "json/sort/usernames",
             defaults: new { controller = "JSON", action = "SearchUserByNames" }
            );

            routes.MapRoute(
            name: "SortEmailsJSON",
            url: "json/sort/email",
            defaults: new { controller = "JSON", action = "SearchUserByEmail" }
           );

            routes.MapRoute(
           name: "SortPhonesJSON",
           url: "json/sort/phone",
           defaults: new { controller = "JSON", action = "SearchUserByPhone" }
          );

            routes.MapRoute(
         name: "SearchProductJSON",
         url: "json/verifyproduct",
         defaults: new { controller = "JSON", action = "ProductValidations" }
        );


            routes.MapRoute(
         name: "AddProductJSON",
         url: "json/agregarproducto",
         defaults: new { controller = "JSON", action = "AgregarProducto" }
        );

            routes.MapRoute(
        name: "LoadProductJSON",
        url: "json/cargarproducto",
        defaults: new { controller = "JSON", action = "CargarProducto" }
       );

        routes.MapRoute(
        name: "EditProductJSON",
        url: "json/editarproducto",
        defaults: new { controller = "JSON", action = "EditarProducto" }
       );

            routes.MapRoute(
        name: "DeleteProductJSON",
        url: "json/eliminarproducto",
        defaults: new { controller = "JSON", action = "EliminarProducto" }
       );

            routes.MapRoute(
       name: "ProductListJSON",
       url: "json/listaproductos",
       defaults: new { controller = "JSON", action = "ListaProductos" }
      );

            routes.MapRoute(
            "404-PageNotFound",
            "{*url}",
            new { controller = "Home", action = "PageNotFound" }
            );

        }





    }
}
