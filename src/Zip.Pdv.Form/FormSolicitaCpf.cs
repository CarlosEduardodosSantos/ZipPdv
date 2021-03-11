using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Eticket.Application.ViewModels;

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

            if(e.KeyCode != Keys.Enter)return;

            _cpf = txtCpf.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
