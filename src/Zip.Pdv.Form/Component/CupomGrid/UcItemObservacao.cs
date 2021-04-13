using System;
using System.Windows.Forms;
using Zip.Pdv.Helpers;

namespace Zip.Pdv.Component.EspeciePagamento
{
    public partial class UcItemObservacao : UserControl
    {
        public string Observacao => lbDisplay.Text;

        public event EventHandler<EventArgs> Click;

        void cClick(object sender, EventArgs e)
        {
            var completedEvent = Click;
            if (completedEvent != null)
            {
                var item = (UcItemObservacao)this;
                completedEvent(item, e);
            }
        }
        public UcItemObservacao()
        {
            InitializeComponent();
        }

        public void AdicionarObswervacao(string observacao)
        {
            lbDisplay.Text = observacao;
            btnDeletar.Click += cClick;
        }

        public void SetFocus()
        {
        }

        private void lbDisplay_Enter(object sender, EventArgs e)
        {
            TecladoVirtualHelper.Open();

        }

        private void lbDisplay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) e.SuppressKeyPress = true;
        }
    }
}
