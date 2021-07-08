using System;
using System.Collections.Generic;
using Eticket.Domain.Entity.DashBoard;

namespace Eticket.Domain.Interface.Repository
{
    public interface IDashBoardResumoGeralRepository: IDisposable
    {
        IEnumerable<DashBoardProduto> ObterProdutosEstoqueBaixo(int empresaId);
        IEnumerable<DashBoardProduto> ObterProdutosEstoqueExcesso(int empresaId);
        IEnumerable<DashBoardProduto> ObterProdutosFalta(int empresaId);
        IEnumerable<DashBoardVendaCompraProduto> ObterProdutosVendidoPorData(int empresaId, DateTime dataInicio, DateTime dataFinal);
        IEnumerable<DashBoardVendaCompraProduto> ObterProdutosCompradoPorData(int empresaId, DateTime dataInicio, DateTime dataFinal);
        IEnumerable<DashBoardVenda> ObterVendasPorData(int empresaId, DateTime dataInicio, DateTime dataFinal);
    }
}