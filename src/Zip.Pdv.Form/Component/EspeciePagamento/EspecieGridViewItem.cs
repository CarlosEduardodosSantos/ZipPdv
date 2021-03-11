using System;
using System.Drawing;
using System.Windows.Forms;
using Eticket.Application.ViewModels;

namespace Zip.Pdv.Component.EspeciePagamento
{
    public partial class EspecieGridViewItem : UserControl
    {
        public EspeciePagamentoViewModel Especie { get; set; }
        public event EventHandler<EventArgs> SelectItem;
        void selectItem(object sender, EventArgs e)
        {
            var completedEvent = SelectItem;
            if (completedEvent != null)
            {
                var item = (EspecieGridViewItem)this;
                completedEvent(item, e);
            }
        }
        private Color _fixColor;
        public bool Selected { get; set; }
        public EspecieGridViewItem()
        {
            InitializeComponent();
            _fixColor = this.BackColor;

            lbEspecie.Click += selectItem;
            lbEspecie.MouseEnter += Item_MouseEnter;
            lbEspecie.MouseLeave += Item_MouseLeave;
        }

        private void EspecieGridViewItem_Load(object sender, EventArgs e)
        {

        }

        public void AdicionarEspecie(EspeciePagamentoViewModel especiePagamento)
        {
            lbEspecie.Text = especiePagamento.Especie;
            Especie = especiePagamento;
        }
        private void Item_MouseEnter(object sender, EventArgs e)
        {
            
            this.BackColor = Color.DarkOrange;
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Item_MouseLeave(object sender, EventArgs e)
        {
            if(Selected)return;

            this.BackColor = _fixColor;
            this.BorderStyle = BorderStyle.None;
        }

        public void Resetar()
        {
            Selected = false;
            this.BackColor = _fixColor;
            this.BorderStyle = BorderStyle.None;
        }

        public void Selecionar()
        {
            Selected = true;
            this.BackColor = Color.DarkOrange;
            this.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
