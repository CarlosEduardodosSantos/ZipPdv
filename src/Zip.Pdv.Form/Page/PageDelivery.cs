using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Cadastro;
using Zip.Pdv.Cadastro.Venda;
using Zip.Pdv.Component.EntregaGridView;

namespace Zip.Pdv.Page
{
    public partial class PageDelivery : PageBase
    {
        private readonly IVendaAppService _vendaAppService;
        public PageDelivery()
        {
            InitializeComponent();
            btnVoltar.Click += closeForm;
            entregaGridView1.EntregaItem += EntregaGridView1_EntregaItem;
            entregaGridView2.RetornoItem += EntregaGridView2OnRetornoItem;

            entregaGridView1.DetalheItem += EntregaGridView2OnDetalheItem;
            entregaGridView2.DetalheItem += EntregaGridView2OnDetalheItem;

            _vendaAppService = Program.Container.GetInstance<IVendaAppService>();

            timer1.Interval = 80000;
            timer1.Start();


        }

        private void EntregaGridView2OnDetalheItem(object sender, EventArgs eventArgs)
        {
            timer1.Stop();
            var item = (EntregaGridViewitem)sender;
            var data = (VendaViewModel)item.DataSource;
            using (var frm = new PageVendaAdministracao(data))
            {
                OpePage(frm);
            }
            timer1.Start();
        }

        private void OpePage(PageBase page)
        {
            using (var form = new FormBase(page))
            {
                form.Width = this.Width - 100;
                form.Height = this.Height - 50;
                form.StartPosition = FormStartPosition.CenterScreen;
                /*form.Location = new Point()
                {
                    X = this.Location.X + 5,
                    Y = this.Location.Y + 5
                };*/
                form.ShowDialog();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CarregaEntregas();
        }

        private void CarregaEntregas()
        {

            timer1.Stop();
            var vendas = _vendaAppService.ObterEntregaPendentes();

            var vendaPendentes = vendas.Where(t => t.Delivery.DataHoraSaida == DateTime.MinValue).ToList();
            entregaGridView1.Atualizar(vendaPendentes, EntregaSituacao.Pendente);
            label1.Text = $"{vendaPendentes.Count} PENDENTE(S)";

            var vendaRetorno = vendas.Where(t => t.Delivery.DataHoraSaida != DateTime.MinValue).ToList();
            entregaGridView2.Atualizar(vendaRetorno, EntregaSituacao.Retorno);
            label3.Text = $"{vendaRetorno.Count} AGUARDANDO RETORNO(S)";

            timer1.Start();
        }

        private void EntregaGridView2OnRetornoItem(object sender, EventArgs eventArgs)
        {
            timer1.Stop();
            var item = (EntregaGridViewitem)sender;
            var vendaView = (VendaViewModel)item.DataSource;


            var valorReceber = vendaView.Delivery.Valor + vendaView.Delivery.Troco;
            using (var form = new FormPagamento(valorReceber))
            {
                form.ShowDialog();

                var isPago = form.IsPago;

                if (!isPago) return;

                //Grava Caixa Itens
                var caixaItem = form.CaixaItemView;
                caixaItem.CaixaPagamentos = form.Pagamentos;

                using (var caixaApp = Program.Container.GetInstance<ICaixaItemAppService>())
                {
                    caixaItem.VendaId = vendaView.VendaId;
                    caixaItem.UsuarioId = Program.Usuario.UsuarioId;
                    caixaItem.Historico = $"VENDA ENTREGA Nº {vendaView.VendaId}";
                    caixaItem.TipoLancamento = "TEL";

                    caixaApp.Adicionar(caixaItem);
                }

                var deliveryView = vendaView.Delivery;
                deliveryView.DataHoraRetorno = DateTime.Now;
                deliveryView.EntregadorId = deliveryView.EntregadorId;
                deliveryView.UsuarioRetorno = Program.Usuario.UsuarioId;

                using (var deliveryAppService = Program.Container.GetInstance<IDeliveryAppService>())
                {
                    deliveryAppService.Retornar(deliveryView);

                    MessageBox.Show("Operação realizada com sucesso.", "Entrega", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CarregaEntregas();

                }

            }

            timer1.Start();
        }

        private void EntregaGridView1_EntregaItem(object sender, EventArgs e)
        {
            timer1.Stop();
            var item = (EntregaGridViewitem)sender;
            var data = (VendaViewModel)item.DataSource;
            using (var form = new FrmDeliverySair(data))
            {
                form.Location = Location;

                form.ShowDialog();

                CarregaEntregas();
            }

            //MessageBox.Show($"Sair : {data.Delivery.ClienteDelivery.Nome}");
            timer1.Start();
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
