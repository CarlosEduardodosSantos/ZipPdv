namespace Zip.TefDial
{
    public static class StringHelpers
    {
        public static string ToRegistro(this string registro)
        {
            return registro.Trim().Length >= 8 ? registro.Substring(0, 7).Trim() : "999-999";
        }
        public static string ToRegistroValor(this string registro)
        {
            var len = registro.Length;
            return len > 7 ? registro.Substring(9, len - 9).Trim() : "";
        }
    }
}