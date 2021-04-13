using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Zip.Pdv.Helpers;

namespace Zip.Pdv
{
    public partial class FormSolicitaTexto : Form
    {
        private string _senha;
        public FormSolicitaTexto(string textFrm)
        {
            InitializeComponent();
            this.label2.Text = textFrm;
        }

        public static string Instace(string textFrm)
        {
            using (var form = new FormSolicitaTexto(textFrm))
            {
                
                var result = form.ShowDialog();
                return result == DialogResult.OK ? form._senha : string.Empty;
            }
        }
        
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            TecladoVirtualHelper.Close();
            _senha = txtSenha.Text;
            DialogResult = DialogResult.OK;
            Close();

            //DialogResult = DialogResult.Cancel;
        }
        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.Num.KeyboardNumEventArgs e)
        {

            if (!string.IsNullOrEmpty(e.KeyboardKeyPressed))
                SendKeys.Send(e.KeyboardKeyPressed.ToUpper());
        }

        private void FormDesconto_Load(object sender, EventArgs e)
        {
            //txtSenha.PasswordChar = char.Parse("•");
            txtSenha.Select();
        }

        private void txtCpf_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode != Keys.Enter)return;

            btnVoltar.PerformClick();
        }

        private void btnKeyBoard_Click(object sender, EventArgs e)
        {
            TecladoVirtualHelper.Open();
            this.txtSenha.Focus();
        }
    }
}
