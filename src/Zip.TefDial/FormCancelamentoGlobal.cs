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
    public partial class FormCancelamentoGlobal : Form
    {
        private readonly ICartaoRequisicaoAppService _cartaoRequisicaoAppService;
        private readonly ICartaoRespostaAppService _cartaoRespostaAppService;
        private CartaoRequisicaoViewModel _cartaoRequisicao;
        public CartaoRespostaViewModel CartaoRespostaView;
        private CartaoRespostaParcelaViewModel _cartaoRespostaParcelaView;
        private readonly int _requisicao;

        public FormCancelamentoGlobal(int requisicao)
        {
            InitializeComponent();
            _requisicao = requisicao;
            _cartaoRequisicaoAppService = ServiceLocator.Current.GetInstance<ICartaoRequisicaoAppService>();
            _cartaoRespostaAppService = ServiceLocator.Current.GetInstance<ICartaoRespostaAppService>();

            var cartaoResposta = _cartaoRespostaAppService.ObterPorRequisicao(requisicao);

            CartaoRespostaView = new CartaoRespostaViewModel();

            _cartaoRequisicao = new CartaoRequisicaoViewModel
            {
                TipoOperacao = CartaoTipoOperacaoEnumView.CNC,
                Requisicao = _cartaoRequisicaoAppService.ObterUltimaRequisicao() + 1,
                Vinculado = cartaoResposta.Vinculado,
                Valor = cartaoResposta.Valor,
                CodigoNsu = cartaoResposta.CodigoNsu
            };

            _cartaoRequisicaoAppService.Adicionar(_cartaoRequisicao);
            
        }

        private bool _isCancel;
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage % 2 == 0)
                label1.Text = "Processando.";
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            var srvFile = new StringBuilder();
            srvFile.AppendLine($"000-000 = {_cartaoRequisicao.TipoOperacao}");
            srvFile.AppendLine($"001-000 = {_cartaoRequisicao.Requisicao}"); //Indica o número de controle da solicitação que está sendo feita (IdPedido)
            srvFile.AppendLine($"003-000 = {_cartaoRequisicao.Valor.ToString("N2").Replace(",", "")}"); //Valor da transação
            srvFile.AppendLine($"012-000 = {_cartaoRequisicao.CodigoNsu}"); //NÚMERO DA TRANSAÇÃO CANCELADA - NS
            srvFile.AppendLine("999-999 = 0"); //Finaliza

            File.WriteAllText($"{MultiPlus.DiretorioReq}/{MultiPlus.NameFileReq}", srvFile.ToString());

            
            var fileSts = $"{MultiPlus.DiretorioResp}/{MultiPlus.NameFileRespTemp}";
            var fileResp = $"{MultiPlus.DiretorioResp}/{MultiPlus.NameFileReq}";

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
                        CartaoRespostaView = Operacoes.ProcessaResposta(fileResp);
                        a = false;
                    }
                }

                Thread.Sleep(200);
            }
            File.Delete(fileSts);
            File.Delete(fileResp);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!CartaoRespostaView.Autorizado)
                MessageBox.Show(CartaoRespostaView.Menssagem, "MultPlus Card", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            CartaoRespostaView.RequisicaoCancelamento = _requisicao;
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
