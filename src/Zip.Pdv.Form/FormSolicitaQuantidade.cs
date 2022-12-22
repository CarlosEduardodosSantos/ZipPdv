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
        private int _qtdeCasas;
        public FormSolicitaQuantidade()
        {
            InitializeComponent();
        }

        public static decimal Instace(string title, bool obrigatorio = false, decimal valueDefaut = 0, int qtdeCasas = 3)
        {
            using (var form = new FormSolicitaQuantidade())
            {
                form._title = title;
                form._obrigatorio = obrigatorio;
                form.txtPeso.ValueNumeric = valueDefaut;
                form._qtdeCasas = qtdeCasas;
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
                    var divisao = _qtdeCasas > 2 ? 1000 : 100;
                    txtPeso.ValueNumeric = str.Length > 0 ? decimal.Parse(str.ToString()) / divisao : 0;
                }

            }
            txtPeso.SelectionStart = txtPeso.Text.Length;
        }

        private void FormDesconto_Load(object sender, EventArgs e)
        {
            txtPeso.FormatDecimal = $"N{_qtdeCasas}";
            txtPeso.KeyPress += txtPeso_KeyPress;
            txtPeso.Select();

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

        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            var ev = new KeyboardClassLibrary.Num.KeyboardNumEventArgs(e.KeyChar.ToString());
            keyboardcontrol1_UserKeyPressed(sender, ev);
        }

        private void txtPeso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                _number = txtPeso.ValueNumeric;
                DialogResult = DialogResult.OK;
                Close();
            }

        }

        private void txtPeso_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void txtPeso_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        private string ConvertKeyCodeToString(System.Windows.Forms.Keys keyCode, bool shiftPressed)
        {
            string key = new System.Windows.Forms.KeysConverter().ConvertToString(keyCode);

            if (key.Contains("NumPad"))
            {
                key = key.Replace("NumPad", "");
            }

            if (key.Equals("Space"))
            {
                key = " ";
            }

            if (!shiftPressed)
            {
                key = key.ToLower();
            }

            return key;
        }
        private void AtualizaDecimal(string charValue)
        {
            if (!string.IsNullOrEmpty(charValue))
            {
                if (txtPeso.SelectionLength > 0)
                    txtPeso.Clear();

                var str = new StringBuilder();
                //Verifica se é comando para apagar
                if (charValue == "voltar")
                {
                    str.Append(int.Parse(txtPeso.ValueNumeric.ToString().Replace(",", "")));
                    str.Remove(str.Length - 1, 1);
                }
                else
                {
                    str.Append(int.Parse(txtPeso.ValueNumeric.ToString().Replace(",", "")));
                    str.Append(charValue);
                }
                var divisao = _qtdeCasas > 2 ? 1000 : 100;
                txtPeso.ValueNumeric = str.Length > 0 ? decimal.Parse(str.ToString()) / divisao : 0;
            }
        }
    }
}
