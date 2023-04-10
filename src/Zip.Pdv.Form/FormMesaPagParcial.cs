using Eticket.Application;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain;
using Eticket.Domain.Entity;
using Eticket.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zip.Pdv.Component;
using Zip.Pdv.Fast;
using Zip.Utils;

namespace Zip.Pdv
{
    public partial class FormMesaPagParcial : Form
    {
        public VendaMesa vendaMesa = new VendaMesa();
        public VendaMesa vendaMesaPag = new VendaMesa();
        public VendaViewModel VendaView = new VendaViewModel();
        public int IdOpMesa1;
        private readonly IOpMesa1AppService _OpMesa1AppService;
        private readonly ICadMesasAppService _CadMesasAppService;
        private readonly IOpMesa2AppService _OpMesa2AppService;
        private readonly IVendaAppService _VendaAppService;
        private readonly IUsuarioAppService _UsuarioAppService;
        bool desenhado = false;
        public FormMesaPagParcial(int idopmesa, VendaViewModel vd)
        {
            IdOpMesa1 = idopmesa;
            VendaView = vd;
            _OpMesa1AppService = Program.Container.GetInstance<IOpMesa1AppService>();
            _OpMesa2AppService = Program.Container.GetInstance<IOpMesa2AppService>();
            _CadMesasAppService = Program.Container.GetInstance<ICadMesasAppService>();
            _VendaAppService = Program.Container.GetInstance<IVendaAppService>();
            _UsuarioAppService = Program.Container.GetInstance<IUsuarioAppService>();
            InitializeComponent();
        }


        private void FormMesaPagParcial_Load(object sender, EventArgs e)
        {
            // CRIA DATASOURCE
            BindingSource bs = new BindingSource();
            bs.DataSource = vendaMesa.VendaItens;
            dtGridMesa.AutoGenerateColumns = false;
            dtGridMesa.DataSource = bs;
            dtGridMesa.AutoGenerateColumns = false;
            dtGridMesa.AutoSize = true;
            dtGridMesa.Refresh();

            carregaItens();
            carregaGrid();
            if (_UsuarioAppService.VerificaPrivilegio("btBonificar", Program.Usuario.UsuarioId))
            {
                btnBonificar.Enabled = true;
            }
            if (_UsuarioAppService.VerificaPrivilegio("BtnPgtoSel", Program.Usuario.UsuarioId))
            {
                btnFinalizar.Enabled = true;
            }
        }

        private void carregaItens()
        {
            //PEGA ITENS DA MESA

            var itens = _OpMesa2AppService.PegarItens(IdOpMesa1);


            foreach (OpMesa2ViewModel a in itens)
            {

                vendaMesa.VendaItens.Add(new VendaMesaItens() { VendaItemId = a.idOpMesa2, Produto = a.DesProduto, ValorUnitatio = a.VlUnit, Quantidade = a.Qtde, Desconto = a.Desconto, Observacao = a.Obs, Pago = a.Pago });

            }
        }

        private void carregaGrid()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = vendaMesa.VendaItens;
            dtGridMesa.AutoGenerateColumns = false;
            dtGridMesa.DataSource = bs;
            dtGridMesa.AutoGenerateColumns = false;
            dtGridMesa.AutoSize = true;
            dtGridMesa.Refresh();


            if (desenhado != true)
            {
                DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
                checkColumn.Name = "X";
                checkColumn.HeaderText = "Selecione";
                checkColumn.Width = 60;
                checkColumn.TrueValue = 1;
                checkColumn.FalseValue = 0;
                checkColumn.ReadOnly = false;
                checkColumn.FillWeight = 10; //if the datagridview is resized (on form resize) the checkbox won't take up too much; value is relative to the other columns' fill values
                dtGridMesa.Columns.Add(checkColumn);
                foreach (DataGridViewRow row in dtGridMesa.Rows)
                {
                    row.Cells[0].Value = false;
                }

                desenhado = true;
                DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
                col1.DataPropertyName = "DescricaoProduto";
                col1.HeaderText = "Descrição";
                col1.Width = 275;
                dtGridMesa.Columns.Add(col1);
                DataGridViewColumn col4 = new DataGridViewTextBoxColumn();
                col4.DataPropertyName = "ValorTotal";
                col4.HeaderText = "Total";
                col4.Width = 60;
                dtGridMesa.Columns.Add(col4);
                DataGridViewColumn col5 = new DataGridViewTextBoxColumn();
                col5.DataPropertyName = "Observacao";
                col5.HeaderText = "Observação";
                col5.Width = 200;
                dtGridMesa.Columns.Add(col5);
                DataGridViewColumn col6 = new DataGridViewTextBoxColumn();
                col6.DataPropertyName = "PagoString";
                col6.HeaderText = "Pago?";
                col6.Width = 75;
                dtGridMesa.Columns.Add(col6);
                DataGridViewButtonColumn button = new DataGridViewButtonColumn();
                {
                    button.Name = "btnDeletar";
                    button.HeaderText = "Deletar Item";
                    button.Width = 75;
                    button.UseColumnTextForButtonValue = true; 
                    dtGridMesa.Columns.Add(button);
                }

            }
        }

        private void dtGridMesa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dtGridMesa.Columns[5].Index && e.RowIndex >= 0)
            {
                var linha = e.RowIndex;
                var itens = _OpMesa2AppService.PegarItens(IdOpMesa1);
                List<OpMesa2ViewModel> mesaItens = new List<OpMesa2ViewModel>();
                foreach (OpMesa2ViewModel a in itens)
                {
                    mesaItens.Add(a);
                }

                if (mesaItens[linha].Pago == true)
                {
                    TouchMessageBox.Show("O item ja foi pago!", "Mesa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    var itemDelete = mesaItens[linha].idOpMesa2;
                    _OpMesa2AppService.DeletarItem(itemDelete);
                    ResetaDataSource();
                    carregaItens();
                    carregaGrid();
                    TotalizaCupom();
                }
            }

            else if (e.RowIndex >= 0)
            {
                var linha = e.RowIndex;

                if (dtGridMesa.Rows[dtGridMesa.CurrentRow.Index].Cells[4].Value.ToString() == "Não")
                {

                    if (Convert.ToInt32(dtGridMesa.Rows[dtGridMesa.CurrentRow.Index].Cells[0].Value) == 0)
                    {

                        dtGridMesa.Rows[dtGridMesa.CurrentRow.Index].Cells[0].Value = 1;
                        vendaMesaPag.VendaItens.Add(vendaMesa.VendaItens[linha]);
                    }

                    else
                    {
                        dtGridMesa.Rows[dtGridMesa.CurrentRow.Index].Cells[0].Value = 0;
                        vendaMesaPag.VendaItens.Remove(vendaMesa.VendaItens[linha]);
                    }
                }

                TotalizaCupom();
            }
        }

        private void TotalizaCupom()
        {
            lbQtdeProduto.Text = $"{vendaMesaPag.VendaItens.Sum(t => t.Quantidade)}";
            var subTotal = vendaMesaPag.VendaItens.Sum(t => t.ValorUnitatio * t.Quantidade);
            lbSubTotal.Text = $"{subTotal.ToString("C2")}";

            var desconto = vendaMesaPag.VendaItens.Sum(t => t.Desconto);
            var inicial = subTotal + desconto;

            if (desconto != 0)
            {
                vendaMesaPag.DescontoPercentual = ((inicial - subTotal) * 100) / inicial;
            }

            lbDesconto.Text = $"({vendaMesaPag.DescontoPercentual.ToString("N2")}%) {desconto.ToString("C2")}";

            var adicional = vendaMesaPag.VendaItens.Sum(t => t.Adicional);
            lbAdicionais.Text = $"{adicional.ToString("C2")}";

            var valorTotal = (subTotal + adicional) - desconto;
            lbValorTotal.Text = valorTotal.ToString("C2");
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            VendaView.VendaItens.Clear();
            foreach (VendaMesaItens a in vendaMesaPag.VendaItens)
            {
                VendaView.VendaItens.Add(new VendaItemViewModel() { Produto = a.Produto, ValorUnitatio = a.ValorUnitatio, Quantidade = a.Quantidade, Desconto = a.Desconto, Observacao = a.Observacao, VendaComplementos = a.VendaComplementos, VendaProdutoOpcoes = a.VendaProdutoOpcoes });
            }

            if (VendaView.VendaItens.Count == 0)
            {
                TouchMessageBox.Show("Venda não iniciada.", "Venda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (VendaView.IsDelivery)
            {
                var result = TouchMessageBox.Show("Venda iniciada como Entrega\nDeseja finalizar como venda balcão?",
                    "Venda", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                    return;

                VendaView.IsDelivery = false;
                VendaView.Delivery = new DeliveryViewModel();

            }
            var valorReceber = VendaView.VendaItens.Sum(t => t.ValorTotal);
            var descPercent = VendaView.DescontoPercentual;
            var descontoTela = VendaView.VendaItens.Sum(t => t.Desconto);
            var totalmesa = vendaMesa.VendaItens.Sum(t => t.ValorUnitatio * t.Quantidade);
            using (var form = new FormPagamento(valorReceber, descontoTela, IdOpMesa1, false, true, totalmesa))
            {
                form.CpfCnpj = VendaView.Cnpj;
                form.ShowDialog();
                

                var isPago = form.IsPago;

                if (!isPago) return;

                VendaView.Cnpj = form.CpfCnpj;

                var isPrazo = form.IsPrazo;
                var clienteView = form.ClienteItemView;

                var CartaoConsumoRep = form.CartaoConsumoView ?? new CartaoConsumoMovRespViewModel();
                if (CartaoConsumoRep.Aproved)
                {
                    AlteraFrete(CartaoConsumoRep.Frete);
                    if (CartaoConsumoRep.Desconto > 0)
                    {
                        var desconto = new DescontoViewResult()
                        {
                            ValorPercentual = CartaoConsumoRep.Desconto,
                            ForceDesc = true
                        };

                        LancaDesconto(desconto);
                    }
                }

                foreach (VendaMesaItens a in vendaMesaPag.VendaItens)
                {
                    _OpMesa2AppService.PagaItem(a.VendaItemId,form._Metodo);
                }

                //Grava venda
                using (var vendaApp = Program.Container.GetInstance<IVendaAppService>())
                {
                    //Verifica se pede numero do pager
                    if (Program.InicializacaoViewAux.HabSenhaPager)
                    {
                        var senhaPager = FormSolicitaNumeric.Instace("INFORME O NÚMERO DO PAGER", true);

                        VendaView.Senha = senhaPager;

                        var opcaoObs = FormSolicitaPergunta.Instace("SELECIONE UMA OPÇÃO", true);
                        VendaView.Observacao += opcaoObs;
                    }
                    else
                    {
                        VendaView.Senha = vendaApp.ObterSenha();
                    }

                    if (Program.InicializacaoViewAux.PerguntaMesaBalcao)
                    {
                        var opcaoObs = FormSolicitaPergunta.Instace("SELECIONE UMA OPÇÃO", true);
                        VendaView.Observacao += opcaoObs;
                    }

                    var vendaId = vendaApp.ObterVendaId();
                    VendaView.VendaId = int.Parse($"{Program.PdvId}{vendaId}");
                    if (isPrazo)
                    {
                        VendaView.Tipo = "P";
                        VendaView.ClienteId = clienteView.Codigo;
                    }
                    //vendaApp.Adicionar(VendaView);

                    TryRetry.Do(() => vendaApp.Adicionar(VendaView), TimeSpan.FromSeconds(3));

                    vendaApp.GeraImpressaoItens(VendaView.VendaId, 0);

                }

                using (var fichaApp = Program.Container.GetInstance<IVendaFichaAppService>())
                {
                    if (VendaView.Fichas != null)
                    {
                        foreach (var ficha in VendaView.Fichas)
                        {
                            fichaApp.FinalizaFicha(ficha.ToString());
                        }
                    }

                }

                //Grava Caixa Itens ou cobrança caso seja prazo
                var caixaItem = form.CaixaItemView;
                caixaItem.CaixaPagamentos = form.Pagamentos;

                if (VendaView.Tipo == "P")
                {
                    VendaView.VendaFinalizadora.Add(new CaixaPagamentoViewModel()
                    {
                        CodigoFiscal = "05", //Crédito loja
                        Valor = VendaView.ValorTotal
                    });
                }
                else
                {
                    using (var caixaApp = Program.Container.GetInstance<ICaixaItemAppService>())
                    {
                        caixaItem.VendaId = VendaView.VendaId;
                        caixaItem.UsuarioId = Program.Usuario.UsuarioId;
                        caixaItem.Historico = $"VENDA Nº {VendaView.VendaId}";
                        caixaItem.TipoLancamento = "VDA";
                        caixaApp.Adicionar(caixaItem);
                    }
                    if (caixaItem.Troco > 0)
                    {
                        var caixaItemTroco = new CaixaItemViewModel();
                        caixaItemTroco.CaixaId = caixaItem.CaixaId;
                        caixaItemTroco.VendaId = VendaView.VendaId;
                        caixaItemTroco.UsuarioId = Program.Usuario.UsuarioId;
                        caixaItemTroco.Valor = -1 * (caixaItem.Troco);
                        caixaItemTroco.Historico = $"TROCO VENDA Nº {VendaView.VendaId}";
                        caixaItemTroco.TipoLancamento = "VDA";

                        using (var especieAppService = Program.Container.GetInstance<IEspeciePagamentoAppService>())
                        {
                            var especie = especieAppService.ObterTodos().FirstOrDefault(t => t.Interno == "ESP1");

                            caixaItemTroco.CaixaPagamentos.Add(new CaixaPagamentoViewModel()
                            {
                                CaixaId = Program.CaixaView.CaixaId,
                                CaixaItemId = caixaItem.CaixaItemId,
                                EspeciePagamentoId = especie.EspeciePagamentoId,
                                Especie = especie.Especie,
                                Valor = caixaItemTroco.Valor,
                                Interno = especie.Interno,
                                CodigoFiscal = especie.CodigoFiscal
                            });

                        }

                        using (var caixaApp = Program.Container.GetInstance<ICaixaItemAppService>())
                        {
                            caixaApp.Adicionar(caixaItemTroco);
                        }

                    }

                    VendaView.VendaFinalizadora = caixaItem.CaixaPagamentos.ToList();
                }

                try
                {
                    var emissarFiscal = Program.IsFrete ? Program.EmissorFiscal : ModeloFiscalEnumView.None;
                    //Imprime Cupom
                    switch (emissarFiscal)
                    {
                        case ModeloFiscalEnumView.None:
                            ImprimeCupomNaoFiscal();
                            ImprimeComprovanteTef(caixaItem);
                            break;
                        case ModeloFiscalEnumView.Ecf:
                            break;
                        case ModeloFiscalEnumView.CfeSAT:
                            var retorno = OperacoeFiscal.ImprimeSat(VendaView);
                            if (!retorno.IsOk)
                            {
                                Funcoes.MensagemError(retorno.Mensagem);
                                break;
                            }
                            using (var retornoSatAppService = Program.Container.GetInstance<IRetornoSatAppService>())
                            {
                                retornoSatAppService.Adicionar(retorno);
                            }

                            ImprimeComprovanteTef(caixaItem);

                            var result = Funcoes.MensagemQuestao("Deseja imprimir o cupom nao fiscal vinculado?");
                            if (result == DialogResult.OK)
                            {
                                ImprimeCupomNaoFiscal();
                            }
                            break;
                        case ModeloFiscalEnumView.NFCe:
                            OperacoeFiscal.ImprimeNfce(VendaView);
                            ImprimeComprovanteTef(caixaItem);
                            var resultNfce = Funcoes.MensagemQuestao("Deseja imprimir o cupom nao fiscal vinculado?");
                            if (resultNfce == DialogResult.OK)
                            {
                                ImprimeCupomNaoFiscal();
                            }
                            break;
                    }
                    if (CartaoConsumoRep.Aproved)
                    {
                        ImprimeComprovanteConsumo(CartaoConsumoRep);
                    }



                }
                catch (Exception exception)
                {
                    Funcoes.MensagemError(exception.Message);
                }
                finally
                {

                    //IniciarVenda();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

            }


        }
        private decimal VerificaDesconto(decimal round)
        {
            if (round > Program.InicializacaoViewAux.DescontoMaximo)
            {
                return Program.InicializacaoViewAux.DescontoMaximo;
            }
            else
                return round;
        }

        private void ImprimeCupomNaoFiscal()
        {
            using (var vendaApp = Program.Container.GetInstance<IVendaAppService>())
            {
                var tipoOperacao = VendaView.IsDelivery ? 5 : 4;
                vendaApp.GeraImpressaoFechamento(VendaView.VendaId, tipoOperacao);
            }

        }
        private void ImprimeComprovanteTef(CaixaItemViewModel caixaItem)
        {
            //Imprime Via TEF
            //Imprime comprovante do cartão TEF
            var report = new RelatorioFastReport();
            foreach (var caixaItemCartaoResposta in caixaItem.CartaoRespostas)
            {
                foreach (var comprovanteTef in caixaItemCartaoResposta.Comprovantes)
                {
                    var parms = new ParameterReportDynamic();
                    parms.Add("Comprovante", comprovanteTef.Comprovante);

                    report = new RelatorioFastReport();
                    report.GerarRelatorio("Imp_ComprovanteTef", parms);
                }
            }

        }
        private void ImprimeComprovanteConsumo(CartaoConsumoMovRespViewModel cartaoConsumoMovResp)
        {
            //Imprime Via CONSUMO
            //Imprime comprovante do cartão CONSUMO
            var report = new RelatorioFastReport();
            var parms = new ParameterReportDynamic();

            var list = new List<CartaoConsumoMovRespViewModel>();
            list.Add(cartaoConsumoMovResp);
            report.GerarRelatorio("Imp_ComprovanteConsumo", parms, list);

        }

        void AlteraFrete(bool move)
        {
            Program.IsFrete = move;

        }

        private void LancaDesconto(DescontoViewResult descontoFinal)
        {
            decimal pDesconto = 0;
            decimal vTotal = 0;
            decimal vDesc = 0;

            if (descontoFinal.ValorReal > 0)
            {
                vTotal = decimal.Round(VendaView.VendaItens.Sum(p => p.ValorTotal), 2);
                pDesconto = (descontoFinal.ValorReal / vTotal) * 100;
            }
            else if (descontoFinal.ValorPercentual > 0)
                pDesconto = descontoFinal.ValorPercentual;

            pDesconto = descontoFinal.ForceDesc ? pDesconto : VerificaDesconto(decimal.Round(pDesconto, 3));
            VendaView.DescontoPercentual = pDesconto;
            foreach (var item in VendaView.VendaItens)
            {
                /*
                if (item.PDesc > 0)
                    pDesconto += item.PDesc;*/

                item.ValorDe = item.ValorUnitatio;
                vDesc = decimal.Round(((item.ValorUnitatio * item.Quantidade) / 100) * pDesconto, 2);
                item.Desconto = decimal.Round(vDesc, 2);
                //item.ValorTotal = decimal.Round((item.VUnit * item.QProd) - vDesc, 2);
            }


            TotalizaCupom();
        }

        private void btnBonificar_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (VendaMesaItens a in vendaMesaPag.VendaItens)
                {
                    _OpMesa2AppService.Bonificar(a.VendaItemId);
                }

                ResetaDataSource();
                carregaItens();
                carregaGrid();
                TotalizaCupom();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                TouchMessageBox.Show("Erro ao Bonificar Item!", "Mesa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void ResetaDataSource()
        {
            vendaMesa.VendaItens.Clear();
            dtGridMesa.Rows.Clear();
            BindingSource bs = new BindingSource();
            bs.DataSource = vendaMesa.VendaItens;
            dtGridMesa.AutoGenerateColumns = false;
            dtGridMesa.DataSource = bs;
            dtGridMesa.AutoGenerateColumns = false;
            dtGridMesa.AutoSize = true;
            dtGridMesa.Refresh();
        }

        private void dtGridMesa_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 5)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var img = Properties.Resources.Trash_16;
                var w = img.Width;
                var h = img.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(img, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }
    }
}
