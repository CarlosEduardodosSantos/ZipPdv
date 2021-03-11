using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component;
using Zip.Pdv.Component.EspeciePagamento;

namespace Zip.Pdv
{
    public partial class FormPagamento : Form
    {
        public bool IsPago;
        private readonly decimal _valorReceber;
        public List<CaixaPagamentoViewModel> Pagamentos;
        public CaixaItemViewModel CaixaItemView;
        private EspeciePagamentoViewModel _especiePagamento;
        public FormPagamento(decimal valorReceber)
        {
            InitializeComponent();
            txtValor.CasasDecimais = "C2";

            _valorReceber = valorReceber;
            
            Pagamentos = new List<CaixaPagamentoViewModel>();
            CaixaItemView = new CaixaItemViewModel()
            {
                CaixaId = Program.CaixaView.CaixaId,
                Valor = _valorReceber,
                
            };
            CaixaItemView.CaixaId = Program.CaixaView.CaixaId;
            CaixaItemView.Valor = _valorReceber;

        }

        private void CarregaEspecies()
        {
            
            using (var especieAppService = Program.Container.GetInstance<IEspeciePagamentoAppService>())
            {
                var especies = especieAppService.ObterTodos();
                flayoutGrupo.Controls.Clear();
                foreach (var especiePagamentoViewModel in especies)
                {
                    var btnEspecie = new EspecieGridViewItem();
                    btnEspecie.AdicionarEspecie(especiePagamentoViewModel);
                    btnEspecie.SelectItem += xButton1_Click;
                    btnEspecie.Width = 131;
                    flayoutGrupo.Controls.Add(btnEspecie);
                }
            }
        }

        private void xButton1_Click(object sender, EventArgs e)
        {

            ResetControls();

            var btnPgto = (EspecieGridViewItem) sender;
            btnPgto.Selecionar();

            _especiePagamento = btnPgto.Especie;
            

            flayoutGrupo.Refresh();
            var valorPago = Pagamentos.Sum(t => t.Valor);
            txtValor.ValueNumeric = _valorReceber - valorPago;

            flowLayoutPanel1.Focus();

            txtValor.Select();
        }

        private void ResetControls()
        {
            for (int i = 0; i < flayoutGrupo.Controls.Count; i++)
            {
                if (flayoutGrupo.Controls[i].GetType() != typeof(EspecieGridViewItem))
                    continue;

                ((EspecieGridViewItem)flayoutGrupo.Controls[i]).Selected = false;
                ((EspecieGridViewItem)flayoutGrupo.Controls[i]).Resetar();

            }

        }

        private void TotalizaPagamento()
        {
            lbValorReceber.Text = _valorReceber.ToString("C2");

            var valorPago = Pagamentos.Sum(t => t.Valor);
            lbValorPago.Text = valorPago.ToString("C2");
            var saldoPagar = (_valorReceber - valorPago);
            lbSaldoPagar.Text = saldoPagar >= 0 ? saldoPagar.ToString("C2") : "R$ 0,00";
            var troco = (valorPago - _valorReceber);
            lbTroco.Text = troco >= 0 ? troco.ToString("C2") : "R$ 0,00";
            CaixaItemView.Troco = troco > 0 ? troco : 0;

            if(_valorReceber <= valorPago)
                btnPagar.PerformClick();
        }

        private void xButton12_Click(object sender, EventArgs e)
        {
            var btnValor = (Button) sender;

            txtValor.Text = btnValor.Text == "=" ? _valorReceber.ToString("C2") : decimal.Parse(btnValor.Text).ToString("C2");
        }

        private void FormPagamento_Load(object sender, EventArgs e)
        {
            CarregaEspecies();
            TotalizaPagamento();
        }

        private void btnLancarPgamento_Click(object sender, EventArgs e)
        {
            if (_especiePagamento == null)
            {
                TouchMessageBox.Show("Informe a espécie de pagamento", "Finalizar", MessageBoxButtons.OK);
                return;
            }
            var valor = txtValor.ValueNumeric;

            if (_especiePagamento.Tef)
            {
                var cartaoResposta = TefDial.AutomacaoTef.AcionaTef(CartaoTipoOperacaoEnumView.CRT, valor, "1", _especiePagamento.TipoCartao);
                if (!cartaoResposta.Autorizado)
                {

                    return;
                }
                Pagamentos.Add(new CaixaPagamentoViewModel()
                {
                    CaixaId = Program.CaixaView.CaixaId,
                    EspeciePagamentoId = _especiePagamento.EspeciePagamentoId,
                    Especie = _especiePagamento.Especie,
                    Valor = valor,
                    Interno = _especiePagamento.Interno,
                    CodigoFiscal = _especiePagamento.CodigoFiscal,
                    CartaoResposta = cartaoResposta,
                    CartaoRespostaGuid = cartaoResposta.CartaoRespostaGuid
                });


                CaixaItemView.CartaoRespostas.Add(cartaoResposta);
            }
            else
            {
                if (Pagamentos.Any(t => t.Especie == _especiePagamento.Especie))
                    Pagamentos.FirstOrDefault(t => t.Especie == _especiePagamento.Especie).Valor += valor;

                Pagamentos.Add(new CaixaPagamentoViewModel()
                {
                    CaixaId = Program.CaixaView.CaixaId,
                    CaixaItemId = CaixaItemView.CaixaItemId,
                    EspeciePagamentoId = _especiePagamento.EspeciePagamentoId,
                    Especie = _especiePagamento.Especie,
                    Valor = valor,
                    Interno = _especiePagamento.Interno,
                    CodigoFiscal = _especiePagamento.CodigoFiscal
                });
            }



            LancarPagamento();
        }

        private void LancarPagamento()
        {
            flpResumoPagamento.Controls.Clear();
            foreach (var pdvPagamento in Pagamentos)
            {
                var resumoItem = new UcPdvItem();
                resumoItem.AdicionarCaixaPagamento(pdvPagamento);
                resumoItem.Click += ResumoItem_Click; ;
                flpResumoPagamento.Controls.Add(resumoItem);
            }

            txtValor.ValueNumeric = 0;
            ResetControls();
            _especiePagamento = null;

            TotalizaPagamento();


        }

        private void ResumoItem_Click(object sender, EventArgs e)
        {
            var item = (UcPdvItem)sender;
            var pagamento = (CaixaPagamentoViewModel)item.CaixaSource;
            if (Pagamentos.Any(t => t.Especie == pagamento.Especie))
            {
                if (pagamento.CartaoResposta.Autorizado)
                {
                    var respostaCancelamento = TefDial.AutomacaoTef.Cancelamento(pagamento.CartaoResposta.Requisicao);
                    if (!respostaCancelamento.Autorizado)
                    {
                        TouchMessageBox.Show(respostaCancelamento.Menssagem, "Cancelamento de operação",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                Pagamentos.Remove(pagamento);
            }

            LancarPagamento();
            TotalizaPagamento();
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            var valorPago = Pagamentos.Sum(t => t.Valor);
            if (valorPago < _valorReceber)
            {
                TouchMessageBox.Show("Valor pago insufisiente para continuar", "Pagamento", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;

            }

            IsPago = true;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            IsPago = false;
            Close();
        }

        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.Num.KeyboardNumEventArgs e)
        {
            if (_especiePagamento == null)
            {
                TouchMessageBox.Show("Informe a espécie de pagamento", "Receber", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (e.KeyboardKeyPressed == "{ENTER}")
            {

                btnLancarPgamento.PerformClick();
            }
            else
            {
                if (!string.IsNullOrEmpty(e.KeyboardKeyPressed))
                {
                    if(txtValor.SelectionLength > 0)
                        txtValor.Clear();

                    var str = new StringBuilder();
                    //Verifica se é comando para apagar
                    if (e.KeyboardKeyPressed == "{BACKSPACE}")
                    {
                        str.Append(int.Parse(txtValor.ValueNumeric.ToString().Replace(",", "")));
                        str.Remove(str.Length-1, 1);
                    }
                    else
                    {
                        str.Append(int.Parse(txtValor.ValueNumeric.ToString().Replace(",", "")));
                        str.Append(e.KeyboardKeyPressed);
                    }

                    txtValor.ValueNumeric = str.Length > 0 ? decimal.Parse(str.ToString())/100 : 0;
                }
                
            }
            txtValor.SelectionStart = txtValor.Text.Length;
        }

        private void FormPagamento_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Pagamentos.Any(t => t.CartaoResposta.Autorizado)) return;
            if (IsPago) return;

            TouchMessageBox.Show("Existe pagamento(s) TEF autorizado!\nÉ necessário efetuar o cancelamento.", "Pagamento", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            e.Cancel = true;
        }
    }
}
