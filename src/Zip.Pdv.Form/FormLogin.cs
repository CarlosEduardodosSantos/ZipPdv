using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Toch.Component;
using Zip.Pdv.Component;

namespace Zip.Pdv
{
    public partial class FormLogin : Form
    {
        private UsuarioViewModel _usuarioViewModel;
        public UsuarioViewModel UsuarioViewModel;
        private XButton _xButton;
        private int _pageQuantidade;
        private int _currentPage = 1;
        public FormLogin()
        {
            InitializeComponent();
        }

        public static UsuarioViewModel EfetuarLogin()
        {
            using (var form = new FormLogin())
            {
                form.ShowDialog();
                return form.UsuarioViewModel;
            }
            
        }
        private void AutenticaVendedor()
        {
            if (_usuarioViewModel == null)
            {
                TouchMessageBox.Show("Selecione um usuário.", "Acesso ao sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenha.Clear();
                return;
            }
            if (_usuarioViewModel.Senha == txtSenha.Text)
            {
                UsuarioViewModel = _usuarioViewModel;
                Close();
            }
            else
            {
                TouchMessageBox.Show("Senha não cadastrada. Tente Novamente.", "Acesso ao sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FormLogin_Load(object sender, System.EventArgs e)
        {

             var dataSistema = DateTime.Now.Date;
            var data = GetNetworkTime().Date;

            if(dataSistema != data)
            {
                MessageBox.Show("Por Favor Ajuste a Data de Seu Sistema Operacional!", "Data Incorreta!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            //lbtextBox.Text = frmTitulo;
            txtSenha.PasswordChar = char.Parse("•");
            txtSenha.Select();

            btnPrevious.Enabled = false;

            CarregaVendedores(_currentPage);
        }

        public static DateTime GetNetworkTime()
        {
            const string ntpServer = "pool.ntp.org";
            var ntpData = new byte[48];
            ntpData[0] = 0x1B; //LeapIndicator = 0 (no warning), VersionNum = 3 (IPv4 only), Mode = 3 (Client Mode)

            var addresses = Dns.GetHostEntry(ntpServer).AddressList;
            var ipEndPoint = new IPEndPoint(addresses[0], 123);
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            socket.Connect(ipEndPoint);
            socket.Send(ntpData);
            socket.Receive(ntpData);
            socket.Close();

            ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
            ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];

            var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
            var networkDateTime = (new DateTime(1900, 1, 1)).AddMilliseconds((long)milliseconds);

            return networkDateTime;
        }


        private void CarregaVendedores(int page)
        {
            using (var appServicVendedor = Program.Container.GetInstance<IUsuarioAppService>())
            {
                var skip = 5 * (page - 1);

                var usuarios = appServicVendedor.GetAll();
                _pageQuantidade = int.Parse(Math.Ceiling(usuarios.Count() / 5d).ToString());
                if (_pageQuantidade > 1)
                    btnNext.Enabled = true;


                if (usuarios.Count() < 6)
                    pnlPaginacao.Visible = false;

                var usuariosPadding = usuarios.Skip(skip).Take(5).ToList();
                flpUsuarios.Controls.Clear();
                foreach (var vendedorViewModel in usuariosPadding)
                {
                    var btnVendedor = new XButton();
                    btnVendedor.Width = 250;
                    btnVendedor.Height = 48;
                    btnVendedor.TextAlign = ContentAlignment.MiddleCenter;
                    btnVendedor.Theme = Theme.MSOffice2010_WHITE;
                    btnVendedor.CodigoProduto = vendedorViewModel.UsuarioId;
                    btnVendedor.Text = vendedorViewModel.Nome;

                    btnVendedor.SourceItem = vendedorViewModel;
                    btnVendedor.Click += BtnVendedor_Click;

                    flpUsuarios.Controls.Add(btnVendedor);
                }
            }
        }
        private void BtnVendedor_Click(object sender, EventArgs e)
        {
            if (_xButton != null)
                _xButton.Theme = Theme.MSOffice2010_WHITE;

            _xButton = (XButton)sender;
            _xButton.Theme = Theme.MSOffice2010_Publisher;

            _usuarioViewModel = (UsuarioViewModel)_xButton.SourceItem;

            flpUsuarios.Refresh();

            lbTextUsuario.Text = $"{_usuarioViewModel.Nome} digite sua senha.";
            txtSenha.Enabled = true;
            keyboardNum1.Enabled = true;

            txtSenha.Focus();
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            _currentPage++;
            CarregaVendedores(_currentPage);
            if (_currentPage == _pageQuantidade)
                btnNext.Enabled = false;
            if (_currentPage > 1)
                btnPrevious.Enabled = true;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            _currentPage--;
            CarregaVendedores(_currentPage);

            if (_currentPage == 1)
                btnPrevious.Enabled = false;
        }

        private void keyboardNum1_UserKeyPressed(object sender, KeyboardClassLibrary.Num.KeyboardNumEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.KeyboardKeyPressed))
                SendKeys.Send(e.KeyboardKeyPressed.ToUpper());
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode != Keys.Enter)return;

            AutenticaVendedor();
        }
    }
}
