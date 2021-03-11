using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAT;
using Zip.Sat;

namespace Zip.EticketSub
{
    public partial class FormConfiguracao : Form
    {
        public FormConfiguracao()
        {
            InitializeComponent();
        }

        private void FormConfiguracao_Load(object sender, EventArgs e)
        {
            lbEmpresa.Text = Global.Empresa.RazaoSocial;
            lbEmpresaCnpj.Text = Global.Empresa.Cnpj;
            lbSoftHouseCnpj.Text = Global.ConfiguracaoInicial.SoftwareHouseCnpj;
            lbSat.Text = Global.ConfiguracaoInicial.SatMarca;
            lbAtivacao.Text = Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao;
            lbPorta.Text = Global.ConfiguracaoInicial.SatPorta.ToString();
            lbAc.Text = Global.Empresa.SignAC;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            var satGerencial = new SatGerencial();

            var msg = string.Empty;
            var result = satGerencial.SatConsultarStatus(ref msg);

            MessageBox.Show(msg);
        }
    }
}
