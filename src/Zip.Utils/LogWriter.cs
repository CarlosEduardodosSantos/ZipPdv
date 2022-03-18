using System;
using System.IO;
using System.Reflection;

namespace Zip.Utils
{
    public class LogWriter
    {
        private string m_exePath = string.Empty;

        public LogWriter(string logMessage)
        {
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                var file = $"{m_exePath}\\Log\\logPDV_{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}.txt";
                using (StreamWriter w = File.AppendText(file))
                {
                    Log(logMessage, w);
                }
            }
            catch
            {
                //ignore
            }
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}