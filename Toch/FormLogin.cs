using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Toch.Component;

namespace Toch
{
    public partial class FormLogin : Form
    {
        public VendedorViewModel VendedorViewAutenticado;

        private VendedorViewModel _vendedorView;
        private XButton _xButton;
        private int pageQuantidade;
        private int currentPage = 1;

        public FormLogin()
        {
            InitializeComponent();
        }
        public int  TipoResult {get; set; }
        public string frmTitulo { get; set; }

        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            if (e.KeyboardKeyPressed == "{ENTER}")
            {

                AutenticaVendedor();
            }
            else
            {
                if (!string.IsNullOrEmpty(e.KeyboardKeyPressed))
                    SendKeys.Send(e.KeyboardKeyPressed.ToUpper());
            }
        }


        private void txtSenha_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AutenticaVendedor();
            }
            
        }

        private void AutenticaVendedor()
        {
            if (_vendedorView == null)
            {
                TouchMessageBox.Show("Selecione um usuário.", "Acesso ao sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenha.Clear();
                return;
            }
            if (_vendedorView.Senha == txtSenha.Text)
            {
                VendedorViewAutenticado = _vendedorView;
                Close();
            }
            else
            {
                TouchMessageBox.Show("Senha não cadastrada. Tente Novamente.", "Acesso ao sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FrmTecladoNumerico_Load(object sender, EventArgs e)
        {
            lbtextBox.Text = frmTitulo;
            txtSenha.PasswordChar = char.Parse("•");
            txtSenha.Select();

            btnPrevious.Enabled = false;

            CarregaVendedores(currentPage);
        }

        private void CarregaVendedores(int page)
        {
            using (var appServicVendedor = Program.Container.GetInstance<IVendedorAppService>())
            {
                var skip = 5 * (page - 1);

                var vendedores = appServicVendedor.GetAll();
                pageQuantidade = int.Parse(Math.Ceiling(vendedores.Count() / 5d).ToString());
                if (pageQuantidade > 1)
                    btnNext.Enabled = true;


                if (vendedores.Count() < 6)
                    pnlPaginacao.Visible = false;

                var vendedoresPadding = vendedores.Skip(skip).Take(5).ToList();
                flpUsuarios.Controls.Clear();
                foreach (var vendedorViewModel in vendedoresPadding)
                {
                    var btnVendedor = new XButton();
                    btnVendedor.Width = 250;
                    btnVendedor.Height = 48;
                    btnVendedor.TextAlign = ContentAlignment.MiddleCenter;
                    btnVendedor.Theme = Theme.MSOffice2010_WHITE;
                    btnVendedor.CodigoProduto = vendedorViewModel.VendedorId;
                    btnVendedor.Text = vendedorViewModel.Nome;

                    btnVendedor.SourceItem = vendedorViewModel;
                    btnVendedor.Click += BtnVendedor_Click;

                    flpUsuarios.Controls.Add(btnVendedor);
                }
            }
        }

        private void BtnVendedor_Click(object sender, EventArgs e)
        {
            if(_xButton != null)
                _xButton.Theme = Theme.MSOffice2010_WHITE;

            _xButton = (XButton) sender;
            _xButton.Theme = Theme.MSOffice2010_Publisher;

            _vendedorView = (VendedorViewModel)_xButton.SourceItem;

            flpUsuarios.Refresh();

            txtSenha.Focus();
        }

        private void FrmTecladoNumerico_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currentPage++;
            CarregaVendedores(currentPage);
            if (currentPage == pageQuantidade)
                btnNext.Enabled = false;
            if (currentPage > 1)
                btnPrevious.Enabled = true;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            currentPage--;
            CarregaVendedores(currentPage);

            if (currentPage == 1)
                btnPrevious.Enabled = false;
        }
    }
}
