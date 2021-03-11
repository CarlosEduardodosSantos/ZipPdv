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
    }
}