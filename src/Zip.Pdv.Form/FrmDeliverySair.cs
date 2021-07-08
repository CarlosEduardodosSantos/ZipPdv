using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component;

namespace Zip.Pdv
{
    public partial class FrmDeliverySair : Form
    {
        private readonly IEntregadorAppService _entregadorAppService;
        private readonly VendaViewModel _vendaView;
        private EntregadorViewModel _entregadorView;
        private Color _colorFix;
        public FrmDeliverySair(VendaViewModel vendaView)
        {
            _vendaView = vendaView;
            InitializeComponent();
            _colorFix = Color.CadetBlue;

            _entregadorAppService = Program.Container.GetInstance<IEntregadorAppService>();
        }

        private void FrmDeliverySair_Load(object sender, EventArgs e)
        {
            txtNome.Text = _vendaView.Delivery.ClienteDelivery.Nome;
            txtEndereco.Text = _vendaView.Delivery.ClienteDelivery.Endereco;
            txtNumero.Text = _vendaView.Delivery.ClienteDelivery.Numero;
            txtFone.Text = _vendaView.Delivery.ClienteDelivery.Telefone;
            txtBairro.Text = _vendaView.Delivery.ClienteDelivery.Bairro;

            if (_vendaView.Delivery.Troco > 0)
                txtTrocoPara.ValueNumeric = _vendaView.Delivery.Valor + _vendaView.Delivery.Troco;


            txtValorTroco.ValueNumeric = _vendaView.Delivery.Troco;
            txtValorTotal.ValueNumeric = _vendaView.Delivery.Valor;
            txtTaxAdic.ValueNumeric = _vendaView.Delivery.TaxaEntrega;


            CarregaEntregadores();
        }

        private void CarregaEntregadores()
        {
            var entrregadores = _entregadorAppService.ObterTodos();

            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Controls.Clear();

            foreach (var entregadorViewModel in entrregadores)
            {
                var item = new Button()
                {
                    Text = entregadorViewModel.Nome,
                    Width = flowLayoutPanel1.Width-10,
                    Height = 55,
                    Tag = entregadorViewModel,
                    BackColor = _colorFix
                };
                item.Click += ItemOnClick;
                flowLayoutPanel1.Controls.Add(item);
            }
        }

        void ResetarControles()
        {
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control.GetType() == typeof(Button))
                    control.BackColor = _colorFix;
            }
        }

        private void ItemOnClick(object sender, EventArgs eventArgs)
        {
            ResetarControles();

            var item = (Button) sender;
            item.BackColor = Color.Aquamarine;

            _entregadorView = (EntregadorViewModel) item.Tag;

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLANCAR_Click(object sender, EventArgs e)
        {
            if (_entregadorView == null)
            {
                TouchMessageBox.Show("Selecione o entregador.", "Entrega", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (_vendaView.Delivery.Troco > 0)
            {
                using (var caixaApp = Program.Container.GetInstance<ICaixaItemAppService>())
                {
                    var caixaItem = new CaixaItemViewModel()
                    {
                        CaixaId = Program.CaixaView.CaixaId,
                        VendaId = _vendaView.Delivery.VendaId,
                        UsuarioId = Program.Usuario.UsuarioId,
                        DataHora = DateTime.Now,
                        Valor = -1*(_vendaView.Delivery.Troco),
                        TipoLancamento = "TEL",
                        Historico = $"TROCO SAI VENDA ENTREGA Nº {_vendaView.Delivery.VendaId}"
                    };

                    using (var especieAppService = Program.Container.GetInstance<IEspeciePagamentoAppService>())
                    {
                        var especie = especieAppService.ObterTodos().FirstOrDefault(t => t.Interno == "ESP1");

                        caixaItem.CaixaPagamentos.Add(new CaixaPagamentoViewModel()
                        {
                            Especie = especie.Especie,
                            Valor = caixaItem.Valor,
                            EspeciePagamentoId = especie.EspeciePagamentoId,
                            Interno = especie.Interno,
                            CodigoFiscal = especie.CodigoFiscal
                        });

                        caixaApp.Adicionar(caixaItem);
                    }
                    
   
                }
            }

            _vendaView.Delivery.DataHoraSaida = DateTime.Now;
            _vendaView.Delivery.EntregadorId = _entregadorView.EntregadorId;
            _vendaView.Delivery.UsuarioSaida = Program.Usuario.UsuarioId;

            using (var deliveryAppService = Program.Container.GetInstance<IDeliveryAppService>())
            {
                deliveryAppService.Entregar(_vendaView.Delivery);

                //Cupom Fiscal
                var resultFiscal = TouchMessageBox.Show("Operação realizada com sucesso.\nDeseja emitir o documento fiscal?", "Entrega", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);

                if (resultFiscal == DialogResult.OK)
                {
                    var valorReceber = _vendaView.Delivery.Valor + _vendaView.Delivery.Troco;
                    using (var form = new FormPagamento(valorReceber))
                    {
                        form.ShowDialog();

                        var isPago = form.IsPago;

                        if (!isPago) return;

                        //Grava Caixa Itens
                        var caixaItem = form.CaixaItemView;
                        caixaItem.CaixaPagamentos = form.Pagamentos;


                        _vendaView.VendaFinalizadora = caixaItem.CaixaPagamentos.ToList();
                        //Imprime Cupom
                        switch (Program.EmissorFiscal)
                        {
                            case ModeloFiscalEnumView.None:
                                //ImprimeCupomNaoFiscal(caixaItem);
                                break;
                            case ModeloFiscalEnumView.Ecf:
                                break;
                            case ModeloFiscalEnumView.CfeSAT:
                                OperacoeFiscal.ImprimeSat(_vendaView);
                                //ImprimeCupomNaoFiscal(caixaItem);
                                break;
                            case ModeloFiscalEnumView.NFCe:
                                OperacoeFiscal.ImprimeNfce(_vendaView);
                                //ImprimeCupomNaoFiscal(caixaItem);
                                break;
                        }
                    }

                }

                Close();
            }
            //Verificar se tem troco pra sair do caixa

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
