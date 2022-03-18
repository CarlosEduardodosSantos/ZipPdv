using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zip.Pdv.Component;
using Zip.Pdv.Fast;
using Zip.Pdv.Helpers;
using Zip.Utils;

namespace Zip.Pdv.Page
{
    public partial class PagePagamento : PageBase
    {
        private static PagePagamento _instance;
        //private static FormPdvToten _instance;
        public static PagePagamento Instance => new PagePagamento();
        

        public VendaViewModel VendaView;
        public PagePagamento()
        {
            InitializeComponent();
            this.btnLimpar.Click += closeForm;
        }

        private void PagePagamento_Load(object sender, EventArgs e)
        {
            IniciaPagamento();
        }

        private void IniciaPagamento()
        {
            VendaView = new VendaViewModel();
            var fichas = FormSolicitaFicha.Instace("INFORME OU ESCANEIE O Nº DA FICHA.");
            if (fichas == null)
            {
                btnLimpar.PerformClick();
                
            }
            else
                CarregaFicha(fichas);
        }
        private void CarregaFicha(int[] ficha)
        {
            using (var vendaFichaApp = Program.Container.GetInstance<IVendaFichaAppService>())
            {
                var vendaFicha = vendaFichaApp.ObterPorFicha(ficha).OrderBy(t => t.ProdutoId).ToList();

                if (vendaFicha.Count == 0)
                {
                    Funcoes.MensagemError($"Ficha(s)  { string.Join(",", ficha)} não encontrada.");
                    IniciaPagamento();
                }

                VendaView = new VendaViewModel()
                {
                    CaixaId = Program.CaixaView.CaixaId,
                    UsuarioId = Program.Usuario.UsuarioId,
                    DataHora = DateTime.Now,
                    Pdv = Program.Pdv,
                    Tipo = "V",
                    Loja = Program.Loja,
                    Fichas = ficha
                };

                cupomGridView1.DataSource = null;
                cupomGridView1.SelectedItem = null;

                //cupomGridView1.Atualizar(VendaView.VendaItens);


                foreach (var vendaFichaItem in vendaFicha)
                {
                    VendaView.AdicionarFichaItemToVendaItem(vendaFichaItem);
                }

                foreach (var vendaViewVendaIten in VendaView.VendaItens)
                {
                    cupomGridView1.AddItem(vendaViewVendaIten, true);
                }
                TotalizaCupom();

            }


        }

        private void TotalizaCupom()
        {
            //lbQtdeProduto.Text = $"{VendaView.VendaItens.Sum(t => t.Quantidade)}";
            var subTotal = VendaView.VendaItens.Sum(t => t.ValorUnitatio * t.Quantidade);
            //lbSubTotal.Text = $"{subTotal.ToString("C2")}";

            var desconto = VendaView.VendaItens.Sum(t => t.Desconto);


            // lbDesconto.Text = $"({VendaView.DescontoPercentual.ToString("N2")}%) {desconto.ToString("C2")}";

            var adicional = VendaView.VendaItens.Sum(t => t.Adicional);
            //lbAdicionais.Text = $"{adicional.ToString("C2")}";

            var valorTotal = (subTotal + adicional) - desconto;
            lbValorTotal.Text = valorTotal.ToString("C2");
            /*
            if (VendaView.VendaItens.Count > 0)
            {
                tableLayoutPrincipal.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPrincipal.RowStyles[1].Height = 35;


            }
            else
            {
                tableLayoutPrincipal.RowStyles[1].SizeType = SizeType.Absolute;
                tableLayoutPrincipal.RowStyles[1].Height = 0;
            }
            this.ResumeLayout();
            */

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (VendaView.VendaItens.Count == 0)
            {
                TouchMessageBox.Show("Venda não iniciada.", "Venda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (VendaView.IsDelivery)
            {
                var result = TouchMessageBox.Show("Venda iniciada como Entrega\nDeseja finalizar como venda balção?",
                    "Venda", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                    return;

                VendaView.IsDelivery = false;
                VendaView.Delivery = new DeliveryViewModel();

            }

            var resultCpf = TouchMessageBox.Show("Deseja informar seu CPF/CNPJ no cupom fiscal??",
                    "Autoatendimento", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultCpf != DialogResult.Cancel)
            {
                var cpf = FormSolicitaCpf.Instace();
                VendaView.Cnpj = Funcoes.OnlyNumeric(cpf);
            }   

            var valorReceber = VendaView.VendaItens.Sum(t => t.ValorTotal);
            using (var form = new FormPagamentoToten(valorReceber))
            {
                form.ShowDialog();

                var isPago = form.IsPago;

                if (!isPago) return;

                //Grava venda
                using (var vendaApp = Program.Container.GetInstance<IVendaAppService>())
                {
                    var vendaId = vendaApp.ObterVendaId();
                    VendaView.VendaId = int.Parse($"{Program.PdvId}{vendaId}");

                    vendaApp.Adicionar(VendaView);

                }
                //Grava Caixa Itens
                var caixaItem = form.CaixaItemView;
                caixaItem.CaixaPagamentos = form.Pagamentos;

                using (var caixaApp = Program.Container.GetInstance<ICaixaItemAppService>())
                {
                    caixaItem.VendaId = VendaView.VendaId;
                    caixaItem.UsuarioId = Program.Usuario.UsuarioId;
                    caixaItem.Historico = $"VENDA Nº {VendaView.VendaId}";
                    caixaItem.TipoLancamento = "VDA";
                    caixaApp.Adicionar(caixaItem);
                }
                if (caixaItem.Troco > 0)
                {
                    var caixaItemTroco = new CaixaItemViewModel();
                    caixaItemTroco.CaixaId = caixaItem.CaixaId;
                    caixaItemTroco.VendaId = VendaView.VendaId;
                    caixaItemTroco.UsuarioId = Program.Usuario.UsuarioId;
                    caixaItemTroco.Valor = -1 * (caixaItem.Troco);
                    caixaItemTroco.Historico = $"TROCO VENDA Nº {VendaView.VendaId}";
                    caixaItemTroco.TipoLancamento = "VDA";

                    using (var especieAppService = Program.Container.GetInstance<IEspeciePagamentoAppService>())
                    {
                        var especie = especieAppService.ObterTodos().FirstOrDefault(t => t.Interno == "ESP1");

                        caixaItemTroco.CaixaPagamentos.Add(new CaixaPagamentoViewModel()
                        {
                            CaixaId = Program.CaixaView.CaixaId,
                            CaixaItemId = caixaItem.CaixaItemId,
                            EspeciePagamentoId = especie.EspeciePagamentoId,
                            Especie = especie.Especie,
                            Valor = caixaItemTroco.Valor,
                            Interno = especie.Interno,
                            CodigoFiscal = especie.CodigoFiscal
                        });

                    }

                    using (var caixaApp = Program.Container.GetInstance<ICaixaItemAppService>())
                    {
                        caixaApp.Adicionar(caixaItemTroco);
                    }
                }

                using (var fichaApp = Program.Container.GetInstance<IVendaFichaAppService>())
                {
                    foreach (var ficha in VendaView.Fichas)
                    {
                        fichaApp.FinalizaFicha(ficha.ToString()) ;
                    }
                    
                }
                VendaView.VendaFinalizadora = caixaItem.CaixaPagamentos.ToList();
                try
                {
                    var emissarFiscal = Program.IsFrete ? Program.EmissorFiscal : ModeloFiscalEnumView.None;
                    //Imprime Cupom
                    switch (emissarFiscal)
                    {

                        case ModeloFiscalEnumView.Ecf:
                            break;
                        case ModeloFiscalEnumView.CfeSAT:
                            var retorno = OperacoeFiscal.ImprimeSat(VendaView);

                            if (retorno.IsOk)
                            {
                                using (var retornoSatAppService = Program.Container.GetInstance<IRetornoSatAppService>())
                                {
                                    retornoSatAppService.Adicionar(retorno);
                                }
                            }
                            else
                            {
                                Program.GravaLog($"Erro ao emitir o SAT {retorno.Mensagem}");
                            }
      
                            break;
                        case ModeloFiscalEnumView.NFCe:
                            OperacoeFiscal.ImprimeNfce(VendaView);
                            
                            break;
                    }

                    //Envia GR
                }
                catch (Exception exception)
                {
                    Program.GravaLog(exception.Message);
                    Funcoes.MensagemError("Todo mundo erra, e dessa vez foram os nossos servidores.\nDirigisse até um caixa e informe o problema.");
                }
                finally
                {
                    ImprimeComprovanteTef(caixaItem);
                    ImprimeCupomNaoFiscalTotem(VendaView);
                    //ImprimeItensTotem(VendaView);
                    //IniciarVenda();
                    Dispose();
                }

            }
        }

        private void ImprimeCupomNaoFiscalTotem(VendaViewModel venda)
        {
            var report = new RelatorioFastReport();

            var parms = new ParameterReportDynamic();

            parms.Add("VendaId", venda.VendaId);
            report.GerarRelatorio("Imp_VendaFinaliza", parms);

        }
        private void ImprimeComprovanteTef(CaixaItemViewModel caixaItem)
        {
            //Imprime Via TEF
            //Imprime comprovante do cartão TEF
            var report = new RelatorioFastReport();
            foreach (var caixaItemCartaoResposta in caixaItem.CartaoRespostas)
            {
                foreach (var comprovanteTef in caixaItemCartaoResposta.Comprovantes)
                {
                    if (comprovanteTef.Comprovante == null) continue;
                    var parms = new ParameterReportDynamic();
                    parms.Add("Comprovante", comprovanteTef.Comprovante);

                    report = new RelatorioFastReport();
                    report.GerarRelatorio("Imp_ComprovanteTef", parms);
                }
            }

        }

        private void ImprimeCupomNaoFiscal()
        {
            using (var vendaApp = Program.Container.GetInstance<IVendaAppService>())
            {
                var tipoOperacao = VendaView.IsDelivery ? 5 : 4;
                vendaApp.GeraImpressaoFechamento(VendaView.VendaId, tipoOperacao);
            }

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void PagePagamento_Resize(object sender, EventArgs e)
        {
            cupomGridView1.Atualizar(VendaView.VendaItens, true);
        }
    }
}
