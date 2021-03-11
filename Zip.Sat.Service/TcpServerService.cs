using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using MobileAppServer.ServerObjects;
using Zip.Sat.Service.InjecaoDependencia;
using Zip.Sat.Service.Interceptadores;

namespace Zip.Sat.Service
{
    partial class TcpServerService : ServiceBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly Server _server;
        private readonly int port = 48888;
        private bool isStopped = false;

        public static TraceSwitch traceSwitch = new TraceSwitch("traceSwitch", "Trace Switch");

        public TcpServerService()
        {
            Program.WriteToFile(String.Format("{0}: TcpServerService()", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            // This call is required by the Windows.Forms Component Designer.
            InitializeComponent();
            // TODO: Add any initialization after the InitComponent call

            _server = new Server();
            _server.Port = 4500;

            /* O BufferSize é usado para
             * determinar o tamanho limite de alocação
             * dos dados respondidos pelo servidor
             * No caso estamos usando 10kb (que é mais que suficiente para maioria dos casos)
             * Caso haja necessidade de retorno de Jsons largos, 
             * com imensas listas de objetos, considere aumentar esse limite
             * Quanto maior o volume dos Buffers de resposta, maior o uso
             * de memória do servidor, porém a memória alocada na resposta
             * é destruida muito rapidamente, não precisando se preocupar com
             * MemoryLeak
             * */
            _server.BufferSize = 38000;

            /* Por padrão esse valor já é false
             * Mas caso informado True, o servidor vai trabalhar no modelo
             * Thread-Safe, ou seja, só vai responder uma request por vez
             * Caso haja mais de uma request ao mesmo tempo, vai haver um lock
             * onde as demais requests vão ter que esperar quem estiver na frente
             * terminar de ser executado
             * */
            _server.IsSingleThreaded = false;

            /* Por padrão, esse valor é de 9999
             * Essa propriedade determina quantras thread/request SIMULTÂNEAS
             * podem ser executadas.
             * Caso haja 60 requests, serão executadas em lote as 50,
             * e as outras 10 vão entrar em espera.
             * A medida que forem liberadas as 50, as requests no grupo das 10 restantes
             * que estão aguardando vão entrando no pool de threads do servidor
             * */
            _server.MaxThreadsCount = 50;

            /* Aqui nós estamos registrando todos os controllers
             * no nosso servidor
             * Usa-se o assembly em execução e o namespace no qual
             * as classes se localizam
             * */
            _server.RegisterAllControllers(Assembly.GetExecutingAssembly(), "Zip.Sat.Service.Controllers");

            /* OPCIONAL
             * Define uma implementação de Logger
             * dos eventos do servidor
             * Isso permite voce capturar os eventos e salvar
             * em algum repositorio de sua preferencia
             * */
            _server.SetDefaultLoggerWrapper(new MeuLogger());

            /* OPCIONAL
             * Caso voce precise interagir com classes nas Actions do seu
             * servidor, devem ser registrados os Models baseados no Assembly 
             * em que eles se localizam
             * Isso vai permitir o servidor identificar a classe correta
             * mediante o body das requests
             * */
            _server.RegisterAllModels(Assembly.Load("Eticket.Application"), "Eticket.Application.ViewModels");

            /* OPCIONAL
             * Aqui podemos registrar todos os
             * nossos "Makers" de injeção de dependencia
             * */
            _server.RegisterDependencyInjectorMaker(new InjetorDeConexaoComBanco());

            /* OPCIONAL
             * Aqui podemos registrar todos os 
             * nosso interceptadores de requisição
             * Os interceptadores são classes que executam
             * qualquer código antes da action ou controller ser
             * invocado. Lá dentro, podemos definir se vamos 
             * aceitar ou rejeitar a requisição
             * */
            _server.RegisterInterceptor(new AutenticacaoInterceptor());

            /* OPCIONAL
             * O encoding padrão do servidor
             * é a opção "Default", que é o padrão
             * adotado pelo sistema operacional correspondente
             * da maquina em que roda o servidor
             * Para nossa comodidade, alteramos para UTF-8
             * */
            _server.ServerEncoding = Encoding.UTF8;


            /* Finalmente, levantamos nosso servidor
             * As configurações acima serão validads nesse 
             * momento
             * */
            _server.Start(false);

            Program.WriteToFile(String.Format("{0}: Thread", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));




#if !DEBUG
            Thread t = new Thread(new ThreadStart(this.Start));
            t.Start();
#endif
        }

        protected override void OnStart(string[] args)
        {

            Program.WriteToFile(String.Format("{0}: OnStart()", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            this.isStopped = false;
            
            Thread t = new Thread(new ThreadStart(this.Start));
            t.Start();
        }

        public void Start()
        {
            Program.WriteToFile(String.Format("{0}: Inicio do serviços", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            while (!this.isStopped)
            {
                   //continua o serviço   
            }

            Program.WriteToFile(String.Format("{0}: Stop()", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            this.isStopped = true;
        }

        
    }
}
