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
    public partial class FrmPromocao : Form
    {
        public FrmPromocao()
        {
            InitializeComponent();
        }
        public string Produto { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal ValorPromocao { get; set; }
        public decimal ValorSelecionado { get; set; }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FrmPromocao_Load(object sender, EventArgs e)
        {
            lbProduto.Text = Produto;
            btnValorPromocao.Text += ValorPromocao.ToString("N2");
            btnValorVenda.Text += ValorVenda.ToString("N2");
        }

        private void btnValorPromocao_Click(object sender, EventArgs e)
        {
            ValorSelecionado = ValorPromocao;
            this.Close();
        }

        private void btnValorVenda_Click(object sender, EventArgs e)
        {
            ValorSelecionado = ValorVenda;
            this.Close();
        }
    }
}
