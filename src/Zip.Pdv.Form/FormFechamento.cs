using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zip.Pdv
{
    public partial class FormFechamento : Form
    {
        public int pessoas { get; set; }
        public FormFechamento()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPessoas.Text))
            {
                this.pessoas = 1;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.pessoas = Convert.ToInt32(txtPessoas.Text);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
