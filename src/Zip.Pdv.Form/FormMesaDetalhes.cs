using Eticket.Application;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain;
using Eticket.Domain.Entity;
using FastReport.Data;
using FastReport.DevComponents.DotNetBar.Controls;
using FastReport.Editor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using Zip.Pdv.Component;
using Zip.Pdv.Component.CupomGrid;
using Zip.Pdv.Component.EspeciePagamento;
using Zip.Pdv.Fast;
using Zip.Utils;

namespace Zip.Pdv
{
    //Módulo de mesa By Cadu666.
    public partial class FormMesaDetalhes : Form
    {
        private readonly IOpMesa1AppService _OpMesa1AppService;
        private readonly ICadMesasAppService _CadMesasAppService;
        private readonly IOpMesa2AppService _OpMesa2AppService;
        private readonly IVendaAppService _VendaAppService;
        private readonly IUsuarioAppService _UsuarioAppService;
        bool desenhado = false;



        private readonly VendaViewModel VendaView;
        public VendaMesa vendaMesa = new VendaMesa();
        public int mesaId;
        private int OpMesa;
        public CadMesasViewModel mesa;
        public List<CaixaPagamentoViewModel> Pagamentos;
        public FormMesaDetalhes(VendaViewModel vd, int mesa)
        {
            _OpMesa1AppService = Program.Container.GetInstance<IOpMesa1AppService>();
            _OpMesa2AppService = Program.Container.GetInstance<IOpMesa2AppService>();
            _CadMesasAppService = Program.Container.GetInstance<ICadMesasAppService>();
            _VendaAppService = Program.Container.GetInstance<IVendaAppService>();
            _UsuarioAppService = Program.Container.GetInstance<IUsuarioAppService>();
            InitializeComponent();
            VendaView = vd;
            mesaId = mesa;
        }

        private void FormMesaDetalhes_Load(object sender, EventArgs e)
        {

            // CRIA DATASOURCE
            BindingSource bs = new BindingSource();
            bs.DataSource = vendaMesa.VendaItens;
            dtGridMesa.AutoGenerateColumns = false;
            dtGridMesa.DataSource = bs;
            dtGridMesa.AutoGenerateColumns = false;
            dtGridMesa.AutoSize = true;
            dtGridMesa.Refresh();
           lblMesa.Text = "Mesa " + mesaId;

            // CHECA STATUS MESA!
            mesa = _CadMesasAppService.GetById(mesaId);
            if (mesa.Status == 1)
            {

                carregaGrid();
                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = "DISPONíVEL";
                MesaDisponivelBtn();
            }
            else if (mesa.Status == 2)
            {
                OpMesa = mesa.OpMesa1Atual;
                lblStatus.ForeColor = Color.YellowGreen;
                lblStatus.Text = "PENDENTE";
                carregaItens();
                TotalizaCupom();
                carregaGrid();
                MesaPendenteBtn();
            }
            else
            {
                OpMesa = mesa.OpMesa1Atual;
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "OCUPADA";
                carregaItens();
                TotalizaCupom();
                carregaGrid();
                mesaOcupadaBtn();
            }

        }


        private void btnLancar_Click(object sender, EventArgs e)
        {
            if (VendaView.VendaItens.Count == 0)
            {
                TouchMessageBox.Show("Sem itens no PDV!", "Venda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            incluirDoPdv();
            BindingSource bs = new BindingSource();
            bs.DataSource = vendaMesa.VendaItens;
            dtGridMesa.AutoGenerateColumns = false;
            dtGridMesa.DataSource = bs;
            dtGridMesa.AutoGenerateColumns = false;
            dtGridMesa.AutoSize = true;
            dtGridMesa.Refresh();
            TotalizaCupom();
        }

        private void TotalizaCupom()
        {
            lbQtdeProduto.Text = $"{vendaMesa.VendaItens.Sum(t => t.Quantidade)}";
            var subTotal = vendaMesa.VendaItens.Sum(t => t.ValorUnitatio * t.Quantidade);
            lbSubTotal.Text = $"{subTotal.ToString("C2")}";
            var valorPago = verificaMesa();
            var desconto = vendaMesa.VendaItens.Sum(t => t.Desconto);
            var inicial = subTotal + desconto;

            if (desconto != 0)
            {
                VendaView.DescontoPercentual = ((inicial - subTotal) * 100) / inicial;
            }

            lbDesconto.Text = $"({VendaView.DescontoPercentual.ToString("N2")}%) {desconto.ToString("C2")}";

            var adicional = vendaMesa.VendaItens.Sum(t => t.Adicional);
            lbAdicionais.Text = $"{adicional.ToString("C2")}";

            var valorTotal = (subTotal + adicional) - desconto;
            lbValorTotal.Text = (valorTotal-valorPago).ToString("C2");
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
                DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
                col1.DataPropertyName = "DescricaoProduto";
                col1.HeaderText = "Descrição";
                col1.Width = 275;
                dtGridMesa.Columns.Add(col1);
                DataGridViewColumn col2 = new DataGridViewTextBoxColumn();
                col2.DataPropertyName = "ValorUnitatio";
                col2.HeaderText = "Preço Unitário";
                col2.Width = 60;
                dtGridMesa.Columns.Add(col2);
                DataGridViewColumn col3 = new DataGridViewTextBoxColumn();
                col3.DataPropertyName = "Quantidade";
                col3.HeaderText = "Quantidade";
                col3.Width = 80;
                dtGridMesa.Columns.Add(col3);
                DataGridViewColumn col4 = new DataGridViewTextBoxColumn();
                col4.DataPropertyName = "ValorTotal";
                col4.HeaderText = "Total";
                col4.Width = 60;
                dtGridMesa.Columns.Add(col4);
                DataGridViewColumn col5 = new DataGridViewTextBoxColumn();
                col5.DataPropertyName = "Observacao";
                col5.HeaderText = "Observação";
                col5.Width = 60;
                dtGridMesa.Columns.Add(col5);


                desenhado = true;

            }
        }

        private void carregaItens()
        {
            ResetaDataSource();

            //PEGA ITENS DA MESA
            var itens = _OpMesa2AppService.PegarItens(OpMesa);


            foreach (OpMesa2ViewModel a in itens)
            {
                var result = vendaMesa.VendaItens.Where(x => x.Produto == a.DesProduto).ToList();
                result = result.Where(x => ConcatenaComplementosMesa(x) == a.Obs).ToList();
                var item = result.FirstOrDefault();
                if (item != null)
                {
                    item.Quantidade += a.Qtde;
                }
                else
                {
                    vendaMesa.VendaItens.Add(new VendaMesaItens() { Produto = a.DesProduto, ValorUnitatio = a.VlUnit, Quantidade = a.Qtde, Desconto = a.Desconto, Observacao = a.Obs });
                }
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

        private void incluirDoPdv()
        {
            //Abre OpMesa1
            var opMesa1 = new OpMesa1ViewModel();
            opMesa1.IdMesa = mesaId;
            opMesa1.IdGarcom = 1;
            opMesa1.QtdePessoas = 1;
            opMesa1.Status = "B";
            opMesa1.Cartao_Credito = 0;
            opMesa1.Venda_Nro = 0;
            opMesa1.dthrInicial = DateTime.Now;
            opMesa1.Cartao_Debito = 0;
            opMesa1.Troco = 0;
            opMesa1.Ticket = 0;
            opMesa1.IdopMesa1 = 0;
            opMesa1.Cheque = 0;
            opMesa1.Dinheiro = 0;

            if (mesa.Status == 1)
            {
                using (var form = new FormAbreMesa())
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        try
                        {
                            var garcom = Convert.ToInt32(form.garcom);
                            var pessoas = Convert.ToInt32(form.pessoas);

                            opMesa1.QtdePessoas = pessoas;
                            opMesa1.IdGarcom = garcom;
                            OpMesa = _OpMesa1AppService.Abrir(opMesa1);
                            mesa.Status = 3;
                            mesa.OpMesa1Atual = OpMesa;
                            _CadMesasAppService.AlterarStatusMesa(mesa);
                            _CadMesasAppService.IncluirOpMesa1(mesa);
                            mesaOcupadaBtn();
                            OcupaMesa();
                        }
                        catch
                        {
                            TouchMessageBox.Show("Erro ao Abrir Mesa!", "Abertura de Mesa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                    }
                }
            }

            //inserta itens no opMesa2
            List<OpMesa2ViewModel> mesaItens = new List<OpMesa2ViewModel>();

            foreach (VendaItemViewModel a in VendaView.VendaItens)
            {
                mesaItens.Add(new OpMesa2ViewModel() { DesProduto = a.Produto, VlUnit = a.ValorUnitatio, Qtde = a.Quantidade, idOpMesa1 = OpMesa, Valor = a.ValorTotal, Desconto = a.Desconto, Obs = ConcatenaComplementos(a) });
            }

            foreach (OpMesa2ViewModel a in mesaItens)
            {
                var quantidade = a.Qtde;
                if (a.Qtde == 1)
                {
                    _OpMesa2AppService.Abrir(a);
                }
                else
                {

                    while (quantidade > 0)
                    {
                        a.Qtde = 1;
                        _OpMesa2AppService.Abrir(a);
                        quantidade -= 1;
                    }
                }
            }
            dtGridMesa.Refresh();
            carregaItens();
            btnLancar.Enabled = false;
        }

        private string ConcatenaComplementos(VendaItemViewModel venda)
        {
            string concat = string.Empty;
            var separator = "\n + ";

            if (venda.VendaComplementos.Count > 0)
            {
                concat = venda.VendaComplementos.Aggregate(concat, (current, subItem) => current + (separator + subItem.Descricao)) + "\n";
            }
            if (venda.VendaProdutoOpcoes.Count > 0)
            {
                concat = venda.VendaProdutoOpcoes.Aggregate(concat, (current, subItem) => current + (separator + subItem.Descricao)) + "\n";
            }
            if (venda.VendaComplementos.Count == 0 && venda.VendaProdutoOpcoes.Count == 0)
            {
                concat = venda.Observacao;
            }
            else
            {
                concat += $" {venda.Observacao}";
            }

            return concat;
        }

        private string ConcatenaComplementosMesa(VendaMesaItens venda)
        {
            string concat = string.Empty;
            var separator = "\n + ";

            if (venda.VendaComplementos.Count > 0)
            {
                concat = venda.VendaComplementos.Aggregate(concat, (current, subItem) => current + (separator + subItem.Descricao)) + "\n";
            }
            if (venda.VendaProdutoOpcoes.Count > 0)
            {
                concat = venda.VendaProdutoOpcoes.Aggregate(concat, (current, subItem) => current + (separator + subItem.Descricao)) + "\n";
            }
            if (venda.VendaComplementos.Count == 0 && venda.VendaProdutoOpcoes.Count == 0)
            {
                concat = venda.Observacao;
            }
            else
            {
                concat += $" {venda.Observacao}";
            }

            return concat;
        }

        private void OcupaMesa()
        {
            if (mesa.Status == 1)
            {
                mesa.Status = 3;
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "OCUPADA";
                _CadMesasAppService.AlterarStatusMesa(mesa);
                lblMesa.Refresh();
                Application.DoEvents();
            }
        }

        private void dtGridMesa_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.ColumnIndex == 1 || e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
            if (e.ColumnIndex == 2)
            {
                e.CellStyle.Format = "N3";
            }

        }

        private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
           
        }

        private void gridMesa_CellClick(object sender, DataGridViewCellEventArgs e)
        {



        }

        private void btnFecharMesa_Click(object sender, EventArgs e)
        {



            if (mesa.Status == 3)
            {
                int pessoas = 1;
                var text = FormSolicitaNumeric.Instace("Digite o número de Pessoas");
                if (!string.IsNullOrEmpty(text))
                {
                    pessoas = Convert.ToInt32(text);
                    mesa.Status = 2;
                    lblStatus.ForeColor = Color.YellowGreen;
                    lblStatus.Text = "PENDENTE";
                    _CadMesasAppService.AlterarStatusMesa(mesa);

                    var opMesa1 = new OpMesa1ViewModel();
                    opMesa1.IdMesa = mesaId;
                    opMesa1.QtdePessoas = pessoas;
                    opMesa1.Status = "F";
                    opMesa1.IdopMesa1 = mesa.OpMesa1Atual;
                    _OpMesa1AppService.Abrir(opMesa1);
                    MesaPendenteBtn();
                }
                else
                {
                    TouchMessageBox.Show("Por favor insira o número de pessoas.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }



            }
            else if (mesa.Status == 1)
            {
                TouchMessageBox.Show("Necessário iniciar operação primeiro.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                TouchMessageBox.Show("Mesa já está fechada!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


        }

        private void FormMesaDetalhes_Activated(object sender, EventArgs e)
        {
            //carregaGrid();
        }

        private void btnServico_Click(object sender, EventArgs e)
        {
            if (mesa.Status != 1)
            {
                _VendaAppService.GeraImpressaoFechamento(mesa.OpMesa1Atual, 2);
                vendaMesa.VendaItens.Clear();
                ResetaDataSource();
                carregaItens();
                carregaGrid();
                TotalizaCupom();
            }
            else
            {
                TouchMessageBox.Show("Venda não iniciada.", "Venda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void btnVenda_Click(object sender, EventArgs e)
        {
            VendaView.VendaItens.Clear();
            foreach (VendaMesaItens a in vendaMesa.VendaItens)
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
                {
                    TotalizaCupom();
                    return;
                }

                VendaView.IsDelivery = false;
                VendaView.Delivery = new DeliveryViewModel();

            }
            var valorReceber = VendaView.VendaItens.Sum(t => t.ValorTotal);
            var descPercent = VendaView.DescontoPercentual;
            var descontoTela = VendaView.VendaItens.Sum(t => t.Desconto);
            using (var form = new FormPagamento(valorReceber, descontoTela, mesa.OpMesa1Atual))
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
                        ImprimeComprovanteConsumo(CartaoConsumoRep);


                }
                catch (Exception exception)
                {
                    Funcoes.MensagemError(exception.Message);
                }
                finally
                {

                    //IniciarVenda();

                    TotalizaCupom();
                }

            }


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

        private void btnTransferir_Click(object sender, EventArgs e)
        {
            var myForm = new FormTransferenciaMesa(mesa);
            myForm.ShowDialog();
            if(myForm.DialogResult == DialogResult.OK)
            {
                this.Close();
            }
            //myForm.FormClosing += (obj, args) => { this.Close(); };
        }

        private void btnGarcom_Click(object sender, EventArgs e)
        {
            var opMesa1 = _OpMesa1AppService.GetById(mesa.OpMesa1Atual);

            using (var form = new FormAlteraGarcom(Convert.ToInt32(opMesa1.IdGarcom)))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    try
                    {
                        var garcom = Convert.ToInt32(form.garcom);
                        opMesa1.IdGarcom = garcom;
                        _OpMesa1AppService.Atualizar(opMesa1);
                    }
                    catch
                    {
                        TouchMessageBox.Show("Erro ao Alterar Garçom!", "Mesa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }
            }
        }

        private void btnBonificar_Click(object sender, EventArgs e)
        {
            try
            {
                var linha = dtGridMesa.SelectedRows[0].Index;
                var itens = _OpMesa2AppService.PegarItens(OpMesa);
                List<OpMesa2ViewModel> mesaItens = new List<OpMesa2ViewModel>();
                foreach (OpMesa2ViewModel a in itens)
                {
                    mesaItens.Add(a);
                }
                var itemMesa = mesaItens[linha].idOpMesa2;
                _OpMesa2AppService.Bonificar(itemMesa);
                ResetaDataSource();
                carregaItens();
                carregaGrid();
                TotalizaCupom();
            }
            catch
            {
                TouchMessageBox.Show("Erro ao Bonificar Item!", "Mesa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void btnBonificarMesa_Click(object sender, EventArgs e)
        {
            try
            {
                if (OpMesa != 0)
                {
                    _OpMesa2AppService.BonificarMesa(OpMesa);
                    ResetaDataSource();
                    carregaItens();
                    carregaGrid();
                    TotalizaCupom();
                }
                else
                {
                    TouchMessageBox.Show("Operação de Mesa não Iniciada!", "Mesa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch
            {
                TouchMessageBox.Show("Erro ao Bonificar Mesa!", "Mesa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnParcelado_Click(object sender, EventArgs e)
        {
            VendaView.VendaItens.Clear();
            foreach (VendaMesaItens a in vendaMesa.VendaItens)
            {
                VendaView.VendaItens.Add(new VendaItemViewModel() { Produto = a.Produto, ValorUnitatio = a.ValorUnitatio, Quantidade = a.Quantidade, Desconto = a.Desconto, VendaComplementos = a.VendaComplementos, VendaProdutoOpcoes = a.VendaProdutoOpcoes });
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
            using (var form = new FormPagamento(valorReceber, descontoTela, mesa.OpMesa1Atual, true))
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
                        ImprimeComprovanteConsumo(CartaoConsumoRep);


                }
                catch (Exception exception)
                {
                    Funcoes.MensagemError(exception.Message);
                }
                finally
                {

                    //IniciarVenda();

                    this.Close();
                }

            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            ResetaDataSource();
            TotalizaCupom();
            if (mesa.Status != 1)
            {
                mesa.Status = 1;
                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = "DISPONÍVEL";
                _CadMesasAppService.AlterarStatusMesa(mesa);
                lblMesa.Refresh();
                Application.DoEvents();
                MesaDisponivelBtn();
            }
        }

        private void btnParcialItem_Click(object sender, EventArgs e)
        {
            var myForm = new FormMesaPagParcial(mesa.OpMesa1Atual, VendaView);
            myForm.ShowDialog();

            ResetaDataSource();
            carregaItens();
            carregaGrid();
            TotalizaCupom();
        }

        private decimal verificaMesa()
        {
            Pagamentos = new List<CaixaPagamentoViewModel>();
            var opmesa = _OpMesa1AppService.GetById(mesa.OpMesa1Atual);
                //Adiciona os pagamentos Parciais pré existentes
                if (opmesa.Dinheiro != 0)
                {
                    Pagamentos.Add(new CaixaPagamentoViewModel()
                    {
                        CaixaId = Program.CaixaView.CaixaId,
                        EspeciePagamentoId = 1,
                        Especie = "DINHEIRO",
                        Valor = (decimal)opmesa.Dinheiro,
                        Interno = "ESP1",
                        CodigoFiscal = "01",
                    });
                }
                if (opmesa.Cartao_Debito != 0)
                {
                    Pagamentos.Add(new CaixaPagamentoViewModel()
                    {
                        CaixaId = Program.CaixaView.CaixaId,
                        EspeciePagamentoId = 2,
                        Especie = "CARTÃO DÉBITO",
                        Valor = (decimal)opmesa.Cartao_Debito,
                        Interno = "ESP3",
                        CodigoFiscal = "04",
                    });
                }

                if (opmesa.Cartao_Credito != 0)
                {
                    Pagamentos.Add(new CaixaPagamentoViewModel()
                    {
                        CaixaId = Program.CaixaView.CaixaId,
                        EspeciePagamentoId = 3,
                        Especie = "CARTÃO CRÉDITO",
                        Valor = (decimal)opmesa.Cartao_Credito,
                        Interno = "ESP4",
                        CodigoFiscal = "03",
                    });
                }

                if (opmesa.Cartao_Consumo != 0)
                {
                    Pagamentos.Add(new CaixaPagamentoViewModel()
                    {
                        CaixaId = Program.CaixaView.CaixaId,
                        EspeciePagamentoId = 9,
                        Especie = "CARTAO CONSUMO",
                        Valor = (decimal)opmesa.Dinheiro,
                        Interno = "ESP6",
                        CodigoFiscal = "99",
                    });
                }

             var valorPago = Pagamentos.Sum(t => t.Valor);
            return valorPago;


        }

        private void MesaDisponivelBtn()
        {
            if (_UsuarioAppService.VerificaPrivilegio("BOTAOINSERIR", Program.Usuario.UsuarioId))
            {
                btnLancar.Enabled = true;
            }
            btnServico.Enabled = false;
            btnTransferir.Enabled = false;
            btnGarcom.Enabled = false;
            btnParcelado.Enabled = false;
            btnBonificarMesa.Enabled = false;
            btnFecharMesa.Enabled = false;
            btnParcialItem.Enabled = false;
            btnVenda.Enabled = false;
            btnLimpar.Enabled = false;
        }

        private void MesaPendenteBtn()
        {
            if (_UsuarioAppService.VerificaPrivilegio("btBonificarMesa", Program.Usuario.UsuarioId))
            {
                btnBonificarMesa.Enabled = true;
            }
            if (_UsuarioAppService.VerificaPrivilegio("BOTAOINSERIR", Program.Usuario.UsuarioId))
            {
                btnLancar.Enabled = true;
            }
            btnServico.Enabled = true;
            if (_UsuarioAppService.VerificaPrivilegio("TRANSFERENCIA", Program.Usuario.UsuarioId))
            {
                btnTransferir.Enabled = true;
            }
            if (_UsuarioAppService.VerificaPrivilegio("BTNTROCAGARCON", Program.Usuario.UsuarioId))
            {
                btnGarcom.Enabled = true;
            }
            if (_UsuarioAppService.VerificaPrivilegio("BTRECEBEMESAP", Program.Usuario.UsuarioId))
            {
                btnParcelado.Enabled = true;
            }
            if (_UsuarioAppService.VerificaPrivilegio("BTPGTOPARCIAL", Program.Usuario.UsuarioId))
            {
                btnParcialItem.Enabled = true;
            }
            if (_UsuarioAppService.VerificaPrivilegio("BTRECEBEMESAV", Program.Usuario.UsuarioId))
            {
                btnVenda.Enabled = true;
            }
            btnLimpar.Enabled = true;
        }
    

        private void mesaOcupadaBtn()
        {
            if(_UsuarioAppService.VerificaPrivilegio("btBonificarMesa", Program.Usuario.UsuarioId))
            {
                btnBonificarMesa.Enabled = true;
            }
            if (_UsuarioAppService.VerificaPrivilegio("BOTAOINSERIR", Program.Usuario.UsuarioId))
            {
                btnLancar.Enabled = true;
            }
            btnServico.Enabled = true;
            if (_UsuarioAppService.VerificaPrivilegio("TRANSFERENCIA", Program.Usuario.UsuarioId))
            {
                btnTransferir.Enabled = true;
            }
            if (_UsuarioAppService.VerificaPrivilegio("BTNTROCAGARCON", Program.Usuario.UsuarioId))
            {
                btnGarcom.Enabled = true;
            }
            if (_UsuarioAppService.VerificaPrivilegio("BTRECEBEMESAP", Program.Usuario.UsuarioId))
            {
                btnParcelado.Enabled = true;
            }
            if (_UsuarioAppService.VerificaPrivilegio("BTFECHAMMESA", Program.Usuario.UsuarioId))
            {
                btnFecharMesa.Enabled = true;
            }
            if (_UsuarioAppService.VerificaPrivilegio("BTPGTOPARCIAL", Program.Usuario.UsuarioId))
            {
                btnParcialItem.Enabled = true;
            }
            if (_UsuarioAppService.VerificaPrivilegio("BTRECEBEMESAV", Program.Usuario.UsuarioId))
            {
                btnVenda.Enabled = true;
            }
            btnLimpar.Enabled = false;
        }
    }
}
