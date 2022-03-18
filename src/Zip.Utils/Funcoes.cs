using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Zip.Utils
{
    public static class Funcoes
    {
        public static bool Val_NumeroKey(string _text)
        {
            Regex er = new Regex("^[0-9,\b8]");
            if (er.Match(_text).Success)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static string OnlyNumbers(string toNormalize)
        {
            string resultString = string.Empty;
            Regex regexObj = new Regex(@"[^\d]");
            resultString = regexObj.Replace(toNormalize, "");
            return resultString;
        }
        public static string RemoveAccents(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return String.Empty;
            }
            byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(text);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}