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
    public partial class GrupoGridViewItem : UserControl
    {
        public event EventHandler<EventArgs> SelectItem;
        void selectItem(object sender, EventArgs e)
        {
            var completedEvent = SelectItem;
            if (completedEvent != null)
            {
                var item = (GrupoGridViewItem)this;
                completedEvent(item, e);
            }
        }
        public int CodigoProduto { get; set; }
        public Color ColorText { get; set; }
        private Color _fixColor;
        public GrupoGridViewItem()
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
                btnIcom.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            else
            {
                btnIcom.Image = null;
                btnIcom.Visible = false;
            }


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
    }
}
