using System;
using System.Configuration;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Infra.CrossCutting.IoC;
using SimpleInjector;
using Zip.Sat;

namespace Zip.Pdv
{
    static class Program
    {
        public static Container Container;
        public static CaixaViewModel CaixaView => VerificaCaixa();
        public static EmpresaViewModel EmpresaView => ObterEmpresa();

        public static InicializacaoViewAux InicializacaoViewAux;
        public static UsuarioViewModel Usuario;
        public static int Pdv = GetValueApp.GetValue<int>("Pdv");
        public static int PdvId = GetValueApp.GetValue<int>("PdvId");
        public static int Loja = GetValueApp.GetValue<int>("Loja");
        public static ModeloFiscalEnumView EmissorFiscal = (ModeloFiscalEnumView)GetValueApp.GetValue<int>("EmissorFiscal");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Inicia IoC
            Bootstrap();

            CarregarConfiguracaoInicial();

            InicializacaoViewAux = new InicializacaoViewAux()
            {
                TipoImpressao = TipoImpressaoViewEnum.Print,
                CaminhoEssencial = "C:\\NF-Eletronica\\Essencial.exe",
                ValorFrete = 8,
                DescontoMaximo = 5,
                CedenteId = 4,
                EspeciePagamentoDinheiroId = 1
            };


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormPrincipal());
        }

        private static void Bootstrap()
        {
            // Create the container as usual.
            Container = new ContainerIoc().GetModule();
        }

        private static CaixaViewModel VerificaCaixa()
        {
            using (var caixaAppService = Container.GetInstance<ICaixaAppService>())
            {
                return caixaAppService.ObterCaixaAberto(Pdv);
            }
        }

        private static EmpresaViewModel ObterEmpresa()
        {
            using (var empresaAppService = Container.GetInstance<IEmpresaAppService>())
            {
                var empresa = empresaAppService.ObterPorLoja(Loja);
                

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
                return empresa;
            }
        }
        static void CarregarConfiguracaoInicial()
        {
            try
            {
                Global.ConfiguracaoInicial.ModeloFiscal = (ModeloFiscalEnumView)(Global.Funcoes.ConvertToInt32(ConfigurationManager.AppSettings["MODELOFISCAL"]));
                Global.ConfiguracaoInicial.NumeroLinhasEntreCupom = Global.Funcoes.ConvertToInt32(ConfigurationManager.AppSettings["NUMEROLINHASENTRECUPOM"], 10);
                Global.ConfiguracaoInicial.SoftwareHouseCnpj = ConfigurationManager.AppSettings["SOFTWAREHOUSECNPJ"];
                Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao = ConfigurationManager.AppSettings["SOFTWAREHOUSECHAVEATICACAO"];
                Global.ConfiguracaoInicial.CaixaNumero = Global.Funcoes.ConvertToInt32(ConfigurationManager.AppSettings["CAIXA"], 1);
                Global.ConfiguracaoInicial.SatServidor = ConfigurationManager.AppSettings["SATSERVIDOR"];
                Global.ConfiguracaoInicial.SatPorta = Global.Funcoes.ConvertToInt32(ConfigurationManager.AppSettings["SATPORTA"], 9999);
                Global.ConfiguracaoInicial.SatImpressora = ConfigurationManager.AppSettings["SATIMPRESSORA"];
                Global.ConfiguracaoInicial.SatTextoRodape = ConfigurationManager.AppSettings["SATTEXTORODAPE"];
                Global.ConfiguracaoInicial.SalvarArquivosEm = ConfigurationManager.AppSettings["SALVARARQUIVOS"];
                Global.ConfiguracaoInicial.SatMarca = ConfigurationManager.AppSettings["SATMARCA"];
                Global.ConfiguracaoInicial.SatLayoutVersao = "0.06";

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
    }
}
