using System;
using System.Configuration;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Infra.CrossCutting.IoC;
using SimpleInjector;
using Zip.Sat;
using Zip.Utils;

namespace Zip.Pdv
{
    static class Program
    {
        public static Container Container;
        public static string caixaEstacao = "S";
        public static CaixaViewModel CaixaView => VerificaCaixa();
        private static EmpresaViewModel _empresaViewModel;
        public static EmpresaViewModel EmpresaView => _empresaViewModel ?? ObterEmpresa();

        public static InicializacaoViewAux InicializacaoViewAux;
        public static UsuarioViewModel Usuario;
        public static int Pdv = GetValueApp.GetValue<int>("Pdv");
        public static int PdvId = GetValueApp.GetValue<int>("PdvId");
        public static string PdvTef = GetValueApp.GetValue<string>("PdvTef");
        public static int Loja = GetValueApp.GetValue<int>("Loja");
        public static ModoPdvEnumView TipoPdv = (ModoPdvEnumView)GetValueApp.GetValue<int>("TipoPdv");
        public static ModeloFiscalEnumView EmissorFiscal = (ModeloFiscalEnumView)GetValueApp.GetValue<int>("EmissorFiscal");
        public static string MensagemTotem = GetValueApp.GetValue<string>("MensagemTotem");

        public static bool TotemHabPagamento = GetValueApp.GetValue<int>("TotemHabPagamento") == 0 ? false : true;
        public static bool TotemHabPedido = GetValueApp.GetValue<int>("TotemHabPedido") == 0 ? false : true;
        public static bool TotemHabFicha = GetValueApp.GetValue<int>("TotemHabFicha") == 0 ? false : true;
        public static bool TotemHabPreco = GetValueApp.GetValue<int>("TotemHabPreco") == 0 ? false : true;

        public static bool HabilitaTef = GetValueApp.GetValue<int>("HabilitaTEF") == 0 ? false : true;

        public static bool IsFrete = GetValueApp.GetValue<int>("Frete") == 0 ? false : true;
        public static bool GetIsFrete => GetValueApp.GetValue<int>("Frete") == 0 ? false : true;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                try
                {
                    //Inicia IoC
                    Bootstrap();
                }
                catch (Exception exBoot)
                {

                    throw new Exception($"Erro ao carregar o Bootstrap\nErro: {exBoot}");
                }

                try
                {
                    CarregarConfiguracaoInicial();
                }
                catch (Exception)
                {

                    throw new Exception("Erro ao carregar o CarregarConfiguracaoInicial");
                }
                try
                {
                    CarregaVariaveis();
                }
                catch (Exception ex)
                {

                    throw new Exception($"Erro ao carregar o CarregaVariaveis\n{ex.Message}");
                }



                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormPrincipal());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro encontrado" + ex.Message);
            }

        }

        private static void Bootstrap()
        {
            Container = new ContainerIoc().GetModule();
        }

        private static CaixaViewModel VerificaCaixa()
        {
            using (var caixaAppService = Container.GetInstance<ICaixaAppService>())
            {
                return caixaAppService.ObterCaixaAberto(Loja, Pdv);
            }
        }

        private static EmpresaViewModel ObterEmpresa()
        {
            using (var empresaAppService = Container.GetInstance<IEmpresaAppService>())
            {
                var empresa = empresaAppService.ObterPorLoja(Loja);

                if (empresa == null)
                {
                    Funcoes.MensagemError($"Empresa {Loja} não cadastrada.\nVerifique e tente novamente.");
                    Application.Exit();
                }
                if (Global.ConfiguracaoInicial.SatMarca == "Emulador")
                {
                    //empresa.Cnpj = "11111111111111";
                    //empresa.Ie = "111111111111";
                    empresa.SignAC = "11111111111112222222222222211111111111111222222222222221111111111111122222222222222111111111111112222222222222211111111111111222222222222221111111111111122222222222222111111111111112222222222222211111111111111222222222222221111111111111122222222222222111111111111112222222222222211111111111111222222222222221111111111111122222222222222111111111";
                }
                else if (Global.ConfiguracaoInicial.SatMarca == "Nitere")
                {
                    //empresa.Cnpj = "10261693000120";
                    //empresa.Ie = "111111111111";
                    //empresa.SignAC = "SGR-SAT SISTEMA DE GESTAO E RETAGUARDA DO SAT";
                    //Global.ConfiguracaoInicial.SoftwareHouseCnpj = "16716114000172";
                    //Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao = "12345678";
                }
                else if (Global.ConfiguracaoInicial.SatMarca == "Gertec")
                {
                    //empresa.Cnpj = "03654119000176";
                    //empresa.Ie = "000052619494";
                    //empresa.SignAC = "SGR-SAT SISTEMA DE GESTAO E RETAGUARDA DO SAT";
                    //Global.ConfiguracaoInicial.SoftwareHouseCnpj = "16716114000172";
                    //Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao = "gertec1234";
                }
                /*else if (Global.ConfiguracaoInicial.SatMarca == "Bematech")
                {
                    empresa.Cnpj = "82373077000171";
                    empresa.Ie = "111111111111";
                    empresa.Im = "12345678";
                    empresa.SignAC = "SGR-SAT SISTEMA DE GESTAO E RETAGUARDA DO SAT";
                    Global.ConfiguracaoInicial.SoftwareHouseCnpj = "16716114000172";
                    Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao = "bema1234";
                }*/
                Global.Empresa = empresa;
                _empresaViewModel = empresa;

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
                Global.ConfiguracaoInicial.PortaServidor = Global.Funcoes.ConvertToInt32(ConfigurationManager.AppSettings["PORTASERVIDOR"], 4000);
                Global.ConfiguracaoInicial.SatPorta = Global.Funcoes.ConvertToInt32(ConfigurationManager.AppSettings["SATPORTA"], 9999);
                Global.ConfiguracaoInicial.SatImpressora = ConfigurationManager.AppSettings["SATIMPRESSORA"];
                Global.ConfiguracaoInicial.SatTextoRodape = ConfigurationManager.AppSettings["SATTEXTORODAPE"];
                Global.ConfiguracaoInicial.SalvarArquivosEm = ConfigurationManager.AppSettings["SALVARARQUIVOS"];
                Global.ConfiguracaoInicial.SatMarca = ConfigurationManager.AppSettings["SATMARCA"];
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
        static void CarregaVariaveis()
        {

            using (var configuracaoAppService = Container.GetInstance<IConfiguracaoSistemaAppService>())
            {
                var descMax = configuracaoAppService.ObterPorVariavel("max_desconto_final");
                var senhaExcluItem = configuracaoAppService.ObterPorVariavel("SENHA_EXCLUI_ITENS");
                var taxaEntrega = configuracaoAppService.ObterPorVariavel("TAXA_ENTREGA");
                var habExcluirItem = configuracaoAppService.ObterPorVariavel("HAB_SENHA_EXCLUI_ITENS");
                var tipoSenhaAleatoria = configuracaoAppService.ObterPorVariavel("SENHA_ALEATORIA");
                var habSenhaPager = configuracaoAppService.ObterPorVariavel("Hab_Senha_Pager");
                var usaBalancaDigitada = configuracaoAppService.ObterPorVariavel("USA_BALANCA_DIGITADA");
                var perguntaMesaBalcao = configuracaoAppService.ObterPorVariavel("PERGUNTA_MESA_BALCAO");
                var calculaValoresMeioMeio = configuracaoAppService.ObterPorVariavel("Hab_Calculo_Valores_MeioMeio");
                var habZerarSenha = configuracaoAppService.ObterPorVariavel("Hab_Zerar_Senha_Automatico");


                InicializacaoViewAux = new InicializacaoViewAux()
                {
                    TipoImpressao = TipoImpressaoViewEnum.Print,
                    CaminhoEssencial = "C:\\NF-Eletronica\\Essencial.exe",
                    ValorFrete = decimal.Parse(taxaEntrega.Valor),
                    DescontoMaximo = decimal.Parse(descMax.Valor),
                    CedenteId = 4,
                    EspeciePagamentoDinheiroId = 1,
                    SenhaExcluirItem = senhaExcluItem.Valor,
                    HabSenhaExcluirItem = habExcluirItem.Valor == "S" ? true : false,
                    ModoPdv = true,
                    HabSenhaPager = habSenhaPager.Valor == "S",
                    PerguntaMesaBalcao = perguntaMesaBalcao.Valor == "S",
                    BalancaDigitada = usaBalancaDigitada?.Valor == "S",
                    Pdv = Pdv.ToString(),
                    PdvTef = PdvTef,
                    CodigoLoja = GetValueApp.GetValue<string>("CodigoLojaTef"),
                    Cnpj =EmpresaView.Cnpj,
                    RestauranteId = GetValueApp.GetValue<int>("RestauranteId"),
                    CalculaValoresMeioMeio = calculaValoresMeioMeio?.Valor == "S",
                    HabZerarSenha = habZerarSenha?.Valor == "S",
                };


            }

        }

        public static void GravaLog(string msg)
        {
            new LogWriter(msg);
        }
    }
}
