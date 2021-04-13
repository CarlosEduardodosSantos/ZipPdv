using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Zip.Pdv
{
    public partial class FormSolicitaSenha : Form
    {
        private string _senha;
        public FormSolicitaSenha()
        {
            InitializeComponent();
        }

        public static string Instace()
        {
            using (var form = new FormSolicitaSenha())
            {
                var result = form.ShowDialog();
                return result == DialogResult.OK ? form._senha : string.Empty;
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
            txtSenha.PasswordChar = char.Parse("•");
            txtSenha.Select();
        }

        private void txtCpf_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode != Keys.Enter)return;

            _senha = txtSenha.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
