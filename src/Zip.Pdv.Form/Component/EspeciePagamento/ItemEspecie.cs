using System;
using System.Windows.Forms;
using Eticket.Application.ViewModels;

namespace Zip.Pdv.Component.EspeciePagamento
{
    public partial class ItemEspecie : UserControl
    {
        public ItemEspecie()
        {
            InitializeComponent();
            txtvTotal.Text = "0,00";
        }

        public String TextEspecie { get { return this.lbEspecie.Text; } set { this.lbEspecie.Text = value; } }
        public Decimal ValorEspecie { get { return Decimal.Parse(txtvTotal.Text); } set { txtvTotal.Text = value.ToString("N2"); } }
        public EspeciePagamentoViewModel EspecieView { get; set; }
    }
}
