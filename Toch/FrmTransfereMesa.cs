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
    public partial class FrmTransfereMesa : Form
    {
        public FrmTransfereMesa()
        {
            InitializeComponent();
        }
        public int qtde = 1;
        private void btnConfirmar_Click(object sender, EventArgs e)
        {

        }

        private void btnAdicionarQtde_Click(object sender, EventArgs e)
        {
            qtde = Convert.ToInt32(txtQuantidadePessoas.Text);
            qtde += 1;
            txtQuantidadePessoas.Text = qtde.ToString();
        }

        private void btnExcluirItens_Click(object sender, EventArgs e)
        {
            qtde = Convert.ToInt32(txtQuantidadePessoas.Text);
            if (qtde > 1)
                qtde -= 1;
            txtQuantidadePessoas.Text = qtde.ToString();
        }
    }
}
