using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoAppService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public ProdutoViewModel ObterPorId(int id)
        {
            return TypeAdapter.Adapt<Produto, ProdutoViewModel>(_produtoRepository.GetById(id));
        }

        public IEnumerable<ProdutoViewModel> ObterPorGrupoId(int grupoId)
        {
            return TypeAdapter.Adapt<IEnumerable<Produto>, IEnumerable<ProdutoViewModel>>(_produtoRepository.GetByGrupoId(grupoId));
        }

        public IEnumerable<ProdutoViewModel> ObterMeioMeio()
        {
            return TypeAdapter.Adapt<IEnumerable<Produto>, IEnumerable<ProdutoViewModel>>(_produtoRepository.GetByMeioMeio());
        }

        public IEnumerable<ProdutoViewModel> ObterPorEan(string ean)
        {
            return TypeAdapter.Adapt<IEnumerable<Produto>, IEnumerable<ProdutoViewModel>>(_produtoRepository.ObterPorEan(ean));
        }

        public IEnumerable<ProdutoViewModel> ObterPorNome(string nome)
        {
            return TypeAdapter.Adapt<IEnumerable<Produto>, IEnumerable<ProdutoViewModel>>(_produtoRepository.ObterPorNome(nome));
        }

        public string ObterImageProdutoId(int produtoId)
        {
            return _produtoRepository.ObterImageProdutoId(produtoId);
        }

        public ProdutoTributacaoViewModel ObterTributacaoPorProdutoId(int produtoId)
        {
            return TypeAdapter.Adapt<ProdutoTributacao, ProdutoTributacaoViewModel>(_produtoRepository
                .ObterTributacaoPorProdutoId(produtoId));
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();

        }

        public IEnumerable<ProdutoObservacaoViewModel> ObterProdutoObservacao(int grupoId)
        {
            return TypeAdapter.Adapt<IEnumerable<ProdutoObservacao>, IEnumerable<ProdutoObservacaoViewModel>>(_produtoRepository
                .ObterProdutoObservacao(grupoId));

        }

        public IEnumerable<ProdutoViewModel> GetSugestaoByGrupoId(int grupoId)
        {
            return TypeAdapter.Adapt<IEnumerable<Produto>, IEnumerable<ProdutoViewModel>>(_produtoRepository.GetSugestaoByGrupoId(grupoId));
        }
    }
}