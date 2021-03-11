using System;
using System.Drawing;
using System.Windows.Forms;
using Eticket.Application.ViewModels;

namespace Zip.EticketSub
{
    public partial class UControlVenda : UserControl
    {
        public event EventHandler<EventArgs> EnviarItem;
        void enviarItem(object sender, EventArgs e)
        {
            var completedEvent = EnviarItem;
            if (completedEvent != null)
            {
                var item = (UControlVenda)this;
                completedEvent(item, e);
            }
        }
        public event EventHandler<EventArgs> CancelarItem;
        void canelarItem(object sender, EventArgs e)
        {
            var completedEvent = CancelarItem;
            if (completedEvent != null)
            {
                var item = (UControlVenda)this;
                completedEvent(item, e);
            }
        }
        public readonly VendaViewModel VendaView;
        public UControlVenda(VendaViewModel vendaView)
        {
            VendaView = vendaView;
            InitializeComponent();
            this.Load += UControlVenda_Load;
            btnEnviar.Click += enviarItem;
            btnCancelar.Click += canelarItem;
        }

        private void UControlVenda_Load(object sender, EventArgs e)
        {
            var cupomOk = !(string.IsNullOrEmpty(VendaView.CupomFiscal) || VendaView.CupomFiscal == "0");

            BackColor = !cupomOk ? Color.LightSalmon : Color.AliceBlue;
            btnEnviar.Visible = !cupomOk;
            btnCancelar.Visible = cupomOk;
            lbData.Text = $"Data: {VendaView.DataHora}";
            //lbNroVenda.Text = $"Nroº {_vendaView.VendaId}";
            lbNroVenda.Text = $"SATº: {VendaView.CupomFiscal}/VENDA: {VendaView.VendaId}";
            lbCpf.Text = $"MSG: {VendaView.MenssagemSat}";
            lbNumeroSat.Text = $"VALOR: {VendaView.ValorTotal.ToString("C2")} - PGTO: {VendaView.TipoPagamento}";
        }
    }
}
