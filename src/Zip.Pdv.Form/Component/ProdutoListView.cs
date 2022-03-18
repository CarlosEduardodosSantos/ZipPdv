using Eticket.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zip.Pdv.Component
{
    public partial class ProdutoListView : UserControl
    {
        private ProdutoOpcaoTipoVewModel _produtoOpcaoTipo;
        private int _pageProdQuantidade;
        private int _currentProdPage = 1;

        public event EventHandler<EventArgs> SelectItem;
        void selectItem(object sender, EventArgs e)
        {
            var completedEvent = SelectItem;
            if (completedEvent != null)
            {
                var item = (ProdutoGridViewItem)sender;
                completedEvent(item, e);
            }
        }

        public ProdutoListView(ProdutoOpcaoTipoVewModel produtoOpcaoTipo)
        {
            InitializeComponent();
            _produtoOpcaoTipo = produtoOpcaoTipo;
        }

        private void ProdutoPaginacao(int page)
        {
            btnNextProd.Enabled = false;
            btnPrevProd.Enabled = false;

            int qItemW = flayoutProduto.Width / 150;
            int qItemH = flayoutProduto.Height / 90;
            int itens = qItemW * qItemH;

            var skip = itens * (page - 1);

            _pageProdQuantidade = int.Parse(Math.Ceiling(_produtoOpcaoTipo.ProdutoOpcaos.Count() / double.Parse(itens.ToString())).ToString());
            if (_pageProdQuantidade > 1)
                btnNextProd.Enabled = true;

            flayoutProduto.Controls.Clear();

            var produtoPadding = _produtoOpcaoTipo.ProdutoOpcaos.Skip(skip).Take(itens).ToList();
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
                    btnGridItem.SelectItem += selectItem;
                    btnGridItem.AdiconaProdutoOpcoes(produtoComplementoViewModel);
                    controls[index] = btnGridItem;

                    index++;

                }

                flayoutProduto.Controls.AddRange(controls);
            }

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

        private void ProdutoListView_Load(object sender, EventArgs e)
        {
            ProdutoPaginacao(1);
            
        }
    }
}
