using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class ProdutoDeptoAppService : IProdutoDeptoAppService
    {
        private readonly IProdutoDeptoRepository _produtoDeptoRepository;

        public ProdutoDeptoAppService(IProdutoDeptoRepository produtoDeptoRepository)
        {
            _produtoDeptoRepository = produtoDeptoRepository;
        }

        public void Dispose()
        {
            _produtoDeptoRepository.Dispose();
        }

        public void Adicionar(ProdutoDeptoViewModel produtoDeptoView)
        {
            var produtoDepto = TypeAdapter.Adapt<ProdutoDeptoViewModel, ProdutoDepto>(produtoDeptoView);
            _produtoDeptoRepository.Adicionar(produtoDepto);
        }

        public void Editar(ProdutoDeptoViewModel produtoDeptoView)
        {
            var produtoDepto = TypeAdapter.Adapt<ProdutoDeptoViewModel, ProdutoDepto>(produtoDeptoView);
            _produtoDeptoRepository.Editar(produtoDepto);
        }

        public void Excluir(ProdutoDeptoViewModel produtoDeptoView)
        {
            var produtoDepto = TypeAdapter.Adapt<ProdutoDeptoViewModel, ProdutoDepto>(produtoDeptoView);
            _produtoDeptoRepository.Excluir(produtoDepto);
        }

        public IEnumerable<ProdutoDeptoViewModel> ObterTodos()
        {
            return TypeAdapter.Adapt<IEnumerable<ProdutoDepto>, IEnumerable<ProdutoDeptoViewModel>>(
                _produtoDeptoRepository.GetAll());
        }
    }
}