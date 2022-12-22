using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component.ProdutoGrid;

namespace Zip.Pdv
{
    public partial class FormBuscaCliente : Form
    {
        private readonly ProdutoGrid _produtoGrid;

        public ClienteViewModel ClienteView { get; set; }
        private List<ClienteViewModel> _clientes;
        public FormBuscaCliente(string busca = null)
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
            var clienteView = itemGrid.ClienteView;

            ClienteView = clienteView;

            this.Close();

            //_produtoGrid_SelectItem(sender, EventArgs.Empty);

        }

        private void _produtoGrid_SelectItem(object sender, EventArgs e)
        {
            var itemGrid = (ProdutoGridItem) sender;
            var clienteView = itemGrid.ClienteView;

            ClienteView = clienteView;

            Close();
        }

        private void BuscarProduto()
        {
            using (var clienteApp = Program.Container.GetInstance<IClienteAppService>())
            {
                
                if (txtBuscarProduto.Text.IsNumeric())
                {
                    var produtoId = int.Parse(txtBuscarProduto.Text);
                    _clientes = clienteApp.ObterPorCodigo(produtoId).ToList();
                }
                else
                {
                    _clientes = clienteApp.ObterPorNome(txtBuscarProduto.Text).ToList();
                }
                if (_clientes.Count == 1)
                {
                    ClienteView = _clientes[0];
                    Close();
                }
                else
                {
                    _produtoGrid.IniciarGridCliente(_clientes);
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
            ClienteView = null;
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
