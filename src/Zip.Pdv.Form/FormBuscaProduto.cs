using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component.ProdutoGrid;

namespace Zip.Pdv
{
    public partial class FormBuscaProduto : Form
    {
        private readonly ProdutoGrid _produtoGrid;
        public ProdutoViewModel ProdutoView { get; set; }
        private List<ProdutoViewModel> _produtos;
        public FormBuscaProduto(string busca = null)
        {
            InitializeComponent();
            _produtoGrid = new ProdutoGrid()
            {
                Dock = DockStyle.Fill
            };
            _produtoGrid.SelectItem += _produtoGrid_SelectItem;
            _produtoGrid.KeyDownItem += _produtoGrid_KeyDownItem;
            panelGrid.Controls.Add(_produtoGrid);

            txtBuscarProduto.Text = busca;
            
        }

        private void _produtoGrid_KeyDownItem(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode != Keys.Enter)return;

            var itemGrid = (ProdutoGridItem)sender;
            var produto = itemGrid.ProdutoView;

            ProdutoView = produto;

            this.Close();

            //_produtoGrid_SelectItem(sender, EventArgs.Empty);

        }

        private void _produtoGrid_SelectItem(object sender, EventArgs e)
        {
            var itemGrid = (ProdutoGridItem) sender;
            var produto = itemGrid.ProdutoView;

            ProdutoView = produto;

            Close();
        }

        private void BuscarProduto()
        {
            using (var produtoApp = Program.Container.GetInstance<IProdutoAppService>())
            {
                
                if (txtBuscarProduto.Text.IsNumeric())
                {
                    _produtos = txtBuscarProduto.Text.ValidarEan13() ? produtoApp.ObterPorEan(txtBuscarProduto.Text).Take(30).ToList() : produtoApp.ObterPorEan(txtBuscarProduto.Text).Take(30).ToList();
                }
                else
                {
                    _produtos = produtoApp.ObterPorNome(txtBuscarProduto.Text).Take(30).ToList();
                }
                if (_produtos.Count == 1)
                {
                    ProdutoView = _produtos[0];
                    Close();
                }
                else
                {
                    _produtoGrid.IniciarGrid(_produtos);
                    _produtoGrid.Focus();
                }
                
            }
        }

        private void txtBuscarProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BuscarProduto();
        }

        private void btnCancelarVenda_Click(object sender, EventArgs e)
        {
            ProdutoView = null;
            Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarProduto();
        }

        private void FormBuscaProduto_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBuscarProduto.Text))
                BuscarProduto();

            txtBuscarProduto.Select();
        }
    }
}
