using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Eticket.Application.ViewModels;
using SAT;

namespace Zip.Sat
{
    public partial class FrmSolicitaSat : Form
    {
        public RetornoSatViewModel RetornoSatView;

        private readonly VendaViewModel _vendaView;
        private string _xStatus;
        private bool _cancelar;
        class Accessor
        {
            static SatGerencial satGerencial;
            internal static SatGerencial SatGerencial
            {
                get
                {
                    try
                    {
                        if (satGerencial == null)
                            satGerencial = new SatGerencial();
                        return satGerencial;
                    }
                    catch (Exception err)
                    {
                        throw new Exception(err.Message);
                    }
                }
            }
        }
        public FrmSolicitaSat(VendaViewModel vendaView)
        {
            _vendaView = vendaView;
            InitializeComponent();
     

            RetornoSatView = new RetornoSatViewModel();

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _xStatus = "Verificando se o SAT esta ativo.";
                backgroundWorker1.ReportProgress(Convert.ToInt32(1 * 100 / 4));


                if (_cancelar)
                {
                    e.Cancel = true;
                    return;
                }

                var vendaSat = Global.Funcoes.ConvetVendaToVendaSat(_vendaView);

                _xStatus = "SAT enviada para o gerenciador.";
                backgroundWorker1.ReportProgress(Convert.ToInt32(2 * 100 / 4));

                if (_cancelar)
                {
                    e.Cancel = true;
                    return;

                }

                RetornoSatView = Accessor.SatGerencial.SatEnviarImprimir(SatOperacaoEnum.Enviar, vendaSat);
                if (RetornoSatView.IsOk)
                {
                    _xStatus = $"SAT emitido com sucesso.";
                }

                backgroundWorker1.ReportProgress(Convert.ToInt32(3 * 100 / 4));

                _xStatus = $"Operação concluida.";
                backgroundWorker1.ReportProgress(Convert.ToInt32(4 * 100 / 4));

                //Thread.Sleep(1000);
            }
            catch (Exception exception)
            {
                
                throw new Exception(exception.Message);
            }

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Global.Funcoes.MensagemDeErro("Operação cancelada com sucesso.");
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
