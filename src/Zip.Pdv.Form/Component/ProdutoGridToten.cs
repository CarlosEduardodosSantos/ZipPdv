using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zip.Pdv.Component
{
    public partial class ProdutoGridToten : UserControl
    {
        public event EventHandler<EventArgs> SelectItem;
        void selectItem(object sender, EventArgs e)
        {
            var completedEvent = SelectItem;
            if (completedEvent != null)
            {
                var item = (ProdutoGridToten)this;
                completedEvent(item, e);
            }
        }
        public object SelectedItem { get; set; }
        public int Index { get; set; }
        [DefaultValue(false)]
        public bool HideValorVenda { get; set; }
        public Color ColorText { get; set; }
        private Color _fixColor;

        public ProdutoGridToten()
        {
            InitializeComponent();

            lbProduto.Text = String.Empty;


            lbProduto.Click += selectItem;
            lbProduto.MouseEnter += lbDescricao_MouseEnter;
            lbProduto.MouseLeave += lbValor_MouseLeave;

            imageProd.Click += selectItem;
            imageProd.MouseEnter += lbDescricao_MouseEnter;
            imageProd.MouseLeave += lbValor_MouseLeave;
        }

        public void AdiconaDataSource(ProdutoViewModel produto)
        {
            lbProduto.Text = produto.Descricao;
            //lbProduto.ForeColor = ColorText;

            lbValorVenda.Visible = Program.TotemHabPreco;
            lbValorVenda.Text = produto.ValorVenda.ToString("C2");
            //lbValorVenda.ForeColor = ColorText;
            SelectedItem = produto;

            if (HideValorVenda)
            {
                lbValorVenda.Visible = false;
            }
            GetImageAsync(produto);

        }
        private void lbDescricao_MouseEnter(object sender, EventArgs e)
        {
            _fixColor = this.BackColor;
            this.BackColor = Color.CornflowerBlue;
        }

        private void lbValor_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = _fixColor;
        }
        public void GetImageAsync(ProdutoViewModel produto)
        {
            var produtoId = produto.ProdutoId;
            var operation = AsyncOperationManager.CreateOperation(null);
            var thread = new Thread(new ThreadStart(delegate ()
            {
                try
                {
                    if (!produto.Visivel)
                    {
                        imageProd.Image = Properties.Resources.produto_indisponivel;
                        return;
                    }

                    using (var produtoAppService = Program.Container.GetInstance<IProdutoAppService>())
                    {
                        var imagem = produtoAppService.ObterImageProdutoId(produtoId);
                        if (imagem == null)
                        {
                            imageProd.ImageLocation = "https://img1.gratispng.com/20180202/pre/kisspng-hamburger-street-food-seafood-fast-food-delicious-food-5a75083c57a5f5.317349121517619260359.jpg";
                            //btnIcom.Visible = false;
                            imageProd.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        else 
                        {
                            imageProd.Image = Funcoes.Base64ToImage(imagem);
                            imageProd.SizeMode = PictureBoxSizeMode.StretchImage;

                            ((ProdutoViewModel)SelectedItem).Imagem = imagem;
                        }
                        //if (imagem == null) throw new Exception("Erro");


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
