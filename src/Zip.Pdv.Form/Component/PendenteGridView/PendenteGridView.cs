using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Eticket.Application.ViewModels;

namespace Zip.Pdv.Component.PendenteGridView
{
    public partial class PendenteGridView : UserControl
    {

        public event EventHandler<EventArgs> EntregaItem;
        public event EventHandler<EventArgs> ProntoItem;
        public event EventHandler<EventArgs> DetalheItem;
        void entregaItem(object sender, EventArgs e)
        {
            var completedEvent = EntregaItem;
            if (completedEvent != null)
            {
                var item = (PendenteGridViewitem)sender;
                completedEvent(item, e);
            }
        }
        void prontoItem(object sender, EventArgs e)
        {
            var completedEvent = ProntoItem;
            if (completedEvent != null)
            {
                var item = (PendenteGridViewitem)sender;
                completedEvent(item, e);
            }
        }
        void detalheItem(object sender, EventArgs e)
        {
            var completedEvent = DetalheItem;
            if (completedEvent != null)
            {
                var item = (PendenteGridViewitem)sender;
                completedEvent(item, e);
            }
        }
        public List<VendaViewModel> DataSource { get; set; }
        public PendenteGridViewitem SelectedItem { get; set; }
        [DefaultValue("FFFFFAFA")]
        public Color ColorHeader { get; set; }
        [DefaultValue(false)]
        public bool HideHeader { get; set; }
        public PendenteGridView()
        {
            InitializeComponent();
        }

        private void EntregaGridView_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.HorizontalScroll.Visible = false;
        }

        public void Atualizar(List<VendaViewModel> entregas)
        {
            var controls = new Control[entregas.Count];
            flowLayoutPanel1.Controls.Clear();
            if (entregas.Count == 0) return;

            for (int i = 0; i < entregas.Count; i++)
            {
                var vendaItemViewModel = entregas[i];

                var largura = flowLayoutPanel1.Width - 24;
                /*if (fLayoutVendaItem.HorizontalScroll.Visible)
                    largura -= 18;
                    */

                var cupomItem = new PendenteGridViewitem
                {
                    Index = i,
                    DataSource = vendaItemViewModel
                };

                cupomItem.CarrregaItem(vendaItemViewModel);
                cupomItem.Width = largura;
                cupomItem.Dock = DockStyle.Top;

                cupomItem.EntregarItem += entregaItem;
                cupomItem.ProntoItem += prontoItem;
                cupomItem.DetalheItem += detalheItem;
                cupomItem.SelectItem += CupomItemOnSelectItem;

                

                controls[i] = cupomItem;

            }

            flowLayoutPanel1.Controls.AddRange(controls);
            var selected = flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1];
            CupomItemOnSelectItem(selected, EventArgs.Empty);

            flowLayoutPanel1.VerticalScroll.Value = flowLayoutPanel1.VerticalScroll.Maximum - 1;
            flowLayoutPanel1.PerformLayout();
        }
        private void CupomItemOnSelectItem(object sender, EventArgs eventArgs)
        {
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control.GetType() != typeof(PendenteGridViewitem)) continue;

                ((PendenteGridViewitem)control).Selected = false;
                ((PendenteGridViewitem)control).BackColor = Color.White;

            }
            var selectedItem = (PendenteGridViewitem)sender;

            selectedItem.Selected = !selectedItem.Selected;
            selectedItem.BackColor = selectedItem.Selected ? Color.Cyan : Color.White;

            SelectedItem = selectedItem;
        }
    }
    public enum EntregaSituacao
    {
        Pendente = 0,
        Retorno = 1
    }
}
