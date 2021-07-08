using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class TributacaoFiscalAppService : ITributacaoFiscalAppService
    {
        private readonly ITributacaoFiscalRepository _tributacaoFiscalRepository;

        public TributacaoFiscalAppService(ITributacaoFiscalRepository tributacaoFiscalRepository)
        {
            _tributacaoFiscalRepository = tributacaoFiscalRepository;
        }

        public void Dispose()
        {
            _tributacaoFiscalRepository.Dispose();
        }

        public void Adicionar(ProdutoTributacaoViewModel tributacaoFiscalView)
        {
            var tributacao = TypeAdapter.Adapt<ProdutoTributacaoViewModel, ProdutoTributacao>(tributacaoFiscalView);
            _tributacaoFiscalRepository.Adicionar(tributacao);
        }

        public void Editar(ProdutoTributacaoViewModel tributacaoFiscalView)
        {
            var tributacao = TypeAdapter.Adapt<ProdutoTributacaoViewModel, ProdutoTributacao>(tributacaoFiscalView);
            _tributacaoFiscalRepository.Editar(tributacao);
        }

        public void Excluir(ProdutoTributacaoViewModel tributacaoFiscalView)
        {
            var tributacao = TypeAdapter.Adapt<ProdutoTributacaoViewModel, ProdutoTributacao>(tributacaoFiscalView);
            _tributacaoFiscalRepository.Excluir(tributacao);
        }

        public IEnumerable<ProdutoTributacaoViewModel> ObterTodos()
        {
            return TypeAdapter.Adapt<IEnumerable<ProdutoTributacao>, IEnumerable<ProdutoTributacaoViewModel>>(
                _tributacaoFiscalRepository.GetAll());
        }
    }
}