using System;
using System.Linq;
using System.Windows.Forms;
using Eticket.Application.CartaoConsumo;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component;

namespace Zip.Pdv.Cadastro.Venda
{
    public partial class VendaDetalhe : CadastroBase
    {
        private VendaViewModel _vendaView;
        public VendaDetalhe()
        {
            InitializeComponent();

        }

        public override void ClassToObjeto(object objeto)
        {
            splitContainer1.Panel2Collapsed = true;
            _vendaView = (VendaViewModel)objeto;

            using (var vendaApp = Program.Container.GetInstance<IVendaAppService>())
            {
                _vendaView = vendaApp.ObterPorId(_vendaView.VendaId);
                if (_vendaView == null)
                {
                    return;
                }
            }

            if (_vendaView.IsDelivery)
            {
                splitContainer1.Panel2Collapsed = false;
                txtNome.Text = _vendaView.Delivery.ClienteDelivery.Nome;
                txtEndereco.Text = _vendaView.Delivery.ClienteDelivery.Endereco;
                txtNumero.Text = _vendaView.Delivery.ClienteDelivery.Numero;
                txtCep.Text = _vendaView.Delivery.ClienteDelivery.Cep;
                txtBairro.Text = _vendaView.Delivery.ClienteDelivery.Bairro;
                txtCidade.Text = _vendaView.Delivery.ClienteDelivery.Cidade;
                txtUf.Text = _vendaView.Delivery.ClienteDelivery.Uf;

                txtDataHoraSaida.Text = _vendaView.Delivery.DataHoraSaida.ToString("dd/MM/yyyy HH:mm");
                txtDataHoraRetorno.Text = _vendaView.Delivery.DataHoraRetorno.ToString("dd/MM/yyyy HH:mm");
            }

            lbVendaId.Text = _vendaView.VendaId.ToString();
            lbCaixaId.Text = _vendaView.CaixaId.ToString();
            lbPdv.Text = _vendaView.Pdv.ToString();

            lbDataHora.Text = _vendaView.DataHora.ToString("dd/MM/yyyy HH:mm");
            lbTipoVenda.Text = _vendaView.Tipo;
            lbFiscal.Text = _vendaView.CupomFiscal;

            lbDesconto.Text = _vendaView.VendaItens.Sum(t => t.Desconto).ToString("C2");
            lbTaxa.Text = _vendaView.Delivery.TaxaEntrega.ToString("C2");
            lbValorTotal.Text = _vendaView.ValorCompra.ToString("C2");


            dgvVendaItens.AutoGenerateColumns = false;
            dgvVendaItens.DataSource = _vendaView.VendaItens.ToList();

            splitButton1.AddDropDownItemAndHandle("Imprimir Gerencial ", imprimirGerencialToolStripMenuItem_Click);

            if (string.IsNullOrEmpty(_vendaView.CupomFiscal))
                splitButton1.AddDropDownItemAndHandle("Imprimir Fiscal", imprimirToolStripMenuItem_Click);
            else
                splitButton1.AddDropDownItemAndHandle($"Reimprimir  {Program.EmissorFiscal}", reimprimirComprovanteToolStripMenuItem_Click);





        }

        private void btnImprimirNaoFiscal_Click(object sender, System.EventArgs e)
        {
            var tpEmissao = Program.EmissorFiscal;
            if (!string.IsNullOrEmpty(_vendaView.CupomFiscal))
                tpEmissao = ModeloFiscalEnumView.None;

            switch (tpEmissao)
            {
                case ModeloFiscalEnumView.None:
                    using (var vendaApp = Program.Container.GetInstance<IVendaAppService>())
                    {
                        var tipoOperacao = _vendaView.IsDelivery ? 5 : 4;
                        vendaApp.GeraImpressaoFechamento(_vendaView.VendaId, tipoOperacao);
                    }
                    break;
                case ModeloFiscalEnumView.Ecf:
                    break;
                case ModeloFiscalEnumView.CfeSAT:
                    var retorno = OperacoeFiscal.ImprimeSat(_vendaView);
                    if (!retorno.IsOk)
                    {
                        Funcoes.MensagemError(retorno.Mensagem);
                        break;
                    }
                    using (var retornoSatAppService = Program.Container.GetInstance<IRetornoSatAppService>())
                    {
                        retornoSatAppService.Adicionar(retorno);
                    }
                    break;
                case ModeloFiscalEnumView.NFCe:
                    OperacoeFiscal.ImprimeNfce(_vendaView);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ClassToObjeto(_vendaView);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var usuarioAppService = Program.Container.GetInstance<IUsuarioAppService>())
                {
                    var podeProceguir = usuarioAppService.VerificaPrivilegio("AdmVendas1", Program.Usuario.UsuarioId);
                    if (!podeProceguir)
                    {
                        Funcoes.MensagemInformation("Você não possui direitos para esta operação!");
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(_vendaView.CupomFiscal))
                {
                    var retorno = OperacoeFiscal.CancelaSat(_vendaView);
                    if (!retorno.IsOk)
                    {
                        Funcoes.MensagemError($"{retorno.Mensagem}\nVenda não pode ser cancelada. Consulte o suporte para mais informações.");
                        return;
                    }
                }

                var motivo = FormSolicitaTexto.Instace("Informe o motivo da exclusão", 5);



                //Verifica se tem TEF
                using (var cartaoRespostaAppService = Program.Container.GetInstance<ICartaoRespostaAppService>())
                {
                    var cartaoResposta = cartaoRespostaAppService.ObterPorVendaId(_vendaView.VendaId);
                    if (cartaoResposta != null)
                    {
                        if (cartaoResposta.Autorizado)
                        {
                            var pdv = Program.InicializacaoViewAux.Pdv;
                            var codLoja = Program.InicializacaoViewAux.CodigoLoja;
                            var cnpj = Program.InicializacaoViewAux.Cnpj;

                            var respostaCancelamento = TefTotem.AutomacaoTef.Cancelamento(cartaoResposta, pdv, codLoja, cnpj);
                            if (!respostaCancelamento.Autorizado)
                            {
                                TouchMessageBox.Show(respostaCancelamento.Menssagem, "Cancelamento de operação",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }
                }
                //Verifica se pagamento foi Vaucher
                using (var caixaItemAppService = Program.Container.GetInstance<ICaixaItemAppService>())
                {
                    var pagamentos = caixaItemAppService.ObterPagamentoPorVendaId(_vendaView.VendaId);
                    var user = Program.Usuario.Nome;
                    foreach (var item in pagamentos)
                    {
                        var cartaoConsumoView = CartaoConsumoAppService.EstornaMovimentacao(item.CartaoRespostaGuid, user, item.Valor, motivo);
                        if (cartaoConsumoView.error)
                        {
                            Funcoes.MensagemError($"{cartaoConsumoView.message}\nVenda não pode ser cancelada, pois ocorreu um erro ao estornar o cartão consumo.\n Consulte o suporte para mais informações.");
                            return;
                        }
                    }
                }

                    using (var vendaApp = Program.Container.GetInstance<IVendaAppService>())
                {
                    vendaApp.Cancelar(_vendaView, motivo);
                }

                Funcoes.MensagemInformation("Venda cancelada com sucesso.");
                LimparTudo();
            }
            catch (Exception ex)
            {
                Funcoes.MensagemInformation("Ocorreu um erro ao excluir a venda!\nConsulte o log para mais informações.");
                Program.GravaLog(ex.Message);
            }

        }

        public override void LimparTudo()
        {
            splitContainer1.Panel2Collapsed = true;

            txtNome.Clear();
            txtEndereco.Clear();
            txtNumero.Clear();
            txtCep.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
            txtUf.Clear();

            txtDataHoraSaida.Clear();
            txtDataHoraRetorno.Clear();
            lbVendaId.Text = string.Empty;
            lbCaixaId.Text = string.Empty;
            lbPdv.Text = string.Empty;

            lbDataHora.Text = string.Empty;
            lbTipoVenda.Text = string.Empty;
            lbFiscal.Text = string.Empty;

            lbDesconto.Text = string.Empty;
            lbTaxa.Text = string.Empty;
            lbValorTotal.Text = string.Empty;


            dgvVendaItens.AutoGenerateColumns = false;
            dgvVendaItens.DataSource = null;
            btnCancelar.Enabled = false;
            splitButton1.Enabled = false;
        }


        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var vendaApp = Program.Container.GetInstance<IVendaAppService>())
            {
                


                if (!string.IsNullOrEmpty(_vendaView.CupomFiscal))
                {
                    TouchMessageBox.Show("Já existe uma cupom fiscal para essa venda.", "Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                switch (Program.EmissorFiscal)
                {
                    case ModeloFiscalEnumView.None:
                            var tipoOperacao = _vendaView.IsDelivery ? 5 : 4;
                            vendaApp.GeraImpressaoFechamento(_vendaView.VendaId, tipoOperacao);
                        break;
                    case ModeloFiscalEnumView.Ecf:
                        break;
                    case ModeloFiscalEnumView.CfeSAT:
                        var retorno = OperacoeFiscal.ImprimeSat(_vendaView);
                        if (!retorno.IsOk)
                        {
                            Funcoes.MensagemError(retorno.Mensagem);
                            break;
                        }
                        using (var retornoSatAppService = Program.Container.GetInstance<IRetornoSatAppService>())
                        {
                            retornoSatAppService.Adicionar(retorno);
                        }
                        break;
                        
                    case ModeloFiscalEnumView.NFCe:
                        OperacoeFiscal.ImprimeNfce(_vendaView);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                ClassToObjeto(_vendaView);
            }

        }

        private void imprimirGerencialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var vendaApp = Program.Container.GetInstance<IVendaAppService>())
            {
                var tipoOperacao = _vendaView.IsDelivery ? 5 : 4;
                vendaApp.GeraImpressaoFechamento(_vendaView.VendaId, tipoOperacao);
            }
        }

        private void reimprimirComprovanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (Program.EmissorFiscal)
            {
                case ModeloFiscalEnumView.CfeSAT:
                    var retorno = OperacoeFiscal.ReimprimeSat(_vendaView);
                    if (!retorno.IsOk)
                    {
                        Funcoes.MensagemError(retorno.Mensagem);
                        break;
                    }
                    break;
                case ModeloFiscalEnumView.NFCe:
                    TouchMessageBox.Show("Reimpressão devera ser feita pelo Essencial..", "Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }

        }
    }
}
