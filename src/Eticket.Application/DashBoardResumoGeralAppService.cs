using System;
using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels.DashBoard;
using Eticket.Domain.Entity.DashBoard;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class DashBoardResumoGeralAppService : IDashBoardResumoGeralAppService
    {
        private readonly IDashBoardResumoGeralRepository _boardResumoGeralRepository;

        public DashBoardResumoGeralAppService(IDashBoardResumoGeralRepository boardResumoGeralRepository)
        {
            _boardResumoGeralRepository = boardResumoGeralRepository;
        }

        public IEnumerable<DashBoardProdutoViewModel> ObterProdutosEstoqueBaixo(int empresaId)
        {
            return TypeAdapter.Adapt<IEnumerable<DashBoardProduto>, IEnumerable<DashBoardProdutoViewModel>>(
                _boardResumoGeralRepository.ObterProdutosEstoqueBaixo(empresaId));
        }

        public IEnumerable<DashBoardProdutoViewModel> ObterProdutosEstoqueExcesso(int empresaId)
        {
            return TypeAdapter.Adapt<IEnumerable<DashBoardProduto>, IEnumerable<DashBoardProdutoViewModel>>(
                _boardResumoGeralRepository.ObterProdutosEstoqueExcesso(empresaId));
        }

        public IEnumerable<DashBoardProdutoViewModel> ObterProdutosFalta(int empresaId)
        {
            return TypeAdapter.Adapt<IEnumerable<DashBoardProduto>, IEnumerable<DashBoardProdutoViewModel>>(
                _boardResumoGeralRepository.ObterProdutosFalta(empresaId));
        }

        public IEnumerable<DashBoardVendaCompraProdutoViewModel> ObterProdutosVendidoPorData(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return TypeAdapter.Adapt<IEnumerable<DashBoardVendaCompraProduto>, IEnumerable<DashBoardVendaCompraProdutoViewModel>>(
                _boardResumoGeralRepository.ObterProdutosVendidoPorData(empresaId, dataInicio, dataFinal));
        }

        public IEnumerable<DashBoardVendaCompraProdutoViewModel> ObterProdutosCompradoPorData(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return TypeAdapter.Adapt<IEnumerable<DashBoardVendaCompraProduto>, IEnumerable<DashBoardVendaCompraProdutoViewModel>>(
                _boardResumoGeralRepository.ObterProdutosCompradoPorData(empresaId, dataInicio, dataFinal));
        }

        public IEnumerable<DashBoardVendaViewModel> ObterVendasPorData(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return TypeAdapter.Adapt<IEnumerable<DashBoardVenda>, IEnumerable<DashBoardVendaViewModel>>(
                _boardResumoGeralRepository.ObterVendasPorData(empresaId, dataInicio, dataFinal));
        }

        public void Dispose()
        {
            _boardResumoGeralRepository.Dispose();
        }
    }
}