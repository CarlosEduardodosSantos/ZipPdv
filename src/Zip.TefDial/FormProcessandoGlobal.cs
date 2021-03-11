using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace Zip.TefDial
{
    public partial class FormProcessandoGlobal : Form
    {
        private readonly ICartaoRequisicaoAppService _cartaoRequisicaoAppService;
        private readonly ICartaoRespostaAppService _cartaoRespostaAppService;
        private CartaoRequisicaoViewModel _cartaoRequisicao;
        public CartaoRespostaViewModel CartaoRespostaView;
        private CartaoRespostaParcelaViewModel _cartaoRespostaParcelaView;
        private int _parcelas;
        private int _quantidadeParcelas;
        string _xMotivo;
        public FormProcessandoGlobal(CartaoTipoOperacaoEnumView tipoOperacao, decimal valorReceber, string numeroCupom, EspecieCartaoTipoEnumView tipoCartao)
        {
            InitializeComponent();

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            _cartaoRequisicaoAppService = ServiceLocator.Current.GetInstance<ICartaoRequisicaoAppService>();
            _cartaoRespostaAppService = ServiceLocator.Current.GetInstance<ICartaoRespostaAppService>();

            CartaoRespostaView = new CartaoRespostaViewModel();

            _cartaoRequisicao = new CartaoRequisicaoViewModel
            {
                TipoOperacao = tipoOperacao,
                Requisicao = _cartaoRequisicaoAppService.ObterUltimaRequisicao() + 1,
                Vinculado = numeroCupom,
                Valor = valorReceber,
                TipoCartao = tipoCartao
            };

            _cartaoRequisicaoAppService.Adicionar(_cartaoRequisicao);
            
        }

        private bool _isCancel;
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
                label1.Text  = _xMotivo;

            if (!btnCancelar.Enabled)
                btnCancelar.Enabled = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            _xMotivo = "Gerando requisição.";
            backgroundWorker1.ReportProgress(Convert.ToInt32(1 * 100 / 4));

            var srvFile = new StringBuilder();
            srvFile.AppendLine($"000-000 = {_cartaoRequisicao.TipoOperacao}");
            srvFile.AppendLine($"001-000 = {_cartaoRequisicao.Requisicao}"); //Indica o número de controle da solicitação que está sendo feita (IdPedido)
            srvFile.AppendLine($"002-000 = {_cartaoRequisicao.Vinculado}"); //Numero Cupom Fiscal
            srvFile.AppendLine($"003-000 = {_cartaoRequisicao.Valor.ToString("N2").Replace(",", "")}"); //Valor da transação
            var cartaoCredito = _cartaoRequisicao.TipoCartao == EspecieCartaoTipoEnumView.CartaoCredito ? 1 : 0; 
            srvFile.AppendLine($"800-001 = {cartaoCredito}");
            srvFile.AppendLine($"800-002 = 0");
            srvFile.AppendLine($"999-999 = 0"); //Finaliza

            var fileReqTemp = $"{MultiPlus.DiretorioReq}/{MultiPlus.NameFileReqTemp}";
            var fileReq = $"{MultiPlus.DiretorioReq}/{MultiPlus.NameFileReq}";

            //Criar arquivo com nome temporario
            File.WriteAllText(fileReqTemp, srvFile.ToString());

            File.Move(fileReqTemp, fileReq);

            var fileSts = $"{MultiPlus.DiretorioResp}/{MultiPlus.NameFileRespTemp}";
            var fileResp = $"{MultiPlus.DiretorioResp}/{MultiPlus.NameFileReq}";

            _xMotivo = "Aguardando retorno.";
            backgroundWorker1.ReportProgress(Convert.ToInt32(2 * 100 / 4));

            bool a = true;
            while (a)
            {
                if (_isCancel)
                {
                    e.Cancel = true;
                    CartaoRespostaView.Menssagem = "Operação cancelada pelo operador.";
                    return;
                }


                if (File.Exists(fileSts))
                {
                    if (File.Exists(fileResp))
                    {
                        _xMotivo = "Processando transação.";
                        backgroundWorker1.ReportProgress(Convert.ToInt32(3 * 100 / 4));

                        CartaoRespostaView = Operacoes.ProcessaResposta(fileResp);
                        a = false;
                    }
                }
                else
                {
                    _xMotivo = "Verifique se o gerenciado padrao esta ativo.";
                    backgroundWorker1.ReportProgress(Convert.ToInt32(3 * 100 / 4));
                    Thread.Sleep(7000);
                }

                
            }

            _xMotivo = "Finalizando transação";
            backgroundWorker1.ReportProgress(Convert.ToInt32(4 * 100 / 4));
            File.Delete(fileSts);
            File.Delete(fileResp);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!CartaoRespostaView.Autorizado)
                MessageBox.Show(CartaoRespostaView.Menssagem, "MultPlus Card", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            _cartaoRespostaAppService.Adicionar(CartaoRespostaView);

            Close();
        }

        private void FormProcessando_Load(object sender, EventArgs e)
        {
            //Verifica se existe a pasta padrao
            if (!Directory.Exists(MultiPlus.DiretorioReq))
                Directory.CreateDirectory(MultiPlus.DiretorioReq);

            if (!Directory.Exists(MultiPlus.DiretorioResp))
                Directory.CreateDirectory(MultiPlus.DiretorioResp);


            backgroundWorker1.RunWorkerAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _isCancel = true;
        }
    }
}
