using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eticket.Application.ViewModels;

namespace Zip.Pdv.Component
{
    public partial class GrupoGridViewTotenItem : UserControl
    {
        public event EventHandler<EventArgs> SelectItem;
        void selectItem(object sender, EventArgs e)
        {
            var completedEvent = SelectItem;
            if (completedEvent != null)
            {
                var item = (GrupoGridViewTotenItem)this;
                completedEvent(item, e);
            }
        }
        public int CodigoProduto { get; set; }
        public string Descricao => lbGrupo.Text;
        public Color ColorText { get; set; }
        private Color _fixColor;
        public GrupoGridViewTotenItem()
        {
            InitializeComponent();
            lbGrupo.Text = String.Empty;


            lbGrupo.Click += selectItem;
            lbGrupo.MouseEnter += lbDescricao_MouseEnter;
            lbGrupo.MouseLeave += lbValor_MouseLeave;

            btnIcom.Click += selectItem;
            btnIcom.MouseEnter += lbDescricao_MouseEnter;
            btnIcom.MouseLeave += lbValor_MouseLeave;

        }

        public void AdiconaDataSource(ProdutoGrupoViewModel grupo)
        {
            lbGrupo.Text = grupo.Descricao;
            lbGrupo.ForeColor = ColorText;
            CodigoProduto = grupo.GrupoId;

            if (!string.IsNullOrEmpty(grupo.Imagem))
            {
                btnIcom.Image = Funcoes.Base64ToImage(grupo.Imagem);
                btnIcom.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                btnIcom.ImageLocation = "https://img1.gratispng.com/20180202/pre/kisspng-hamburger-street-food-seafood-fast-food-delicious-food-5a75083c57a5f5.317349121517619260359.jpg";
                //btnIcom.Visible = false;
                btnIcom.SizeMode = PictureBoxSizeMode.StretchImage;
            }


        }

        private void lbDescricao_MouseEnter(object sender, EventArgs e)
        {
            _fixColor = this.BackColor;
            this.BackColor = Color.WhiteSmoke;
        }

        private void lbValor_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = _fixColor;
        }
    }
}
