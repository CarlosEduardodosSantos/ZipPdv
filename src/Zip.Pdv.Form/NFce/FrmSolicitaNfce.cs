using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Zip.Pdv.Component;

namespace Zip.Pdv.NFce
{
    public partial class FrmSolicitaNfce : Form
    {
        private readonly INfceServicoAppService _nfceServicoAppService;
        private readonly int _vendaId;
        private string _xStatus;
        private DateTime _dateEnvio;
        private bool _cancelar;
        public FrmSolicitaNfce(int vendaId)
        {
            _vendaId = vendaId;
            InitializeComponent();

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            _nfceServicoAppService = Program.Container.GetInstance<INfceServicoAppService>();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            bool createdNew = true;

            _xStatus = "Verificando se o gerenciador esta ativo.";
            backgroundWorker1.ReportProgress(Convert.ToInt32(1 * 100 / 4));


            // Nome do processo atual
            string nomeProcesso = "Essencial";
            Process[] processes = Process.GetProcessesByName(nomeProcesso);

            // Maior do que 1, porque a instância atual também conta
            if (processes.Length <= 0)
            {
                _xStatus = "Aguarde enquanto estamos ativando o gerenciador de NFCe.";
                backgroundWorker1.ReportProgress(Convert.ToInt32(2 * 100 / 4));

                Process.Start(Program.InicializacaoViewAux.CaminhoEssencial);

                Thread.Sleep(5000);
            }

            if (_cancelar)
            {
                e.Cancel = true;
                return;
            }

            var nfce = _nfceServicoAppService.GravaNfce(_vendaId);

            if (nfce == null) return;

            var txtNfce = _nfceServicoAppService.GeraNFce(nfce.NfceId);

            if (string.IsNullOrEmpty(txtNfce)) return;

            var diretorio = _nfceServicoAppService.ObterDiretorioNfce(Program.Loja);

            File.WriteAllText($"{diretorio}NotaFiscal_{nfce.NumeroNfce}.txt", txtNfce);

            _xStatus = "NFCe enviada para o gerenciador.";
            backgroundWorker1.ReportProgress(Convert.ToInt32(2 * 100 / 4));

            _dateEnvio = DateTime.Now;
            bool isOk = false;
            while (!isOk)
            {
                try
                {
                    if (_cancelar)
                    {
                        if (File.Exists($"{diretorio}NotaFiscal_{nfce.NumeroNfce}.txt"))
                        {
                            File.Delete($"{diretorio}NotaFiscal_{nfce.NumeroNfce}.txt");

                            e.Cancel = true;
                            return;
                        }
                        
                    }
                    
                    var situacao = _nfceServicoAppService.ConsultaSituacao(nfce.NumeroNfce, nfce.Serie, nfce.Modelo, Program.EmpresaView.Cnpj);

                    if (situacao == null)
                        if (_dateEnvio.AddMinutes(1) <= DateTime.Now)
                            situacao = new Eticket.Application.ViewModels.NfceSituacaoViewModel()
                            {
                                CodigoSituacao = 8,
                                DataSituacao = DateTime.Now,
                                SituacaoNfe = "Tempo limite alcançado."
                            };
                        else
                            continue;

                    _xStatus = $"NFCe {situacao.SituacaoNfe}";
                    backgroundWorker1.ReportProgress(Convert.ToInt32(3 * 100 / 4));

                    switch (situacao.CodigoSituacao)
                    {
                        case 8:
                            isOk = true;
                            break;
                        case 14:
                            isOk = true;
                            break;
                        case 15:
                            isOk = true;
                            break;
                        default:
                            Thread.Sleep(300);
                            break;
                    }
                }
                catch
                {
                    //ignore
                }
            }

            //_xStatus += $"Operação concluida";
            backgroundWorker1.ReportProgress(Convert.ToInt32(4 * 100 / 4));

            Thread.Sleep(1000);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                TouchMessageBox.Show("Operação cancelada com sucesso.", "NFCe", MessageBoxButtons.OK);
            }
            
            Close();

        }

        private void FrmSolicitaNfce_Load(object sender, System.EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lbStatus.Text = _xStatus;

            pictureBoxCancelar.Enabled = true;

        }

        private void pictureBoxCancelar_Click(object sender, EventArgs e)
        {
            _cancelar = true;
        }
    }
}
