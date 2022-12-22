using System;
using System.Windows.Forms;
using Zip.Pdv.Component;

namespace Zip.Pdv
{
    public partial class FormSolicitaNumeric : Form
    {
        private string _number;
        private string _title { set => label2.Text = value; }
        private bool _obrigatorio;
        public FormSolicitaNumeric()
        {
            InitializeComponent();
        }

        public static string Instace(string title, bool obrigatorio = false, string valueDafaut = "")
        {
            using (var form = new FormSolicitaNumeric())
            {
                form._title = title;
                form._obrigatorio = obrigatorio;
                form.txtCpf.Text = valueDafaut;
                form.txtCpf.SelectAll();

                var result = form.ShowDialog();
                return result == DialogResult.OK ? form._number : string.Empty;
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

            _number = txtCpf.Text;
            DialogResult = DialogResult.OK;
            Close();
        }



        private void txtCpf_KeyUp(object sender, KeyEventArgs e)
        {
           // AplicarMascaraCpfCnpj(txtCpf);
        }

        private void txtCpf_Validated(object sender, EventArgs e)
        {

        }

        private void FormSolicitaFicha_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_obrigatorio && string.IsNullOrEmpty(_number))
            {
                var result = TouchMessageBox.Show("É obrigatório informar o numero do pager!",
                    "Venda Finaliza", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCpf.Focus();
                e.Cancel = true;
            }

        }
    }
}
