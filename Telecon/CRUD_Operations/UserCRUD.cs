using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telecon.Models;
using Telecon.Encryption;
using Telecon.Data_Formatting;
using System.IO;

namespace Telecon.Model_Operations
{
    public class UserCRUD
    {

        // Search for an user's ID - Busca el ID de un usuario

        public int SearchID(string userName)
        {
            int id = 0;
            
            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios where s.username == userName select s.ID).SingleOrDefault();
                id = search;
            }
            return id;

        }
       
        // Agregar un nuevo usuario al sitio web - Adds a new user to the website.

        public void AddUser(string usuario, string contraseña, string nombres, string apellidos, string direccion, int edad, 
            string correo, string telefono, bool IsAdmin, string imagen)
        {
            Security sec = new Security();

            using (var context = new DataContext())
            {
                var empleado = new User
                {
                    username = usuario,
                    password = sec.EncryptPassword(contraseña),
                    firstnames = nombres,
                    lastnames = apellidos,
                    address = direccion,
                    age = edad,
                    email = correo,
                    number = telefono,
                    admin = IsAdmin,
                    picturePath = imagen,
                };

                context.Usuarios.Add(empleado);
                context.SaveChanges();

            }  
        }

        // Update an user - Actualizar usuario

        public void UpdateUser(int id, string usuario, string nombres, string apellidos, int edad,
            string correo, string telefono, bool IsAdmin, string imagen = null)
        {
            Security sec = new Security();

            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios where s.ID == id select s).FirstOrDefault();
                
                // Updating user data

                search.username = usuario;
                search.firstnames = nombres;
                search.lastnames = apellidos;
                search.age = edad;
                search.email = correo;
                search.number = telefono;
                search.admin = IsAdmin;
                if(imagen != null) search.picturePath = imagen;

                context.SaveChanges();

            }
        }

    }
}