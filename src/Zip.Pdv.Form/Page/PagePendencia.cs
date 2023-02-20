using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Cadastro;
using Zip.Pdv.Cadastro.Venda;
using Zip.Pdv.Component.PendenteGridView;
using Zip.Pdv.Fast;
using Zip.Utils;

namespace Zip.Pdv.Page
{
    public partial class PagePendencia : PageBase
    {
        private readonly IVendaPendenteAppService _vendaPendenteAppService;
        public PagePendencia()
        {
            InitializeComponent();
            btnVoltar.Click += closeForm;
            pendenciaGridView1.EntregaItem += EntregaGridView1_EntregaItem;

            //entregaGridView2.RetornoItem += EntregaGridView2OnRetornoItem;

            pendenciaGridView1.DetalheItem += EntregaGridView2OnDetalheItem;
            //entregaGridView2.DetalheItem += EntregaGridView2OnDetalheItem;
            pendenciaGridView1.ProntoItem += PendenciaGridView1_ProntoItem;

            _vendaPendenteAppService = Program.Container.GetInstance<IVendaPendenteAppService>();

            timer1.Interval = 80000;
            timer1.Start();


        }

        private void PendenciaGridView1_ProntoItem(object sender, EventArgs e)
        {
            var oK = Funcoes.MensagemQuestao("Deseja notificar que o pedido esta pronto?");
            if (oK != DialogResult.OK)
                return;

            timer1.Stop();
            var item = (PendenteGridViewitem)sender;
            var vendaView = (VendaViewModel)item.DataSource;

            _vendaPendenteAppService.NotificarPronto(vendaView.PendenciaId);

            Funcoes.MensagemInformation("Notificação efetuada com sucesso.");

                timer1.Start();
        }

        private void EntregaGridView2OnDetalheItem(object sender, EventArgs eventArgs)
        {
            timer1.Stop();
            var item = (PendenteGridViewitem)sender;
            var vendaView = (VendaViewModel)item.DataSource;

            dgvVendaItens.AutoGenerateColumns = false;
            dgvVendaItens.DataSource = vendaView.VendaItens.ToList();

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CarregaEntregas();
        }

        private void CarregaEntregas()
        {

            timer1.Stop();
            var vendas = new List<VendaViewModel>();
            var vendaPendentes = _vendaPendenteAppService.ObterTodos().ToList();


            foreach (var item in vendaPendentes.GroupBy(t => t.Nro))
            {
                var vendapendente = vendaPendentes.Where(t => t.Nro == item.Key);
                var venda = new VendaViewModel()
                {
                    PendenciaId = vendapendente.Max(t => t.Nro),
                    CaixaId = Program.CaixaView.CaixaId,
                    UsuarioId = Program.Usuario.UsuarioId,
                    DataHora = vendapendente.Max(t => t.DataHora),
                    HoraPendencia = vendapendente.Max(t => t.Hora),
                    Pdv = Program.Pdv,
                    Tipo = "V",
                    Loja = Program.Loja,
                    ClientePendencia = vendapendente.Max(t => t.Cliente),
                    ValorCompra = vendapendente.Sum(t => t.Total),
                    DescontoPercentual = vendapendente.Sum(t => t.Desconto)
                };

                foreach (var itemPendente in vendapendente)
                {
                    venda.VendaItens.Add(new VendaItemViewModel
                    {
                        Quantidade = itemPendente.Quantidade,
                        ProdutoId = itemPendente.ProdutoId,
                        ValorUnitatio = itemPendente.Unitario,
                        Produto = itemPendente.Produto,
                        SeqProduto = itemPendente.SeqProduto,
                        Observacao = itemPendente.Observacao,
                        Desconto = itemPendente.Desconto
                    });
                }

                vendas.Add(venda);
            }

            //var vendaPendentes = vendas;
            pendenciaGridView1.Atualizar(vendas);
            label1.Text = $"{vendas.Count} PENDENTE(S)";

            //var vendaRetorno = vendas.Where(t => t.Delivery.DataHoraSaida != DateTime.MinValue).ToList();
            //entregaGridView2.Atualizar(vendaRetorno, EntregaSituacao.Retorno);
            //label3.Text = $"{vendaRetorno.Count} AGUARDANDO RETORNO(S)";

            timer1.Start();
        }

        private void EntregaGridView1_EntregaItem(object sender, EventArgs e)
        {
            timer1.Stop();
            var item = (PendenteGridViewitem)sender;
            var venda = (VendaViewModel)item.DataSource;

            var valorReceber = venda.VendaItens.Sum(t => t.ValorTotal);
            using (var form = new FormPagamento(valorReceber))
            {
                form.CpfCnpj = venda.Cnpj;
                form.ShowDialog();

                var isPago = form.IsPago;

                if (!isPago) return;

                venda.Cnpj = form.CpfCnpj;

                //Grava venda
                using (var vendaApp = Program.Container.GetInstance<IVendaAppService>())
                {
                    var vendaId = vendaApp.ObterVendaId();
                    venda.VendaId = int.Parse($"{Program.PdvId}{vendaId}");

                    //vendaApp.Adicionar(VendaView);

                    TryRetry.Do(() => vendaApp.Adicionar(venda), TimeSpan.FromSeconds(3));

                    //vendaApp.GeraImpressaoItens(venda.VendaId, 0);

                }
                //Apaga venda Pendente
                _vendaPendenteAppService.Remover(venda.PendenciaId);

                //Grava Caixa Itens
                var caixaItem = form.CaixaItemView;
                caixaItem.CaixaPagamentos = form.Pagamentos;

                using (var caixaApp = Program.Container.GetInstance<ICaixaItemAppService>())
                {
                    caixaItem.VendaId = venda.VendaId;
                    caixaItem.UsuarioId = Program.Usuario.UsuarioId;
                    caixaItem.Historico = $"VENDA Nº {venda.VendaId}";
                    caixaItem.TipoLancamento = "VDA";
                    caixaApp.Adicionar(caixaItem);
                }
                if (caixaItem.Troco > 0)
                {
                    var caixaItemTroco = new CaixaItemViewModel();
                    caixaItemTroco.CaixaId = caixaItem.CaixaId;
                    caixaItemTroco.VendaId = venda.VendaId;
                    caixaItemTroco.UsuarioId = Program.Usuario.UsuarioId;
                    caixaItemTroco.Valor = -1 * (caixaItem.Troco);
                    caixaItemTroco.Historico = $"TROCO VENDA Nº {venda.VendaId}";
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

                venda.VendaFinalizadora = caixaItem.CaixaPagamentos.ToList();
                try
                {
                    var emissarFiscal = Program.IsFrete ? Program.EmissorFiscal : ModeloFiscalEnumView.None;
                    //Imprime Cupom
                    switch (emissarFiscal)
                    {
                        case ModeloFiscalEnumView.None:
                            ImprimeCupomNaoFiscal(venda);
                            ImprimeComprovanteTef(caixaItem);
                            break;
                        case ModeloFiscalEnumView.Ecf:
                            break;
                        case ModeloFiscalEnumView.CfeSAT:
                            var retorno = OperacoeFiscal.ImprimeSat(venda);
                            if (!retorno.IsOk)
                            {
                                Funcoes.MensagemError(retorno.Mensagem);
                                break;
                            }
                            using (var retornoSatAppService = Program.Container.GetInstance<IRetornoSatAppService>())
                            {
                                retornoSatAppService.Adicionar(retorno);
                            }
                            //VendaView.CupomFiscal = retorno.CfeSatNumeroNf.ToString();
                            ImprimeComprovanteTef(caixaItem);

                            var result = Funcoes.MensagemQuestao("Desenja imprimir o cupom nao fiscal vinculado?");
                            if (result == DialogResult.OK)
                            {
                                ImprimeCupomNaoFiscal(venda);
                            }
                            break;
                        case ModeloFiscalEnumView.NFCe:
                            OperacoeFiscal.ImprimeNfce(venda);
                            ImprimeComprovanteTef(caixaItem);
                            break;
                    }
                }
                catch (Exception exception)
                {
                    Funcoes.MensagemError(exception.Message);
                }

            }
            //MessageBox.Show($"Sair : {data.Delivery.ClienteDelivery.Nome}");
            timer1.Start();
            btnCarregar.PerformClick();
        }

        private void ImprimeCupomNaoFiscal(VendaViewModel venda)
        {
            using (var vendaApp = Program.Container.GetInstance<IVendaAppService>())
            {
                var tipoOperacao = 4;
                vendaApp.GeraImpressaoFechamento(venda.VendaId, tipoOperacao);
            }

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
                    var parms = new ParameterReportDynamic();
                    parms.Add("Comprovante", comprovanteTef.Comprovante);

                    report = new RelatorioFastReport();
                    report.GerarRelatorio("Imp_ComprovanteTef", parms);
                }
            }

        }

        private void PageDelivery_Resize(object sender, EventArgs e)
        {
            CarregaEntregas();
        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            CarregaEntregas();
        }
    }
}
