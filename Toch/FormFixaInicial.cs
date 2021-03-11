using System;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;

namespace Toch
{
    public partial class FormFixaInicial : Form
    {
        private VendedorViewModel _vendedorView;
        public FormFixaInicial()
        {
            InitializeComponent();
        }

        private void CarregaTeclado()
        {
            var ficha = FrmTecladoNumerico.TecladoNumerico(0, "Entre com o número da Ficha.");
            if(ficha == "Close")
                Close();
            else if(!string.IsNullOrEmpty(ficha))
                IniciaFicha(ficha);
        }

        private void IniciaFicha(string ficha)
        {
            using (var appServiceFicha = Program.Container.GetInstance<IFichaAppService>())
            {
                var fichaView = appServiceFicha.GetByFichaId(ficha);

                if (fichaView == null)
                {
                    TouchMessageBox.Show("Ficha: N. " + ficha + " inválida. ", "Principal", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    btnChamaTecladoNumerico.PerformClick();
                    return;
                }
                

                Int32 IdFicha = 0;
                if (Int32.TryParse(fichaView.FichaNumero, out IdFicha))
                {
                    var frm = new FormLancaFicha(fichaView, _vendedorView);
                    frm.ShowDialog();

                    toolStripLabel1.Text = "Ultima ficha  lançada > " + ficha;

                    btnChamaTecladoNumerico.PerformClick();

                }
                else
                {
                    var result = TouchMessageBox.Show("Ficha: " + ficha + " inválida. ", "Principal", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (result == DialogResult.Cancel)
                        Close();
                }

            }
        }

        private void FormFixaInicial_Load(object sender, EventArgs e)
        {
            btnLogin.PerformClick();
        }

        private void btnChamaTecladoNumerico_Click(object sender, EventArgs e)
        {
            CarregaTeclado();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            //toolStripLabel2.Text = string.Empty;
            btnLogin.Text = "Login";

            var form = new FormLogin();
            form.frmTitulo = "Entre com sua senha de acesso.";
            form.ShowDialog();

            _vendedorView = form.VendedorViewAutenticado;

            if (_vendedorView == null)
            {
                Close();
            }
            else
            {
                btnLogin.Text = "Logoff/" + _vendedorView.Nome;
                btnChamaTecladoNumerico.PerformClick();
            }

        }
    }
}
