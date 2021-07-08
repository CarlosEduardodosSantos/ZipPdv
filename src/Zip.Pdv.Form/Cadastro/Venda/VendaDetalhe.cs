﻿using System;
using System.Linq;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component;

namespace Zip.Pdv.Cadastro.Venda
{
    public partial class VendaDetalhe : CadastroBase
    {
        private  VendaViewModel _vendaView;
        public VendaDetalhe()
        {
            InitializeComponent();

           

        }

        public override void ClassToObjeto(object objeto)
        {
            splitContainer1.Panel2Collapsed = true;
            _vendaView = (VendaViewModel)objeto;

            if (_vendaView.IsDelivery)
            {
                splitContainer1.Panel2Collapsed = false;
                txtNome.Text = _vendaView.Delivery.ClienteDelivery.Nome;
                txtEndereco.Text = _vendaView.Delivery.ClienteDelivery.Endereco;
                txtNumero.Text = _vendaView.Delivery.ClienteDelivery.Numero;
                txtCep.Text = _vendaView.Delivery.ClienteDelivery.Cep;
                txtBairro.Text = _vendaView.Delivery.ClienteDelivery.Bairro;
                txtCidade.Text = _vendaView.Delivery.ClienteDelivery.Cidade;
                txtUf.Text = _vendaView.Delivery.ClienteDelivery.Uf;

                txtDataHoraSaida.Text = _vendaView.Delivery.DataHoraSaida.ToString("dd/MM/yyyy HH:mm");
                txtDataHoraRetorno.Text = _vendaView.Delivery.DataHoraRetorno.ToString("dd/MM/yyyy HH:mm");
            }

            lbVendaId.Text = _vendaView.VendaId.ToString();
            lbCaixaId.Text = _vendaView.CaixaId.ToString();
            lbPdv.Text = _vendaView.Pdv.ToString();

            lbDataHora.Text = _vendaView.DataHora.ToString("dd/MM/yyyy HH:mm");
            lbTipoVenda.Text = _vendaView.Tipo;
            lbFiscal.Text = _vendaView.CupomFiscal;

            lbDesconto.Text = _vendaView.VendaItens.Sum(t => t.Desconto).ToString("C2");
            lbTaxa.Text = _vendaView.Delivery.TaxaEntrega.ToString("C2");
            lbValorTotal.Text = _vendaView.ValorTotal.ToString("C2");


            dgvVendaItens.AutoGenerateColumns = false;
            dgvVendaItens.DataSource = _vendaView.VendaItens.ToList();

            splitButton1.AddDropDownItemAndHandle("Imprimir Gerencial ", imprimirGerencialToolStripMenuItem_Click);
            if (string.IsNullOrEmpty(_vendaView.CupomFiscal))
                splitButton1.AddDropDownItemAndHandle("Imprimir Fiscal", imprimirToolStripMenuItem_Click);

        }

        private void btnImprimirNaoFiscal_Click(object sender, System.EventArgs e)
        {
            var tpEmissao = Program.EmissorFiscal;
            if (!string.IsNullOrEmpty(_vendaView.CupomFiscal))
                tpEmissao = ModeloFiscalEnumView.None;

            switch (tpEmissao)
            {
                case ModeloFiscalEnumView.None:
                    using (var vendaApp = Program.Container.GetInstance<IVendaAppService>())
                    {
                        var tipoOperacao = _vendaView.IsDelivery ? 5 : 4;
                        vendaApp.GeraImpressaoFechamento(_vendaView.VendaId, tipoOperacao);
                    }
                    break;
                case ModeloFiscalEnumView.Ecf:
                    break;
                case ModeloFiscalEnumView.CfeSAT:
                    var retorno = OperacoeFiscal.ImprimeSat(_vendaView);
                    if (!retorno.IsOk)
                    {
                        Funcoes.MensagemError(retorno.Mensagem);
                    }
                    break;
                case ModeloFiscalEnumView.NFCe:
                    OperacoeFiscal.ImprimeNfce(_vendaView);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_vendaView.CupomFiscal))
            {
                TouchMessageBox.Show("Já existe uma cupom fiscal para essa venda.", "Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            switch (Program.EmissorFiscal)
            {
                case ModeloFiscalEnumView.None:
                    using (var vendaApp = Program.Container.GetInstance<IVendaAppService>())
                    {
                        var tipoOperacao = _vendaView.IsDelivery ? 5 : 4;
                        vendaApp.GeraImpressaoFechamento(_vendaView.VendaId, tipoOperacao);
                    }
                    break;
                case ModeloFiscalEnumView.Ecf:
                    break;
                case ModeloFiscalEnumView.CfeSAT:
                    var retorno = OperacoeFiscal.ImprimeSat(_vendaView);
                    if (!retorno.IsOk)
                    {
                        Funcoes.MensagemError(retorno.Mensagem);
                    }
                    break;
                case ModeloFiscalEnumView.NFCe:
                    OperacoeFiscal.ImprimeNfce(_vendaView);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void imprimirGerencialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var vendaApp = Program.Container.GetInstance<IVendaAppService>())
            {
                var tipoOperacao = _vendaView.IsDelivery ? 5 : 4;
                vendaApp.GeraImpressaoFechamento(_vendaView.VendaId, tipoOperacao);
            }
        }
    }
}
