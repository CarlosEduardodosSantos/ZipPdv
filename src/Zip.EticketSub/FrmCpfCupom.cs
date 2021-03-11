using System;
using System.Windows.Forms;
using Zip.Utils;

namespace Zip.EticketSub
{
    public partial class FrmCpfCupom : Form
    {
        public string CpfCliente;
        public FrmCpfCupom()
        {
            InitializeComponent();
        }

        public static string Instance()
        {
            using (var form = new FrmCpfCupom())
            {
                form.ShowDialog();
                return form.CpfCliente;
            }
        }

        private void FrmCpfCupom_Load(object sender, EventArgs e)
        {
            txtCpfCliente.Select();
        }

        private void FrmCpfCupom_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Escape)
                btnCancelar.PerformClick();

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            var cpf = Funcoes.OnlyNumbers(txtCpfCliente.Text);
            if (!Utils.ValidaCpfCnpj.ValidaCpf(cpf))
            {
                MessageBox.Show("CPF inválido","Validação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCpfCliente.Clear();
                txtCpfCliente.Select();
                return;
            }

            CpfCliente = cpf;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CpfCliente = String.Empty;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtCpfCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnConfirmar.PerformClick();
        }
    }
}
