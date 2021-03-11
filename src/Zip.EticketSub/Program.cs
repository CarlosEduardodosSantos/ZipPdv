using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Infra.CrossCutting.IoC;
using SimpleInjector;
using Zip.EticketSub.Model;
using Zip.Sat;
using Zip.Utils;

namespace Zip.EticketSub
{
    static class Program
    {
        public static Container Container;
        public static InicializacaoViewAux InicializacaoViewAux;
        public static EmpresaViewModel EmpresaView => ObterEmpresa();
        public static int Pdv => GetValueApp.GetValue<int>("Pdv");
        public static int Loja => GetValueApp.GetValue<int>("Loja");
        public static int VendedorId => GetValueApp.GetValue<int>("Vendedor");
        public static string CodigoAtivacao => GetValueApp.GetValue<string>("CODIGOATICACAO");
        public static string SatMarca => GetValueApp.GetValue<string>("SATMARCA");
        public static int ConcentradorId => GetValueApp.GetValue<int>("CONCENTRADOR");
        public static string SerieSat => GetValueApp.GetValue<string>("NUMEROSERIE");
        public static string PastaOperacao => GetValueApp.GetValue<string>("PastaOperacao");
        public static string PastaConcluidos => GetValueApp.GetValue<string>("PastaConcluidos");

        public static int CaixaId => new CaixaRepository().ObterCaixaId();
        public static bool IsServer = true;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            bool createdNew = true;
            using (var mutex = new Mutex(true, "Zip.EticketSub.exe", out createdNew))
            {
                try
                {
                    if (createdNew)
                    {
                        //Inicia IoC
                        Bootstrap();

                        CarregarConfiguracaoInicial();


                        InicializacaoViewAux = new InicializacaoViewAux()
                        {
                            TipoImpressao = TipoImpressaoViewEnum.Print,
                            ValorFrete = 8,
                            DescontoMaximo = 5,
                            CedenteId = 4,
                            EspeciePagamentoDinheiroId = 1
                        };

                        validacaoChk();

                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new FormPrincipal());
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
                    MessageBox.Show(e.Message);
                    new LogWriter(e.Message);
                }
            }
        }

        private static void Bootstrap()
        {
            // Create the container as usual.
            Container = new ContainerIoc().GetModule();
        }
        private static EmpresaViewModel ObterEmpresa()
        {
            using (var empresaAppService = Container.GetInstance<IEmpresaAppService>())
            {
                var empresa = empresaAppService.ObterPorLoja(Loja);

                /*
                if (Global.ConfiguracaoInicial.SatMarca == "Emulador")
                {
                    empresa.Cnpj = "11111111111111";
                    empresa.Ie = "111111111111";
                    empresa.SignAC = "11111111111112222222222222211111111111111222222222222221111111111111122222222222222111111111111112222222222222211111111111111222222222222221111111111111122222222222222111111111111112222222222222211111111111111222222222222221111111111111122222222222222111111111111112222222222222211111111111111222222222222221111111111111122222222222222111111111";
                }
                else if (Global.ConfiguracaoInicial.SatMarca == "Nitere")
                {
                    empresa.Cnpj = "10261693000120";
                    empresa.Ie = "111111111111";
                    empresa.SignAC = "SGR-SAT SISTEMA DE GESTAO E RETAGUARDA DO SAT";
                    Global.ConfiguracaoInicial.SoftwareHouseCnpj = "16716114000172";
                    Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao = "12345678";
                }
                else if (Global.ConfiguracaoInicial.SatMarca == "Gertec")
                {
                    empresa.Cnpj = "03654119000176";
                    empresa.Ie = "000052619494";
                    empresa.SignAC = "SGR-SAT SISTEMA DE GESTAO E RETAGUARDA DO SAT";
                    Global.ConfiguracaoInicial.SoftwareHouseCnpj = "16716114000172";
                    Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao = "gertec1234";
                }

                Global.Empresa = empresa;
                */
                return empresa;
            }
        }
        private static void CarregarConfiguracaoInicial()
        {

            try
            {
                Global.ConfiguracaoInicial.ModeloFiscal = ModeloFiscalEnumView.CfeSAT;
                Global.ConfiguracaoInicial.NumeroLinhasEntreCupom =
                    Global.Funcoes.ConvertToInt32(ConfigurationManager.AppSettings["NUMEROLINHASENTRECUPOM"], 10);

                Global.ConfiguracaoInicial.CaixaNumero =
                    Global.Funcoes.ConvertToInt32(ConfigurationManager.AppSettings["Pdv"], 1);
                Global.ConfiguracaoInicial.SatServidor = ConfigurationManager.AppSettings["SATSERVIDOR"];

                Global.ConfiguracaoInicial.PortaServidor =
                    Global.Funcoes.ConvertToInt32(ConfigurationManager.AppSettings["PORTASERVIDOR"], 4000);
                Global.ConfiguracaoInicial.SatPorta =
                    Global.Funcoes.ConvertToInt32(ConfigurationManager.AppSettings["SATPORTA"], 9999);
                Global.ConfiguracaoInicial.SatImpressora = ConfigurationManager.AppSettings["SATIMPRESSORA"];
                Global.ConfiguracaoInicial.SatTextoRodape = ConfigurationManager.AppSettings["SATTEXTORODAPE"];
                Global.ConfiguracaoInicial.SalvarArquivosEm =
                    ConfigurationManager.AppSettings["SALVARARQUIVOS"];
                Global.ConfiguracaoInicial.SatMarca = ConfigurationManager.AppSettings["SATMARCA"];
                Global.ConfiguracaoInicial.SatLayoutVersao = "0.07";

                using (var empresaAppService = Container.GetInstance<IEmpresaAppService>())
                {
                    var empresa = empresaAppService.ObterPorLoja(Loja);
                    Global.ConfiguracaoInicial.SoftwareHouseCnpj = empresa.SoftwareHouseCnpj;
                    Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao = empresa.SoftwareHouseChaveAtivacao;

                    Global.Empresa = empresa;
                    Global.Empresa.EmpresaConfiguracao.CodigoRegimeTributario = empresa.Crt;
                }


                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SATLAYOUTVERSAO"]))
                    Global.ConfiguracaoInicial.SatLayoutVersao =
                        ConfigurationManager.AppSettings["SATLAYOUTVERSAO"];

                if (string.IsNullOrEmpty(Global.ConfiguracaoInicial.SatMarca))
                    Global.ConfiguracaoInicial.SatMarca = "Dimep";

                if (Global.ConfiguracaoInicial.SatMarca == "Emulador")
                    Global.ConfiguracaoInicial.SoftwareHouseCnpj = "22222222222222";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                
            }

        }

        static void validacaoChk()
        {
            var validacao = new br.com.ddns.validacaosolucoesvip.CHK();
            var cnpj = EmpresaView.Cnpj;
            var pcName = Environment.MachineName;
            var chave = "";
            var Ip = "";
            var strStatus = validacao.StatusCliente(cnpj, chave, pcName, Ip);
            var status = XmlHelpers.Deserialize<Validacao>(strStatus);

            switch (status.Table.Situacao)
            {
                case 1:
                    MessageBox.Show("Entrar em contato com a ZIP Software. Sistema Bloqueado!\nFone: (16)3442-1771 ou Plantão: (16)9102-1757.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                    break;
                case 2:
                    MessageBox.Show("Entrar em contato com a ZIP Software. Sistema Inativo!\nFone: (16)3442-1771 ou Plantão: (16)9102-1757.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                    break;
                case 4:
                    MessageBox.Show("Entrar em contato com a ZIP Software. Sistema Aguardando Liberação!\nFone: (16)3442-1771 ou Plantão: (16)9102-1757.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 5:
                    MessageBox.Show("Entrar em contato com a ZIP Software. Cópia Pirata!\nFone: (16)3442-1771 ou Plantão: (16)9102-1757.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                    break;
                default:
                    break;

            }


        }
        
    }
}
