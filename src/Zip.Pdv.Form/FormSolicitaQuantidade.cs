using System;
using System.Text;
using System.Windows.Forms;
using Zip.Pdv.Component;

namespace Zip.Pdv
{
    public partial class FormSolicitaQuantidade : Form
    {
        private decimal _number;
        private string _title { set => label2.Text = value; }
        private bool _obrigatorio;
        public FormSolicitaQuantidade()
        {
            InitializeComponent();
        }

        public static decimal Instace(string title, bool obrigatorio = false)
        {
            using (var form = new FormSolicitaQuantidade())
            {
                form._title = title;
                form._obrigatorio = obrigatorio;
                var result = form.ShowDialog();
                return result == DialogResult.OK ? form._number : 0;
            }
        }
        
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.Num.KeyboardNumEventArgs e)
        {
            /*
            if (!string.IsNullOrEmpty(e.KeyboardKeyPressed))
                SendKeys.Send(e.KeyboardKeyPressed.ToUpper());*/

            if (e.KeyboardKeyPressed == "{ENTER}")
            {

                _number = txtPeso.ValueNumeric;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                if (!string.IsNullOrEmpty(e.KeyboardKeyPressed))
                {
                    if (txtPeso.SelectionLength > 0)
                        txtPeso.Clear();

                    var str = new StringBuilder();
                    //Verifica se é comando para apagar
                    if (e.KeyboardKeyPressed == "{BACKSPACE}")
                    {
                        str.Append(int.Parse(txtPeso.ValueNumeric.ToString().Replace(",", "")));
                        str.Remove(str.Length - 1, 1);
                    }
                    else
                    {
                        str.Append(int.Parse(txtPeso.ValueNumeric.ToString().Replace(",", "")));
                        str.Append(e.KeyboardKeyPressed);
                    }

                    txtPeso.ValueNumeric = str.Length > 0 ? decimal.Parse(str.ToString()) / 1000 : 0;
                }

            }
            txtPeso.SelectionStart = txtPeso.Text.Length;
        }

        private void FormDesconto_Load(object sender, EventArgs e)
        {
            txtPeso.Select();

        }

        private void txtCpf_KeyDown(object sender, KeyEventArgs e)
        {
            

            if (e.KeyCode != Keys.Enter)return;


            _number = txtPeso.ValueNumeric;
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
            if (_obrigatorio && _number == 0)
            {
                var result = TouchMessageBox.Show("É obrigatório informar a quantidade!",
                    "Venda Finaliza", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPeso.Focus();
                e.Cancel = true;
            }

        }
    }
}
