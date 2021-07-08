using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eticket.Application.Interface;

namespace Zip.Pdv.Page.PageShare
{
    public partial class PageShareProduto : PageBase
    {
        private readonly IDashBoardResumoGeralAppService _boardResumoGeralAppService;
        public PageShareProduto()
        {
            InitializeComponent();
            _boardResumoGeralAppService = Program.Container.GetInstance<IDashBoardResumoGeralAppService>();
        }

        private void PageShareProduto_Load(object sender, EventArgs e)
        {
            CarregaDashBoard();
        }

        private void CarregaDashBoard()
        {
            var empresaId = Program.Loja;

            var produtosExcesso = _boardResumoGeralAppService.ObterProdutosEstoqueExcesso(empresaId).ToList();
            var produtosAbaixoDoMinimo = _boardResumoGeralAppService.ObterProdutosEstoqueBaixo(empresaId).ToList();
            var produtosEmFalta = _boardResumoGeralAppService.ObterProdutosFalta(empresaId).ToList();

            var dataInicio = DateTime.Now.AddMonths(-1).Date;
            var dataFinal = DateTime.Now.Date;

            var vendas = _boardResumoGeralAppService.ObterProdutosVendidoPorData(empresaId, dataInicio, dataFinal);
            var compras = _boardResumoGeralAppService.ObterProdutosCompradoPorData(empresaId, dataInicio, dataFinal);

            lbEstoqueBaixo.Text = produtosAbaixoDoMinimo.Count.ToString();
            lbEstoqueExcesso.Text = produtosExcesso.Count.ToString();
            lbEstoqueFalta.Text = produtosEmFalta.Count.ToString();

            dgvProdutoEstoqueBaixo.AutoGenerateColumns = false;
            dgvProdutoEstoqueBaixo.DataSource = produtosAbaixoDoMinimo;

            dgvProdutoExcesso.AutoGenerateColumns = false;
            dgvProdutoExcesso.DataSource = produtosExcesso;

            var groupVendas = vendas.GroupBy(t => t.ProdutoNome).Select(s => new
            {
                ProdutoNome = s.Key,
                Quantidade = s.Sum(t=> t.Quantidade),
                ValorVenda = s.Sum(t => t.ValorVenda),
                ValorCusto = s.Sum(t => t.ValorCusto),
                CustoTotal = s.Sum(t => t.ValorVenda * t.Quantidade)
            }).OrderByDescending(o => o.Quantidade).ToList();
            dgvVendas.AutoGenerateColumns = false;
            dgvVendas.DataSource = groupVendas;


            var groupCompras = compras.GroupBy(t => t.ProdutoNome).Select(s => new
            {
                ProdutoNome = s.Key,
                Quantidade = s.Sum(t => t.Quantidade),
                ValorVenda = s.Sum(t => t.ValorVenda),
                ValorCusto = s.Sum(t => t.ValorCusto),
                CustoTotal = s.Sum(t => t.ValorCusto* t.Quantidade)
            }).OrderByDescending(o => o.Quantidade).ToList();
            dgvVendas.AutoGenerateColumns = false;
            dgvCompras.AutoGenerateColumns = false;
            dgvCompras.DataSource = groupCompras;

        }

        public override void Atualizar()
        {
            CarregaDashBoard();
        }
    }
}
