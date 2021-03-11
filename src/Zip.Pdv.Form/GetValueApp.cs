using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace Zip.Pdv
{
    public static class GetValueApp
    {
        public static T GetValue<T>(string key)
        {
            try
            {
                String value = ConfigurationManager.AppSettings[key];
                return (T) Convert.ChangeType(value, typeof(T));
            }
            catch (Exception)
            {
                //MessageBox.Show("Erro ao iniciar a variável: " + key);
                return default(T);
            }
            
        }
        public static void AddOrUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}