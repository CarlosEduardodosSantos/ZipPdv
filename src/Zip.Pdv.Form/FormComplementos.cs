using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component;
using Zip.Pdv.Component.EspeciePagamento;

namespace Zip.Pdv
{
    public partial class FormComplementos : Form
    {
        private List<ProdutoComplementoViewModel> _complementos;
        public VendaItemViewModel VendaItem;
        private int _pageProdQuantidade;
        private int _currentProdPage = 1;

        public FormComplementos(List<ProdutoComplementoViewModel> complementos, VendaItemViewModel vendaItem)
        {
            _complementos = complementos;
            VendaItem = vendaItem;
            
            InitializeComponent();

            lbProdutoNome.Text = vendaItem.Produto;

        }

        private void FormComplementos_Load(object sender, System.EventArgs e)
        {
            ProdutoPaginacao(1);
            CarregaDescricao();
        }

        private void ProdutoPaginacao(int page)
        {
            btnNextProd.Enabled = false;
            btnPrevProd.Enabled = false;

            int qItemW = flowLayoutPanel1.Width / 110;
            int qItemH = flowLayoutPanel1.Height / 90;
            int itens = qItemW * qItemH;

            var skip = itens * (page - 1);

            _pageProdQuantidade = int.Parse(Math.Ceiling(_complementos.Count() / double.Parse(itens.ToString())).ToString());
            if (_pageProdQuantidade > 1)
                btnNextProd.Enabled = true;

            flowLayoutPanel1.Controls.Clear();

            var produtoPadding = _complementos.Skip(skip).Take(itens).ToList();
            if (produtoPadding.Count > 0)
            {
                var controls = new Control[produtoPadding.Count];
                var index = 0;
                foreach (var produtoComplementoViewModel in produtoPadding)
                {
                    var btnGridItem = new ProdutoGridViewItem()
                    {
                        Index = index,
                        BackColor = Color.Orange
                    };
                    btnGridItem.SelectItem += Item_SelectItem;
                    btnGridItem.AdiconaComplemento(produtoComplementoViewModel);
                    controls[index] = btnGridItem;

                    index++;

                }

                flowLayoutPanel1.Controls.AddRange(controls);
            }
                /*
                foreach (var produtoComplementoViewModel in _complementos)
            {
                var item = new ProdutoGridViewItem();
                item.BackColor = Color.Orange;
                item.AdiconaComplemento(produtoComplementoViewModel);
                item.SelectItem += Item_SelectItem;
                flowLayoutPanel1.Controls.Add(item);
            }*/

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


        private void Item_SelectItem(object sender, System.EventArgs e)
        {
            var obj = (ProdutoGridViewItem) sender;
            var complemento = (ProdutoComplementoViewModel)obj.SelectedItem;

            VendaItem.VendaComplementos.Add(new VendaComplementoViewModel()
            {
                ComplementoId = complemento.ComplementoId,
                ProdutoId = VendaItem.ProdutoId,
                Sequencia = VendaItem.SeqProduto,
                Valor = complemento.Valor,
                Descricao = complemento.Descricao
            });
            CarregaDescricao();
        }

        private void CarregaDescricao()
        {
            flowLayoutPanel2.Controls.Clear();
            foreach (var vendaItemVendaComplemento in VendaItem.VendaComplementos)
            {
                var item = new UcPdvItem();
                item.AdicionarComplemento(vendaItemVendaComplemento);
                item.Click += Item_Click;
                flowLayoutPanel2.Controls.Add(item);
                //btn.Anchor = AnchorStyles.None;
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var item = (UcPdvItem) sender;
            var source = (VendaComplementoViewModel)item.CaixaSource;
            VendaItem.VendaComplementos.Remove(source);
            CarregaDescricao();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
