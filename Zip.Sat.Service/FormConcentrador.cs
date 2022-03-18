using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using MobileAppServer.ServerObjects;
using SAT;
using Zip.Sat.Service.InjecaoDependencia;
using Zip.Sat.Service.Interceptadores;

namespace Zip.Sat.Service
{
    public partial class FormConcentrador : Form
    {
        private readonly Server _server;
        private bool isStopped = false;
        private PerformanceCounter cpuCounter = new PerformanceCounter();
        private PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        public string getCurrentCpuUsage;
        public string getAvailableRAM;
        private BackgroundWorker _backgroundWorker;
        private Timer _timerServico;
        public FormConcentrador()
        {
            Program.WriteToFile(String.Format("{0}: TcpServerService()", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            InitializeComponent();
            _server = new Server();

            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            cpuCounter = new PerformanceCounter("Process", "% Processor Time", "appB");

            _timerServico = new Timer();

            _timerServico.Tick += TimerServicoOnTick;
            cpuCounter.CategoryName = "Processor"; //Invalid token '=' in class, struct, or interface member declaration; ... MainBox.cpuCounter' is a 'field' but is used like a 'type';
            cpuCounter.CounterName = "% Processor Time"; //Invalid token '=' in class, struct, or interface member declaration; ... MainBox.cpuCounter' is a 'field' but is used like a 'type';
            cpuCounter.InstanceName = "_Total"; //Invalid token '=' in class, struct, or interface member declaration; ... MainBox.cpuCounter' is a 'field' but is used like a 'type';


        }

        private void FormConcentrador_Load(object sender, EventArgs e)
        {
            _server.Port = 3103;

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
            _server.MaxThreadsCount = 9999;

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
            _ultData = DateTime.Now;
            btnIniciar.PerformClick();

            WindowState = FormWindowState.Minimized;
            Hide();

        }

        private DateTime _ultData;
        private void TimerServicoOnTick(object sender, EventArgs eventArgs)
        {
            if (_ultData.AddSeconds(5) <= DateTime.Now)
            {
                _ultData = DateTime.Now;
                getCurrentCpuUsage = cpuCounter.NextValue().ToString("N0") + "%".ToString();
                getAvailableRAM = ramCounter.NextValue() + "Mb".ToString();

                lbCPU.Text = getCurrentCpuUsage;
                lbMemoria.Text = getAvailableRAM;

                label2.Text = DateTime.Now.ToString("dd-MM-yyyy : HH:mm");

            }

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            Program.WriteToFile(String.Format("{0}: Inicio do serviços", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));

            isStopped = false;
            btnIniciar.Enabled = !btnIniciar.Enabled;
            iniciarToolStripMenuItem.Enabled = !iniciarToolStripMenuItem.Enabled;
            try
            {
                StatusSat();
                _timerServico.Start();
            }
            catch 
            {
                Program.WriteToFile(String.Format("{0}: Erro ao consultar o SAT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            this.isStopped = true;
            btnIniciar.Enabled = !btnIniciar.Enabled;
            iniciarToolStripMenuItem.Enabled = !iniciarToolStripMenuItem.Enabled;
            _timerServico.Stop();
            Program.WriteToFile(String.Format("{0}: Stop()", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
        }

        private void FormConcentrador_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                this.Hide();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
        }

        private void StatusSat()
        {
            var satGerencial = new SatGerencial();

            var msg = string.Empty;
            var result = satGerencial.SatConsultarStatus(ref msg);
            lbStatusSat.Text = msg;
        }

        private void iniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnIniciar.PerformClick();
        }

        private void pararToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.PerformClick();
        }

        private void fecharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.PerformClick();
            Close();
        }
    }
}
