using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Media;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;

namespace Zip.Pdv.Component
{
    public partial class ProdutoGridViewItemImage : UserControl
    {
        public event EventHandler<EventArgs> SelectItem;
        void selectItem(object sender, EventArgs e)
        {
            var completedEvent = SelectItem;
            if (completedEvent != null)
            {
                var item = (ProdutoGridViewItemImage)this;
                completedEvent(item, e);
            }
        }
        public object SelectedItem { get; set; }
        public int Index { get; set; }
        [DefaultValue(false)]
        public bool HideValorVenda { get; set; }
        public Color ColorText { get; set; }
        private Color _fixColor;
        public ProdutoGridViewItemImage()
        {
            InitializeComponent();
            lbGrupo.Text = String.Empty;


            lbGrupo.Click += selectItem;
            lbGrupo.MouseEnter += lbDescricao_MouseEnter;
            lbGrupo.MouseLeave += lbValor_MouseLeave;

            imageProd.Click += selectItem;
            imageProd.MouseEnter += lbDescricao_MouseEnter;
            imageProd.MouseLeave += lbValor_MouseLeave;

        }

        public void AdiconaDataSource(ProdutoViewModel produto)
        {
            lbGrupo.Text = produto.Descricao;
            lbGrupo.ForeColor = ColorText;

            lbValorVenda.Text = produto.ValorVenda.ToString("C2");
            //lbValorVenda.ForeColor = ColorText;
            SelectedItem = produto;

            if (HideValorVenda)
            {
                lbValorVenda.Visible = false;
            }
            GetImageAsync(produto.ProdutoId);

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
        public void GetImageAsync(int produtoId)
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

                        ((ProdutoViewModel) SelectedItem).Imagem = imagem;
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
