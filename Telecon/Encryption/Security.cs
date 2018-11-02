using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telecon.Models;


namespace Telecon.Encryption
{
    public class Security
    {
        public string EncryptPassword(string rawPass)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(15);
            string HashedPass = BCrypt.Net.BCrypt.HashPassword(rawPass, salt);
            return HashedPass;
        }

        public bool PasswordMatch(string userName, string rawPass)
        {
            bool matches = false;
            string user = char.ToUpper(userName.First()) + userName.Substring(1).ToLower().Trim();
            var pass = "";
            using(var context = new DataContext())
            {
                pass = (from s in context.Usuarios where s.username == user select s.password).Single();
            }
            matches = BCrypt.Net.BCrypt.Verify(rawPass.Trim(), pass);
        
            return matches;
        }
    }


}