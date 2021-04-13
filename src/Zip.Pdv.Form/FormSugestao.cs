using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component;
using Zip.Pdv.Component.EspeciePagamento;

namespace Zip.Pdv
{
    public partial class FormSugestao : Form
    {
        private List<ProdutoViewModel> _produtos;
        public List<VendaItemViewModel> VendaSugestaoItens;
        
        private int _pageProdQuantidade;
        private int _currentProdPage = 1;

        public FormSugestao(List<ProdutoViewModel> produtos, string produtoNome)
        {
            _produtos = produtos;
            VendaSugestaoItens = new List<VendaItemViewModel>();

            InitializeComponent();

            lbProdutoNome.Text = produtoNome;

        }
        private void FormComplementos_Load(object sender, System.EventArgs e)
        {
            ProdutoPaginacao(1);
        }


        private void ProdutoPaginacao(int page)
        {
            btnNextProd.Enabled = false;
            btnPrevProd.Enabled = false;

            int qItemW = flayoutProduto.Width / 95;
            int qItemH = flayoutProduto.Height / 53;
            int itens = qItemW * qItemH;

            var skip = itens * (page - 1);

            _pageProdQuantidade = int.Parse(Math.Ceiling(_produtos.Count() / double.Parse(itens.ToString())).ToString());
            if (_pageProdQuantidade > 1)
                btnNextProd.Enabled = true;

            flayoutProduto.Controls.Clear();

            var produtoPadding = _produtos.Skip(skip).Take(itens).ToList();
            if (produtoPadding.Count > 0)
            {
                var controls = new Control[produtoPadding.Count];
                var index = 0;
                foreach (var t in produtoPadding)
                {
                    var btnGridItem = new ProdutoGridViewItemImage()
                    {
                        Index = index
                    };
                    btnGridItem.SelectItem += BtnGridItem_SelectItem;
                    btnGridItem.AdiconaDataSource(t);
  
                    controls[index] = btnGridItem;

                    index++;

                }
                flayoutProduto.Controls.AddRange(controls);
            }
        }
        private void BtnGridItem_SelectItem(object sender, EventArgs e)
        {

            var item = (ProdutoGridViewItemImage)sender;
            var produto = (ProdutoViewModel)item.SelectedItem;

            var vendaItem = new VendaItemViewModel()
            {
                ProdutoId = produto.ProdutoId,
                Produto = produto.Descricao,
                ValorUnitatio = produto.ValorVenda,
                Quantidade = 1,
                ProdutoViewModel = produto
            };
            VendaSugestaoItens.Add(vendaItem);

            CarregaDescricao();
        }


        private void btnNextProd_Click(object sender, EventArgs e)
        {
            _currentProdPage++;
            ProdutoPaginacao(_currentProdPage);
            if (_currentProdPage == _pageProdQuantidade)
                btnNextProd.Enabled = false;
            if (_currentProdPage > 1)
                btnPrevProd.Enabled = true;
        }

        private void btnPrevProd_Click(object sender, EventArgs e)
        {
            _currentProdPage--;
            ProdutoPaginacao(_currentProdPage);

            if (_currentProdPage == 1)
                btnPrevProd.Enabled = false;
        }

        private void CarregaDescricao()
        {
            flowLayoutPanel2.Controls.Clear();
            foreach (var vendaItemVendas in VendaSugestaoItens)
            {
                var item = new UcPdvItem();
                item.AdicionarComplemento(vendaItemVendas);
                item.Click += Item_Click;
                flowLayoutPanel2.Controls.Add(item);
                //btn.Anchor = AnchorStyles.None;
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var item = (UcPdvItem) sender;
            var source = (VendaItemViewModel)item.CaixaSource;
            VendaSugestaoItens.Remove(source);
            CarregaDescricao();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
