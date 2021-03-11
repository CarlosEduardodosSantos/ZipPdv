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
    public partial class FrmSelecionaGarcom : Form
    {
        public FrmSelecionaGarcom()
        {
            InitializeComponent();
            txtQuantidadePessoas.Text = "1";
           
        }
        public int qtde = 1;
        public int IdGarcom { get; set; }
        public CadGarcom cadGarcom;
        private List<CadGarcom> ListaGarcom;
        
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

        private void FrmSelecionaGarcom_Load(object sender, EventArgs e)
        {
            ListaGarcom = new List<CadGarcom>();
            clsMesa clsmesa = new clsMesa();
            cbGarcom.DataSource = clsmesa.Listagarcom();
            cbGarcom.ValueMember = "IdGarcom";
            cbGarcom.DisplayMember = "Descricao";
            cbGarcom.SelectedValue = IdGarcom;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            cadGarcom = new CadGarcom();

            cadGarcom = (CadGarcom)cbGarcom.SelectedItem;
            this.Close();

        }

        private void FrmSelecionaGarcom_FormClosed(object sender, FormClosedEventArgs e)
        {
            cadGarcom = new CadGarcom();

            cadGarcom = (CadGarcom)cbGarcom.SelectedItem;
            this.Close();
        }
    }
}
