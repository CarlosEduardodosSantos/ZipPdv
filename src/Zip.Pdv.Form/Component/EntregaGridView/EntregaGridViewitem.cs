using System;
using System.Windows.Forms;
using Eticket.Application.ViewModels;

namespace Zip.Pdv.Component.EntregaGridView
{
    public partial class EntregaGridViewitem : UserControl
    {
        public event EventHandler<EventArgs> EntregarItem;
        public event EventHandler<EventArgs> RetornarItem;
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
                var item = (EntregaGridViewitem)this;
                completedEvent(item, e);
            }
        }
        void retornarItem(object sender, EventArgs e)
        {
            var completedEvent = RetornarItem;
            if (completedEvent != null)
            {
                var item = (EntregaGridViewitem)this;
                completedEvent(item, e);
            }
        }
        void detalheItem(object sender, EventArgs e)
        {
            var completedEvent = DetalheItem;
            if (completedEvent != null)
            {
                var item = (EntregaGridViewitem)this;
                completedEvent(item, e);
            }
        }
        void selectItem(object sender, EventArgs e)
        {
            var completedEvent = SelectItem;
            if (completedEvent != null)
            {
                var item = (EntregaGridViewitem)this;
                completedEvent(item, e);
            }
        }
        public EntregaGridViewitem()
        {
            InitializeComponent();
        }

        public void CarrregaItem(VendaViewModel vendaView, EntregaSituacao situacao)
        {
            lbNome.Text = $"{vendaView.Delivery.ClienteDelivery.Nome}";
            lbEndereco.Text = $"{vendaView.Delivery.ClienteDelivery.Endereco} | {vendaView.Delivery.ClienteDelivery.Bairro}";
            lbFone.Text = $"Fone:{vendaView.Delivery.ClienteDelivery.Telefone}";
            lbVendaId.Text = $"N.{vendaView.VendaId}";
            lbValor.Text = $"Vlr. {vendaView.ValorTotal.ToString("N2")}";
            DataSource = vendaView;


            if (situacao == EntregaSituacao.Pendente)
            {
                lbDataHora.Text = vendaView.Delivery.DataHora.ToString("dd/MM/yyyy HH:mm");
                btnRetornar.BackgroundImage = Zip.Pdv.Properties.Resources.delivery;
                btnRetornar.Click += entregarItem;
            }
            else
            {
                lbDataHora.Text = vendaView.Delivery.DataHoraSaida.ToString("dd/MM/yyyy HH:mm");
                lbMoto.Text = vendaView.Delivery.Entregador;

                btnRetornar.BackgroundImage = Zip.Pdv.Properties.Resources.ic_checkout;
                btnRetornar.Click += retornarItem;
            }
            

        }
    }
}
