using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Eticket.Application.ViewModels;

namespace Zip.Pdv.Component
{
    public partial class ProdutoGridViewItem : UserControl
    {
        public event EventHandler<EventArgs> SelectItem;
        void selectItem(object sender, EventArgs e)
        {
            var completedEvent = SelectItem;
            if (completedEvent != null)
            {
                var item = (ProdutoGridViewItem)this;
                completedEvent(item, e);
            }
        }

        public object SelectedItem { get; set; }
        public int Index { get; set; }
        [DefaultValue(false)]
        public bool HideValorVenda { get; set; }
        public Color ColorText { get; set; }
        private Color _fixColor;
        public ProdutoGridViewItem()
        {
            InitializeComponent();
            lbGrupo.Text = String.Empty;


            lbGrupo.Click += selectItem;
            lbGrupo.MouseEnter += lbDescricao_MouseEnter;
            lbGrupo.MouseLeave += lbValor_MouseLeave;

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
            
        }
        public void AdiconaComplemento(ProdutoComplementoViewModel complemento)
        {
            lbGrupo.Text = complemento.Descricao;
            lbGrupo.ForeColor = ColorText;

            lbValorVenda.Text = complemento.Valor.ToString("C2");
            //lbValorVenda.ForeColor = ColorText;
            SelectedItem = complemento;

            if (HideValorVenda)
            {
                lbValorVenda.Visible = false;
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
