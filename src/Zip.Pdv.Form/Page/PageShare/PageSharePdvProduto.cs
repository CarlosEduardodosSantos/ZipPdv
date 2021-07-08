using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component;

namespace Zip.Pdv.Page.PageShare
{
    public partial class PageSharePdvProduto : PageBase
    {
        private ProdutoViewModel _produtoView;
        public PageSharePdvProduto()
        {
            InitializeComponent();
        }

        private void txtProdutoPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            PesquisaProduto();
            txtProdutoPesquisa.Clear();
            btnIncluir.Select();
        }

        void PesquisaProduto()
        {
            using (var form = new FormBuscaProduto(txtProdutoPesquisa.Text))
            {
                form.ShowDialog();

                _produtoView = form.ProdutoView;
                if (_produtoView == null) return;

                CarregarItem();
            }
        }

        void CarregarItem()
        {
            if (_produtoView == null) return;

            lbItem.Text = _produtoView.Descricao;
            lbUnitario.Text = _produtoView.ValorVenda.ToString("C2");
            GetImageAsync(_produtoView.ProdutoId);
        }
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            IncluirProdutoPesquisa();
        }
        private void IncluirProdutoPesquisa()
        {
            if (_produtoView == null)
            {
                TouchMessageBox.Show("Produto não encontrado!", "Leitura Ean", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            var item = new ProdutoGridViewItemImage()
            {
                SelectedItem = _produtoView,
                Quantidade = txtQuantidade.ValueNumeric
            };
            selectedItem(item, EventArgs.Empty);

            lbItem.Text = "AGUARDANDO PESQUISA...";
            lbUnitario.Text = 0.ToString("C2");

            txtQuantidade.ValueNumeric = 1;
            txtProdutoPesquisa.Select();
            imageProd.Image = null;


        }

        private void GetImageAsync(int produtoId)
        {
            var operation = AsyncOperationManager.CreateOperation(null);
            var thread = new Thread(new ThreadStart(delegate ()
            {
                try
                {
                    using (var produtoAppService = Program.Container.GetInstance<IProdutoAppService>())
                    {
                        var imagem = produtoAppService.ObterImageProdutoId(produtoId);
                        if (imagem == null) throw new Exception("Erro");

                        imageProd.Image = Funcoes.Base64ToImage(imagem);
                        imageProd.SizeMode = PictureBoxSizeMode.StretchImage;

                        _produtoView.Imagem = imagem;
                    }
                }
                catch (Exception ex)
                {
                    operation.PostOperationCompleted(ReadError, ex);
                }
            }));
            thread.Start();
        }
        private void ReadError(object error)
        {
            if (error is Exception)
            {
                imageProd.Visible = false;
            }
        }
    }
}
