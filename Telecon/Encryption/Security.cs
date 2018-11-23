using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telecon.Models;
using Telecon.Data_Formatting;

namespace Telecon.Encryption
{
    public class Security
    {
        DataFormats df = new DataFormats();

        public string EncryptPassword(string rawPass)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(15);
            string HashedPass = BCrypt.Net.BCrypt.HashPassword(rawPass, salt);
            return HashedPass;
        }

        public bool PasswordMatch(string userName, string rawPass)
        {
            bool matches = false, optmatches = false;
            string user = df.FirstLetterToUpper(userName);
            var pass = "";
            string optPassword = "";
            using(var context = new DataContext())
            {
                pass = (from s in context.Usuarios where s.username == user select s.password).SingleOrDefault();
                optPassword = (from s in context.Usuarios where s.username == user select s.optPassword).SingleOrDefault();
            }
            if(pass == null)
            {
                matches = false;
            }
            if(pass != null)
            {
                matches = BCrypt.Net.BCrypt.Verify(rawPass, pass);
            }
            if (optPassword != null)
            {
                optmatches = BCrypt.Net.BCrypt.Verify(rawPass, optPassword);
            }
            if (matches == true || optmatches == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetLoginAttempts(string ip)
        {
            using(var context = new DataContext())
            {
                var search = (from s in context.LoginAttempt where s.IP == ip select s.attemptAmmounts).FirstOrDefault();
                return search;
            }
        }

        public void RegisterLoginAttempt(string ip)
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.LoginAttempt where s.IP == ip select s).FirstOrDefault();

                if(search != null)
                {
                    search.attemptAmmounts++;
                    context.SaveChanges();
                    return;
                }
                if(search == null)
                {
                    var attempt = new LoginAttempts
                    {
                        IP = ip,
                        attemptAmmounts = 1
                    };
                    context.LoginAttempt.Add(attempt);
                    context.SaveChanges();
                    return;
                }
            }
        }

        public void ResetAttempts(string ip)
        {
            using (var context = new DataContext())
            {
                var search = (from s in context.LoginAttempt where s.IP == ip select s).FirstOrDefault();
                if(search != null)
                {
                    context.LoginAttempt.Remove(search);
                    context.SaveChanges();
                } 
            }
        }

    }
}