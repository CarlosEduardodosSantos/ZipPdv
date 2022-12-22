using System;
using System.Drawing;
using System.Windows.Forms;
using Eticket.Application.ViewModels;

namespace Zip.Pdv.Component.ProdutoGrid
{
    public partial class ProdutoGridItem : Button
    {
        public readonly ProdutoViewModel ProdutoView;
        public readonly ClienteViewModel ClienteView;
        public event EventHandler<EventArgs> SelectItem;
        public event EventHandler<PreviewKeyDownEventArgs> KeyDownItem;
        void selectItem(object sender, EventArgs e)
        {
            var completedEvent = SelectItem;
            if (completedEvent != null)
            {
                panel1.BackColor = Color.DarkCyan;

                var item = (ProdutoGridItem)this;
                completedEvent(item, e);
            }
        }

        void keyDownItem(object sender, PreviewKeyDownEventArgs e)
        {

            var completedEvent = KeyDownItem;
            if (completedEvent != null)
            {
                //panel1.BackColor = Color.DarkCyan;

                var item = (ProdutoGridItem)this;
                completedEvent(item, e);
            }
        }
        public ProdutoGridItem(ProdutoViewModel produtoView)
        {
            ProdutoView = produtoView;
            InitializeComponent();
            lbDescricao.Click += selectItem;
            lbUnidade.Click += selectItem;
            lbEstoque.Click += selectItem;
            lbValor.Click += selectItem;

            this.PreviewKeyDown += keyDownItem;


            ProdutoGridItem_Load();
        }

        public ProdutoGridItem(ClienteViewModel clienteViewModel)
        {
            ClienteView = clienteViewModel;
            InitializeComponent();
            lbDescricao.Click += selectItem;

            tableLayoutPanel1.ColumnCount = 1;
            lbUnidade.Visible = false;
            lbUnidade.Click += selectItem;
            lbEstoque.Click += selectItem;
            lbValor.Click += selectItem;

            this.PreviewKeyDown += keyDownItem;


            ClienteGridItem_Load();
        }

        public void ProdutoGridItem_Load()
        {
            lbDescricao.Text = ProdutoView.Descricao;
            lbUnidade.Text = ProdutoView.Unidade;
            lbEstoque.Text = ProdutoView.ProdutoTipo;
            lbValor.Text = ProdutoView.ValorVenda.ToString("N2");
        }

        public void ClienteGridItem_Load()
        {
            lbDescricao.Text = ClienteView.Nome;
            lbUnidade.Text = ClienteView.Cpf;
            lbEstoque.Text = ClienteView.Cidade;
            lbValor.Text = ClienteView.Telefone;
        }

        private void lbDescricao_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.DarkCyan;
        }

        private void lbValor_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }
    }
}
