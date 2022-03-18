using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Zip.TefTotem;
using Zip.TefTotem.Interface;
using Zip.Utils;

namespace Zip.TefTotem
{
    public partial class FormProcessandoGlobal : Form
    {
        private readonly ICartaoRequisicaoAppService _cartaoRequisicaoAppService;
        private readonly ICartaoRespostaAppService _cartaoRespostaAppService;
        private CartaoRequisicaoViewModel _cartaoRequisicao;
        public CartaoRespostaViewModel CartaoRespostaView;
        private CartaoRespostaParcelaViewModel _cartaoRespostaParcelaView;
        private ITefTotem _tefTotem;
        private int _parcelas;
        private int _quantidadeParcelas;
        string _xMotivo;
        int _vRet;
        public FormProcessandoGlobal(CartaoTipoOperacaoEnumView tipoOperacao, decimal valorReceber, 
            string numeroCupom, EspecieCartaoTipoEnumView tipoCartao, string pdv, string codLoja, string cnpj)
        {
            InitializeComponent();

            _tefTotem = new TefClientMC();
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
                TipoCartao = tipoCartao,
                Pdv = pdv,
                CodigoLoja = codLoja,
                EmpresaCnpj = cnpj
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            _xMotivo = "Iniciando o processo de pagamento, Aguarde...";
            backgroundWorker1.ReportProgress(Convert.ToInt32(1 * 100 / 4));

            var tipoCartao = _cartaoRequisicao.TipoCartao == EspecieCartaoTipoEnumView.CartaoCredito ? TipoOperacaoEnum.VendaCreditoAvista : TipoOperacaoEnum.CartaoDebito;
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
                //cnpj = "16613759000180";
                _vRet = int.Parse(_tefTotem.IIniciaFuncaoMCInterativo((int)tipoCartao, cnpj, parcelas, cupom, valor.ToString("N2"), nsu, data, pdv, codLoja, tipoComunicacao));
                
                if (_vRet != 0)
                {
                    _xMotivo = "Erro ao chamar IniciaFuncaoMCInterativo";
                    e.Cancel = true;
                    return;
                }

                ContinuaDLL();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gravar a resposta do cartão\nChame um atendente para te ajudar.", "Autoatendimento", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                _tefTotem.ICancelarFluxoMCInterativo();
            }


        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (_isCancel)
            {
                _tefTotem.ICancelarFluxoMCInterativo();
            }
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
                _tefTotem.ICancelarFluxoMCInterativo();
            }


            Close();
        }
        private void GerarCnf()
        {

            //Criar arquivo com nome temporario

        }

        private void FormProcessando_Load(object sender, EventArgs e)
        {


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

        void ContinuaDLL()
        {
            var retorno = string.Empty;

            while (retorno.Trim() == "")
            {
                Thread.Sleep(50);
                retorno = _tefTotem.IAguardaFuncaoMCInterativo();

                if (_isCancel)
                {

                    CartaoRespostaView.Menssagem = "Operação cancelada pelo cliente.";
                    _tefTotem.IContinuaFuncaoMCInterativo("ABORTAR");

                }

            }



            var sDadosPergunta = string.Empty;

            var arr = retorno.Split('#');
            switch (arr[0])
            {
                case "[RETORNO]":
                case "[RETORNO]Â":
                    btnCancelar.Enabled = false;
                    CartaoRespostaView.Autorizado = true;
                    CartaoRespostaView.Bandeira = arr[3].Replace("CAMPO0132=", "");
                    CartaoRespostaView.CodigoNsu = arr[6].Replace("CAMPO0133=", ""); 
                    CartaoRespostaView.CnpjRede = arr[12].Replace("CAMPO1003=", ""); 
                    CartaoRespostaView.NumeroCartao = arr[11].Replace("CAMPO0950=", ""); 
                    CartaoRespostaView.CodigoAutorizacao = arr[5].Replace("CAMPO0135=", "");
                    CartaoRespostaView.Requisicao = 0;

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
                    
                    _tefTotem.IFinalizaFuncaoMCInterativo(98, cnpj, 1, cupom, valor.ToString("N2"), nsu, data, pdv, codLoja, 0);

                    _xMotivo = "Transação aprovada, aguarde a impressão do comprovante";
                    backgroundWorker1.ReportProgress(Convert.ToInt32(4 * 100 / 4));
                    break;

                case "[MSG]":
                case "[MSG]Â":

                    if (arr[1] != "REDE-REDE")
                        _xMotivo = arr[1].Replace("Â", "");

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
                    _xMotivo = arr[1].Replace("Â", "");
                    int.Parse(_tefTotem.IContinuaFuncaoMCInterativo("OK"));
                    CartaoRespostaView.Menssagem = _xMotivo;
                    backgroundWorker1.ReportProgress(Convert.ToInt32(4 * 100 / 4));
                    break;
                case "[ERRODISPLAY]":
                case "[ERRODISPLAY]Â":
                    _xMotivo = arr[1].Replace("Â", "");
                    CartaoRespostaView.Menssagem = _xMotivo;
                    int.Parse(_tefTotem.IContinuaFuncaoMCInterativo("OK"));
                   
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


                        _xMotivo = arr[1].Replace("Â", "");
                        backgroundWorker1.ReportProgress(Convert.ToInt32(2 * 100 / 4));
                        _vRet = int.Parse(_tefTotem.IContinuaFuncaoMCInterativo("OK"));
                        ContinuaDLL();
                        break;
                    }


            }


        }
    }

}
