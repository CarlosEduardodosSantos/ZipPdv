using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Eticket.Application.ViewModels;

namespace Zip.Pdv
{
    public partial class FormDesconto : Form
    {
        private Color _fixColor;
        private decimal vDescPerc;
        private decimal vDescReal;
        private int _tipoDesconto;
        public FormDesconto()
        {
            InitializeComponent();
            _fixColor = btnDescontoReal.BackColor;
        }

        public static DescontoViewResult Instace()
        {
            using (var form = new FormDesconto())
            {
                var resultOk = form.ShowDialog() == DialogResult.OK;
                var valorPercentual = form.vDescPerc;
                var valorReal = form.vDescReal;

                return new DescontoViewResult()
                {
                    ResultOk = resultOk,
                    ValorPercentual = valorPercentual,
                    ValorReal = valorReal
                };
            }
        }

        private void btnDescontoReal_Click(object sender, EventArgs e)
        {
            _tipoDesconto = 0;
            btnDescontoReal.BackColor = Color.Orange;
            btnDescontoPercentual.BackColor = _fixColor;
            txtValor.FormatDecimal = "C";
            txtValor.ValueNumeric = 0;
            txtValor.Select();

        }

        private void btnDescontoPercentual_Click(object sender, EventArgs e)
        {
            _tipoDesconto = 1;
            btnDescontoPercentual.BackColor = Color.Orange;
            btnDescontoReal.BackColor = _fixColor;
            txtValor.FormatDecimal = "N";
            txtValor.ValueNumeric = 0;
            txtValor.Select();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.Num.KeyboardNumEventArgs e)
        {

            if (e.KeyboardKeyPressed == "{ENTER}")
            {
                if(_tipoDesconto == 0)
                    vDescReal = txtValor.ValueNumeric;
                else
                    vDescPerc = txtValor.ValueNumeric;

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                if (!string.IsNullOrEmpty(e.KeyboardKeyPressed))
                {
                    if (txtValor.SelectionLength > 0)
                        txtValor.Clear();

                    var str = new StringBuilder();
                    //Verifica se é comando para apagar
                    if (e.KeyboardKeyPressed == "{BACKSPACE}")
                    {
                        str.Append(int.Parse(txtValor.ValueNumeric.ToString().Replace(",", "")));
                        str.Remove(str.Length - 1, 1);
                    }
                    else
                    {
                        str.Append(int.Parse(txtValor.ValueNumeric.ToString().Replace(",", "")));
                        str.Append(e.KeyboardKeyPressed);
                    }

                    txtValor.ValueNumeric = str.Length > 0 ? decimal.Parse(str.ToString()) / 100 : 0;
                }

            }
            txtValor.SelectionStart = txtValor.Text.Length;
        }

        private void FormDesconto_Load(object sender, EventArgs e)
        {
            btnDescontoReal.PerformClick();
        }
    }
}
