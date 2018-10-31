using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


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

        public bool PasswordMatch(string rawPass)
        {
            bool matches = false;

            string salt = BCrypt.Net.BCrypt.GenerateSalt(15);
            string myHash = BCrypt.Net.BCrypt.HashPassword(rawPass, salt);
            matches = BCrypt.Net.BCrypt.Verify(rawPass, myHash);
        
            return matches;
        }
    }


}