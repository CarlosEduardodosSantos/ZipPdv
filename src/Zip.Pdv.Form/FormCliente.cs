using System;
using System.Windows.Forms;
using Eticket.Application.CEPBrasil;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;

namespace Zip.Pdv
{
    public partial class FormCliente : Form
    {
        private readonly IClienteDeliveryAppService _clienteDeliveryAppService;
        private ClienteDeliveryViewModel _cliente;
        public FormCliente()
        {
            InitializeComponent();
            _clienteDeliveryAppService = Program.Container.GetInstance<IClienteDeliveryAppService>();
            _cliente = new ClienteDeliveryViewModel();

            txtFone.Select();
        }

        public static ClienteDeliveryViewModel Instance()
        {
            using (var form = new FormCliente())
            {
                var result = form.ShowDialog();

                return result == DialogResult.OK ? form._cliente : null;
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Limpar();
            _cliente = _clienteDeliveryAppService.ObterPorFone(txtFone.Text);
            if (_cliente == null)
            {
                txtNome.Select();
                return;
            }
            CarregaCliente();
        }

        private void CarregaCliente()
        {
            txtCep.Text = _cliente.Cep;
            txtFone.Text = _cliente.Telefone;
            txtNome.Text = _cliente.Nome;
            txtEndereco.Text = _cliente.Endereco;
            txtNumero.Text = _cliente.Numero;
            txtBairro.Text = _cliente.Bairro;
            txtCidade.Text = _cliente.Cidade;
            txtObservacao.Text = _cliente.Observacao;
            txtUf.Text = _cliente.Uf;
        }

        private void Limpar()
        {
            txtNome.Clear();
            txtEndereco.Clear();
            txtNumero.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
            txtObservacao.Clear();
            txtUf.Clear();
            txtCep.Clear();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (_cliente == null) _cliente = new ClienteDeliveryViewModel();


            _cliente.Telefone = txtFone.Text;
            _cliente.Nome = txtNome.Text;
            _cliente.Cep = txtCep.Text;
            _cliente.Endereco = txtEndereco.Text;
            _cliente.Numero = txtNumero.Text;
            _cliente.Bairro = txtBairro.Text;
            _cliente.Cidade = txtCidade.Text;
            _cliente.Uf = txtUf.Text;
            _cliente.Observacao = txtObservacao.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtFone_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                btnPesquisar.PerformClick();
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{Tab}");
        }
        private void txtCep_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) & e.KeyChar != '\b')
            {
                e.Handled = true;
                return;
            }


            if (txtCep.Text.Length != 7) return;

            try
            {
                var consultaCepView = ConsultaCepAppService.ConsultaCep(txtCep.Text + e.KeyChar);
                if (consultaCepView == null) return;

                txtEndereco.Text = !string.IsNullOrEmpty(consultaCepView.Logradouro) ? consultaCepView.Logradouro : txtEndereco.Text;
                txtBairro.Text = !string.IsNullOrEmpty(consultaCepView.Bairro)
                    ? consultaCepView.Bairro
                    : txtBairro.Text;

                txtCidade.Text = consultaCepView.Localidade;
                txtUf.Text = consultaCepView.Uf;
                //txtIbge.Text = consultaCepView.Ibge;

                txtNumero.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Alguma coisa deu errado na consulta do Cep. Verifique o Cep e tente novamente. #13" + ex.Message);
            }
        }

    }
}
