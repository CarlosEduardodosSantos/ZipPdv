using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Toch
{
    public partial class FrmTecladoNumerico : Form
    {
        static FrmTecladoNumerico newFrmTecladoNumerico;

        public FrmTecladoNumerico()
        {
            InitializeComponent();
        }
        public int  TipoResult {get; set; }
        public string frmTitulo { get; set; }

        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            if (e.KeyboardKeyPressed == "{ENTER}")
            {
                if (!string.IsNullOrEmpty(txtSenha.Text))
                    this.Close();
            }
            else
            {
                if (!string.IsNullOrEmpty(e.KeyboardKeyPressed))
                    SendKeys.Send(e.KeyboardKeyPressed.ToUpper());
            }
        }
        public static string TecladoNumerico(int tipo, string Titulo)
        {
            newFrmTecladoNumerico = new FrmTecladoNumerico();
            newFrmTecladoNumerico.TipoResult = tipo;
            newFrmTecladoNumerico.Text = Titulo;
            
            newFrmTecladoNumerico.ShowDialog();
            return newFrmTecladoNumerico.txtSenha.Text;
        }

        private void txtSenha_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtSenha.Text))
                    this.Close();
            }
            
        }

        private void FrmTecladoNumerico_Load(object sender, EventArgs e)
        {
            lbtextBox.Text = this.Text;

            if (TipoResult == 1)
                txtSenha.PasswordChar = char.Parse("•");
            else
                txtSenha.PasswordChar = new char();
            txtSenha.Select();
        }

        private void FrmTecladoNumerico_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSenha.Text))
                txtSenha.Text = "Close";
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
