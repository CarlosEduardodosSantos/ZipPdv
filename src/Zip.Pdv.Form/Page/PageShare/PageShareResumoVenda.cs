using System;
using System.Linq;
using Eticket.Application.Interface;

namespace Zip.Pdv.Page.PageShare
{
    public partial class PageShareResumoVenda : PageBase
    {
        private readonly IDashBoardResumoGeralAppService _boardResumoGeralAppService;
        public PageShareResumoVenda()
        {
            InitializeComponent();
            _boardResumoGeralAppService = Program.Container.GetInstance<IDashBoardResumoGeralAppService>();
        }

        private void CarregaDashBoard()
        {
            var empresaId = Program.Loja;
            var dataInicio = DateTime.Now.AddMonths(-1).Date;
            var dataFinal = DateTime.Now.Date;
            var vendas = _boardResumoGeralAppService.ObterVendasPorData(empresaId, dataInicio, dataFinal);

            lbVendaLiq.Text = vendas.Sum(t => t.ValorTotal).ToString("C2");
            lbTicketMedio.Text = (vendas.Sum(t => t.ValorTotal)/vendas.Count()).ToString("C2");
            lbDesconto.Text = vendas.Sum(t => t.Desconto).ToString("C2");

        }
        public override void Atualizar()
        {
            CarregaDashBoard();
        }

        private void PageShareResumoVenda_Load(object sender, EventArgs e)
        {
            CarregaDashBoard();
        }
    }
}
