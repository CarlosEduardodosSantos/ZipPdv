using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component;
using Zip.Pdv.Component.EspeciePagamento;

namespace Zip.Pdv
{
    public partial class FormPagamentoToten : Form
    {
        public bool IsPago;
        private readonly decimal _valorReceber;
        public List<CaixaPagamentoViewModel> Pagamentos;
        public CaixaItemViewModel CaixaItemView;
        private EspeciePagamentoViewModel _especiePagamento;
        private List<EspeciePagamentoViewModel> _especies;
        public FormPagamentoToten(decimal valorReceber)
        {
            InitializeComponent();
            //txtValor.CasasDecimais = "C2";

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
                _especies = especieAppService.ObterTodos().Where(t => t.Tef).ToList();
                flayoutGrupo.Controls.Clear();
                var width = (flayoutGrupo.Width / _especies.Count) - 20;
                var height = flayoutGrupo.Height - 40;
                foreach (var especiePagamentoViewModel in _especies)
                {
                    var btnEspecie = new EspecieGridViewItem();
                    btnEspecie.Name = especiePagamentoViewModel.EspeciePagamentoId.ToString();
                    btnEspecie.AdicionarEspecie(especiePagamentoViewModel);
                    btnEspecie.SelectItem += xButton1_Click;
                    btnEspecie.Width = width;
                    btnEspecie.Height = height;
                    flayoutGrupo.Controls.Add(btnEspecie);
                }
            }
        }

        private void BtnEspecie_SelectKeyDown(object sender, KeyEventArgs e)
        {
            var btnPgto = (EspecieGridViewItem)sender;
            var atalho = btnPgto.Especie.KeyAtalho;

            if (string.IsNullOrEmpty(atalho)) return;

            Keys key = (Keys)Enum.Parse(typeof(Keys), atalho, true);

            if (e.KeyCode == key)
            {
                xButton1_Click(btnPgto.Especie, new EventArgs());
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

            btnLancarPgamento_Click();
            //txtValor.ValueNumeric = _valorReceber - valorPago;

            //flowLayoutPanel1.Focus();

            //txtValor.Select();
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
            //lbValorReceber.Text = _valorReceber.ToString("C2");

            //var valorPago = Pagamentos.Sum(t => t.Valor);
            //lbValorPago.Text = valorPago.ToString("C2");
            //var saldoPagar = (_valorReceber - valorPago);
            //lbSaldoPagar.Text = saldoPagar >= 0 ? saldoPagar.ToString("C2") : "R$ 0,00";
            //var troco = (valorPago - _valorReceber);
            //lbTroco.Text = troco >= 0 ? troco.ToString("C2") : "R$ 0,00";
            //CaixaItemView.Troco = troco > 0 ? troco : 0;

            //if(_valorReceber <= valorPago)
            //    btnPagar.PerformClick();
        }

        private void xButton12_Click(object sender, EventArgs e)
        {
            //var btnValor = (Button) sender;

            //txtValor.Text = btnValor.Text == "=" ? _valorReceber.ToString("C2") : decimal.Parse(btnValor.Text).ToString("C2");
        }

        private void FormPagamento_Load(object sender, EventArgs e)
        {
            CarregaEspecies();
            TotalizaPagamento();
        }

        private void btnLancarPgamento_Click()
        {
            if (_especiePagamento == null)
            {
                TouchMessageBox.Show("Informe a espécie de pagamento", "Finalizar", MessageBoxButtons.OK);
                return;
            }
            var valor =_valorReceber;
            var codLoja = Program.InicializacaoViewAux.CodigoLoja;
            var pdv = Program.InicializacaoViewAux.PdvTef;
            var cnpj = Program.InicializacaoViewAux.Cnpj;

            if (_especiePagamento.Tef)
            {
                var cartaoResposta = TefTotem.AutomacaoTef.AcionaTef(CartaoTipoOperacaoEnumView.CRT, valor, "1", 
                    _especiePagamento.TipoCartao, pdv, codLoja, cnpj);
                if (!cartaoResposta.Autorizado)
                {
                    Program.GravaLog($"TEF não autorizado {cartaoResposta.Menssagem}");
                    TouchMessageBox.Show(cartaoResposta.Menssagem, "Autoatendimento", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            IsPago = true;
            Close();

            //LancarPagamento();
        }

        private void LancarPagamento()
        {
            //flpResumoPagamento.Controls.Clear();
            //foreach (var pdvPagamento in Pagamentos)
            //{
            //    var resumoItem = new UcPdvItem();
            //    resumoItem.AdicionarCaixaPagamento(pdvPagamento);
            //    resumoItem.Click += ResumoItem_Click; ;
            //    flpResumoPagamento.Controls.Add(resumoItem);
            //}

            //txtValor.ValueNumeric = 0;
            //ResetControls();
            //_especiePagamento = null;

            //TotalizaPagamento();


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
            //var valorPago = Pagamentos.Sum(t => t.Valor);
            //if (valorPago < _valorReceber)
            //{
            //    if (txtValor.ValueNumeric == _valorReceber)
            //        btnLancarPgamento.PerformClick();
            //    else
            //    TouchMessageBox.Show("Valor pago insufisiente para continuar", "Pagamento", MessageBoxButtons.OK,
            //        MessageBoxIcon.Information);
                
                
            //    return;

            //}

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
        //    if (_especiePagamento == null)
        //    {
        //        TouchMessageBox.Show("Informe a espécie de pagamento", "Receber", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return;
        //    }

        //    if (e.KeyboardKeyPressed == "{ENTER}")
        //    {

        //        btnLancarPgamento.PerformClick();
        //    }
        //    else
        //    {
        //        if (!string.IsNullOrEmpty(e.KeyboardKeyPressed))
        //        {
        //            if(txtValor.SelectionLength > 0)
        //                txtValor.Clear();

        //            var str = new StringBuilder();
        //            //Verifica se é comando para apagar
        //            if (e.KeyboardKeyPressed == "{BACKSPACE}")
        //            {
        //                str.Append(int.Parse(txtValor.ValueNumeric.ToString().Replace(",", "")));
        //                str.Remove(str.Length-1, 1);
        //            }
        //            else
        //            {
        //                str.Append(int.Parse(txtValor.ValueNumeric.ToString().Replace(",", "")));
        //                str.Append(e.KeyboardKeyPressed);
        //            }

        //            txtValor.ValueNumeric = str.Length > 0 ? decimal.Parse(str.ToString())/100 : 0;
        //        }
                
        //    }
        //    txtValor.SelectionStart = txtValor.Text.Length;
        }

        private void FormPagamento_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Pagamentos.Any(t => t.CartaoResposta.Autorizado)) return;
            if (IsPago) return;

            TouchMessageBox.Show("Existe pagamento(s) TEF autorizado!\nÉ necessário efetuar o cancelamento.", "Pagamento", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            e.Cancel = true;
        }

        private void FormPagamento_KeyDown(object sender, KeyEventArgs e)
        {
            var key = e.KeyCode.ToString();
            var especie = _especies.Where(t => t.KeyAtalho == key).FirstOrDefault();
            if (especie == null) return;


            var btnEspecie = flayoutGrupo.Controls.Find(especie.EspeciePagamentoId.ToString(), true).FirstOrDefault();


            xButton1_Click(btnEspecie, new EventArgs());
        }

        private void txtValor_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode != Keys.Enter) return;

            //btnLancarPgamento.PerformClick();
        }
    }
}
