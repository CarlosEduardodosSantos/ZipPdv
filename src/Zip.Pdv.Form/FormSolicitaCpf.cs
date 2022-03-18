using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component;

namespace Zip.Pdv
{
    public partial class FormSolicitaCpf : Form
    {
        private string _cpf;
        public FormSolicitaCpf()
        {
            InitializeComponent();
        }

        public static string Instace()
        {
            using (var form = new FormSolicitaCpf())
            {
                var result = form.ShowDialog();
                return result == DialogResult.OK ? form._cpf : string.Empty;
            }
        }
        
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.Num.KeyboardNumEventArgs e)
        {

            if (!string.IsNullOrEmpty(e.KeyboardKeyPressed))
                SendKeys.Send(e.KeyboardKeyPressed.ToUpper());
        }

        private void FormDesconto_Load(object sender, EventArgs e)
        {
            txtCpf.Select();
        }

        private void txtCpf_KeyDown(object sender, KeyEventArgs e)
        {
            

            if (e.KeyCode != Keys.Enter)return;

            var isValid = txtCpf.Text.Length > 11 ? ValidacaoHelper.ValidarCNPJ(txtCpf.Text) : ValidacaoHelper.ValidarCpf(txtCpf.Text);
            if (!isValid)
            {
                TouchMessageBox.Show("CPF/CNPJ informado não é válido.", "Autoatendimento", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

                return;
            }

            _cpf = txtCpf.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        void AplicarMascaraCpfCnpj(MaskedTextBox objTextBox)
        {
            var pComponente = objTextBox;

            var txtNoMask = Funcoes.OnlyNumeric(pComponente.Text);
            if (txtNoMask.Length <= 11)
                pComponente.Mask = "000,000,000-00";
            else
                pComponente.Mask = "00,000,000/0000-00";


            //pComponente.Text = txtNoMask;
        }

        private void txtCpf_KeyUp(object sender, KeyEventArgs e)
        {
           // AplicarMascaraCpfCnpj(txtCpf);
        }

        private void txtCpf_Validated(object sender, EventArgs e)
        {
            AplicarMascaraCpfCnpj(txtCpf);
        }
    }
}
