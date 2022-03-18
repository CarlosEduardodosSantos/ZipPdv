using System;
using System.Windows.Forms;
using Zip.Pdv.Component;

namespace Zip.Pdv
{
    public partial class FormSolicitaPergunta : Form
    {
        private string _selecao;
        private string _title { set => label2.Text = value; }
        private bool _obrigatorio;
        public FormSolicitaPergunta()
        {
            InitializeComponent();
        }

        public static string Instace(string title, bool obrigatorio = false)
        {
            using (var form = new FormSolicitaPergunta())
            {
                form._title = title;
                form._obrigatorio = obrigatorio;
                var result = form.ShowDialog();
                return result == DialogResult.OK ? form._selecao : string.Empty;
            }
        }
        


        private void FormDesconto_Load(object sender, EventArgs e)
        {
            
        }


        private void FormSolicitaFicha_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_obrigatorio && string.IsNullOrEmpty(_selecao))
            {
                var result = TouchMessageBox.Show("É obrigatório selecionar uma opção!",
                    "Venda Finaliza", MessageBoxButtons.OK, MessageBoxIcon.Information);
         
                e.Cancel = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var item = (Button)sender;

            _selecao = item.Text;
            DialogResult = DialogResult.OK;
            Close();
            
        }
    }
}
