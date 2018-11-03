using System.Linq;
using Telecon.Models;
using Telecon.Encryption;
using Telecon.Data_Formatting;
using System.Globalization;
using Telecon.CRUD_Operations;

namespace Telecon.Model_Operations
{
    public class UserCRUD
    {
        Email email = new Email();

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

        public void AddUser(User modelo, bool isAdmin = false, string path = null)
        {
            Security sec = new Security();
            DataFormats df = new DataFormats();

            TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;

            var usuario = char.ToUpper(modelo.username.First()) + modelo.username.Substring(1).ToLower().Trim();
            var contraseña = df.GenerateString(12);
            var nombres = cultInfo.ToTitleCase(modelo.firstnames).Trim();
            var apellidos = cultInfo.ToTitleCase(modelo.lastnames).Trim();
            var direccion = df.AddressCorrector(modelo.address).Trim();
            var correo = modelo.email.ToLower().Trim();
            var edad = modelo.age;
            var telefono = modelo.number.Trim();
           
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
                    admin = isAdmin,
                    picturePath = path,
                };

                context.Usuarios.Add(empleado);
                context.SaveChanges();
            }
            email.SendPasswordEmail(correo, usuario, contraseña);
        }

        // Update an user - Actualizar usuario

        public void UpdateUser(User modelo, bool isAdmin = false, string path = null)
        {
            TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;

            int id = modelo.ID;
            var usuario = char.ToUpper(modelo.username.First()) + modelo.username.Substring(1).ToLower().Trim();
            var nombres = cultInfo.ToTitleCase(modelo.firstnames).Trim();
            var apellidos = cultInfo.ToTitleCase(modelo.lastnames).Trim();
            var correo = modelo.email.ToLower().Trim();
            var edad = modelo.age;
            var telefono = modelo.number.Trim();
          
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
                search.admin = isAdmin;
                if(path != null) search.picturePath = path;

                context.SaveChanges();
            }
        }

        // Deletes an user from database - Eliminar usuarios de la base de datos

        public void DeleteUser(int id)
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios where s.ID == id select s).SingleOrDefault();
                context.Usuarios.Remove(search);
                context.SaveChanges();
            }
        }

    }
}