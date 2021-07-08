using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class ProdutoSecaoAppService : IProdutoSecaoAppService
    {
        private readonly IProdutoSecaoRepository _produtoSecaoRepository;

        public ProdutoSecaoAppService(IProdutoSecaoRepository produtoSecaoRepository)
        {
            _produtoSecaoRepository = produtoSecaoRepository;
        }

        public void Dispose()
        {
            _produtoSecaoRepository.Dispose();
        }

        public void Adicionar(ProdutoSecaoViewModel produtoSecaoView)
        {
            var produtoSecao = TypeAdapter.Adapt<ProdutoSecaoViewModel, ProdutoSecao>(produtoSecaoView);
            _produtoSecaoRepository.Adicionar(produtoSecao);
        }

        public void Editar(ProdutoSecaoViewModel produtoSecaoView)
        {
            var produtoSecao = TypeAdapter.Adapt<ProdutoSecaoViewModel, ProdutoSecao>(produtoSecaoView);
            _produtoSecaoRepository.Editar(produtoSecao);
        }

        public void Excluir(ProdutoSecaoViewModel produtoSecaoView)
        {
            var produtoSecao = TypeAdapter.Adapt<ProdutoSecaoViewModel, ProdutoSecao>(produtoSecaoView);
            _produtoSecaoRepository.Editar(produtoSecao);
        }

        public IEnumerable<ProdutoSecaoViewModel> ObterTodos()
        {
            return TypeAdapter.Adapt<IEnumerable<ProdutoSecao>, IEnumerable<ProdutoSecaoViewModel>>(
                _produtoSecaoRepository.GetAll());
        }
    }
}