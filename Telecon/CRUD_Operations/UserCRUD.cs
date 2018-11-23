using System.Linq;
using Telecon.Models;
using Telecon.Encryption;
using Telecon.Data_Formatting;
using System.Globalization;
using Telecon.CRUD_Operations;
using System;

namespace Telecon.Model_Operations
{
    public class UserCRUD
    {
        Email email = new Email();
        DataFormats df = new DataFormats();
        Security sec = new Security();

        // Load an users privileges 

        public bool LoadUserRole(string userName)
        {
            string user = df.FirstLetterToUpper(userName);
            using (var context = new DataContext())
            {
                bool search = (from s in context.Usuarios where s.username == user select s.admin).FirstOrDefault();
                return search;
            }
        }

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
                    optPassword = null,
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

        // Updates an user profile - Actualiza el perfil de usuario

        public void UpdateProfile(string userName, User model)
        {
            string user = df.FirstLetterToUpper(userName);
            using(var context = new DataContext())
            {
                var profile = (from s in context.Usuarios where s.username == user select s).FirstOrDefault();

                profile.firstnames = df.FirstLetterToUpper(model.firstnames);
                profile.lastnames = df.FirstLetterToUpper(model.lastnames);
                profile.age = model.age;
                profile.address = df.AddressCorrector(model.address);
                profile.number = model.number;
                profile.email = model.email.ToLower();
                context.SaveChanges();
            }
        }


        // Change account password

        public void ChangePassword(string userName, string nContraseña)
        {
            string user = df.FirstLetterToUpper(userName);
            using(var context = new DataContext())
            {
                var search = (from s in context.Usuarios where s.username == user select s).FirstOrDefault();
                search.password = sec.EncryptPassword(nContraseña);
                search.optPassword = null;
                context.SaveChanges();
            }
        }

        // Password Recovery Processes

        public void SendPasswordRecovery(string userName)
        {
            string user = df.FirstLetterToUpper(userName);
            string optionalPassword = df.GenerateString(12);
            using (var context = new DataContext())
            {
                var search = (from s in context.Usuarios where s.username == user select s.ID).FirstOrDefault();
                if (search != 0)
                {
                    var recovery = (from s in context.Recovery where s.userEmail == search select s).FirstOrDefault();
                    var acc = (from s in context.Usuarios where s.ID == search select s).FirstOrDefault();

                    if (recovery == null)
                    {
                        var accountRecovery = new PassRecovery
                        {
                            userEmail = search,
                            requestDate = DateTime.Today,
                            requestAmmount = 1
                        };
                        context.Recovery.Add(accountRecovery);
                        acc.optPassword = sec.EncryptPassword(optionalPassword);
                        context.SaveChanges();
                        email.SendRecoveryEmail(user, optionalPassword);
                        return;
                    }

                    if (recovery != null && recovery.requestAmmount < 2)
                    {
                        recovery.requestAmmount++;
                        acc.optPassword = sec.EncryptPassword(optionalPassword);
                        context.SaveChanges();
                        email.SendRecoveryEmail(user, optionalPassword);
                        return;
                    }

                    if (recovery != null && recovery.requestDate != DateTime.Today)
                    {
                        recovery.userEmail = search;
                        recovery.requestDate = DateTime.Today;
                        recovery.requestAmmount = 1;
                        acc.optPassword = sec.EncryptPassword(optionalPassword);
                        context.SaveChanges();
                        email.SendRecoveryEmail(user, optionalPassword);
                        return;
                    }
                }
            }
        }
    }
}