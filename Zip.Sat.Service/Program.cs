using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Eticket.Application.ViewModels;

namespace Zip.Sat.Service
{

    static class Program
    {

        [STAThread]
        static void Main()
        {
            bool createdNew = true;
            using (var mutex = new Mutex(true, "Zip.Sat.Service.exe", out createdNew))
            {
                try
                {
                    if (createdNew)
                    {
                        CarregarConfiguracaoInicial();

                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new FormConcentrador());
                    }
                    else
                    {
                        Process current = Process.GetCurrentProcess();
                        foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                        {
                            if (process.Id != current.Id)
                            {
                                //SetForegroundWindow(process.MainWindowHandle);
                                break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    WriteToFile(e.Message);
                }
            }
        }

        private static void CarregarConfiguracaoInicial()
        {
            try
            {
                Global.ConfiguracaoInicial.ModeloFiscal = ModeloFiscalEnumView.CfeSAT;
                Global.ConfiguracaoInicial.NumeroLinhasEntreCupom = Global.Funcoes.ConvertToInt32(ConfigurationManager.AppSettings["NUMEROLINHASENTRECUPOM"], 10);
                Global.ConfiguracaoInicial.SoftwareHouseCnpj = ConfigurationManager.AppSettings["SOFTWAREHOUSECNPJ"];
                Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao = ConfigurationManager.AppSettings["SOFTWAREHOUSECHAVEATICACAO"];
                Global.ConfiguracaoInicial.CaixaNumero = Global.Funcoes.ConvertToInt32(ConfigurationManager.AppSettings["Pdv"], 1);
                Global.ConfiguracaoInicial.SatServidor = ConfigurationManager.AppSettings["SATSERVIDOR"];


                Global.ConfiguracaoInicial.SatImpressora = ConfigurationManager.AppSettings["SATIMPRESSORA"];
                Global.ConfiguracaoInicial.SatTextoRodape = ConfigurationManager.AppSettings["SATTEXTORODAPE"];
                Global.ConfiguracaoInicial.SalvarArquivosEm = ConfigurationManager.AppSettings["SALVARARQUIVOS"];
                Global.ConfiguracaoInicial.SatMarca = ConfigurationManager.AppSettings["SATMARCA"];
                Global.ConfiguracaoInicial.SatPorta = Global.Funcoes.ConvertToInt32(ConfigurationManager.AppSettings["SATPORTA"],1);
                Global.ConfiguracaoInicial.SatLayoutVersao = "0.07";


                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SATLAYOUTVERSAO"]))
                    Global.ConfiguracaoInicial.SatLayoutVersao = ConfigurationManager.AppSettings["SATLAYOUTVERSAO"];

                if (string.IsNullOrEmpty(Global.ConfiguracaoInicial.SatMarca))
                    Global.ConfiguracaoInicial.SatMarca = "Dimep";

                if (Global.ConfiguracaoInicial.SatMarca == "Emulador")
                    Global.ConfiguracaoInicial.SoftwareHouseCnpj = "22222222222222";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static  void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" +
                              DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
