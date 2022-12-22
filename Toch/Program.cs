using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Eticket.Infra.CrossCutting.IoC;
using SimpleInjector;

namespace Toch
{
    static class Program
    {
        static private String ConAux;
        public static Container Container;

        /// <summary>
        /// Abre conexão banco
        /// </summary>
        /// <returns>Retornar ClientEnvironment</returns>
        static public SqlConnection CreateManager()
        {
            //Conexao com banco de dados
            try
            {
                SqlConnection globalConn = new System.Data.SqlClient.SqlConnection();
                globalConn.ConnectionString = ConAux;
                globalConn.Open();
                return globalConn;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Fecha conexão com o banco de dados
        /// </summary>
        /// <param name="manager">EssencialOperacoes.ClientEnvironment</param>
        static public void DisposeManager(SqlConnection manager)
        {
            if (manager != null && manager != null && manager.State == System.Data.ConnectionState.Open)
            {
                manager.Close();
            }
        }

        /// <summary>
        /// Variavel de configuração retorna 0 se cliente usa senha garcom na mesa 1 nao
        /// </summary>
        static public Int32 UsaLoginMesa
        {
            get { return Convert.ToInt32(IniFile.IniReadValue("Gerais", "UsaLoginMesa")); }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ConAux = IniFile.IniReadConnect();

            //Inicia IoC
            Bootstrap();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(Container.GetInstance<FormFixaInicial>());

            //Application.Run(new FormFixaInicial());
            Application.Run(new FrmPrincipal());
        }

        private static void Bootstrap()
        {
            // Create the container as usual.
            Container = new ContainerIoc().GetModule();
        }
    }
}
