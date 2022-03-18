using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Zip.Utils;

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

            var requisicao = _cartaoRequisicaoAppService.ObterUltimaRequisicao() + 1;
            _cartaoRequisicao = new CartaoRequisicaoViewModel
            {
                TipoOperacao = tipoOperacao,
                Requisicao = requisicao,
                Vinculado = numeroCupom,
                Valor = valorReceber,
                TipoCartao = tipoCartao
            };

            _cartaoRequisicaoAppService.Adicionar(_cartaoRequisicao);

        }

        private bool _isCancel;
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label1.Text = _xMotivo;

            if (!btnCancelar.Enabled)
                btnCancelar.Enabled = true;
        }
        int _tentativas;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            _xMotivo = "Gerando requisição.";
            backgroundWorker1.ReportProgress(Convert.ToInt32(1 * 100 / 4));

            var srvFile = new StringBuilder();
            srvFile.AppendLine($"000-000 = {_cartaoRequisicao.TipoOperacao}");
            srvFile.AppendLine($"001-000 = {_cartaoRequisicao.Requisicao}"); //Indica o número de controle da solicitação que está sendo feita (IdPedido)
            srvFile.AppendLine($"002-000 = {_cartaoRequisicao.Vinculado}"); //Numero Cupom Fiscal
            srvFile.AppendLine($"003-000 = {_cartaoRequisicao.Valor.ToString("N2").Replace(",", "")}"); //Valor da transação
            var cartaoCredito = _cartaoRequisicao.TipoCartao == EspecieCartaoTipoEnumView.CartaoCredito ? 0 : 1;
            srvFile.AppendLine($"800-001 = {cartaoCredito}");
            srvFile.AppendLine($"800-002 = 0");
            srvFile.AppendLine($"999-999 = 0"); //Finaliza

            var fileReqTemp = $"{MultiPlus.DiretorioReq}/{MultiPlus.NameFileReqTemp}";
            var fileReq = $"{MultiPlus.DiretorioReq}/{MultiPlus.NameFileReq}";

            //Criar arquivo com nome temporario
            File.WriteAllText(fileReqTemp, srvFile.ToString());

            if (File.Exists(fileReq))
                File.Delete(fileReq);


            File.Move(fileReqTemp, fileReq);

            var fileSts = $"{MultiPlus.DiretorioResp}/{MultiPlus.NameFileRespTemp}";
            var fileResp = $"{MultiPlus.DiretorioResp}/{MultiPlus.NameFileReq}";

            _xMotivo = "Iniciando o processo de pagamento, Aguarde...";
            backgroundWorker1.ReportProgress(Convert.ToInt32(2 * 100 / 4));

            Thread.Sleep(10000);

            bool a = true;
            while (a)
           {
                if (_isCancel)
                {
                    e.Cancel = true;
                    CartaoRespostaView.Menssagem = "Operação cancelada pelo operador.";
                    return;
                }

                if (!IsProcessOpen("ClientTEF"))
                {
                    try
                    {
                        // Start the process with the info we specified.
                        // Call WaitForExit and then the using statement will close.
                        using (Process exeProcess = Process.Start("C:\\Client TEF\\ClientTEF.exe"))
                        {

                            _xMotivo = " INDISPONIBILIDADE DE REDE. AGUARDE E ENQUANTO ESTAMOS RESOLVENDO...";
                            backgroundWorker1.ReportProgress(Convert.ToInt32(3 * 100 / 4));
                            Thread.Sleep(25000);
                            
                            File.WriteAllText(fileResp, MultiPlus.FinalizaProcesso);

                            CartaoRespostaView = Operacoes.ProcessaResposta(fileResp);

                            a = false;
                        }
                    }
                    catch
                    {
                        // Log error.
                    }
                }


                if (File.Exists(fileSts) || File.Exists(fileResp))
                {
                    if (File.Exists(fileResp))
                    {
                        _xMotivo = "Aguarde...";
                        backgroundWorker1.ReportProgress(Convert.ToInt32(3 * 100 / 4));

                        try
                        {
                            CartaoRespostaView = Operacoes.ProcessaResposta(fileResp);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Erro ao gravar a resposta do cartão\nChame um atendente para te ajudar.", "Autoatendimento", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }

                        
                        a = false;
                    }
                }
                else
                {
                   
                    _xMotivo = "Aguarde enquanto estamos iniciando o processo de pagamento...";
                    backgroundWorker1.ReportProgress(Convert.ToInt32(3 * 100 / 4));
                    Thread.Sleep(4000);
                    _tentativas++;

                    if (_tentativas >= 5) 
                    {
                        _xMotivo = " INDISPONIBILIDADE DE REDE. AGUARDE E ENQUANTO ESTAMOS RESOLVENDO...";
                        backgroundWorker1.ReportProgress(Convert.ToInt32(3 * 100 / 4));
                        Thread.Sleep(10000);


                        File.WriteAllText(fileResp, MultiPlus.FinalizaProcesso);

                        CartaoRespostaView = Operacoes.ProcessaResposta(fileResp);

                        a = false;
                    }
                }


            }

            _xMotivo = "Aguarde a conclusão.";
            backgroundWorker1.ReportProgress(Convert.ToInt32(4 * 100 / 4));
            File.Delete(fileSts);
            File.Delete(fileResp);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (CartaoRespostaView.Autorizado)
            {
                //Finaliza o processo
                TryRetry.Do(() => GerarCnf(), TimeSpan.FromSeconds(5));
            }
            try
            {
                _cartaoRespostaAppService.Adicionar(CartaoRespostaView);
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao gravar a resposta do cartão", "MultPlus Card", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }


            Close();
        }
        private void GerarCnf()
        {
            var srvFile = new StringBuilder();
            srvFile.AppendLine($"000-000 = CNF");
            srvFile.AppendLine($"001-000 = 1"); //Identificação da solicitação
            srvFile.AppendLine($"002-000 = {_cartaoRequisicao.Vinculado}"); //Numero Cupom Fiscal
            srvFile.AppendLine($"010-000 = {CartaoRespostaView.NomeRede}"); //Rede responsável pela autorização
            srvFile.AppendLine($"012-000 = {CartaoRespostaView.CodigoNsu}");
            srvFile.AppendLine($"027-000 = 123XXYTZAAABC"); //Finalização
            srvFile.AppendLine($"999-999 = 0"); //Finaliza

            var fileReqTemp = $"{MultiPlus.DiretorioReq}/{MultiPlus.NameFileReqTemp}";
            var fileReq = $"{MultiPlus.DiretorioReq}/{MultiPlus.NameFileReq}";

            //Criar arquivo com nome temporario
            File.WriteAllText(fileReqTemp, srvFile.ToString());

            if (File.Exists(fileReq))
                File.Delete(fileReq);


            File.Move(fileReqTemp, fileReq);
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

        public bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
