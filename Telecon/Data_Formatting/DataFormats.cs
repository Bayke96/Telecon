using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telecon.Data_Formatting
{
    public class DataFormats
    {
        Random rand = new Random();

        public const string Alphabet =
        "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public string GenerateString(int size)
        {
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = Alphabet[rand.Next(Alphabet.Length)];
            }
            return new string(chars);
        }

        public String AddressCorrector(String str)
        {
            String result = "";
            String palabra = "";

            foreach (string word in str.Split(' '))
            {
                palabra = char.ToUpper(word.First()) + word.Substring(1).ToLower();
                if (palabra.Equals("de", StringComparison.InvariantCultureIgnoreCase)) palabra = char.ToLower(word.First()) + word.Substring(1).ToLower();
                if (palabra.Equals("of", StringComparison.InvariantCultureIgnoreCase)) palabra = char.ToLower(word.First()) + word.Substring(1).ToLower();
                if (palabra.Equals("y", StringComparison.InvariantCultureIgnoreCase)) palabra = char.ToLower(word.First()) + word.Substring(1).ToLower();
                if (palabra.Equals("and", StringComparison.InvariantCultureIgnoreCase)) palabra = char.ToLower(word.First()) + word.Substring(1).ToLower();

                result += palabra + " ";
            }
            return result;
        }
    }
}