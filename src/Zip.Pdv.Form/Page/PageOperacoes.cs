using System;
using System.Windows.Forms;

namespace Zip.Pdv.Page
{
    public partial class PageOperacoes : PageBase
    {
        public PageOperacoes()
        {
            InitializeComponent();
            btnVoltar.Click += closeForm;
        }

        private void btnCaixaAbertura_Click(object sender, EventArgs e)
        {
            using (var form = new FormAbrirCaixa())
            {
                form.ShowDialog();
            }
            btnVoltar.PerformClick();
        }

        private void btnCaixaFechamento_Click(object sender, EventArgs e)
        {
            using (var form = new FormCaixaFechamento())
            {
                form.ShowDialog();
            }
            btnVoltar.PerformClick();
        }

        private void PageOperacoes_Load(object sender, EventArgs e)
        {
            btnCaixaAbertura.Enabled = Program.CaixaView == null;
            btnCaixaFechamento.Enabled = !btnCaixaAbertura.Enabled;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {

        }
    }
}
