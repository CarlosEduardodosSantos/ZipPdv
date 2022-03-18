using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Zip.TefTotem.Interface;

namespace Zip.TefTotem
{
    public partial class FormCancelamentoGlobal : Form
    {
        private readonly ICartaoRequisicaoAppService _cartaoRequisicaoAppService;
        private readonly ICartaoRespostaAppService _cartaoRespostaAppService;
        private CartaoRequisicaoViewModel _cartaoRequisicao;
        public CartaoRespostaViewModel CartaoRespostaView;
        private CartaoRespostaParcelaViewModel _cartaoRespostaParcelaView;
        private readonly int _requisicao;
        private string xMotivo;
        private ITefTotem _tefTotem;
        int _vRet;
        public FormCancelamentoGlobal(CartaoRespostaViewModel cartaoResposta, string pdv, string codLoja, string cnpj)
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            _cartaoRequisicaoAppService = ServiceLocator.Current.GetInstance<ICartaoRequisicaoAppService>();
            _cartaoRespostaAppService = ServiceLocator.Current.GetInstance<ICartaoRespostaAppService>();

            _tefTotem = new TefClientMC(); 
            

            CartaoRespostaView = new CartaoRespostaViewModel();

            _cartaoRequisicao = new CartaoRequisicaoViewModel
            {
                TipoOperacao = CartaoTipoOperacaoEnumView.CNC,
                Requisicao = _cartaoRequisicaoAppService.ObterUltimaRequisicao() + 1,
                Vinculado = cartaoResposta.Vinculado,
                Valor = cartaoResposta.Valor,
                CodigoNsu = cartaoResposta.CodigoNsu,
                Pdv = pdv,
                CodigoLoja = codLoja,
                EmpresaCnpj = cnpj
            };

            _cartaoRequisicaoAppService.Adicionar(_cartaoRequisicao);
            
        }

        private bool _isCancel;
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage % 2 == 0)
                label1.Text = xMotivo;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            xMotivo = "Iniciando o processo de cancelamento, Aguarde...";
            backgroundWorker1.ReportProgress(Convert.ToInt32(1 * 100 / 4));

            var tipoOperacao = TipoOperacaoEnum.Cancelamento;
            var valor = _cartaoRequisicao.Valor;
            var cupom = _cartaoRequisicao.Vinculado;
            var data = $"{DateTime.Now.Year}{DateTime.Now.Month.ToString("d2")}{DateTime.Now.Day.ToString("d2")}";
            var cnpj = _cartaoRequisicao.EmpresaCnpj;
            var nsu = _cartaoRequisicao.CodigoNsu ?? "";
            var codLoja = _cartaoRequisicao.CodigoLoja;
            var pdv = _cartaoRequisicao.Pdv;
            var parcelas = 1;
            var tipoComunicacao = 0;
            try
            {

                _vRet = int.Parse(_tefTotem.IIniciaFuncaoMCInterativo((int)tipoOperacao, cnpj, parcelas, cupom, valor.ToString("N2"), nsu, data, pdv, codLoja, tipoComunicacao));
                
                if (_vRet != 0)
                {
                    xMotivo = "Erro ao chamar IniciaFuncaoMCInterativo";
                    e.Cancel = true;
                    return;
                }

                ContinuaDLL();

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao gravar a resposta do cartão\nChame um atendente para te ajudar.", "Autoatendimento", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                _tefTotem.ICancelarFluxoMCInterativo();
            }

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {


            try
            {
                if (_isCancel)
                {
                    _tefTotem.ICancelarFluxoMCInterativo();
                }
                else
                {
                    _cartaoRespostaAppService.Adicionar(CartaoRespostaView);
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao gravar a resposta do cartão", "MultPlus Card", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                _tefTotem.ICancelarFluxoMCInterativo();
            }
            Close();
        }

        private void FormProcessando_Load(object sender, EventArgs e)
        {
           

            backgroundWorker1.RunWorkerAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _isCancel = true;
        }

        void ContinuaDLL()
        {
            var retorno = string.Empty;

            while (retorno.Trim() == "")
            {
                Thread.Sleep(50);
                retorno = _tefTotem.IAguardaFuncaoMCInterativo();


            }

            if (_isCancel)
            {
                //e.Cancel = true;
                CartaoRespostaView.Menssagem = "Operação cancelada pelo operador.";
                backgroundWorker1.ReportProgress(Convert.ToInt32(4 * 100 / 4));
                return;
            }

            var sDadosPergunta = string.Empty;

            var arr = retorno.Split('#');
            switch (arr[0])
            {
                case "[RETORNO]":
                case "[RETORNO]Â":
                    CartaoRespostaView.Autorizado = true;
                    CartaoRespostaView.Bandeira = arr[3].Replace("CAMPO0132=", "");
                    CartaoRespostaView.CodigoNsu = arr[6].Replace("CAMPO0133=", "");
                    CartaoRespostaView.CnpjRede = arr[12].Replace("CAMPO1003=", "");
                    CartaoRespostaView.NumeroCartao = arr[11].Replace("CAMPO0950=", "");
                    CartaoRespostaView.CodigoAutorizacao = arr[5].Replace("CAMPO0135=", "");


                    var sComprovante = arr[15].Replace("CAMPO122=", "");
                    arr = sComprovante.Split('|');
                    sComprovante = "";
                    for (int i = 0; i < arr.Length; i++)
                    {
                        sComprovante = sComprovante + arr[i] + Environment.NewLine;
                    }

                    CartaoRespostaView.Comprovante = sComprovante;
                    CartaoRespostaView.Comprovantes.Add(new ComprovanteTef { Comprovante = sComprovante });
                    CartaoRespostaView.Valor = _cartaoRequisicao.Valor;
                    CartaoRespostaView.Requisicao = _cartaoRequisicao.Requisicao;

                    var valor = _cartaoRequisicao.Valor;
                    var cupom = _cartaoRequisicao.Vinculado;
                    var data = $"{DateTime.Now.Year}{DateTime.Now.Month.ToString("d2")}{DateTime.Now.Day.ToString("d2")}";
                    var cnpj = _cartaoRequisicao.EmpresaCnpj;
                    var nsu = _cartaoRequisicao.CodigoNsu ?? "";
                    var codLoja = _cartaoRequisicao.CodigoLoja;
                    var pdv = _cartaoRequisicao.Pdv;

                    _vRet = int.Parse(_tefTotem.IFinalizaFuncaoMCInterativo(98, cnpj, 1, cupom, valor.ToString("N2"), nsu, data, pdv, codLoja, 0));

                    xMotivo = "Transação aprovada, aguarde a impressão do comprovante";
                    backgroundWorker1.ReportProgress(Convert.ToInt32(4 * 100 / 4));
                    break;

                case "[MSG]":
                case "[MSG]Â":

                    if (arr[1] != "REDE-REDE")
                        xMotivo = arr[1].Replace("Â", "");

                    _vRet = int.Parse(_tefTotem.IContinuaFuncaoMCInterativo("OK"));
                    backgroundWorker1.ReportProgress(Convert.ToInt32(2 * 100 / 4));
                    ContinuaDLL();
                    break;
                case "[MENU]":
                case "[MENU]Â":
                    // _xMotivo = arr[1].Replace("Â", "");

                    if (arr[1] == "TIPO DE FINANCIAMENTO")
                        _vRet = int.Parse(_tefTotem.IContinuaFuncaoMCInterativo("1"));
                    else
                        _vRet = int.Parse(_tefTotem.IContinuaFuncaoMCInterativo("OK"));
                    backgroundWorker1.ReportProgress(Convert.ToInt32(2 * 100 / 4));

                    ContinuaDLL();
                    break;
                case "[ERROABORTAR]":
                case "[ERROABORTAR]Â":
                    xMotivo = arr[1].Replace("Â", ""); 
                    int.Parse(_tefTotem.IContinuaFuncaoMCInterativo("OK"));
                    CartaoRespostaView.Menssagem = xMotivo;

                    backgroundWorker1.ReportProgress(Convert.ToInt32(4 * 100 / 4));
                    break;
                case "[PERGUNTA]":
                case "[PERGUNTA]Â":
                    {
                        sDadosPergunta = arr[1] + Environment.NewLine + Environment.NewLine;


                        sDadosPergunta = sDadosPergunta + "TIPO DE DADO: " + arr[2] + Environment.NewLine;
                        sDadosPergunta = sDadosPergunta + "TAM. MINIMO: " + arr[3] + Environment.NewLine;
                        sDadosPergunta = sDadosPergunta + "TAM. MAXIMO: " + arr[4] + Environment.NewLine;
                        sDadosPergunta = sDadosPergunta + "VALOR. MINIMO: " + arr[5] + Environment.NewLine;
                        sDadosPergunta = sDadosPergunta + "VALOR. MAXIMO: " + arr[6] + Environment.NewLine;
                        sDadosPergunta = sDadosPergunta + "CASAS DECIMAIS: " + arr[7] + Environment.NewLine;
                        //TODO: rever
                        sDadosPergunta = sDadosPergunta.Replace("Â", "");


                        xMotivo = arr[1].Replace("Â", "");
                        CartaoRespostaView.Menssagem = xMotivo;
                        backgroundWorker1.ReportProgress(Convert.ToInt32(2 * 100 / 4));
                        _vRet = int.Parse(_tefTotem.IContinuaFuncaoMCInterativo("OK"));
                        ContinuaDLL();
                        break;
                    }


            }


        }
    }
}
