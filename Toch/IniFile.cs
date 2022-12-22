using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Reflection;

namespace Toch
{
    public class IniFile
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string Section, string Key, string Value, string path)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public static string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6) + "/vip_config.ini");
            return temp.ToString();

        }
        public static string IniReadConnect()
        {
            StringBuilder _connection = new StringBuilder();
            _connection.Append("Data Source=" + IniReadValue("Caminho", "Servidor"));
            _connection.Append(";Initial Catalog=" + IniReadValue("Caminho", "Banco"));
            _connection.Append(";User ID=" + IniReadValue("Autenticacao", "Login"));
            _connection.Append(";Password=" + IniReadValue("Autenticacao", "Password"));

            return _connection.ToString();
        }

    }
}
