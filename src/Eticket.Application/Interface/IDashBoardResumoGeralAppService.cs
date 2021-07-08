using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels.DashBoard;

namespace Eticket.Application.Interface
{
    public interface IDashBoardResumoGeralAppService : IDisposable
    {
        IEnumerable<DashBoardProdutoViewModel> ObterProdutosEstoqueBaixo(int empresaId);
        IEnumerable<DashBoardProdutoViewModel> ObterProdutosEstoqueExcesso(int empresaId);
        IEnumerable<DashBoardProdutoViewModel> ObterProdutosFalta(int empresaId);
        IEnumerable<DashBoardVendaCompraProdutoViewModel> ObterProdutosVendidoPorData(int empresaId, DateTime dataInicio, DateTime dataFinal);
        IEnumerable<DashBoardVendaCompraProdutoViewModel> ObterProdutosCompradoPorData(int empresaId, DateTime dataInicio, DateTime dataFinal);
        IEnumerable<DashBoardVendaViewModel> ObterVendasPorData(int empresaId, DateTime dataInicio, DateTime dataFinal);
    }
}