using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Eticket.Application.CartaoConsumo;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Zip.Pdv.Component;
using Zip.Pdv.Component.EspeciePagamento;

namespace Zip.Pdv
{
    public partial class FormPagamento : Form
    {
        public bool IsPago;
        public string CpfCnpj;
        private decimal _valorReceber;
        private int _IdOpMesa1;
        private bool _parceled;
        public List<CaixaPagamentoViewModel> Pagamentos;
        public CaixaItemViewModel CaixaItemView;
        public ClienteViewModel ClienteItemView;
        public bool IsPrazo;
        public decimal _TotalMesa;
        public string _Metodo;
        private bool _itemParceled;
        public CartaoConsumoMovRespViewModel CartaoConsumoView;
        private EspeciePagamentoViewModel _especiePagamento;
        private List<EspeciePagamentoViewModel> _especies;
        private readonly IOpMesa1AppService _OpMesa1AppService;
        private readonly ICadMesasAppService _CadMesasAppService;
        public FormPagamento(decimal valorReceber, decimal desconto, int IdOpMesa1 = 0, bool parceled = false, bool itemParceled = false, decimal TotalMesa = 0)
        {
            InitializeComponent();
            txtValor.CasasDecimais = "C2";
            lbDesconto.Text = desconto.ToString("C2"); ;
            _valorReceber = valorReceber;
            _IdOpMesa1 = IdOpMesa1;
            _parceled = parceled;
            _TotalMesa = TotalMesa;
            _itemParceled = itemParceled;
            _OpMesa1AppService = Program.Container.GetInstance<IOpMesa1AppService>();
            _CadMesasAppService = Program.Container.GetInstance<ICadMesasAppService>();

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
                _especies = especieAppService.ObterTodos().Where(t => t.Situacao == 1).ToList();
                flayoutGrupo.Controls.Clear();
                foreach (var especiePagamentoViewModel in _especies)
                {
                    var btnEspecie = new EspecieGridViewItem();
                    btnEspecie.Name = especiePagamentoViewModel.EspeciePagamentoId.ToString();
                    btnEspecie.AdicionarEspecie(especiePagamentoViewModel);
                    btnEspecie.SelectItem += xButton1_Click;
                    btnEspecie.Width = 126;
                    btnEspecie.Height = btnEspecie.Height - 13;
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

            var btnPgto = (EspecieGridViewItem)sender;
            btnPgto.Selecionar();

            _especiePagamento = btnPgto.Especie;


            flayoutGrupo.Refresh();
            var valorPago = Pagamentos.Sum(t => t.Valor);
            if (_itemParceled == false)
            {
                txtValor.ValueNumeric = _valorReceber - valorPago;
            }
            else
            {
                txtValor.ValueNumeric = _valorReceber;
            }

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

        private void verificaMesa()
        {
            if(_IdOpMesa1 != 0)
            {
               var opmesa = _OpMesa1AppService.GetById(_IdOpMesa1);
                //Adiciona os pagamentos Parciais pré existentes
                if (opmesa.Dinheiro != 0)
                {
                    Pagamentos.Add(new CaixaPagamentoViewModel()
                    {
                        CaixaId = Program.CaixaView.CaixaId,
                        EspeciePagamentoId = 1,
                        Especie = "DINHEIRO",
                        Valor = (decimal)opmesa.Dinheiro,
                        Interno = "ESP1",
                        CodigoFiscal = "01",
                    });
                }
                if (opmesa.Cartao_Debito != 0)
                {
                    Pagamentos.Add(new CaixaPagamentoViewModel()
                    {
                        CaixaId = Program.CaixaView.CaixaId,
                        EspeciePagamentoId = 2,
                        Especie = "CARTÃO DÉBITO",
                        Valor = (decimal)opmesa.Cartao_Debito,
                        Interno = "ESP3",
                        CodigoFiscal = "04",
                    });
                }

                if (opmesa.Cartao_Credito != 0)
                {
                    Pagamentos.Add(new CaixaPagamentoViewModel()
                    {
                        CaixaId = Program.CaixaView.CaixaId,
                        EspeciePagamentoId = 3,
                        Especie = "CARTÃO CRÉDITO",
                        Valor = (decimal)opmesa.Cartao_Credito,
                        Interno = "ESP4",
                        CodigoFiscal = "03",
                    });
                }

                if (opmesa.Cartao_Consumo != 0)
                {
                    Pagamentos.Add(new CaixaPagamentoViewModel()
                    {
                        CaixaId = Program.CaixaView.CaixaId,
                        EspeciePagamentoId = 9,
                        Especie = "CARTAO CONSUMO",
                        Valor = (decimal)opmesa.Dinheiro,
                        Interno = "ESP6",
                        CodigoFiscal = "99",
                    });
                }

                var valorPago = Pagamentos.Sum(t => t.Valor);
                lbValorPago.Text = valorPago.ToString("C2");

                foreach(var pdvPagamento in Pagamentos)
                {
                    var resumoItem = new UcPdvItem();
                    resumoItem.AdicionarCaixaPagamento(pdvPagamento);
                    resumoItem.Click += ResumoItem_Click; ;
                    flpResumoPagamento.Controls.Add(resumoItem);
                }

            }
            else
            {
                return;
            }
        }

        private void TotalizaPagamento()
        {   
            var valorPago = Pagamentos.Sum(t => t.Valor);
            lbValorPago.Text = valorPago.ToString("C2");
            decimal saldoPagar;
            decimal troco;
            if (_itemParceled == true)
            {
                saldoPagar = _valorReceber;
                troco = 0;
                lbValorReceber.Text = _TotalMesa.ToString("C2");
            }
            else
            {
                saldoPagar = (_valorReceber - valorPago);
                troco = (valorPago - _valorReceber);
                lbValorReceber.Text = _valorReceber.ToString("C2");
            }
            lbSaldoPagar.Text = saldoPagar >= 0 ? saldoPagar.ToString("C2") : "R$ 0,00";
            lbTroco.Text = troco >= 0 ? troco.ToString("C2") : "R$ 0,00";
            CaixaItemView.Troco = troco > 0 ? troco : 0;

            if (_valorReceber <= valorPago && troco == 0 && _IdOpMesa1 == 0)
                btnPagar.PerformClick();
        }

        private void xButton12_Click(object sender, EventArgs e)
        {
            var btnValor = (Button)sender;

            txtValor.Text = btnValor.Text == "=" ? _valorReceber.ToString("C2") : decimal.Parse(btnValor.Text).ToString("C2");
        }

        private void FormPagamento_Load(object sender, EventArgs e)
        {
            btnSolicitaCpf.Text = !string.IsNullOrEmpty(CpfCnpj) ? $"CPF/CNPJ: {CpfCnpj}" : "Informar CPF/CNPJ?";
            verificaMesa();
            CarregaEspecies();
            TotalizaPagamento();
        }

        private void btnLancarPgamento_Click(object sender, EventArgs e)
        {
            btnLancarPgamento.Enabled = false;
            if (_especiePagamento == null)
            {
                TouchMessageBox.Show("Informe a espécie de pagamento", "Finalizar", MessageBoxButtons.OK);
                btnLancarPgamento.Enabled = true;
                return;
            }
            var valor = txtValor.ValueNumeric;
            if (valor == 0)
            {   
                TouchMessageBox.Show("Informe o valor.", "Finalizar", MessageBoxButtons.OK);
                btnLancarPgamento.Enabled = true;
                return;
            }

            if (_especiePagamento.Tef && Program.HabilitaTef)
            {
                var pdv = Program.InicializacaoViewAux.PdvTef;
                var codLoja = Program.InicializacaoViewAux.CodigoLoja;
                var cnpj = Program.InicializacaoViewAux.Cnpj;

                try
                {
                    var cartaoResposta = TefTotem.AutomacaoTef.AcionaTef(CartaoTipoOperacaoEnumView.CRT, valor, "1",
                        _especiePagamento.TipoCartao, pdv, codLoja, cnpj);
                    if (!cartaoResposta.Autorizado)
                    {
                        TouchMessageBox.Show(cartaoResposta.Menssagem, "Autoatendimento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnLancarPgamento.Enabled = true;
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
                catch (Exception exx)
                {

                    TouchMessageBox.Show($"Erro TEF {exx.Message}.", "Cartão Consumo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else if (_especiePagamento.Vaucher)
            {
                var restauranteId = Program.InicializacaoViewAux.RestauranteId;
                if (restauranteId == 0)
                {
                    TouchMessageBox.Show("Restaurante não associado.\nEntre em contato com nosso suporte.", "Cartão Consumo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnLancarPgamento.Enabled = true;
                    return;
                }

                var numeroCartao = FormSolicitaTexto.Instace("Informe o número do cartão");
                if (string.IsNullOrEmpty(numeroCartao))
                {
                    TouchMessageBox.Show("Número de cartão não informado.", "Cartão Consumo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnLancarPgamento.Enabled = true;
                    return;
                }
                var tipoOp = 2; //Debto
                CartaoConsumoView = CartaoConsumoAppService.AutorizarMovimentacao(restauranteId, numeroCartao, valor, "Venda PDV", tipoOp);
                if (!CartaoConsumoView.Aproved)
                {
                    TouchMessageBox.Show(CartaoConsumoView.Mensage, "Cartão Consumo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnLancarPgamento.Enabled = true;
                    return;
                }

                CartaoConsumoView.Cliente += $" - {numeroCartao}";

                if (CartaoConsumoView.Desconto > 0)
                    _valorReceber -= (_valorReceber / 100) * CartaoConsumoView.Desconto;

                Pagamentos.Add(new CaixaPagamentoViewModel()
                {
                    CaixaId = Program.CaixaView.CaixaId,
                    CaixaItemId = CaixaItemView.CaixaItemId,
                    EspeciePagamentoId = _especiePagamento.EspeciePagamentoId,
                    CartaoRespostaGuid = CartaoConsumoView.MovId,
                    Especie = _especiePagamento.Especie,
                    Valor = CartaoConsumoView.Valor,
                    Interno = _especiePagamento.Interno,
                    CodigoFiscal = _especiePagamento.CodigoFiscal,
                    Vaucher = numeroCartao
                });
            }
            else
            {
                if (_especiePagamento.Crediario || _parceled == true)
                {
                    using (var frmCliente = new FormBuscaCliente())
                    {
                        frmCliente.ShowDialog();
                        ClienteItemView = frmCliente.ClienteView;
                        if (ClienteItemView == null)
                        {
                            btnLancarPgamento.Enabled = true;
                            return;
                        }
                        IsPrazo = true;

                    }
                }

                if (Pagamentos.Any(t => t.Especie == _especiePagamento.Especie))
                    Pagamentos.FirstOrDefault(t => t.Especie == _especiePagamento.Especie).Valor += valor;
                else
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

            if (_valorReceber > Pagamentos.Sum(t => t.Valor))
                btnLancarPgamento.Enabled = true;

            LancarPagamento();
        }

        private void LancarPagamento()
        {
            flpResumoPagamento.Controls.Clear();
            foreach (var pdvPagamento in Pagamentos)
            {
                if(_IdOpMesa1 != 0) //Checa se é mesa e insere no OpMesa1!
                {
                    OpMesa1ViewModel mesapag = new OpMesa1ViewModel();
                    mesapag.IdopMesa1 = _IdOpMesa1;
                    if (pdvPagamento.EspeciePagamentoId == 1)
                    {
                        mesapag.Dinheiro = pdvPagamento.Valor;
                        _Metodo = "DINHEIRO";
                    }
                    if (pdvPagamento.EspeciePagamentoId == 2)
                    {
                        mesapag.Cartao_Debito = pdvPagamento.Valor;
                        _Metodo = "CARTAO_DEBITO";
                    }
                    if (pdvPagamento.EspeciePagamentoId == 3)
                    {
                        mesapag.Cartao_Credito = pdvPagamento.Valor;
                        _Metodo = "CARTAO_CREDITO";
                    }
                    if (pdvPagamento.EspeciePagamentoId == 9)
                    {
                        mesapag.Cartao_Consumo = pdvPagamento.Valor;
                        _Metodo = "CARTAO_CONSUMO";
                    }

                    _OpMesa1AppService.Pagamento(mesapag);
                }
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
                    var pdv = Program.InicializacaoViewAux.Pdv;
                    var codLoja = Program.InicializacaoViewAux.CodigoLoja;
                    var cnpj = Program.InicializacaoViewAux.Cnpj;

                    var respostaCancelamento = TefTotem.AutomacaoTef.Cancelamento(pagamento.CartaoResposta, pdv, codLoja, cnpj);
                    if (!respostaCancelamento.Autorizado)
                    {
                        TouchMessageBox.Show(respostaCancelamento.Menssagem, "Cancelamento de operação",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(pagamento.Vaucher))
                {
                    var user = Program.Usuario.Nome;
                    var motivo = "Cancelamento de pagamento";
                    var cartaoConsumoView = CartaoConsumoAppService.EstornaMovimentacao(pagamento.CartaoRespostaGuid, user, pagamento.Valor, motivo);
                    if (cartaoConsumoView.error)
                    {
                        Funcoes.MensagemError($"{cartaoConsumoView.message}\nVenda não pode ser cancelada, pois ocorreu um erro ao estornar o cartão consumo.\n Consulte o suporte para mais informações.");
                    }
                }

                if(_IdOpMesa1 != 0) //Checa se é mesa e deleta da opmesa1
                {
                    var mesa = _OpMesa1AppService.GetById(_IdOpMesa1);
                    var mesapag = new OpMesa1ViewModel();
                    mesapag.IdopMesa1 = _IdOpMesa1;

                    if(pagamento.EspeciePagamentoId == 1)
                    {
                        mesapag.Dinheiro -= pagamento.Valor;
                        mesapag.Cartao_Debito = mesa.Cartao_Debito;
                        mesapag.Cartao_Credito = mesa.Cartao_Credito;
                        mesapag.Cartao_Consumo = mesa.Cartao_Consumo;
                    }

                    if (pagamento.EspeciePagamentoId == 2)
                    {
                        mesapag.Dinheiro = mesa.Dinheiro;
                        mesapag.Cartao_Debito -= pagamento.Valor;
                        mesapag.Cartao_Credito = mesa.Cartao_Credito;
                        mesapag.Cartao_Consumo = mesa.Cartao_Consumo;
                    }

                    if (pagamento.EspeciePagamentoId == 3)
                    {
                        mesapag.Dinheiro = mesa.Dinheiro;
                        mesapag.Cartao_Debito = mesa.Cartao_Debito;
                        mesapag.Cartao_Credito -= pagamento.Valor;
                        mesapag.Cartao_Consumo = mesa.Cartao_Consumo;
                    }

                    if (pagamento.EspeciePagamentoId == 9)
                    {
                        mesapag.Dinheiro = mesa.Dinheiro;
                        mesapag.Cartao_Debito = mesa.Cartao_Debito;
                        mesapag.Cartao_Credito = mesa.Cartao_Credito;
                        mesapag.Cartao_Consumo -= pagamento.Valor;
                    }

                    _OpMesa1AppService.Pagamento(mesapag);
                }

                    Pagamentos.Remove(pagamento);
            }

            LancarPagamento();
            TotalizaPagamento();
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            var valorPago = Pagamentos.Sum(t => t.Valor);
            if (valorPago < _valorReceber && _IdOpMesa1 == 0)//PAGAMENTO COMUM
            {
                if (txtValor.ValueNumeric == _valorReceber)
                {
                    btnLancarPgamento.PerformClick();
                    IsPago = true;
                    Close();
                }
                else
                {
                    TouchMessageBox.Show("Valor pago insuficiente para continuar", "Pagamento", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);


                    IsPago = false;
                    Close();
                }
            }

            else if(_IdOpMesa1 != 0 && !_itemParceled)//PAGAMENTO MESA E PARCIAL POR VALOR
            {
                if (txtValor.ValueNumeric <= _valorReceber)
                {
                    btnLancarPgamento.PerformClick();
                    IsPago = true;
                    Close();
                }
                else if(txtValor.ValueNumeric == _valorReceber)
                {
                    btnLancarPgamento.PerformClick();
                    var opmesa= _OpMesa1AppService.GetById(_IdOpMesa1);
                    var mesa = new CadMesasViewModel();
                    mesa.IdMesa = (int)opmesa.IdMesa;
                    mesa.Status = 1;
                    _CadMesasAppService.AlterarStatusMesa(mesa);
                    IsPago = true;
                    Close();
                }
            }

            else if(_IdOpMesa1 != 0 && _itemParceled)//PAGAMENTO MESA E PARCIAL POR ITEM
            {
                if (txtValor.ValueNumeric == _valorReceber)
                {
                    btnLancarPgamento.PerformClick();
                    IsPago = true;
                    Close();
                }
                else
                {
                    TouchMessageBox.Show("Valor pago insuficiente para continuar", "Pagamento", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);


                    IsPago = false;
                    Close();
                }
            }

            else
            {
                IsPago = true;
                Close();
            }
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
                    if (txtValor.SelectionLength > 0)
                        txtValor.Clear();

                    var str = new StringBuilder();
                    //Verifica se é comando para apagar
                    if (e.KeyboardKeyPressed == "{BACKSPACE}")
                    {
                        str.Append(int.Parse(txtValor.ValueNumeric.ToString().Replace(",", "")));
                        str.Remove(str.Length - 1, 1);
                    }
                    else
                    {
                        str.Append(int.Parse(txtValor.ValueNumeric.ToString().Replace(",", "")));
                        str.Append(e.KeyboardKeyPressed);
                    }

                    txtValor.ValueNumeric = str.Length > 0 ? decimal.Parse(str.ToString()) / 100 : 0;
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
            if (e.KeyCode != Keys.Enter) return;

            btnLancarPgamento.PerformClick();
        }

        private void btnSolicitaCpf_Click(object sender, EventArgs e)
        {
            var cpf = FormSolicitaCpf.Instace();
            CpfCnpj = Funcoes.OnlyNumeric(cpf);
            if (!string.IsNullOrEmpty(CpfCnpj))
                btnSolicitaCpf.Text = $"CPF/CNPJ: {cpf}";
        }
    }
}
