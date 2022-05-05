using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component;
using Zip.Pdv.Component.CupomGrid;
using Zip.Pdv.Eventos;
using Zip.Pdv.Fast;
using Zip.Pdv.Helpers;
using Zip.Pdv.ModuloBalanca;
using Zip.Utils;

namespace Zip.Pdv
{
    public partial class FormPdvToten : UserControl
    {
        private static FormPdvToten _instance;
        public static FormPdvToten Instance => _instance ?? (_instance = new FormPdvToten());

        private readonly IProdutoAppService _produtoAppService;
        private readonly IProdutoOpcaoAppService _produtoOpcaoAppService;
        private readonly IProdutoComplementoAppService _complementoAppService;
        private BarCodeListener ScannerListener;
        private List<ProdutoGrupoViewModel> _grupos;
        private List<ProdutoViewModel> _produtos;
        public VendaViewModel VendaView;
        private List<string> myListFichas;
        //public List<VendaItemViewModel> _vendaItens;
        private int _pageQuantidade;
        private int _currentPage = 1;

        private int _pageProdQuantidade;
        private int _currentProdPage = 1;

        public FormPdvToten()
        {
            InitializeComponent();

            //CheckForIllegalCrossThreadCalls = false;

            //int beam0 = (int)tableLayoutPrincipal.ColumnStyles[0].Width;
            //tableLayoutPrincipal.RowStyles[0].Height = 100;

            tableLayoutPrincipal.RowStyles[1].SizeType = SizeType.Absolute;
            tableLayoutPrincipal.RowStyles[1].Height = 0;

            _produtoAppService = Program.Container.GetInstance<IProdutoAppService>();
            _produtoOpcaoAppService = Program.Container.GetInstance<IProdutoOpcaoAppService>();
            _complementoAppService = Program.Container.GetInstance<IProdutoComplementoAppService>();
            ScannerListener = new BarCodeListener(this.ParentForm);
            ScannerListener.BarCodeScanned += ScannerListener_BarCodeScanned;

            timerPrincipal.Interval = 100000;
            timerPrincipal.Tick += timerPrincipal_Tick;
            timerPrincipal.Start();

        }

        private void FormPdv_Load(object sender, EventArgs e)
        {
            cupomGridView1.TaskItem += CupomGridView1_TaskItem;
            cupomGridView1.AdicionarItem += CupomGridView1_AddItem;
            cupomGridView1.RemoverItem += CupomGridView1_RemoveItem;
            //btnDesconto.Enabled = Program.InicializacaoViewAux.DescontoMaximo > 0;
            //txtPesquisaProduto.Select();
            IniciaVenda();
        }

        private void ScannerListener_BarCodeScanned(object sender, BarcodeScannedEventArgs e)
        {
            var loja = Program.Loja;
            var produto = _produtoAppService.ObterPorEan(loja, e.ScannedText).FirstOrDefault();
            IncluirProdutoPesquisa(produto);
        }

        private void IncluirProdutoPesquisa(ProdutoViewModel produto)
        {
            if (produto == null)
            {
                TouchMessageBox.Show("Produto não encontrado!", "Leitura Ean", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var balancaPeso = new Leitura();

            var quantidade = produto.ParaBalanca ? (decimal)balancaPeso.ObterPeso() : (decimal)1;
            if (VendaView.VendaItens.Any(t => t.ProdutoId == produto.ProdutoId))
            {
                var updateItem = VendaView.VendaItens.FirstOrDefault(t => t.ProdutoId == produto.ProdutoId);
                if (updateItem == null) return;

                VendaView.VendaItens.FirstOrDefault(t => t.ProdutoId == produto.ProdutoId).Quantidade += quantidade;

                cupomGridView1.Atualizar(VendaView.VendaItens);
            }
            else
            {
                var vendaItem = new VendaItemViewModel()
                {
                    ProdutoId = produto.ProdutoId,
                    Produto = produto.Descricao,
                    ValorUnitatio = produto.ValorVenda,
                    Quantidade = quantidade,
                    ProdutoViewModel = produto
                };
                VendaView.VendaItens.Add(vendaItem);
                cupomGridView1.AddItem(vendaItem);
            }

            TotalizaCupom();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool res = false;
            if (ScannerListener != null)
            {
                res = ScannerListener.ProcessCmdKey(ref msg, keyData);
            }
            res = keyData == Keys.Enter ? res : base.ProcessCmdKey(ref msg, keyData);
            //if (res)
            //    MessageBox.Show(ScannerListener.BarCodeScanned);
            return res;
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CupomGridView1_TaskItem(object sender, EventArgs e)
        {
            //if (!VerificaPermissaoExclusao()) return;

            var gridItem = (CupomItemTotem)sender;
            VendaView.VendaItens.RemoveAt(gridItem.Index);

            cupomGridView1.Atualizar(VendaView.VendaItens);

            TotalizaCupom();
        }
        private void CupomGridView1_AddItem(object sender, EventArgs e)
        {
            //if (!VerificaPermissaoExclusao()) return;

            var gridItem = (CupomItemTotem)sender;
            var index = gridItem.Index;

            VendaView.VendaItens[gridItem.Index].Quantidade += 1;
            var item = VendaView.VendaItens[gridItem.Index];
            cupomGridView1.Atualizar(item, index);

            TotalizaCupom();
        }

        private void CupomGridView1_RemoveItem(object sender, EventArgs e)
        {
            var gridItem = (CupomItemTotem)sender;
            var index = gridItem.Index;
            if (VendaView.VendaItens[index].Quantidade > 1)
                VendaView.VendaItens[index].Quantidade -= 1;

            var item = VendaView.VendaItens[index];
            cupomGridView1.Atualizar(item, index);

            TotalizaCupom();
        }
        private void GrupoPaginacao(int page)
        {

            int itens = (flayoutGrupo.Height) / 110 * 2;

            var skip = itens * (page - 1);

            //_pageQuantidade = int.Parse(Math.Ceiling(_grupos.Count() / double.Parse(itens.ToString())).ToString());
            //if (_pageQuantidade > 1)
            //    btnNext.Enabled = true;
            new FlowLayoutPanelTouch(flayoutGrupo);
            flayoutGrupo.AutoScroll = true;
            flayoutGrupo.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            var gruposPadding = _grupos;//.Skip(skip).Take(itens).ToList();

            if (_grupos.Count > 0)
            {
                flayoutGrupo.Controls.Clear();
                foreach (var t in gruposPadding)
                {
                    var btnGrupo = new GrupoGridViewTotenItem();
                    btnGrupo.Name = t.GrupoId.ToString();
                    // btnGrupo.BackColor = string.IsNullOrEmpty(t.GrupoCor) ? ColorHelper.ObterColor() : StringToColor(t.GrupoCor);
                    //btnGrupo.ColorText = ColorHelper.ObterColorFonte(btnGrupo.BackColor);
                    btnGrupo.AdiconaDataSource(t);
                    btnGrupo.SelectItem += BtnGrupo_Click;

                    flayoutGrupo.Controls.Add(btnGrupo);

                }
                flayoutGrupo.Refresh();

                var grupoPadrao = _grupos.Where(t => t.IsPadrao).FirstOrDefault();
                if (grupoPadrao == null) return;

                var item = (GrupoGridViewTotenItem)flayoutGrupo.Controls[grupoPadrao.GrupoId.ToString()];

                BtnGrupo_Click(item, new EventArgs());
            }
            else
                TouchMessageBox.Show("Grupo do PDV não cadastrado.", "E-Ticket", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }

        public static Color StringToColor(string colorStr)
        {
            TypeConverter cc = TypeDescriptor.GetConverter(typeof(Color));
            var result = (Color)cc.ConvertFromString(colorStr);
            return result;
        }

        private void CarregaGrupos()
        {


            using (var appServer = Program.Container.GetInstance<IProdutoGrupoAppService>())
            {
                var loja = Program.Loja;
                _grupos = appServer.ObterTodos(loja).Where(t => t.HabTotem).ToList();
            }

            GrupoPaginacao(1);

        }

        private void BtnGrupo_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < flayoutGrupo.Controls.Count; i++)
            {
                if (flayoutGrupo.Controls[i].GetType() != typeof(GrupoGridViewTotenItem))
                    continue;

                ((GrupoGridViewTotenItem)flayoutGrupo.Controls[i]).BorderStyle = BorderStyle.None;

            }

            var btn = (GrupoGridViewTotenItem)sender;
            btn.BorderStyle = BorderStyle.Fixed3D;

            //btn.Theme = Theme.MSOffice2010_Green;


            lbGrupo.Text = btn.Descricao;
            CarregaProdutos(btn.CodigoProduto);

            flayoutGrupo.Refresh();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //_currentPage++;
            //GrupoPaginacao(_currentPage);
            //if (_currentPage == _pageQuantidade)
            //    btnNext.Enabled = false;
            //if (_currentPage > 1)
            //    btnPrevious.Enabled = true;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            //_currentPage--;
            //GrupoPaginacao(_currentPage);

            //if (_currentPage == 1)
            //    btnPrevious.Enabled = false;
        }

        private void CarregaProdutos(int grupoId)
        {
            var loja = Program.Loja;
            using (var appServer = Program.Container.GetInstance<IProdutoAppService>())
            {
                _produtos = appServer.ObterPorGrupoId(loja, int.Parse(grupoId.ToString())).ToList();
            }

            ProdutoPaginacao(1);
        }

        private void ProdutoPaginacao(int page)
        {
            //btnNextProd.Enabled = false;
            // btnPrevProd.Enabled = false;

            int qItemW = flayoutProduto.Width / 250;
            int qItemH = flayoutProduto.Height / 220;
            int itens = qItemW * qItemH;

            var skip = itens * (page - 1);

            _pageProdQuantidade = int.Parse(Math.Ceiling(_produtos.Count() / double.Parse(itens.ToString())).ToString());
            //if (_pageProdQuantidade > 1)
            // btnNextProd.Enabled = true;
            new FlowLayoutPanelTouch(flayoutProduto);
            flayoutProduto.AutoScroll = true;
            flayoutProduto.Controls.Clear();

            var produtoPadding = _produtos;//.Skip(skip).Take(itens).ToList();
            if (produtoPadding.Count > 0)
            {
                var controls = new Control[produtoPadding.Count];
                var index = 0;
                foreach (var t in produtoPadding)
                {
                    var btnGridItem = new ProdutoGridToten()
                    {
                        Index = index
                    };
                    btnGridItem.SelectItem += BtnGridItem_SelectItem;
                    btnGridItem.AdiconaDataSource(t);
                    /*var btnProduto = new XButton
                    {
                        Name = "Produto_" + index,
                        Font = new Font("Arial", 8F, FontStyle.Bold),
                        ForeColor = Color.Black,
                        Theme = Theme.MSOffice2010_Publisher,
                        Text = t.Descricao,
                        Height = 53,
                        Width = 95,
                        CodigoProduto = t.ProdutoId,
                        ValorUnitario = t.ValorVenda,
                        TipoProduto = t.ProdutoTipo
                    };

                    btnProduto.MouseClick += new MouseEventHandler(BtnProd_Click);
                    //btnProduto.DoubleClick += new EventHandler(BtnComplemento_DoubleClick);
                    */

                    controls[index] = btnGridItem;

                    index++;

                }
                flayoutProduto.Controls.AddRange(controls);
            }
        }

        private void BtnGridItem_SelectItem(object sender, EventArgs e)
        {

            var item = (ProdutoGridToten)sender;
            var produto = (ProdutoViewModel)item.SelectedItem;

            if (!produto.Visivel)
            {
                Funcoes.MensagemError("Produto não disponível no momento.");
                return;
            }
            //var balancaPeso = new Leitura();
            var quantidade = produto.ParaBalanca ? (decimal)FormLeituraBalanca.ObterPeso() : (decimal)1;

            if (quantidade == 0)
            {
                Funcoes.MensagemError("Produto requer balança configurada.");
                return;
            }

            var seqLanc = VendaView.VendaItens.Count + 1;
            var vendaItem = new VendaItemViewModel()
            {
                ProdutoId = produto.ProdutoId,
                Produto = produto.Descricao,
                ValorUnitatio = produto.ValorVenda,
                Quantidade = quantidade,
                ProdutoViewModel = produto,
                SeqProduto = seqLanc
            };


            //ProdutoOpções 
            var produtosTipoOpoes = _produtoOpcaoAppService.GetByprodutoId(produto.ProdutoId).ToList();
            if (produtosTipoOpoes.Any())
            {
                using (var form = new FrmProdutoOpcaoToten(produtosTipoOpoes, vendaItem))
                {
                    form.Height = (this.Height / 100) * 70;
                    var reult = form.ShowDialog();
                    if (reult != DialogResult.OK)
                        return;

                    vendaItem = form.VendaItem;
                    vendaItem.ValorUnitatio += vendaItem.VendaProdutoOpcoes.Sum(t => t.Valor);

                }
            }

            if (VendaView.VendaItens.Any(t => t.ProdutoId == produto.ProdutoId && t.VendaProdutoOpcoes.Count == 0 && t.VendaComplementos.Count == 0 && string.IsNullOrEmpty(t.Observacao)))
            {
                VendaView.VendaItens.FirstOrDefault(t => t.ProdutoId == produto.ProdutoId && t.VendaProdutoOpcoes.Count == 0 && t.VendaComplementos.Count == 0 && string.IsNullOrEmpty(t.Observacao)).Quantidade += quantidade;


                cupomGridView1.Atualizar(VendaView.VendaItens);

            }
            else
            {
                VendaView.VendaItens.Add(vendaItem);

                cupomGridView1.AddItem(vendaItem);
            }

            /*
            var complementos = _complementoAppService.ObterPorGrupoId(produto.GrupoId).ToList();

            if (complementos.Any())
            {
                using (var form = new FormComplementos(complementos, vendaItem))
                {
                    form.ShowDialog();
                    vendaItem = form.VendaItem;

                    vendaItem.ValorUnitatio += vendaItem.VendaComplementos.Sum(t => t.Valor);
                }

            }
            /*
            if (VendaView.VendaItens.Any(t => t.ProdutoId == produto.ProdutoId && t.VendaComplementos.Count == 0 && string.IsNullOrEmpty(t.Observacao)))
            {
                VendaView.VendaItens.FirstOrDefault(t => t.ProdutoId == produto.ProdutoId && t.VendaComplementos.Count == 0 && string.IsNullOrEmpty(t.Observacao)).Quantidade += quantidade;


                cupomGridView1.Atualizar(VendaView.VendaItens);

            }
            */

            /*
            VendaView.VendaItens.Add(vendaItem);

            cupomGridView1.AddItem(vendaItem);

            //Verifica sugestão para o grupo do produto lançado
            var sugestoes = _produtoAppService.GetSugestaoByGrupoId(produto.GrupoId).ToList();
            if (sugestoes.Any())
            {
                using (var form = new FormSugestao(sugestoes, produto.Descricao))
                {
                    form.ShowDialog();
                    var vendaItens = form.VendaSugestaoItens;

                    foreach (var vendaSugestaoItemItem in vendaItens)
                    {
                        vendaSugestaoItemItem.SeqProduto = VendaView.VendaItens.Count + 1;
                        VendaView.VendaItens.Add(vendaSugestaoItemItem);

                        cupomGridView1.AddItem(vendaSugestaoItemItem);
                    }
                }
            }*/


            CarregaVendaItem();
        }

        private void BtnProd_Click(object sender, MouseEventArgs e)
        {
        }

        private void CarregaVendaItem()
        {

            //cupomGridView1.DataSource = _vendaItens;
            //cupomGridView1.CarregaGrid();

            TotalizaCupom();
        }

        private void TotalizaCupom()
        {
            //lbQtdeProduto.Text = $"{VendaView.VendaItens.Sum(t => t.Quantidade)}";
            var subTotal = VendaView.VendaItens.Sum(t => t.ValorUnitatio * t.Quantidade);
            //lbSubTotal.Text = $"{subTotal.ToString("C2")}";

            var desconto = VendaView.VendaItens.Sum(t => t.Desconto);


            // lbDesconto.Text = $"({VendaView.DescontoPercentual.ToString("N2")}%) {desconto.ToString("C2")}";

            var adicional = VendaView.VendaItens.Sum(t => t.Adicional);
            //lbAdicionais.Text = $"{adicional.ToString("C2")}";

            var valorTotal = (subTotal + adicional) - desconto;
            lbValorTotal.Text = valorTotal.ToString("C2");

            if (VendaView.VendaItens.Count > 0)
            {
                tableLayoutPrincipal.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPrincipal.RowStyles[1].Height = 35;


            }
            else
            {
                tableLayoutPrincipal.RowStyles[1].SizeType = SizeType.Absolute;
                tableLayoutPrincipal.RowStyles[1].Height = 0;
            }
            this.ResumeLayout();

        }

        private void btnNextProd_Click(object sender, EventArgs e)
        {
            //_currentProdPage++;
            //ProdutoPaginacao(_currentProdPage);
            //if (_currentProdPage == _pageProdQuantidade)
            //    btnNextProd.Enabled = false;
            //if (_currentProdPage > 1)
            //    btnPrevProd.Enabled = true;
        }

        private void btnPrevProd_Click(object sender, EventArgs e)
        {
            //_currentProdPage--;
            //ProdutoPaginacao(_currentProdPage);

            //if (_currentProdPage == 1)
            //    btnPrevProd.Enabled = false;
        }

        private void btnCancelarVenda_Click(object sender, EventArgs e)
        {
            //if (!VerificaPermissaoExclusao()) return;
            var reult = TouchMessageBox.Show("Deseja realmente limpar seu carrinho?.", "CANCELAR PEDIDO", MessageBoxButtons.OKCancel,
                 MessageBoxIcon.Error);

            if (reult != DialogResult.Cancel)
                IniciarVenda();
        }

        private bool VerificaPermissaoExclusao()
        {
            if (!Program.InicializacaoViewAux.HabSenhaExcluirItem)
            {
                TouchMessageBox.Show("Operação não permitida para esse PDV.", "Autorização", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            var senha = FormSolicitaSenha.Instace();

            if (Program.InicializacaoViewAux.SenhaExcluirItem != senha)
            {
                TouchMessageBox.Show("Senha invalida! Consulte o supervisor", "Autorização", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnObservacao_Click(object sender, EventArgs e)
        {
            //StartOSK();
            var selected = cupomGridView1.SelectedItem;
            if (cupomGridView1.SelectedItem == null)
            {
                TouchMessageBox.Show("Nenhum produto selecionado", "Venda PDV", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            using (var form = new FormObservacao(selected.DataSource))
            {

                form.ShowDialog();
                cupomGridView1.Atualizar(VendaView.VendaItens);

            }
        }


        static void StartOSK()
        {
            string windir = Environment.GetEnvironmentVariable("WINDIR");
            string osk = null;

            if (osk == null)
            {
                osk = Path.Combine(Path.Combine(windir, "sysnative"), "osk.exe");
                if (!File.Exists(osk))
                    osk = null;
            }

            if (osk == null)
            {
                osk = Path.Combine(Path.Combine(windir, "system32"), "osk.exe");
                if (!File.Exists(osk))
                {
                    osk = null;
                }
            }

            if (osk == null)
                osk = "osk.exe";

            //Process.Start(osk);
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            StartOSK();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (VendaView.VendaItens.Count == 0)
            {
                TouchMessageBox.Show("Venda não iniciada.", "Venda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (VendaView.IsDelivery)
            {
                var result = TouchMessageBox.Show("Venda iniciada como Entrega\nDeseja finalizar como venda balção?",
                    "Venda", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                    return;

                VendaView.IsDelivery = false;
                VendaView.Delivery = new DeliveryViewModel();

            }
            var valorReceber = VendaView.VendaItens.Sum(t => t.ValorTotal);
            using (var form = new FormPagamentoToten(valorReceber))
            {
                form.ShowDialog();

                var isPago = form.IsPago;

                if (!isPago) return;

                //Verifica se pede numero do pager
                if (Program.InicializacaoViewAux.HabSenhaPager)
                {
                    var opcaoObs = FormSolicitaPergunta.Instace("SELECIONE UMA OPÇÃO", true);
                    VendaView.Observacao += opcaoObs;
                }


                //Grava venda
                using (var vendaApp = Program.Container.GetInstance<IVendaAppService>())
                {
                    var vendaId = vendaApp.ObterVendaId();
                    VendaView.VendaId = int.Parse($"{Program.PdvId}{vendaId}");

                    vendaApp.Adicionar(VendaView);

                    vendaApp.GeraImpressaoItens(VendaView.VendaId, 0);

                }
                //Grava Caixa Itens
                var caixaItem = form.CaixaItemView;
                caixaItem.CaixaPagamentos = form.Pagamentos;

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
                try
                {
                    var emissarFiscal = Program.IsFrete ? Program.EmissorFiscal : ModeloFiscalEnumView.None;
                    //Imprime Cupom
                    switch (emissarFiscal)
                    {
                        case ModeloFiscalEnumView.None:
                            ImprimeCupomNaoFiscal();
                            
                            break;
                        case ModeloFiscalEnumView.Ecf:
                            break;
                        case ModeloFiscalEnumView.CfeSAT:
                            var retorno = OperacoeFiscal.ImprimeSat(VendaView);
                            if (retorno.IsOk)
                            {
                                using (var retornoSatAppService = Program.Container.GetInstance<IRetornoSatAppService>())
                                {
                                    retornoSatAppService.Adicionar(retorno);
                                }
                            }
                            else
                            {
                                Program.GravaLog($"Erro ao emitir o SAT {retorno.Mensagem}");
                                ImprimeCupomNaoFiscalTotem(VendaView);
                            }

                            break;
                        case ModeloFiscalEnumView.NFCe:
                            OperacoeFiscal.ImprimeNfce(VendaView);
                            
                            break;
                    }

                    //Envia GR
                }
                catch (Exception exception)
                {
                    Program.GravaLog(exception.Message);
                    Funcoes.MensagemError("Todo mundo erra, e dessa vez foram os nossos servidores.\nDirigisse até um caixa e informe o problema.");
                }
                finally
                {
                    ImprimeComprovanteTef(caixaItem);
                    ImprimeItensTotem(VendaView);
                    //ImprimeCupomNaoFiscalTotem(VendaView);
                    
                    Dispose();
                }

            }
        }
        private void ImprimeItensTotem(VendaViewModel venda)
        {
            foreach (var item in venda.VendaItens)
            {
                var qtde = item.Quantidade;
                for (int i = 0; i < item.Quantidade; i++)
                {
                    var itemToList = new List<VendaItemViewModel>();

                    itemToList.Add(new VendaItemViewModel()
                    {
                        Quantidade = 1,
                        Produto = item.DescricaoProduto,
                        ValorUnitatio = item.ValorUnitatio,
                        Observacao = item.Observacao

                    });
                    var parms = new ParameterReportDynamic();

                    var report = new RelatorioFastReport();
                    report.GerarRelatorio("Imp_ItemTotem", parms, itemToList);
                }
            }
        }

        private void ImprimeCupomNaoFiscal()
        {
            using (var vendaApp = Program.Container.GetInstance<IVendaAppService>())
            {
                var tipoOperacao = VendaView.IsDelivery ? 5 : 4;
                vendaApp.GeraImpressaoFechamento(VendaView.VendaId, tipoOperacao);
            }

        }
        private void ImprimeCupomNaoFiscalTotem(VendaViewModel venda)
        {
            var report = new RelatorioFastReport();

            var parms = new ParameterReportDynamic();
            
            parms.Add("VendaId", venda.VendaId);
            report.GerarRelatorio("Imp_VendaFinaliza", parms);

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
                    if (comprovanteTef.Comprovante == null) continue;
                    var parms = new ParameterReportDynamic();
                    parms.Add("Comprovante", comprovanteTef.Comprovante);

                    report = new RelatorioFastReport();
                    report.GerarRelatorio("Imp_ComprovanteTef", parms);
                }
            }

        }

        private void IniciarVenda()
        {
            // btnSolicitaCpf.Text = "CPF na nota?";
            //lbClienteDelivery.Text = "VENDA BALÇÃO";
            myListFichas = new List<string>();

            AlteraFrete(Program.GetIsFrete);


            VendaView = new VendaViewModel()
            {
                CaixaId = Program.CaixaView.CaixaId,
                UsuarioId = Program.Usuario.UsuarioId,
                DataHora = DateTime.Now,
                Pdv = Program.Pdv,
                Tipo = "V",
                Loja = Program.Loja
            };
            cupomGridView1.DataSource = null;
            cupomGridView1.SelectedItem = null;

            cupomGridView1.Atualizar(VendaView.VendaItens);

            _produtos = new List<ProdutoViewModel>();
            flayoutProduto.Controls.Clear();

            _currentPage = 1;
            CarregaGrupos();



            TotalizaCupom();
            //txtPesquisaProduto.Select();
        }

        private void btnBuscarProduto_Click(object sender, EventArgs e)
        {/*
            using (var form = new FormBuscaProduto(txtPesquisaProduto.Text))
            {
                form.ShowDialog();

                var produto = form.ProdutoView;
                if (produto == null) return;

                IncluirProdutoPesquisa(produto);
            }*/
        }
        private void FormPdv_Resize(object sender, EventArgs e)
        {
            //CarregaGrupos();
            //IniciaVenda();
            _produtos = new List<ProdutoViewModel>();
            flayoutProduto.Controls.Clear();
            CarregaGrupos();



            //TotalizaCupom();
            // txtPesquisaProduto.Select();

        }

        private void btnComplemento_Click(object sender, EventArgs e)
        {
            var gridItem = cupomGridView1.SelectedItem;
            if (gridItem == null)
            {
                TouchMessageBox.Show("Nenhum produto selecionado", "Venda PDV", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var vendaItem = gridItem.DataSource;

            var produto = _produtoAppService.ObterPorId(vendaItem.ProdutoId);
            var complementos = _complementoAppService.ObterPorGrupoId(produto.GrupoId).ToList();

            if (complementos.Any())
            {
                using (var form = new FormComplementos(complementos, vendaItem))
                {
                    form.ShowDialog();
                    vendaItem = form.VendaItem;

                    vendaItem.ValorUnitatio += vendaItem.VendaComplementos.Sum(t => t.Valor);
                }
                VendaView.VendaItens[gridItem.Index] = vendaItem;
                cupomGridView1.Atualizar(VendaView.VendaItens);
            }
            else
            {
                TouchMessageBox.Show("Não foi encontrado complementos para esse produto!", "Complemento",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDesconto_Click(object sender, EventArgs e)
        {
            if (!VendaView.VendaItens.Any())
            {
                TouchMessageBox.Show("Não existe item na venda.", "Venda PDV", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var descontoFinal = FormDesconto.Instace();
            if (!descontoFinal.ResultOk) return;

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

            pDesconto = VerificaDesconto(decimal.Round(pDesconto, 3));
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

            if (descontoFinal.ValorReal > 0)
            {
                //descontoFinal.ValorReal = VendaView.VendaItens.Sum(t => t.Desconto);
                for (var i = 0; i < 100; i++)
                {
                    decimal totalNovo = decimal.Round(VendaView.VendaItens.Sum(p => p.ValorTotal), 2);
                    decimal comparar = (vTotal - descontoFinal.ValorReal) - totalNovo;
                    if (comparar != 0)
                    {
                        VendaView.VendaItens.First().ValorUnitatio += comparar;
                    }
                    else
                        break;

                }
            }
            cupomGridView1.Atualizar(VendaView.VendaItens);
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

        private void btnSolicitaCpf_Click(object sender, EventArgs e)
        {
            var cpf = FormSolicitaCpf.Instace();
            VendaView.Cnpj = cpf.Replace(".", "").Replace("-", "");
            //if (!string.IsNullOrEmpty(VendaView.Cnpj))
            //    btnSolicitaCpf.Text = $"CPF: {cpf}";
        }

        private void IniciaVenda()
        {

            IniciarVenda();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            var cliente = FormCliente.Instance();

            if (cliente == null)
                return;

            //lbClienteDelivery.Text = $"VENDA ENTREGA: {cliente.Nome} | {cliente.Endereco}";

            VendaView.IsDelivery = true;
            VendaView.Delivery.ClienteDelivery = cliente;
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            if (VendaView.VendaItens.Count == 0)
            {
                TouchMessageBox.Show("Venda não iniciada.", "Venda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var valorReceber = VendaView.VendaItens.Sum(t => t.ValorTotal);
            var deliveryView = FormPdvDelivery.Instance(VendaView.Delivery, valorReceber);

            if (deliveryView == null) return;

            VendaView.Delivery = deliveryView;
            VendaView.IsDelivery = true;

            //Grava venda
            using (var vendaApp = Program.Container.GetInstance<IVendaAppService>())
            {
                var vendaId = vendaApp.ObterVendaId();
                VendaView.VendaId = int.Parse($"{Program.PdvId}{vendaId}");

                vendaApp.Adicionar(VendaView);

                vendaApp.GeraImpressaoItens(VendaView.VendaId, vendaId = 0);

                ImprimeCupomNaoFiscal();
            }

            IniciarVenda();

        }

        private void btnFidelidade_Click(object sender, EventArgs e)
        {
            VendaView.Fidelidade = FormSolicitaCpf.Instace();
            Funcoes.MensagemInformation($"Cliente Fidelidade: {VendaView.Fidelidade}");
        }


        void AlteraFrete(bool move)
        {
            Program.IsFrete = move;
            //lbClienteDelivery.BackColor = Program.IsFrete ? Color.WhiteSmoke : Color.LightSalmon;
        }


        void timerPrincipal_Tick(object sender, EventArgs e)
        {
            try
            {

                ImportExterno.tagLASTINPUTINFO LastInput = new ImportExterno.tagLASTINPUTINFO();
                Int32 IdleTime;
                LastInput.cbSize = (uint)Marshal.SizeOf(LastInput);
                LastInput.dwTime = 0;

                if (ImportExterno.GetLastInputInfo(ref LastInput))
                {
                    IdleTime = ((Environment.TickCount - LastInput.dwTime) / 1000) / 60;
                    if (IdleTime >= 1 && VendaView.VendaItens.Count > 0)
                    {
                        timerPrincipal.Stop();

                        var reult = TouchMessageBox.Show("Cancelar o pedido por falta de interação?", "Pedido", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        var formOpen = reult != DialogResult.Cancel;
                        if (formOpen)
                        {
                            // _isReset = true;
                            IniciarVenda();
                        }
                        timerPrincipal.Start();
                    }
                }

            }
            catch
            {
                //Ignore
            }

        }

    }
}

