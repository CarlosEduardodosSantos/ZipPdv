using System;
using System.Windows.Forms;
using Eticket.Application.ViewModels;

namespace Zip.Pdv.Component.PendenteGridView
{
    public partial class PendenteGridViewitem : UserControl
    {
        public event EventHandler<EventArgs> EntregarItem;
        public event EventHandler<EventArgs> ProntoItem;
        public event EventHandler<EventArgs> DetalheItem;
        public event EventHandler<EventArgs> SelectItem;
        public object DataSource { get; set; }
        public bool Selected { get; set; }
        public int Index { get; set; }
        void entregarItem(object sender, EventArgs e)
        {
            var completedEvent = EntregarItem;
            if (completedEvent != null)
            {
                var item = (PendenteGridViewitem)this;
                completedEvent(item, e);
            }
        }
        void prontoItem(object sender, EventArgs e)
        {
            var completedEvent = ProntoItem;
            if (completedEvent != null)
            {
                var item = (PendenteGridViewitem)this;
                completedEvent(item, e);
            }
        }
        void detalheItem(object sender, EventArgs e)
        {
            var completedEvent = DetalheItem;
            if (completedEvent != null)
            {
                var item = (PendenteGridViewitem)this;
                completedEvent(item, e);
            }
        }
        void selectItem(object sender, EventArgs e)
        {
            var completedEvent = SelectItem;
            if (completedEvent != null)
            {
                var item = (PendenteGridViewitem)this;
                completedEvent(item, e);
            }
        }
        public PendenteGridViewitem()
        {
            InitializeComponent();
        }

        public void CarrregaItem(VendaViewModel vendaView)
        {
            lbNome.Text = $"{vendaView.ClientePendencia}";
            lbVendaId.Text = $"N.{vendaView.PendenciaId}";
            lbValor.Text = $"Vlr. {vendaView.ValorTotal.ToString("N2")}";
            DataSource = vendaView;


            lbDataHora.Text = $"{vendaView.DataHora.ToString("dd/MM/yyyy")} {vendaView.HoraPendencia}";
            btnRetornar.BackgroundImage = Zip.Pdv.Properties.Resources.pag_48;
            btnRetornar.Click += entregarItem;
            btnPronto.Click += prontoItem;
            btnDetalhe.Click += detalheItem;

        }
    }
}
