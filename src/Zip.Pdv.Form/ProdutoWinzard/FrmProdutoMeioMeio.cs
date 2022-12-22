using Eticket.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Zip.Pdv.Component;
using Zip.Pdv.Component.EspeciePagamento;

namespace Zip.Pdv
{
    public partial class FrmProdutoMeioMeio : Form
    {
        public VendaItemViewModel VendaItem;
        public List<VendaItemViewModel> VendaItensAdicionais;
        private readonly List<ProdutoViewModel> _produtosMeioMeio;
        public ProdutoOpcaoTipoVewModel _tipoVewModel;
        private string[] _tabs;
        private int _tabindex;
        public FrmProdutoMeioMeio(List<ProdutoViewModel> produtosMeioMeio, VendaItemViewModel vendaItem)
        {
            InitializeComponent();
            _produtosMeioMeio = produtosMeioMeio;
            VendaItem = vendaItem;
            VendaItensAdicionais = new List<VendaItemViewModel>();
        }

        private void FormBase_Load(object sender, EventArgs e)
        {
            tablessControl1.Selected += tabControl1_Selected;
            lbProdutoNome.Text = VendaItem.DescricaoProduto;
    

            CarregaOpcoes();

        }

        void CarregaOpcoes()
        {


            _tipoVewModel = new ProdutoOpcaoTipoVewModel() { 
                Nome = "Selecione os sabores",
                Obrigatorio = true,
                QtdeMax = 2,
                Quantidade = 2,
                ProdutosOpcaoTipoId = 1
            };

            _tipoVewModel.ProdutoOpcaos = new List<ProdutoOpcaoViewModel>();
            foreach (var itemMeioMeio in _produtosMeioMeio)
            {
                _tipoVewModel.ProdutoOpcaos.Add(new ProdutoOpcaoViewModel() {
                    Nome = itemMeioMeio.Descricao,
                    ProdutoId = itemMeioMeio.ProdutoId,
                    Valor = itemMeioMeio.ValorVenda,
                    ProdutosOpcaoTipoId = 1,
                    ProdutoPdv = itemMeioMeio.ProdutoId.ToString()
                });
            }

            //_tabs[0] = $"tabMeioMeio";

            var table = new TabPage(_tipoVewModel.Nome);
            table.Name = $"tabMeioMeio";

            // table.Text = item.Nome;

            tablessControl1.TabPages.Add(table);

            var ucOpcoes = new ProdutoListView(_tipoVewModel);
            ucOpcoes.Dock = DockStyle.Fill;
            ucOpcoes.SelectItem += UcOpcoes_SelectItem;
            table.Controls.Add(ucOpcoes);

            tablessControl1.SelectedIndex = _tabindex;
            tabControl1_Selected(tablessControl1, null);
        }

        private void UcOpcoes_SelectItem(object sender, EventArgs e)
        {
            var item = (ProdutoGridViewItem)sender;
            var produtoOpcao = (ProdutoOpcaoViewModel)item.SelectedItem;
    
            decimal quatidade = 1;
            var qtdeLancTipo = VendaItem.VendaProdutoMeioMeio.Where(t => t.ProdutosOpcaoTipoId == produtoOpcao.ProdutosOpcaoTipoId).Sum(t => t.Quantidade)+1;
            if (_tipoVewModel.QtdeMax > 0)
            {
                if (qtdeLancTipo > _tipoVewModel.QtdeMax)
                {
                    TouchMessageBox.Show($"Não é permitido escolher mais que {_tipoVewModel.QtdeMax}\npara essa opção.", "Validação", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    return;
                }
            }


            if (!string.IsNullOrEmpty(produtoOpcao.ProdutoPdv) && produtoOpcao.Valor > 999)
            {
                var seq = VendaItensAdicionais.Any() ? VendaItensAdicionais.Max(t => t.SeqProduto) + 1 : VendaItem.SeqProduto + 1;
                VendaItensAdicionais.Add(new VendaItemViewModel()
                {
                    Produto = produtoOpcao.Nome,
                    ValorUnitatio = quatidade*produtoOpcao.Valor,
                    ProdutoId = int.Parse(produtoOpcao.ProdutoPdv),
                    Quantidade = quatidade,
                    SeqProduto = seq

                });
            }
            else
            {
                VendaItem.VendaProdutoMeioMeio.Add(new VendaProdutoOpcaoViewModel()
                {
                    ProdutosOpcaoId = produtoOpcao.ProdutosOpcaoId,
                    ProdutosOpcaoTipoId = produtoOpcao.ProdutosOpcaoTipoId,
                    ProdutoId = VendaItem.ProdutoId,
                    Sequencia = VendaItem.SeqProduto,
                    Valor = quatidade * produtoOpcao.Valor,
                    Descricao = $"1/2 {produtoOpcao.Nome}",
                    Quantidade = quatidade,
                    ProdutoPdv = produtoOpcao.ProdutoPdv
                });

            }

            CarregaDescricao();


            //Valida fim
            bunifuFlatButton3.Enabled = qtdeLancTipo == _tipoVewModel.QtdeMax;

        }

        private void CarregaDescricao()
        {
            flowLayoutPanel2.Controls.Clear();
            foreach (var vendaItemVendaProdutoOpcoes in VendaItem.VendaProdutoMeioMeio)
            {
                var item = new UcPdvItem();
                item.AdicionarProdutoOpcoes(vendaItemVendaProdutoOpcoes);
                item.Click += Item_Click;
                flowLayoutPanel2.Controls.Add(item);
                //btn.Anchor = AnchorStyles.None;
            }

            foreach (var vendaItemVendaProdutoOpcoes in VendaItensAdicionais)
            {
                var item = new UcPdvItem();
                item.AdicionarComplemento(vendaItemVendaProdutoOpcoes);
                item.Click += Item_Click;
                flowLayoutPanel2.Controls.Add(item);
                //btn.Anchor = AnchorStyles.None;
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var item = (UcPdvItem)sender;
            var source = (VendaProdutoOpcaoViewModel)item.CaixaSource;
            VendaItem.VendaProdutoMeioMeio.Remove(source);
            CarregaDescricao();
        }


        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            var tab = tablessControl1.TabPages[_tabindex];
            lbTabNome.Text = tab.Text;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if (!bunifuFlatButton3.Enabled) return;
            DialogResult = DialogResult.OK;
            Close();
        }
    }

}
