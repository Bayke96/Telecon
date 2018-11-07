using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telecon.Models;

namespace Telecon.CRUD_Operations
{
    public class AppSettings
    {
        public void UpdateProductSettings(Settings modelo, bool add = false, bool edit = false, bool delete = false)
        {
            using(var context = new DataContext())
            {
                var selection = (from s in context.appSettings where s.ID == 1 select s).FirstOrDefault();
                if (selection == null)
                {
                    var settings = new Settings
                    {
                       ID = 1,
                       precioMinimo = modelo.precioMinimo,
                       precioMaximo = modelo.precioMaximo,
                       adicionProductos = add,
                       modificacionProductos = edit,
                       eliminarProductos = delete
                    };

                    context.appSettings.Add(settings);
                    context.SaveChanges();
                }

                if(selection != null)
                {
                    selection.precioMinimo = modelo.precioMinimo;
                    selection.precioMaximo = modelo.precioMaximo;
                    selection.adicionProductos = add;
                    selection.modificacionProductos = edit;
                    selection.eliminarProductos = delete;

                    context.SaveChanges();
                }
            }
        }

        public void UpdateAccountSettings(bool profilePics = false, bool passwordChange = false, bool emailChange = false, 
            bool userAccess = false)
        {
            using (var context = new DataContext())
            {
                var selection = (from s in context.appSettings where s.ID == 1 select s).FirstOrDefault();
                if (selection == null)
                {
                    var settings = new Settings
                    {
                        ID = 1,
                        precioMinimo = null,
                        precioMaximo = null,
                        adicionProductos = true,
                        modificacionProductos = true,
                        eliminarProductos = true,
                        fotosPerfil = profilePics,
                        cambioContraseña = passwordChange,
                        cambioCorreo = emailChange,
                        accesoUsuario = userAccess
                    };

                    context.appSettings.Add(settings);
                    context.SaveChanges();
                }

                if (selection != null)
                {
                    selection.fotosPerfil = profilePics;
                    selection.cambioContraseña = passwordChange;
                    selection.cambioCorreo = emailChange;
                    selection.accesoUsuario = userAccess;

                    context.SaveChanges();
                }
            }
        }

    }
}