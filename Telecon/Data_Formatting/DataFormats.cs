using System;
using System.Collections.Generic;
using System.Globalization;
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

        public String AddressCorrector(string str)
        {
            string result = "", palabra = "";
            TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;

            foreach (string word in str.Split(' '))
            {
                palabra = word.ToLower();

                if (!word.Equals("con", StringComparison.InvariantCultureIgnoreCase) && 
                    !word.Equals("with", StringComparison.InvariantCultureIgnoreCase) &&
                    !word.Equals("and", StringComparison.InvariantCultureIgnoreCase) &&
                    !word.Equals("y", StringComparison.InvariantCultureIgnoreCase) &&
                    !word.Equals("de", StringComparison.InvariantCultureIgnoreCase) &&
                    !word.Equals("of", StringComparison.InvariantCultureIgnoreCase))
                {
                    palabra = cultInfo.ToTitleCase(palabra);
                }
                result += palabra + " ";
            }
            return result;
        }

        public string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
    }
}