using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;

namespace Zip.Pdv
{
    public partial class FormCaixaPesquisa : Form
    {
        private static CaixaViewModel _caixaViewModel;
        public FormCaixaPesquisa()
        {
            InitializeComponent();

            txtNroCaixa.Select();
        }
        public static CaixaViewModel RetornaCaixaPesquisa()
        {
            using (var form = new FormCaixaPesquisa())
            {
                form.ShowDialog();
                return _caixaViewModel;
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            

            using (var caixaApp = Program.Container.GetInstance<ICaixaAppService>())
            {
                if (dtpCaixa.Checked)
                    _caixaViewModel = caixaApp.ObterCaixaData(dtpCaixa.Value.Date);
                else
                {

                    int caixaId = 0;
                    if (int.TryParse(txtNroCaixa.Text, out caixaId))
                    {
                        _caixaViewModel = caixaApp.ObterCaixaId(caixaId);
                    }
                }

                if (_caixaViewModel == null)
                {
                    Funcoes.MensagemInformation("Caixa não encontrado!\nVerifique o numero ou data e tente novamente");
                    return;
                }
                Close();

            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            _caixaViewModel = null;
            Close();
        }

        private void keyboardNum1_UserKeyPressed(object sender, KeyboardClassLibrary.Num.KeyboardNumEventArgs e)
        {

            if (e.KeyboardKeyPressed == "{ENTER}")
            {

                btnAbrir.PerformClick();
                return;
            }
           
            //txtNroCaixa.SelectionStart = txtNroCaixa.Text.Length;
        }
    }
}
