using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using System;
using System.Text;
using System.Windows.Forms;
using Eticket.Application.CEPBrasil;
using Zip.Pdv.Component;
using Zip.Pdv.Helpers;

namespace Zip.Pdv
{
    public partial class FormPdvDelivery : Form
    {
        private readonly decimal _valorReceber;
        private readonly IClienteDeliveryAppService _clienteDeliveryAppService;

        private static DeliveryViewModel _deliveryView;
        private static ClienteDeliveryViewModel _cliente;
        public FormPdvDelivery(DeliveryViewModel deliveryView, decimal valorReceber)
        {
            _valorReceber = valorReceber;
            InitializeComponent();

            _clienteDeliveryAppService = Program.Container.GetInstance<IClienteDeliveryAppService>();

            _deliveryView = deliveryView;
            _cliente = deliveryView.ClienteDelivery;
            CarregaCliente();

            txtTaxAdic.ValueNumeric = Program.InicializacaoViewAux.ValorFrete;
            txtValorTotal.ValueNumeric = _valorReceber + txtTaxAdic.ValueNumeric;

            txtFone.Select();

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            TecladoVirtualHelper.Close();
            Close();
        }

        public static DeliveryViewModel Instance(DeliveryViewModel deliveryView, decimal valorReceber)
        {
            using (var form = new FormPdvDelivery(deliveryView, valorReceber))
            {
                var result = form.ShowDialog();

                return result == DialogResult.OK ? _deliveryView : null;
            }
        }

        private void btnVoltar_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Limpar();
            _cliente = _clienteDeliveryAppService.ObterPorFone(txtFone.Text);
            if (_cliente == null)
            {
                _cliente = new ClienteDeliveryViewModel();
                txtNome.Select();
                return;
            }
            else {
                TecladoVirtualHelper.Close();
            }
            CarregaCliente();
        }

        private void CarregaCliente()
        {
            txtFone.Text = _cliente.Telefone;
            txtNome.Text = _cliente.Nome;
            txtEndereco.Text = _cliente.Endereco;
            txtNumero.Text = _cliente.Numero;
            txtBairro.Text = _cliente.Bairro;
            txtCidade.Text = _cliente.Cidade;
            txtCep.Text = _cliente.Cep;
            txtUf.Text = _cliente.Uf;
            txtObservacao.Text = _cliente.Observacao;
        }

        private void Limpar()
        {
            txtCep.Clear();
            txtNome.Clear();
            txtEndereco.Clear();
            txtNumero.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
            txtUf.Clear();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            _cliente.Telefone = txtFone.Text;
            _cliente.Nome = txtNome.Text;
            _cliente.Cep = txtCep.Text;
            _cliente.Endereco = txtEndereco.Text;
            _cliente.Numero = txtNumero.Text;
            _cliente.Bairro = txtBairro.Text;
            _cliente.Cidade = txtCidade.Text;
            _cliente.Uf = txtUf.Text;
            _cliente.Observacao = txtObservacao.Text;


            _deliveryView = new DeliveryViewModel()
            {
                Troco = txtValorTroco.ValueNumeric,
                Valor = txtValorTotal.ValueNumeric,
                TaxaEntrega = txtTaxAdic.ValueNumeric,
                ClienteDelivery = _cliente,
                EmpresaId = Program.EmpresaView.EmpresaId
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void txtValor_Leave(object sender, EventArgs e)
        {
           // txtValorTroco.ValueNumeric = txtTrocoPara.ValueNumeric - txtValorTotal.ValueNumeric;
        }

        private void keyboardNum1_UserKeyPressed(object sender, KeyboardClassLibrary.Num.KeyboardNumEventArgs e)
        {
            TextBoxDecimal text;

            text = txtTrocoPara.Focused ? txtTrocoPara : txtTaxAdic;
            if (e.KeyboardKeyPressed == "{ENTER}")
            {

                if (!string.IsNullOrEmpty(e.KeyboardKeyPressed))
                    SendKeys.Send("{TAB}");

                txtValorTroco.ValueNumeric = txtTrocoPara.ValueNumeric - txtValorTotal.ValueNumeric;
            }
            else
            {
                if (!string.IsNullOrEmpty(e.KeyboardKeyPressed))
                {
                    if (text.SelectionLength > 0)
                        text.Clear();

                    var str = new StringBuilder();
                    //Verifica se é comando para apagar
                    if (e.KeyboardKeyPressed == "{BACKSPACE}")
                    {
                        str.Append(int.Parse(text.ValueNumeric.ToString().Replace(",", "")));
                        str.Remove(str.Length - 1, 1);
                    }
                    else
                    {
                        str.Append(int.Parse(text.ValueNumeric.ToString().Replace(",", "")));
                        str.Append(e.KeyboardKeyPressed);
                    }

                    text.ValueNumeric = str.Length > 0 ? decimal.Parse(str.ToString()) / 100 : 0;
                }

            }
            text.SelectionStart = text.Text.Length;
        }

        private void txtTrocoPara_MouseLeave(object sender, EventArgs e)
        {
            //txtValorTroco.ValueNumeric = txtTrocoPara.ValueNumeric - txtValorTotal.ValueNumeric;
        }

        private void txtFone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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

        private void txtTaxAdic_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            txtValorTotal.ValueNumeric = _valorReceber + txtTaxAdic.ValueNumeric;

            txtValorTroco.ValueNumeric = txtTrocoPara.ValueNumeric - txtValorTotal.ValueNumeric;
        }

        private void btnKeyBoard_Click(object sender, EventArgs e)
        {

            TecladoVirtualHelper.Open();
            this.txtFone.Focus();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnVoltar.PerformClick();
        }
    }
}
