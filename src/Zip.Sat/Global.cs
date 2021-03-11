using Eticket.Application.ViewModels;

namespace Zip.Sat
{
    public static class Global
    {
        public static string SatDll = "";

        static FuncoesSat funcoes;
        public static FuncoesSat Funcoes
        {
            get
            {
                if (Global.funcoes == null)
                    Global.funcoes = new FuncoesSat();
                return Global.funcoes;
            }
        }

        static EmpresaViewModel empresa;
        public static EmpresaViewModel Empresa
        {
            get
            {
                if (Global.empresa == null)
                    Global.empresa = new EmpresaViewModel();
                return Global.empresa;
            }
            set { Global.empresa = value; }
        }

        static ConfiguracaoInicialViewModel configuracaoInicial;
        public static ConfiguracaoInicialViewModel ConfiguracaoInicial
        {
            get
            {
                if (Global.configuracaoInicial == null)
                    Global.configuracaoInicial = new ConfiguracaoInicialViewModel();
                return Global.configuracaoInicial;
            }
            set { Global.configuracaoInicial = value; }
        }
    }
}